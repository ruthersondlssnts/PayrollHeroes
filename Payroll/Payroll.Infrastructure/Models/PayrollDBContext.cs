using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Payroll.Infrastructure.Models
{
    public partial class PayrollDBContext : DbContext
    {
        public PayrollDBContext()
        {
        }

        public PayrollDBContext(DbContextOptions<PayrollDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ref_group_dto> ref_group_dto { get; set; }
        public virtual DbSet<employee_timesheet_dto> employee_timesheet_dto { get; set; }
        public virtual DbSet<employee> employee { get; set; }
        public virtual DbSet<employee_balance> employee_balance { get; set; }
        public virtual DbSet<employee_balance_transaction> employee_balance_transaction { get; set; }
        public virtual DbSet<employee_group> employee_group { get; set; }
        public virtual DbSet<employee_role> employee_role { get; set; }
        public virtual DbSet<employee_timesheet> employee_timesheet { get; set; }
        public virtual DbSet<payroll> payroll { get; set; }
        public virtual DbSet<payroll_config> payroll_config { get; set; }
        public virtual DbSet<payroll_details> payroll_details { get; set; }
        public virtual DbSet<payroll_earning_deduction> payroll_earning_deduction { get; set; }
        public virtual DbSet<permission> permission { get; set; }
        public virtual DbSet<ref_bir> ref_bir { get; set; }
        public virtual DbSet<ref_calendar_activity> ref_calendar_activity { get; set; }
        public virtual DbSet<ref_configuration> ref_configuration { get; set; }
        public virtual DbSet<ref_day_type> ref_day_type { get; set; }
        public virtual DbSet<ref_department> ref_department { get; set; }
        public virtual DbSet<ref_department_approver> ref_department_approver { get; set; }
        public virtual DbSet<ref_employee_type> ref_employee_type { get; set; }
        public virtual DbSet<ref_group> ref_group { get; set; }
        public virtual DbSet<ref_leave_type> ref_leave_type { get; set; }
        public virtual DbSet<ref_overtime_type> ref_overtime_type { get; set; }
        public virtual DbSet<ref_pagibig> ref_pagibig { get; set; }
        public virtual DbSet<ref_pay_type> ref_pay_type { get; set; }
        public virtual DbSet<ref_payroll_cutoff> ref_payroll_cutoff { get; set; }
        public virtual DbSet<ref_payroll_details_type> ref_payroll_details_type { get; set; }
        public virtual DbSet<ref_payroll_status> ref_payroll_status { get; set; }
        public virtual DbSet<ref_philhealth> ref_philhealth { get; set; }
        public virtual DbSet<ref_shift> ref_shift { get; set; }
        public virtual DbSet<ref_shift_detail> ref_shift_detail { get; set; }
        public virtual DbSet<ref_sss> ref_sss { get; set; }
        public virtual DbSet<ref_status> ref_status { get; set; }
        public virtual DbSet<request_dtr> request_dtr { get; set; }
        public virtual DbSet<request_leave> request_leave { get; set; }
        public virtual DbSet<request_overtime> request_overtime { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<role_menu> role_menu { get; set; }
        public virtual DbSet<role_permission> role_permission { get; set; }
        public virtual DbSet<timesheet_log> timesheet_log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseLazyLoadingProxies();
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("PayrollDBEntities"));
                //optionsBuilder.UseSqlServer(Startup.Configuration.Get("Data:DefaultConnection:ConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
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

            modelBuilder.Entity<employee>(entity =>
            {
                entity.HasKey(e => e.employee_id);

                entity.HasIndex(e => e.ref_department_id)
                    .HasName("IX_FK_employee_ref_department");

                entity.HasIndex(e => e.ref_shift_id)
                    .HasName("IX_FK_employee_ref_shift");

                entity.Property(e => e.employee_id).ValueGeneratedOnAdd();

                entity.Property(e => e.contact_number)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.date_hire).HasColumnType("datetime");

                entity.Property(e => e.date_regular).HasColumnType("datetime");

                entity.Property(e => e.date_resign).HasColumnType("datetime");

                entity.Property(e => e.email_address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.employee_serial)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.fingerprint)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.first_name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.last_name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.marital_status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.middle_name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.pagibig)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.philhealth)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ref_employee_type_id).HasDefaultValueSql("((1))");

                entity.Property(e => e.ref_pay_type_id).HasDefaultValueSql("((1))");

                entity.Property(e => e.role_id).HasDefaultValueSql("((1))");

                entity.Property(e => e.sss)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.employee_)
                    .WithOne(p => p.Inverseemployee_)
                    .HasForeignKey<employee>(d => d.employee_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_employee");

                entity.HasOne(d => d.ref_department_)
                    .WithMany(p => p.employee)
                    .HasForeignKey(d => d.ref_department_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_ref_department");

                entity.HasOne(d => d.ref_employee_type_)
                    .WithMany(p => p.employee)
                    .HasForeignKey(d => d.ref_employee_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_ref_employee_type");

                entity.HasOne(d => d.ref_pay_type_)
                    .WithMany(p => p.employee)
                    .HasForeignKey(d => d.ref_pay_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_ref_pay_type");

                entity.HasOne(d => d.ref_shift_)
                    .WithMany(p => p.employee)
                    .HasForeignKey(d => d.ref_shift_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_ref_shift");

                entity.HasOne(d => d.role_)
                    .WithMany(p => p.employee)
                    .HasForeignKey(d => d.role_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_role");
            });

            modelBuilder.Entity<employee_balance>(entity =>
            {
                entity.HasKey(e => e.employee_balance_id);

                entity.Property(e => e.acquire_date).HasColumnType("datetime");

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.expire_date).HasColumnType("datetime");

                entity.Property(e => e.remarks)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.employee_)
                    .WithMany(p => p.employee_balance)
                    .HasForeignKey(d => d.employee_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_balance_employee");

                entity.HasOne(d => d.ref_leave_type_)
                    .WithMany(p => p.employee_balance)
                    .HasForeignKey(d => d.ref_leave_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_balance_ref_leave_type");
            });

            modelBuilder.Entity<employee_balance_transaction>(entity =>
            {
                entity.HasKey(e => e.employee_balance_transaction_id);

                entity.Property(e => e.approved_date).HasColumnType("datetime");

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.requested_date).HasColumnType("datetime");

                entity.HasOne(d => d.employee_balance_)
                    .WithMany(p => p.employee_balance_transaction)
                    .HasForeignKey(d => d.employee_balance_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_balance_transaction_employee_balance");

                entity.HasOne(d => d.employee_)
                    .WithMany(p => p.employee_balance_transaction)
                    .HasForeignKey(d => d.employee_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_balance_transaction_employee");
            });

            modelBuilder.Entity<employee_group>(entity =>
            {
                entity.HasKey(e => e.employee_id);

                entity.Property(e => e.employee_id).ValueGeneratedNever();

                entity.HasOne(d => d.employee_)
                    .WithOne(p => p.employee_group)
                    .HasForeignKey<employee_group>(d => d.employee_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_group_employee");

                entity.HasOne(d => d.group_)
                    .WithMany(p => p.employee_group)
                    .HasForeignKey(d => d.group_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_group_ref_group");
            });

            modelBuilder.Entity<employee_role>(entity =>
            {
                entity.HasKey(e => e.employee_role_id);
            });

            modelBuilder.Entity<employee_timesheet>(entity =>
            {
                entity.HasKey(e => e.employee_timesheet_id);

                entity.HasIndex(e => e.employee_id)
                    .HasName("IX_FK_employee_timesheet_employee");

                entity.HasIndex(e => e.ref_day_type_id)
                    .HasName("IX_FK_employee_timesheet_ref_day_type");

                entity.HasIndex(e => e.ref_shift_id)
                    .HasName("IX_FK_employee_timesheet_ref_shift");

                entity.Property(e => e.ot_in)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ot_out)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.shift_date).HasColumnType("datetime");

                entity.Property(e => e.time_in1)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.time_in2)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.time_out1)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.time_out2)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.employee_)
                    .WithMany(p => p.employee_timesheet)
                    .HasForeignKey(d => d.employee_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_timesheet_employee");

                entity.HasOne(d => d.ref_day_type_)
                    .WithMany(p => p.employee_timesheet)
                    .HasForeignKey(d => d.ref_day_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_timesheet_ref_day_type");

                entity.HasOne(d => d.ref_shift_)
                    .WithMany(p => p.employee_timesheet)
                    .HasForeignKey(d => d.ref_shift_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_timesheet_ref_shift");
            });

            modelBuilder.Entity<payroll>(entity =>
            {
                entity.HasKey(e => e.payroll_id);

                entity.Property(e => e.payroll_id).ValueGeneratedOnAdd();

                entity.Property(e => e.date_process).HasColumnType("datetime");

                entity.Property(e => e.total_deduction).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.total_earnings).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.payroll_)
                    .WithOne(p => p.Inversepayroll_)
                    .HasForeignKey<payroll>(d => d.payroll_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_payroll_payroll");
            });

            modelBuilder.Entity<payroll_config>(entity =>
            {
                entity.HasKey(e => e.payroll_config_id);

                entity.Property(e => e.config_name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<payroll_details>(entity =>
            {
                entity.HasKey(e => e.payroll_details_id);

                entity.Property(e => e.payroll_details_id).ValueGeneratedOnAdd();

                entity.Property(e => e.amount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.qty).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.payroll_details_)
                    .WithOne(p => p.Inversepayroll_details_)
                    .HasForeignKey<payroll_details>(d => d.payroll_details_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_payroll_details_payroll_details");

                entity.HasOne(d => d.payroll_)
                    .WithMany(p => p.payroll_details)
                    .HasForeignKey(d => d.payroll_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_payroll_details_payroll");

                entity.HasOne(d => d.ref_payroll_details_type_)
                    .WithMany(p => p.payroll_details)
                    .HasForeignKey(d => d.ref_payroll_details_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_payroll_details_ref_payroll_details_type");
            });

            modelBuilder.Entity<payroll_earning_deduction>(entity =>
            {
                entity.HasKey(e => e.payroll_earning_deduction_id);

                entity.Property(e => e.amount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.cutoff).HasDefaultValueSql("((1))");

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.exact_date).HasColumnType("datetime");

                entity.Property(e => e.ref_employee_type_id).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ref_payroll_details_type_)
                    .WithMany(p => p.payroll_earning_deduction)
                    .HasForeignKey(d => d.ref_payroll_details_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_payroll_earning_deduction_ref_payroll_details_type");
            });

            modelBuilder.Entity<permission>(entity =>
            {
                entity.HasKey(e => e.permission_id);

                entity.Property(e => e.permission_code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.permission_name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_bir>(entity =>
            {
                entity.HasKey(e => e.ref_bir_id);

                entity.Property(e => e.add_tax).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.multiplier).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.salary_from).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.salary_to).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.subtract_tax_over).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<ref_calendar_activity>(entity =>
            {
                entity.HasKey(e => e.ref_calendar_activity_id);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.work_date).HasColumnType("datetime");

                entity.HasOne(d => d.ref_day_type_)
                    .WithMany(p => p.ref_calendar_activity)
                    .HasForeignKey(d => d.ref_day_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ref_calendar_activity_ref_day_type");
            });

            modelBuilder.Entity<ref_configuration>(entity =>
            {
                entity.HasKey(e => e.config_id);

                entity.Property(e => e.ptext)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.pvalue)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_day_type>(entity =>
            {
                entity.HasKey(e => e.ref_day_type_id);

                entity.Property(e => e.date_type_code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.date_type_name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.nightdif_multiplier1).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.nightdif_multiplier2).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ot8_multiplier).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ot_multiplier).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<ref_department>(entity =>
            {
                entity.HasKey(e => e.ref_department_id);

                entity.Property(e => e.department_name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_department_approver>(entity =>
            {
                entity.HasKey(e => e.ref_department_approver_id);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.HasOne(d => d.employee_)
                    .WithMany(p => p.ref_department_approver)
                    .HasForeignKey(d => d.employee_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ref_department_approver_employee");

                entity.HasOne(d => d.ref_department_)
                    .WithMany(p => p.ref_department_approver)
                    .HasForeignKey(d => d.ref_department_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ref_department_approver_ref_department");
            });

            modelBuilder.Entity<ref_employee_type>(entity =>
            {
                entity.HasKey(e => e.ref_employee_type_id);

                entity.Property(e => e.date_deleted)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ref_employee_type_name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_group>(entity =>
            {
                entity.HasKey(e => e.group_id);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.is_enable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<ref_leave_type>(entity =>
            {
                entity.HasKey(e => e.ref_leave_type_id);

                entity.Property(e => e.ref_leave_type_code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ref_leave_type_name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_overtime_type>(entity =>
            {
                entity.HasKey(e => e.ref_overtime_type_id);

                entity.Property(e => e.ref_overtime_type_name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_pagibig>(entity =>
            {
                entity.HasKey(e => e.ref_pagibig_id);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_pay_type>(entity =>
            {
                entity.HasKey(e => e.ref_pay_type_id);

                entity.Property(e => e.ref_pay_type_id).ValueGeneratedOnAdd();

                entity.Property(e => e.ref_pay_type_name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ref_pay_type_)
                    .WithOne(p => p.Inverseref_pay_type_)
                    .HasForeignKey<ref_pay_type>(d => d.ref_pay_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ref_pay_type_ref_pay_type");
            });

            modelBuilder.Entity<ref_payroll_cutoff>(entity =>
            {
                entity.HasKey(e => e.ref_payroll_cutoff_id);

                entity.Property(e => e.cutoff).HasDefaultValueSql("((1))");

                entity.Property(e => e.cutoff_date_end).HasColumnType("datetime");

                entity.Property(e => e.cutoff_date_start).HasColumnType("datetime");

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.pay_date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ref_payroll_details_type>(entity =>
            {
                entity.HasKey(e => e.ref_payroll_details_type_id);

                entity.Property(e => e.ref_payroll_details_type_code)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ref_payroll_details_type_name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_payroll_status>(entity =>
            {
                entity.HasKey(e => e.ref_payroll_status_id);

                entity.Property(e => e.payroll_status_name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_philhealth>(entity =>
            {
                entity.HasKey(e => e.ref_philhealth_id);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_shift>(entity =>
            {
                entity.HasKey(e => e.ref_shift_id);

                entity.Property(e => e.break_in)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.break_out)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.is_auto_ot)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.nd_end)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.nd_end2)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.nd_start)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.nd_start2)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.shift_in)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.shift_name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.shift_out)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_shift_detail>(entity =>
            {
                entity.HasKey(e => e.ref_shift_detail_id);

                entity.HasIndex(e => e.ref_shift_id)
                    .HasName("IX_FK_ref_shift_detail_ref_shift");

                entity.Property(e => e.day)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ref_shift_)
                    .WithMany(p => p.ref_shift_detail)
                    .HasForeignKey(d => d.ref_shift_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ref_shift_detail_ref_shift");
            });

            modelBuilder.Entity<ref_sss>(entity =>
            {
                entity.HasKey(e => e.ref_sss_id);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ref_status>(entity =>
            {
                entity.HasKey(e => e.ref_status_id);

                entity.Property(e => e.ref_status_name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<request_dtr>(entity =>
            {
                entity.HasKey(e => e.request_dtr_id);

                entity.Property(e => e.approver_date).HasColumnType("datetime");

                entity.Property(e => e.approver_remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.group_id).HasDefaultValueSql("((1))");

                entity.Property(e => e.reason)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.shift_date).HasColumnType("datetime");

                entity.Property(e => e.time_in)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.time_out)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.employee_)
                    .WithMany(p => p.request_dtr)
                    .HasForeignKey(d => d.employee_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_dtr_employee");

                entity.HasOne(d => d.ref_shift_)
                    .WithMany(p => p.request_dtr)
                    .HasForeignKey(d => d.ref_shift_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_dtr_ref_shift");

                entity.HasOne(d => d.ref_status_)
                    .WithMany(p => p.request_dtr)
                    .HasForeignKey(d => d.ref_status_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_dtr_ref_status");
            });

            modelBuilder.Entity<request_leave>(entity =>
            {
                entity.HasKey(e => e.request_leave_id);

                entity.Property(e => e.approver_date).HasColumnType("datetime");

                entity.Property(e => e.approver_remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.group_id).HasDefaultValueSql("((1))");

                entity.Property(e => e.leave_date).HasColumnType("datetime");

                entity.Property(e => e.leave_day).HasColumnType("numeric(3, 2)");

                entity.Property(e => e.reason)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.employee_)
                    .WithMany(p => p.request_leave)
                    .HasForeignKey(d => d.employee_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_leave_employee");

                entity.HasOne(d => d.ref_department_)
                    .WithMany(p => p.request_leave)
                    .HasForeignKey(d => d.ref_department_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_leave_ref_department");

                entity.HasOne(d => d.ref_leave_type_)
                    .WithMany(p => p.request_leave)
                    .HasForeignKey(d => d.ref_leave_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_leave_ref_leave_type");

                entity.HasOne(d => d.ref_status_)
                    .WithMany(p => p.request_leave)
                    .HasForeignKey(d => d.ref_status_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_leave_ref_status");
            });

            modelBuilder.Entity<request_overtime>(entity =>
            {
                entity.HasKey(e => e.request_overtime_id);

                entity.Property(e => e.approver_date).HasColumnType("datetime");

                entity.Property(e => e.approver_remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.group_id).HasDefaultValueSql("((1))");

                entity.Property(e => e.overtime_date).HasColumnType("datetime");

                entity.Property(e => e.reason)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.time_in)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.time_out)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.employee_)
                    .WithMany(p => p.request_overtime)
                    .HasForeignKey(d => d.employee_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_overtime_employee");

                entity.HasOne(d => d.ref_department_)
                    .WithMany(p => p.request_overtime)
                    .HasForeignKey(d => d.ref_department_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_overtime_ref_department");

                entity.HasOne(d => d.ref_overtime_type_)
                    .WithMany(p => p.request_overtime)
                    .HasForeignKey(d => d.ref_overtime_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_overtime_ref_overtime_type");

                entity.HasOne(d => d.ref_status_)
                    .WithMany(p => p.request_overtime)
                    .HasForeignKey(d => d.ref_status_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_request_overtime_ref_status");
            });

            modelBuilder.Entity<role>(entity =>
            {
                entity.HasKey(e => e.role_id);

                entity.Property(e => e.role_id).ValueGeneratedOnAdd();

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.role_name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.role_)
                    .WithOne(p => p.Inverserole_)
                    .HasForeignKey<role>(d => d.role_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_role_role");
            });

            modelBuilder.Entity<role_menu>(entity =>
            {
                entity.HasKey(e => e.role_menu_id);

                entity.Property(e => e.action_name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.controller_name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.date_deleted).HasColumnType("datetime");

                entity.Property(e => e.display_icon)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.display_name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.is_enable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<role_permission>(entity =>
            {
                entity.HasKey(e => e.role_permission_id);

                entity.HasOne(d => d.permission_)
                    .WithMany(p => p.role_permission)
                    .HasForeignKey(d => d.permission_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_role_permission_permission");

                entity.HasOne(d => d.role_)
                    .WithMany(p => p.role_permission)
                    .HasForeignKey(d => d.role_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_role_permission_role");
            });

            modelBuilder.Entity<timesheet_log>(entity =>
            {
                entity.HasKey(e => e.timesheet_log_id);

                entity.Property(e => e.timesheet_log_id).ValueGeneratedNever();

                entity.Property(e => e.datetime_in).HasColumnType("datetime");

                entity.Property(e => e.datetime_out).HasColumnType("datetime");

                entity.Property(e => e.employee_serial)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
