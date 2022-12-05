using AutoMapper;
using Payroll.Core.Entities;
using Payroll.Core.Interface;
using Payroll.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Infrastructure.Repositories
{
  public  class RefGroupRepository :IRefGroup
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;

        public RefGroupRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RefGroupEntity, ref_group>();
                cfg.CreateMap<ref_group, RefGroupEntity>();

                cfg.CreateMap<RefGroupEntity, ref_group_dto>();
                cfg.CreateMap<ref_group_dto, RefGroupEntity>();
            });
            _mapper = config.CreateMapper();
        }

        public bool CreateOrUpdate(RefGroupEntity obj)
        {
            bool blnReturn = true;

            if (obj.group_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public bool Add(RefGroupEntity entity)
        {
            bool blnReturn = true;
            var getParent = GetAllList().Where(a => a.group_id == entity.currentval).FirstOrDefault();
            string name = entity.name;
            string level = getParent.level.ToString() + getParent.nextval + "/";

            db.Database.ExecuteSqlCommand(string.Format("INSERT dbo.[ref_group] ([Name], [Level], is_enable, date_deleted) VALUES (N'{0}', N'{1}', 1, null);", name,level ));

            db.SaveChanges();
            return blnReturn;
        }

        public bool Update(RefGroupEntity entity)
        {
            bool blnReturn = true;
            db.Database.ExecuteSqlCommand(string.Format("UPDATE dbo.[ref_group] SET Name='{0}' WHERE group_id={1};", entity.name, entity.group_id));

            db.SaveChanges();
            return blnReturn;
        }

        public bool Delete(int id)
        {
            bool blnReturn = true;
            db.Database.ExecuteSqlCommand(string.Format("UPDATE dbo.[ref_group] SET date_deleted=GETDATE() WHERE group_id={0};", id));

            db.SaveChanges();
            return blnReturn;
        }

        public RefGroupEntity GetByID(int id)
        {
            var data = db.ref_group.Where(a => a.group_id == id).FirstOrDefault();
            return _mapper.Map<ref_group, RefGroupEntity>(data);
        }

        public IEnumerable<RefGroupEntity> GetList(int id)
        {
            var getdata = db.ref_group.Where(a => a.group_id == id).FirstOrDefault();
            string level = getdata.level.ToString();
            var data = db.ref_group.FromSql(string.Format("select [group_id], [Name],[level],is_enable,date_deleted from [dbo].[ref_group] where [level].GetAncestor(1) = hierarchyid::Parse('{0}')", level)).Where(a=>a.is_enable && a.date_deleted == null).ToList();
            return _mapper.Map<IEnumerable<ref_group>, IEnumerable<RefGroupEntity>>(data);
        }

        public IEnumerable<RefGroupEntity> GetAllList()
        {
            var data = db.ref_group_dto.FromSql(@"
                with main as (
                Select group_id,name, 
                 level,
                level.GetLevel() current_level
                ,[Level].GetAncestor(1).ToString()  as [ancestor1]
                ,[Level].GetAncestor(2).ToString()  as [ancestor2]
                ,is_enable
                ,date_deleted
                from ref_group WHERE is_enable=1 AND date_deleted is null)
                SELECT group_id,name,level,current_level,
                (SELECT group_id FROM ref_group WHERE level.ToString()=main.[ancestor1]) AS  [ancestor1],
                (SELECT group_id FROM ref_group WHERE level.ToString()=main.[ancestor2]) AS  [ancestor2],
                ((select 
                    count(*)
                from ref_group  
                where [level].GetAncestor(1) = main.level)+1) AS nextval
                ,is_enable
                ,date_deleted
                FROM main").Where(a => a.is_enable && a.date_deleted == null).ToList();
            return _mapper.Map<IEnumerable<ref_group_dto>, IEnumerable<RefGroupEntity>>(data);
        }
    }

}
