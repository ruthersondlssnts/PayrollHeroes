using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class RefPayrollCutoffRepository : IRefPayrollCutoff
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public RefPayrollCutoffRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RefPayrollCutoffEntity, ref_payroll_cutoff>();
                cfg.CreateMap<ref_payroll_cutoff, RefPayrollCutoffEntity>();

            });
            _mapper = config.CreateMapper();
        }
        public bool CreateOrUpdate(RefPayrollCutoffEntity obj)
        {
            bool blnReturn = true;
            if (obj.ref_payroll_cutoff_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public IEnumerable<RefPayrollCutoffEntity> GetList()
        {
            IEnumerable<RefPayrollCutoffEntity> record;
            var data = db.ref_payroll_cutoff.Where(a =>  a.date_deleted == null).ToList();
            record = _mapper.Map<IEnumerable<ref_payroll_cutoff>, IEnumerable<RefPayrollCutoffEntity>>(data);

            return record;
        }

    

        public bool Add(RefPayrollCutoffEntity entity)
        {
            try
            {
                var dataEntity = _mapper.Map<RefPayrollCutoffEntity, ref_payroll_cutoff>(entity);
                db.ref_payroll_cutoff.Add(dataEntity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Update(RefPayrollCutoffEntity entity)
        {
            try
            {
                var data = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == entity.ref_payroll_cutoff_id && a.date_deleted == null).FirstOrDefault();

                if (data != null)
                {
                    data.cutoff_date_start = entity.cutoff_date_start;
                    data.cutoff_date_end = entity.cutoff_date_end;
                    data.government = entity.government;
                    db.SaveChanges();
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
        public RefPayrollCutoffEntity GetByID(int id)
        {
            var data = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == id && a.date_deleted == null).FirstOrDefault();
            return _mapper.Map<ref_payroll_cutoff, RefPayrollCutoffEntity>(data);
        }

        public RefPayrollCutoffEntity GetDetailByID(int id)
        {
            var data = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == id).FirstOrDefault();
            return _mapper.Map<ref_payroll_cutoff, RefPayrollCutoffEntity>(data);
        }

        public bool Delete(int id)
        {
            var data = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == id).FirstOrDefault();
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
