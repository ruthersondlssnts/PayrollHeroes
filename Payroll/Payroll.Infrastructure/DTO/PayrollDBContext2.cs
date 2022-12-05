using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Payroll.Infrastructure.Models
{
    public partial class PayrollDBContext2
    {
        //THIS IS JUST A TEMPORARY.Copy this to PayrollDBContext

        public virtual DbSet<ref_group_dto> ref_group_dto { get; set; }
        public virtual DbSet<employee_timesheet_dto> employee_timesheet_dto { get; set; }

        protected void OnModelCreating2(ModelBuilder modelBuilder)
        {

            // Necessary, since our model isnt a EF model
            modelBuilder.Entity<ref_group_dto>(entity =>
            {
                entity.HasKey(e => e.group_id);

            });

            modelBuilder.Entity<employee_timesheet_dto>(entity =>
            {
                entity.HasKey(e => e.employee_timesheet_id);

            });
        }
    }
}
