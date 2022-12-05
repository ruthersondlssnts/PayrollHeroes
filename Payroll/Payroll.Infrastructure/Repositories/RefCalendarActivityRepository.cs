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
    public class RefCalendarActivityRepository : IRefCalendarActivity
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public RefCalendarActivityRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RefCalendarActivityEntity, ref_calendar_activity>();
                cfg.CreateMap<ref_calendar_activity, RefCalendarActivityEntity>();

                cfg.CreateMap<ref_day_type, RefDayTypeEntity>();
                cfg.CreateMap<RefDayTypeEntity, ref_day_type>();

            });
            _mapper = config.CreateMapper();
        }
        public bool Add(RefCalendarActivityEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<RefCalendarActivityEntity, ref_calendar_activity>(entity);

            db.ref_calendar_activity.Add(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public bool Update(RefCalendarActivityEntity entity)
        {
            bool blnReturn = true;

            var dataEntity = _mapper.Map<RefCalendarActivityEntity, ref_calendar_activity>(entity);
            db.ref_calendar_activity.Update(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public IEnumerable<RefCalendarActivityEntity> GetList()
        {
            var data = db.ref_calendar_activity.Include(a=>a.ref_day_type_).Where(a=>a.date_deleted == null).ToList();
            var dataEntity = _mapper.Map<IEnumerable<ref_calendar_activity>, IEnumerable<RefCalendarActivityEntity>>(data);
            return dataEntity;
        }
        public bool CreateOrUpdate(RefCalendarActivityEntity obj)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<RefCalendarActivityEntity, ref_calendar_activity>(obj);

            if (dataEntity.ref_calendar_activity_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public RefCalendarActivityEntity GetByID(int id)
        {
            var data = db.ref_calendar_activity.Where(a => a.ref_calendar_activity_id == id).FirstOrDefault();
            return _mapper.Map<ref_calendar_activity, RefCalendarActivityEntity>(data);
        }

        public bool Delete(int id)
        {
            var data = db.ref_calendar_activity.Where(a => a.ref_calendar_activity_id == id).FirstOrDefault();
            if (data != null)
            {
                data.date_deleted = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<RefDayTypeEntity> GetDayType()
        {
            //Exclude RWD, RD and + RD
            // Get only holidays
            List<int> listEx = new List<int>() { 1,2,4,6,8 };
            //var data = db.ref_day_type.Where(a => a.ref_day_type_id != 1 || a.ref_day_type_id != 2 ).ToList();
            var data = db.ref_day_type.Where(p => !listEx.Contains(p.ref_day_type_id)).ToList();
            return _mapper.Map<IEnumerable<ref_day_type>, IEnumerable<RefDayTypeEntity>>(data);
            
        }

        public bool Exist(DateTime dtDate)
        {


            var data = db.ref_calendar_activity.Where(a => a.work_date == dtDate && a.date_deleted == null).FirstOrDefault();
            if (data != null) return true;

            return false;
        }
    }
}