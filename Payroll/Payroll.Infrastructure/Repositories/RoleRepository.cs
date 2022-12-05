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
    public class RoleRepository : IRole
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public RoleRepository()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RoleEntity, role>();
                cfg.CreateMap<role, RoleEntity>();

                cfg.CreateMap<RolePermissionEntity, role_permission>();
                cfg.CreateMap<role_permission, RolePermissionEntity>();

                cfg.CreateMap<PermissionEntity, permission>();
                cfg.CreateMap<permission, PermissionEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public bool Add(RoleEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<RoleEntity, role>(entity);
            db.role.Add(dataEntity);
            db.SaveChanges();
            //foreach (var row in entity.role_permission)
            //{
            //    RolePermissionEntity objNew = new RolePermissionEntity();
            //    objNew.permission_id = row.permission_id;
            //    objNew.role_id = dataEntity.role_id;
            //    objNew.is_enable = row.is_enable;
            //    var insert = _mapper.Map<RolePermissionEntity, role_permission>(objNew);
            //    db.role_permission.Add(insert);
            //    db.SaveChanges();
            //}

            return blnReturn;
        }

        public bool Update(RoleEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<RoleEntity, role>(entity);
            var data = db.role.Where(a => a.role_id == entity.role_id).FirstOrDefault();
            data.role_name = entity.role_name;
            db.SaveChanges();
            foreach (var row in entity.role_permission)
            {
                var check = db.role_permission.Where(a => a.role_id == entity.role_id && a.permission_id == row.permission_id).FirstOrDefault();
                if (check != null)
                {
                    check.is_enable = row.is_enable;
                }
                else
                {
                    RolePermissionEntity objNew = new RolePermissionEntity();
                    objNew.permission_id = row.permission_id;
                    objNew.role_id = dataEntity.role_id;
                    objNew.is_enable = row.is_enable;
                    var insert = _mapper.Map<RolePermissionEntity, role_permission>(objNew);
                    db.role_permission.Add(insert);
                }

                db.SaveChanges();
            }
            return blnReturn;
        }

        public IEnumerable<RoleEntity> GetList()
        {
            var data = db.role.ToList();
            var dataEntity = _mapper.Map<IEnumerable<role>, IEnumerable<RoleEntity>>(data);
            return dataEntity;
        }

        public bool CreateOrUpdate(RoleEntity obj)
        {
            bool blnReturn = true;
            var detail = JsonConvert.DeserializeObject<List<RolePermissionEntity>>(obj.role_permission_.ToString());
            obj.role_permission = detail;
            if (obj.role_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public RoleEntity GetByID(int id)
        {
            var data = db.role.Where(a => a.role_id == id).FirstOrDefault();
            return _mapper.Map<role, RoleEntity>(data);
        }

        public bool Delete(int id)
        {
            var data = db.role.Where(a => a.role_id == id).FirstOrDefault();
            if (data != null)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<RolePermissionEntity> GetPermission(int id)
        {
            var data = db.role_permission.Include(a=>a.permission_).Where(a => a.role_id == id).OrderBy(a=>a.permission_.permission_name).ToList();
            var dataid = data.Select(a => a.permission_id);
            var empty = db.permission.Where(x => !dataid.Contains(x.permission_id));

            List<RolePermissionEntity> objList = new List<RolePermissionEntity>();

            foreach (var row in data)
            {
                RolePermissionEntity newrec = new RolePermissionEntity();
                newrec.permission_id = row.permission_id;
                newrec.permission_name = row.permission_.permission_name;
                newrec.is_enable = row.is_enable;
                objList.Add(newrec);
            }

            foreach (var row in empty)
            {
                RolePermissionEntity newrec = new RolePermissionEntity();
                newrec.permission_id = row.permission_id;
                newrec.permission_name = row.permission_name;
                newrec.is_enable = false;
                objList.Add(newrec);
            }

            return objList.OrderBy(a => a.permission_name);
        }

        public IEnumerable<RolePermissionEntity> GetPermissionEmpty()
        {
            var data = db.permission.Where(a => a.is_enable).OrderBy(a=>a.permission_name).ToList();
            List<RolePermissionEntity> objList = new List<RolePermissionEntity>();

            foreach (var row in data)
            {
                RolePermissionEntity newrec = new RolePermissionEntity();
                newrec.permission_id = row.permission_id;
                newrec.permission_name = row.permission_name;
                newrec.is_enable = true;
                objList.Add(newrec);
            }

            return objList;
        }
    }
}