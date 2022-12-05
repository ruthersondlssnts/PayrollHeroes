using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Payroll.Core.Entities;
using Payroll.Core.Interface;
using Payroll.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Repositories
{
    public class RefShiftRepository : IRefShift
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public RefShiftRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RefShiftEntity, ref_shift>();
                cfg.CreateMap<ref_shift, RefShiftEntity>();

                cfg.CreateMap<RefShiftDetailEntity, ref_shift_detail>();
                cfg.CreateMap<ref_shift_detail, RefShiftDetailEntity>();

            });
            _mapper = config.CreateMapper();
        }
        public bool CreateOrUpdate(RefShiftEntity obj)
        {
            bool blnReturn = true;
            
            var detail = JsonConvert.DeserializeObject<List<RefShiftDetailEntity>>(obj.ref_shift_detail_.ToString());
            obj.ref_shift_detail = detail;
            if (obj.ref_shift_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public IEnumerable<RefShiftEntity> GetList()
        {
            IEnumerable<RefShiftEntity> record;
            var data = db.ref_shift.Where(a =>  a.date_deleted == null).ToList();
            record = _mapper.Map<IEnumerable<ref_shift>, IEnumerable<RefShiftEntity>>(data);

            return record;
        }

        public RefShiftEntity GetShift(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RefShiftDetailEntity> GetShiftDetails(int id)
        {
            IEnumerable<RefShiftDetailEntity> record;
            var data = db.ref_shift_detail.Where(a => a.ref_shift_id == id).ToList();
            return _mapper.Map<IEnumerable<ref_shift_detail>, IEnumerable<RefShiftDetailEntity>>(data);
        }

        public bool Add(RefShiftEntity entity)
        {
            try
            {
                var dataEntity = _mapper.Map<RefShiftEntity, ref_shift>(entity);
                db.ref_shift.Add(dataEntity);
                db.SaveChanges();

                //Insert Shift Detailes
                foreach (var row in entity.ref_shift_detail)
                {
                    //var main = db.ref_shift.Where(a => a.shift_name == entity.shift_name).FirstOrDefault();
                    var datadetail = _mapper.Map<RefShiftDetailEntity, ref_shift_detail>(row);
                    if (datadetail.rest_day) { datadetail.required_hour = 0; }
                    if (row.required_hour > 0) datadetail.rest_day = false;
                    datadetail.ref_shift_id = dataEntity.ref_shift_id;
                    db.ref_shift_detail.Add(datadetail);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Update(RefShiftEntity entity)
        {
            try
            {
                var data = db.ref_shift.Where(a => a.ref_shift_id == entity.ref_shift_id && a.date_deleted == null).FirstOrDefault();

                if (data != null)
                {
                    data.shift_name = entity.shift_name;
                    data.break_in = entity.break_in;
                    data.break_out = entity.break_out;
                    data.required_hour = entity.required_hour;
                    data.shift_in = entity.shift_in;
                    data.shift_out = entity.shift_out;
                    data.nd_start = entity.nd_start;
                    data.nd_end = entity.nd_end;

                    data.grace_period = entity.grace_period;
                    data.include_grace_period = entity.include_grace_period;
                    data.is_auto_ot = entity.is_auto_ot;
                    data.is_nd = entity.is_nd;


                    db.SaveChanges();

                    //Update Shift Detailes
                    foreach (var row in entity.ref_shift_detail)
                    {
                        var detail = db.ref_shift_detail.Where(a=>a.ref_shift_detail_id == row.ref_shift_detail_id).FirstOrDefault();

                        if (detail != null)
                        {
                            detail.required_hour = row.required_hour;
                            if (row.required_hour > 0) detail.rest_day = false;
                            //if (row.rest_day) { detail.required_hour = 0; }
                            db.SaveChanges();
                        } 
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public RefShiftEntity GetByID(int id)
        {
            var data = db.ref_shift.Where(a => a.ref_shift_id == id && a.date_deleted == null).FirstOrDefault();
            return _mapper.Map<ref_shift, RefShiftEntity>(data);
        }

        public RefShiftDetailEntity GetDetailByID(int id)
        {
            var data = db.ref_shift_detail.Where(a => a.ref_shift_id == id).FirstOrDefault();
            return _mapper.Map<ref_shift_detail, RefShiftDetailEntity>(data);
        }

        public bool Delete(int id)
        {
            var data = db.ref_shift.Where(a => a.ref_shift_id == id).FirstOrDefault();
            if (data != null)
            {
                data.date_deleted = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Exist(string name)
        {
            var data = db.ref_shift.Where(a => a.shift_name == name).FirstOrDefault();
            if (data != null)
            {
                return true;
            }
            return false;
        }

        public List<RefShiftDetailEntity> GetDaysEmpty()
        {
            List<RefShiftDetailEntity> newRec = new List<RefShiftDetailEntity>();
            //MON
            RefShiftDetailEntity rec = new RefShiftDetailEntity();
            rec.day = "MON";
            rec.required_hour = 9;
            rec.rest_day = false;
            newRec.Add(rec);

            rec = new RefShiftDetailEntity();
            rec.day = "TUE";
            rec.required_hour = 9;
            rec.rest_day = false;
            newRec.Add(rec);

            rec = new RefShiftDetailEntity();
            rec.day = "WED";
            rec.required_hour = 9;
            rec.rest_day = false;
            newRec.Add(rec);

            rec = new RefShiftDetailEntity();
            rec.day = "THU";
            rec.required_hour = 9;
            rec.rest_day = false;
            newRec.Add(rec);

            rec = new RefShiftDetailEntity();
            rec.day = "FRI";
            rec.required_hour = 9;
            rec.rest_day = false;
            newRec.Add(rec);

            rec = new RefShiftDetailEntity();
            rec.day = "SAT";
            rec.required_hour = 0;
            rec.rest_day = true;
            newRec.Add(rec);

            rec = new RefShiftDetailEntity();
            rec.day = "SUN";
            rec.required_hour = 0;
            rec.rest_day = true;
            newRec.Add(rec);

            return newRec;
        }
    }
}
