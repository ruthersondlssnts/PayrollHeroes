using AutoMapper;
using Payroll.Core.Interface;
using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Payroll.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Payroll.Infrastructure.Repositories 
{
    public class RoleMenuRepository : IRoleMenu
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public RoleMenuRepository()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RoleMenuEntity, role_menu>();
                cfg.CreateMap<role_menu, RoleMenuEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public bool Add(RoleMenuEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<RoleMenuEntity, role_menu>(entity);
            db.role_menu.Add(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public bool Update(RoleMenuEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<RoleMenuEntity, role_menu>(entity);
            db.role_menu.Update(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public IEnumerable<RoleMenuGridEntity> GetList()
        {
            var data = db.role_menu.Where(a=>a.is_enable && a.date_deleted == null).ToList();
            var dataEntity = _mapper.Map<IEnumerable<role_menu>, IEnumerable<RoleMenuEntity>>(data);

            var finData = dataEntity.Select(a => new RoleMenuGridEntity
            {
                role_menu_id = a.role_menu_id,
                role_id = a.role_id,
                role_name = db.role.Where(b=>b.role_id == a.role_id).FirstOrDefault().role_name,
                display_name = a.display_name,
                role_menu_parent_id = a.role_menu_parent_id,
                action_name = a.action_name,
                controller_name = a.controller_name,
                display_icon = a.display_icon,
                ordering = a.ordering,
                is_enable = a.is_enable,
                date_deleted = a.date_deleted,

            });

            return finData;
        }
        public IEnumerable<RoleMenuEntity> GetList(int role_id)
        {
            var data = db.role_menu.Where(a=>a.role_id == role_id && a.is_enable && a.date_deleted == null).OrderBy(a=>a.ordering).ToList();
            var dataEntity = _mapper.Map<IEnumerable<role_menu>, IEnumerable<RoleMenuEntity>>(data);
            return dataEntity;
        }
        public bool CreateOrUpdate(RoleMenuEntity obj)
        {
            bool blnReturn = true;

            var detail = JsonConvert.DeserializeObject<RefShiftDetailEntity>(obj.role_menu_id.ToString());
            if (obj.role_menu_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public RoleMenuEntity GetByID(int id)
        {
            var data = db.role_menu.Where(a => a.role_menu_id == id).FirstOrDefault();
            return _mapper.Map<role_menu, RoleMenuEntity>(data);
        }

        public bool Delete(int id)
        {
            var data = db.role_menu.Where(a => a.role_menu_id == id).FirstOrDefault();
            if (data != null)
            {
                data.date_deleted = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}