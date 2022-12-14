USE [master]
GO
/****** Object:  Database [PayrollDB]    Script Date: 4/12/2020 9:07:19 AM ******/
CREATE DATABASE [PayrollDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PayrollDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PayrollDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PayrollDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PayrollDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PayrollDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PayrollDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PayrollDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PayrollDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PayrollDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PayrollDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PayrollDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PayrollDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PayrollDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PayrollDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PayrollDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PayrollDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PayrollDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PayrollDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PayrollDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PayrollDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PayrollDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PayrollDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PayrollDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PayrollDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PayrollDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PayrollDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PayrollDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PayrollDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PayrollDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PayrollDB] SET  MULTI_USER 
GO
ALTER DATABASE [PayrollDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PayrollDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PayrollDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PayrollDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PayrollDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PayrollDB] SET QUERY_STORE = OFF
GO
USE [PayrollDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [PayrollDB]
GO
/****** Object:  Table [dbo].[employee]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee](
	[employee_id] [int] IDENTITY(1,1) NOT NULL,
	[employee_serial] [varchar](20) NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[last_name] [varchar](200) NULL,
	[first_name] [varchar](200) NULL,
	[middle_name] [varchar](200) NULL,
	[email_address] [varchar](200) NULL,
	[gender] [varchar](10) NULL,
	[contact_number] [varchar](100) NULL,
	[sss] [varchar](50) NULL,
	[pagibig] [varchar](50) NULL,
	[philhealth] [varchar](50) NULL,
	[marital_status] [varchar](20) NULL,
	[date_hire] [datetime] NULL,
	[date_resign] [datetime] NULL,
	[ref_shift_id] [int] NOT NULL,
	[ref_pay_type_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
	[ref_department_id] [int] NOT NULL,
	[fingerprint] [varchar](50) NULL,
	[basic_pay] [decimal](18, 2) NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_employees] PRIMARY KEY CLUSTERED 
(
	[employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employee_balance]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee_balance](
	[employee_balance_id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[ref_leave_type_id] [int] NOT NULL,
	[acquire_date] [datetime] NOT NULL,
	[expire_date] [datetime] NOT NULL,
	[quantity] [decimal](18, 2) NOT NULL,
	[remarks] [varchar](200) NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_employee_balance] PRIMARY KEY CLUSTERED 
(
	[employee_balance_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employee_balance_transaction]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee_balance_transaction](
	[employee_balance_transaction_id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[employee_balance_id] [int] NOT NULL,
	[requested_date] [datetime] NOT NULL,
	[approved_date] [datetime] NOT NULL,
	[quantity] [decimal](18, 2) NOT NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_employee_balance_transaction] PRIMARY KEY CLUSTERED 
(
	[employee_balance_transaction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employee_group]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee_group](
	[employee_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[approver_all] [bit] NOT NULL,
	[ot_approver] [bit] NOT NULL,
	[ob_approver] [bit] NOT NULL,
	[leave_approver] [bit] NOT NULL,
	[dtr_approver] [bit] NOT NULL,
 CONSTRAINT [PK_employee_group] PRIMARY KEY CLUSTERED 
(
	[employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employee_role]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee_role](
	[employee_role_id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
 CONSTRAINT [PK_employee_role] PRIMARY KEY CLUSTERED 
(
	[employee_role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employee_timesheet]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee_timesheet](
	[employee_timesheet_id] [bigint] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[shift_date] [datetime] NULL,
	[ref_shift_id] [int] NOT NULL,
	[ref_day_type_id] [int] NOT NULL,
	[holiday_day_type_id] [int] NULL,
	[required_hour] [decimal](18, 2) NULL,
	[rendered_hour] [decimal](18, 2) NULL,
	[time_in1] [varchar](5) NULL,
	[time_out1] [varchar](5) NULL,
	[time_in2] [varchar](5) NULL,
	[time_out2] [varchar](5) NULL,
	[ot_in] [varchar](5) NULL,
	[ot_out] [varchar](5) NULL,
	[ot] [decimal](18, 2) NULL,
	[ot8] [decimal](18, 2) NULL,
	[night_dif] [decimal](18, 2) NULL,
	[night_dif8] [decimal](18, 2) NULL,
	[absent] [decimal](18, 2) NULL,
	[late] [decimal](18, 2) NULL,
	[undertime] [decimal](18, 2) NULL,
	[approve_leave] [decimal](18, 2) NULL,
	[ref_leave_type_id] [int] NULL,
	[payroll_process] [bit] NOT NULL,
 CONSTRAINT [PK_employee_timesheet] PRIMARY KEY CLUSTERED 
(
	[employee_timesheet_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payroll]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payroll](
	[payroll_id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[ref_payroll_cutoff_id] [int] NOT NULL,
	[total_earnings] [numeric](18, 2) NOT NULL,
	[total_deduction] [numeric](18, 2) NOT NULL,
	[date_process] [datetime] NOT NULL,
 CONSTRAINT [PK_payroll] PRIMARY KEY CLUSTERED 
(
	[payroll_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payroll_config]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payroll_config](
	[payroll_config_id] [int] IDENTITY(1,1) NOT NULL,
	[config_name] [varchar](50) NULL,
	[config_value] [int] NOT NULL,
 CONSTRAINT [PK_payroll_config] PRIMARY KEY CLUSTERED 
(
	[payroll_config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payroll_details]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payroll_details](
	[payroll_details_id] [int] IDENTITY(1,1) NOT NULL,
	[payroll_id] [int] NOT NULL,
	[ref_payroll_details_type_id] [int] NOT NULL,
	[qty] [numeric](18, 2) NULL,
	[amount] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_payroll_details] PRIMARY KEY CLUSTERED 
(
	[payroll_details_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payroll_earning_deduction]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payroll_earning_deduction](
	[payroll_earning_deduction_id] [int] IDENTITY(1,1) NOT NULL,
	[ref_payroll_details_type_id] [int] NOT NULL,
	[exact_date] [datetime] NULL,
	[cutoff] [int] NOT NULL,
	[amount] [numeric](18, 2) NOT NULL,
	[recurring] [bit] NOT NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_payroll_earning_deduction] PRIMARY KEY CLUSTERED 
(
	[payroll_earning_deduction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[permission]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permission](
	[permission_id] [int] IDENTITY(1,1) NOT NULL,
	[permission_name] [varchar](50) NULL,
	[permission_code] [varchar](50) NULL,
	[is_enable] [bit] NOT NULL,
 CONSTRAINT [PK_permission] PRIMARY KEY CLUSTERED 
(
	[permission_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_bir]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_bir](
	[ref_bir_id] [int] IDENTITY(1,1) NOT NULL,
	[ref_pay_type_id] [int] NOT NULL,
	[salary_from] [numeric](18, 2) NOT NULL,
	[salary_to] [numeric](18, 2) NOT NULL,
	[add_tax] [numeric](18, 2) NOT NULL,
	[subtract_tax_over] [numeric](18, 2) NOT NULL,
	[multiplier] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_ref_bir] PRIMARY KEY CLUSTERED 
(
	[ref_bir_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_calendar_activity]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_calendar_activity](
	[ref_calendar_activity_id] [int] IDENTITY(1,1) NOT NULL,
	[work_date] [datetime] NOT NULL,
	[ref_day_type_id] [int] NOT NULL,
	[description] [varchar](200) NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_ref_calendar_activity] PRIMARY KEY CLUSTERED 
(
	[ref_calendar_activity_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_configuration]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_configuration](
	[config_id] [int] IDENTITY(1,1) NOT NULL,
	[ptext] [varchar](50) NOT NULL,
	[pvalue] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ref_configuration] PRIMARY KEY CLUSTERED 
(
	[config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_day_type]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_day_type](
	[ref_day_type_id] [int] IDENTITY(1,1) NOT NULL,
	[date_type_code] [varchar](50) NULL,
	[date_type_name] [varchar](50) NULL,
	[ot_multiplier] [numeric](18, 2) NOT NULL,
	[nightdif_multiplier1] [numeric](18, 2) NOT NULL,
	[nightdif_multiplier2] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_ref_day_type] PRIMARY KEY CLUSTERED 
(
	[ref_day_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_department]    Script Date: 4/12/2020 9:07:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_department](
	[ref_department_id] [int] IDENTITY(1,1) NOT NULL,
	[department_name] [varchar](100) NULL,
 CONSTRAINT [PK_ref_department] PRIMARY KEY CLUSTERED 
(
	[ref_department_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_department_approver]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_department_approver](
	[ref_department_approver_id] [int] IDENTITY(1,1) NOT NULL,
	[ref_department_id] [int] NOT NULL,
	[employee_id] [int] NOT NULL,
	[ordering] [int] NOT NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_ref_department_approver] PRIMARY KEY CLUSTERED 
(
	[ref_department_approver_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_group]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_group](
	[group_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](256) NOT NULL,
	[level] [hierarchyid] NULL,
	[date_deleted] [datetime] NULL,
	[is_enable] [bit] NOT NULL,
 CONSTRAINT [PK_group_id] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_leave_type]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_leave_type](
	[ref_leave_type_id] [int] IDENTITY(1,1) NOT NULL,
	[ref_leave_type_name] [varchar](100) NULL,
	[ref_leave_type_code] [varchar](50) NULL,
	[with_pay] [bit] NOT NULL,
 CONSTRAINT [PK_ref_leave_type] PRIMARY KEY CLUSTERED 
(
	[ref_leave_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_overtime_type]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_overtime_type](
	[ref_overtime_type_id] [int] IDENTITY(1,1) NOT NULL,
	[ref_overtime_type_name] [varchar](100) NULL,
 CONSTRAINT [PK_ref_overtime_type] PRIMARY KEY CLUSTERED 
(
	[ref_overtime_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_pagibig]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_pagibig](
	[ref_pagibig_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[salary_from] [decimal](18, 2) NULL,
	[salary_to] [decimal](18, 2) NULL,
	[employee_contribution] [decimal](18, 2) NULL,
	[employer_contribution] [decimal](18, 2) NULL,
	[flat_rate] [bit] NOT NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_ref_pagibig] PRIMARY KEY CLUSTERED 
(
	[ref_pagibig_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_pay_type]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_pay_type](
	[ref_pay_type_id] [int] IDENTITY(1,1) NOT NULL,
	[ref_pay_type_name] [varchar](20) NULL,
 CONSTRAINT [PK_ref_pay_type] PRIMARY KEY CLUSTERED 
(
	[ref_pay_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_payroll_cutoff]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_payroll_cutoff](
	[ref_payroll_cutoff_id] [int] IDENTITY(1,1) NOT NULL,
	[pay_date] [datetime] NOT NULL,
	[cutoff_date_start] [datetime] NOT NULL,
	[cutoff_date_end] [datetime] NOT NULL,
	[is_processed] [bit] NOT NULL,
	[date_deleted] [datetime] NULL,
	[government] [bit] NOT NULL,
	[cutoff] [int] NOT NULL,
 CONSTRAINT [PK_ref_payroll_cutoff] PRIMARY KEY CLUSTERED 
(
	[ref_payroll_cutoff_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_payroll_details_type]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_payroll_details_type](
	[ref_payroll_details_type_id] [int] IDENTITY(1,1) NOT NULL,
	[ref_payroll_details_type_name] [varchar](100) NULL,
	[ref_payroll_details_type_code] [varchar](100) NULL,
	[ref_day_type_id] [int] NULL,
	[earnings] [bit] NOT NULL,
	[taxable] [bit] NOT NULL,
	[company_contribution] [bit] NOT NULL,
 CONSTRAINT [PK_ref_payroll_details_type] PRIMARY KEY CLUSTERED 
(
	[ref_payroll_details_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_payroll_status]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_payroll_status](
	[ref_payroll_status_id] [int] IDENTITY(1,1) NOT NULL,
	[payroll_status_name] [varchar](50) NULL,
 CONSTRAINT [PK_ref_payroll_status] PRIMARY KEY CLUSTERED 
(
	[ref_payroll_status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_philhealth]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_philhealth](
	[ref_philhealth_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[salary_from] [decimal](18, 2) NULL,
	[salary_to] [decimal](18, 2) NULL,
	[employee_contribution] [decimal](18, 2) NULL,
	[employer_contribution] [decimal](18, 2) NULL,
	[flat_rate] [bit] NOT NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_ref_philhealth] PRIMARY KEY CLUSTERED 
(
	[ref_philhealth_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_shift]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_shift](
	[ref_shift_id] [int] IDENTITY(1,1) NOT NULL,
	[shift_name] [varchar](50) NULL,
	[shift_in] [varchar](5) NOT NULL,
	[shift_out] [varchar](5) NOT NULL,
	[break_in] [varchar](5) NOT NULL,
	[break_out] [varchar](5) NOT NULL,
	[break_hour] [decimal](18, 2) NULL,
	[required_hour] [decimal](18, 2) NULL,
	[nd_start] [varchar](5) NULL,
	[nd_end] [varchar](5) NULL,
	[nd_start2] [varchar](5) NULL,
	[nd_end2] [varchar](5) NULL,
	[grace_period] [decimal](18, 2) NOT NULL,
	[include_grace_period] [bit] NOT NULL,
	[is_auto_ot] [bit] NOT NULL,
	[is_nd] [bit] NOT NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_ref_shift] PRIMARY KEY CLUSTERED 
(
	[ref_shift_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_shift_detail]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_shift_detail](
	[ref_shift_detail_id] [int] IDENTITY(1,1) NOT NULL,
	[ref_shift_id] [int] NOT NULL,
	[day] [varchar](10) NULL,
	[required_hour] [decimal](18, 2) NULL,
	[rest_day] [bit] NOT NULL,
 CONSTRAINT [PK_ref_shift_detail] PRIMARY KEY CLUSTERED 
(
	[ref_shift_detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_sss]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_sss](
	[ref_sss_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[salary_from] [decimal](18, 2) NULL,
	[salary_to] [decimal](18, 2) NULL,
	[monthly_salary_credit] [decimal](18, 2) NULL,
	[employee_share] [decimal](18, 2) NULL,
	[employer_share] [decimal](18, 2) NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_ref_sss] PRIMARY KEY CLUSTERED 
(
	[ref_sss_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ref_status]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ref_status](
	[ref_status_id] [int] IDENTITY(1,1) NOT NULL,
	[ref_status_name] [varchar](50) NULL,
 CONSTRAINT [PK_ref_status] PRIMARY KEY CLUSTERED 
(
	[ref_status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[request_dtr]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[request_dtr](
	[request_dtr_id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[ref_shift_id] [int] NOT NULL,
	[shift_date] [datetime] NOT NULL,
	[time_in] [varchar](5) NOT NULL,
	[time_out] [varchar](5) NOT NULL,
	[reason] [varchar](500) NOT NULL,
	[ref_status_id] [int] NOT NULL,
	[approver_remark] [varchar](500) NULL,
	[approver_id] [int] NOT NULL,
	[approver_date] [datetime] NULL,
	[date_deleted] [datetime] NULL,
	[group_id] [int] NOT NULL,
 CONSTRAINT [PK_request_dtr] PRIMARY KEY CLUSTERED 
(
	[request_dtr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[request_leave]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[request_leave](
	[request_leave_id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[leave_date] [datetime] NOT NULL,
	[ref_leave_type_id] [int] NOT NULL,
	[leave_day] [numeric](3, 2) NOT NULL,
	[ref_department_id] [int] NOT NULL,
	[reason] [varchar](500) NOT NULL,
	[ref_status_id] [int] NOT NULL,
	[approver_id] [int] NULL,
	[approver_date] [datetime] NULL,
	[approver_remark] [varchar](500) NULL,
	[date_deleted] [datetime] NULL,
	[group_id] [int] NOT NULL,
 CONSTRAINT [PK_request_leave] PRIMARY KEY CLUSTERED 
(
	[request_leave_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[request_overtime]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[request_overtime](
	[request_overtime_id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[overtime_date] [datetime] NOT NULL,
	[ref_overtime_type_id] [int] NOT NULL,
	[time_in] [varchar](5) NOT NULL,
	[time_out] [varchar](5) NOT NULL,
	[ref_department_id] [int] NOT NULL,
	[reason] [varchar](500) NOT NULL,
	[ref_status_id] [int] NOT NULL,
	[approver_remark] [varchar](500) NULL,
	[approver_id] [int] NOT NULL,
	[approver_date] [datetime] NULL,
	[date_deleted] [datetime] NULL,
	[group_id] [int] NOT NULL,
 CONSTRAINT [PK_request_overtime] PRIMARY KEY CLUSTERED 
(
	[request_overtime_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [varchar](50) NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role_menu]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_menu](
	[role_menu_id] [int] IDENTITY(1,1) NOT NULL,
	[role_menu_parent_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
	[display_name] [varchar](100) NULL,
	[display_icon] [varchar](100) NULL,
	[controller_name] [varchar](100) NULL,
	[action_name] [varchar](100) NULL,
	[ordering] [int] NOT NULL,
	[is_enable] [bit] NOT NULL,
	[date_deleted] [datetime] NULL,
 CONSTRAINT [PK_role_menu] PRIMARY KEY CLUSTERED 
(
	[role_menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role_permission]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_permission](
	[role_permission_id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NOT NULL,
	[permission_id] [int] NOT NULL,
	[is_enable] [bit] NOT NULL,
 CONSTRAINT [PK_role_permission] PRIMARY KEY CLUSTERED 
(
	[role_permission_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[timesheet_log]    Script Date: 4/12/2020 9:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[timesheet_log](
	[timesheet_log_id] [bigint] NOT NULL,
	[employee_serial] [varchar](20) NULL,
	[datetime_in] [datetime] NULL,
	[datetime_out] [datetime] NULL,
 CONSTRAINT [PK_timesheet_log] PRIMARY KEY CLUSTERED 
(
	[timesheet_log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[employee] ON 

INSERT [dbo].[employee] ([employee_id], [employee_serial], [username], [password], [last_name], [first_name], [middle_name], [email_address], [gender], [contact_number], [sss], [pagibig], [philhealth], [marital_status], [date_hire], [date_resign], [ref_shift_id], [ref_pay_type_id], [role_id], [ref_department_id], [fingerprint], [basic_pay], [date_deleted]) VALUES (2, N'9999', N'admin', N'1', N'Bachiller', N'Charlie', N'Bersabe', N'char@gmail.com', N'Male', N'9999', N'9999', N'999', N'999', N'Married', CAST(N'2019-03-05T00:00:00.000' AS DateTime), NULL, 1, 1, 1, 1, NULL, CAST(40000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[employee] ([employee_id], [employee_serial], [username], [password], [last_name], [first_name], [middle_name], [email_address], [gender], [contact_number], [sss], [pagibig], [philhealth], [marital_status], [date_hire], [date_resign], [ref_shift_id], [ref_pay_type_id], [role_id], [ref_department_id], [fingerprint], [basic_pay], [date_deleted]) VALUES (3, N'00000', N'ana', N'1', N'Bachiller', N'Jonella Ana', NULL, N'char@gmail.com', N'Female', N'888', N'111', N'1111', N'1111', N'Married', CAST(N'2019-11-01T00:00:00.000' AS DateTime), NULL, 1, 1, 1, 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[employee] OFF
SET IDENTITY_INSERT [dbo].[employee_balance] ON 

INSERT [dbo].[employee_balance] ([employee_balance_id], [employee_id], [ref_leave_type_id], [acquire_date], [expire_date], [quantity], [remarks], [date_deleted]) VALUES (3, 2, 1, CAST(N'2020-01-01T00:00:00.000' AS DateTime), CAST(N'2020-12-30T00:00:00.000' AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'test', NULL)
INSERT [dbo].[employee_balance] ([employee_balance_id], [employee_id], [ref_leave_type_id], [acquire_date], [expire_date], [quantity], [remarks], [date_deleted]) VALUES (4, 3, 1, CAST(N'2020-01-01T00:00:00.000' AS DateTime), CAST(N'2020-12-30T00:00:00.000' AS DateTime), CAST(20.00 AS Decimal(18, 2)), N'With pay leaves', NULL)
INSERT [dbo].[employee_balance] ([employee_balance_id], [employee_id], [ref_leave_type_id], [acquire_date], [expire_date], [quantity], [remarks], [date_deleted]) VALUES (5, 2, 2, CAST(N'2020-01-01T00:00:00.000' AS DateTime), CAST(N'2020-12-30T00:00:00.000' AS DateTime), CAST(15.00 AS Decimal(18, 2)), N'VL Leave 2020', NULL)
INSERT [dbo].[employee_balance] ([employee_balance_id], [employee_id], [ref_leave_type_id], [acquire_date], [expire_date], [quantity], [remarks], [date_deleted]) VALUES (6, 2, 7, CAST(N'2020-01-01T00:00:00.000' AS DateTime), CAST(N'2020-12-30T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(18, 2)), N'RW', NULL)
INSERT [dbo].[employee_balance] ([employee_balance_id], [employee_id], [ref_leave_type_id], [acquire_date], [expire_date], [quantity], [remarks], [date_deleted]) VALUES (7, 2, 8, CAST(N'2020-01-01T00:00:00.000' AS DateTime), CAST(N'2020-12-30T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(18, 2)), N'asda', NULL)
SET IDENTITY_INSERT [dbo].[employee_balance] OFF
SET IDENTITY_INSERT [dbo].[employee_balance_transaction] ON 

INSERT [dbo].[employee_balance_transaction] ([employee_balance_transaction_id], [employee_id], [employee_balance_id], [requested_date], [approved_date], [quantity], [date_deleted]) VALUES (1, 2, 5, CAST(N'2020-01-31T00:00:00.000' AS DateTime), CAST(N'2020-03-05T00:21:44.060' AS DateTime), CAST(1.00 AS Decimal(18, 2)), NULL)
SET IDENTITY_INSERT [dbo].[employee_balance_transaction] OFF
INSERT [dbo].[employee_group] ([employee_id], [group_id], [approver_all], [ot_approver], [ob_approver], [leave_approver], [dtr_approver]) VALUES (2, 8, 1, 0, 0, 0, 0)
INSERT [dbo].[employee_group] ([employee_id], [group_id], [approver_all], [ot_approver], [ob_approver], [leave_approver], [dtr_approver]) VALUES (3, 8, 0, 0, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[employee_timesheet] ON 

INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (1, 2, CAST(N'2020-01-16T00:00:00.000' AS DateTime), 2, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(8.95 AS Decimal(18, 2)), N'20:00', N'29:00', NULL, NULL, N'22:00', N'30:00', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.05 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (2, 2, CAST(N'2020-01-17T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(8.98 AS Decimal(18, 2)), N'12:16', N'22:00', NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.02 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (3, 2, CAST(N'2020-01-18T00:00:00.000' AS DateTime), 1, 2, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (4, 2, CAST(N'2020-01-19T00:00:00.000' AS DateTime), 1, 2, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (5, 2, CAST(N'2020-01-20T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'12:10', N'22:00', NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (6, 2, CAST(N'2020-01-21T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'08:00', N'22:15', NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (7, 2, CAST(N'2020-01-22T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'08:00', N'22:04', NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (8, 2, CAST(N'2020-01-23T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'11:57', N'22:20', NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (9, 2, CAST(N'2020-01-24T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'12:00', N'22:18', NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (10, 2, CAST(N'2020-01-25T00:00:00.000' AS DateTime), 1, 2, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (11, 2, CAST(N'2020-01-26T00:00:00.000' AS DateTime), 1, 2, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (12, 2, CAST(N'2020-01-27T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'07:57', N'22:14', NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (13, 2, CAST(N'2020-01-28T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'11:44', N'22:06', NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (14, 2, CAST(N'2020-01-29T00:00:00.000' AS DateTime), 1, 2, 4, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'11:51', N'22:09', NULL, NULL, N'12:00', N'22:00', CAST(8.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (15, 2, CAST(N'2020-01-30T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), N'11:39', N'22:06', NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (16, 2, CAST(N'2020-01-31T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), 2, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (17, 3, CAST(N'2020-01-16T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (18, 3, CAST(N'2020-01-17T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (19, 3, CAST(N'2020-01-18T00:00:00.000' AS DateTime), 1, 2, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (20, 3, CAST(N'2020-01-19T00:00:00.000' AS DateTime), 1, 2, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (21, 3, CAST(N'2020-01-20T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (22, 3, CAST(N'2020-01-21T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (23, 3, CAST(N'2020-01-22T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (24, 3, CAST(N'2020-01-23T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (25, 3, CAST(N'2020-01-24T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (26, 3, CAST(N'2020-01-25T00:00:00.000' AS DateTime), 1, 2, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (27, 3, CAST(N'2020-01-26T00:00:00.000' AS DateTime), 1, 2, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (28, 3, CAST(N'2020-01-27T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (29, 3, CAST(N'2020-01-28T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (30, 3, CAST(N'2020-01-29T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (31, 3, CAST(N'2020-01-30T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
INSERT [dbo].[employee_timesheet] ([employee_timesheet_id], [employee_id], [shift_date], [ref_shift_id], [ref_day_type_id], [holiday_day_type_id], [required_hour], [rendered_hour], [time_in1], [time_out1], [time_in2], [time_out2], [ot_in], [ot_out], [ot], [ot8], [night_dif], [night_dif8], [absent], [late], [undertime], [approve_leave], [ref_leave_type_id], [payroll_process]) VALUES (32, 3, CAST(N'2020-01-31T00:00:00.000' AS DateTime), 1, 1, NULL, CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[employee_timesheet] OFF
SET IDENTITY_INSERT [dbo].[payroll] ON 

INSERT [dbo].[payroll] ([payroll_id], [employee_id], [ref_payroll_cutoff_id], [total_earnings], [total_deduction], [date_process]) VALUES (2, 1, 1, CAST(1.00 AS Numeric(18, 2)), CAST(1.00 AS Numeric(18, 2)), CAST(N'2020-12-12T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[payroll] OFF
SET IDENTITY_INSERT [dbo].[payroll_config] ON 

INSERT [dbo].[payroll_config] ([payroll_config_id], [config_name], [config_value]) VALUES (1, N'WorkDayPerYear', 331)
INSERT [dbo].[payroll_config] ([payroll_config_id], [config_name], [config_value]) VALUES (2, N'WorkHourPerDay', 9)
INSERT [dbo].[payroll_config] ([payroll_config_id], [config_name], [config_value]) VALUES (3, N'Months', 12)
INSERT [dbo].[payroll_config] ([payroll_config_id], [config_name], [config_value]) VALUES (4, N'TRANSPO', 1250)
INSERT [dbo].[payroll_config] ([payroll_config_id], [config_name], [config_value]) VALUES (5, N'MEAL', 750)
SET IDENTITY_INSERT [dbo].[payroll_config] OFF
SET IDENTITY_INSERT [dbo].[payroll_details] ON 

INSERT [dbo].[payroll_details] ([payroll_details_id], [payroll_id], [ref_payroll_details_type_id], [qty], [amount]) VALUES (2, 2, 1, NULL, CAST(1.00 AS Numeric(18, 2)))
SET IDENTITY_INSERT [dbo].[payroll_details] OFF
SET IDENTITY_INSERT [dbo].[payroll_earning_deduction] ON 

INSERT [dbo].[payroll_earning_deduction] ([payroll_earning_deduction_id], [ref_payroll_details_type_id], [exact_date], [cutoff], [amount], [recurring], [date_deleted]) VALUES (1, 2, NULL, 1, CAST(1250.00 AS Numeric(18, 2)), 1, NULL)
INSERT [dbo].[payroll_earning_deduction] ([payroll_earning_deduction_id], [ref_payroll_details_type_id], [exact_date], [cutoff], [amount], [recurring], [date_deleted]) VALUES (2, 2, NULL, 2, CAST(1250.00 AS Numeric(18, 2)), 1, NULL)
INSERT [dbo].[payroll_earning_deduction] ([payroll_earning_deduction_id], [ref_payroll_details_type_id], [exact_date], [cutoff], [amount], [recurring], [date_deleted]) VALUES (3, 3, NULL, 1, CAST(750.00 AS Numeric(18, 2)), 1, NULL)
INSERT [dbo].[payroll_earning_deduction] ([payroll_earning_deduction_id], [ref_payroll_details_type_id], [exact_date], [cutoff], [amount], [recurring], [date_deleted]) VALUES (4, 3, NULL, 2, CAST(750.00 AS Numeric(18, 2)), 1, NULL)
SET IDENTITY_INSERT [dbo].[payroll_earning_deduction] OFF
SET IDENTITY_INSERT [dbo].[permission] ON 

INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (1, N'view_settings_menu', N'view_settings_menu', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (2, N'view_employee', N'view_employee', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (3, N'view_shift', N'view_shift', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (4, N'view_cutoff', N'view_cutoff', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (5, N'view_department', N'view_department', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (6, N'view_timesheet_menu', N'view_timesheet_menu', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (7, N'view_timesheet_processing', N'view_timesheet_processing', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (8, N'view_dtr', N'view_dtr', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (9, N'view_request_menu', N'view_request_menu', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (10, N'view_request_overtime', N'view_request_overtime', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (11, N'view_request_leave', N'view_request_leave', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (12, N'view_request_dtr', N'view_request_dtr', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (13, N'view_dtr_process', N'view_dtr_process', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (14, N'add_shift', N'add_shift', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (15, N'add_cutoff', N'add_cutoff', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (16, N'add_department', N'add_department', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (17, N'add_employee', N'add_employee', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (18, N'view_role', N'view_role', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (19, N'add_role', N'add_role', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (20, N'edit_role', N'edit_role', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (21, N'view_approval_menu', N'view_approval_menu', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (22, N'view_leave_type', N'view_leave_type', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (23, N'add_leave_type', N'add_leave_type', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (24, N'edit_leave_type', N'edit_leave_type', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (25, N'view_approval_calendar', N'view_approval_calendar', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (26, N'view_leave_balance', N'view_leave_balance', 0)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (27, N'view_holiday', N'view_holiday', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (28, N'add_holiday', N'add_holiday', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (29, N'edit_holiday', N'edit_holiday', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (30, N'view_employee_balance', N'view_employee_balance', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (31, N'add_employee_balance', N'add_employee_balance', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (32, N'edit_employee_balance', N'edit_employee_balance', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (33, N'view_employee_group', N'view_employee_group', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (34, N'add_employee_group', N'add_employee_group', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (35, N'edit_employee_group', N'edit_employee_group', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (36, N'report_dtr_all', N'report_dtr_all', 1)
INSERT [dbo].[permission] ([permission_id], [permission_name], [permission_code], [is_enable]) VALUES (37, N'report_view_menu', N'report_view_menu', 1)
SET IDENTITY_INSERT [dbo].[permission] OFF
SET IDENTITY_INSERT [dbo].[ref_bir] ON 

INSERT [dbo].[ref_bir] ([ref_bir_id], [ref_pay_type_id], [salary_from], [salary_to], [add_tax], [subtract_tax_over], [multiplier]) VALUES (1, 1, CAST(0.00 AS Numeric(18, 2)), CAST(10417.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[ref_bir] ([ref_bir_id], [ref_pay_type_id], [salary_from], [salary_to], [add_tax], [subtract_tax_over], [multiplier]) VALUES (2, 1, CAST(10417.00 AS Numeric(18, 2)), CAST(16000.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10417.00 AS Numeric(18, 2)), CAST(0.20 AS Numeric(18, 2)))
INSERT [dbo].[ref_bir] ([ref_bir_id], [ref_pay_type_id], [salary_from], [salary_to], [add_tax], [subtract_tax_over], [multiplier]) VALUES (3, 1, CAST(16667.00 AS Numeric(18, 2)), CAST(33332.00 AS Numeric(18, 2)), CAST(1250.00 AS Numeric(18, 2)), CAST(16667.00 AS Numeric(18, 2)), CAST(0.25 AS Numeric(18, 2)))
INSERT [dbo].[ref_bir] ([ref_bir_id], [ref_pay_type_id], [salary_from], [salary_to], [add_tax], [subtract_tax_over], [multiplier]) VALUES (4, 1, CAST(33333.00 AS Numeric(18, 2)), CAST(83332.00 AS Numeric(18, 2)), CAST(5416.67 AS Numeric(18, 2)), CAST(33333.00 AS Numeric(18, 2)), CAST(0.30 AS Numeric(18, 2)))
INSERT [dbo].[ref_bir] ([ref_bir_id], [ref_pay_type_id], [salary_from], [salary_to], [add_tax], [subtract_tax_over], [multiplier]) VALUES (6, 1, CAST(83333.00 AS Numeric(18, 2)), CAST(333332.00 AS Numeric(18, 2)), CAST(20416.67 AS Numeric(18, 2)), CAST(83333.00 AS Numeric(18, 2)), CAST(0.32 AS Numeric(18, 2)))
INSERT [dbo].[ref_bir] ([ref_bir_id], [ref_pay_type_id], [salary_from], [salary_to], [add_tax], [subtract_tax_over], [multiplier]) VALUES (9, 1, CAST(333333.00 AS Numeric(18, 2)), CAST(1000000.00 AS Numeric(18, 2)), CAST(100416.67 AS Numeric(18, 2)), CAST(333333.00 AS Numeric(18, 2)), CAST(0.35 AS Numeric(18, 2)))
SET IDENTITY_INSERT [dbo].[ref_bir] OFF
SET IDENTITY_INSERT [dbo].[ref_calendar_activity] ON 

INSERT [dbo].[ref_calendar_activity] ([ref_calendar_activity_id], [work_date], [ref_day_type_id], [description], [date_deleted]) VALUES (1, CAST(N'2020-01-29T00:00:00.000' AS DateTime), 3, N'Charlie Day asd asd asd', NULL)
SET IDENTITY_INSERT [dbo].[ref_calendar_activity] OFF
SET IDENTITY_INSERT [dbo].[ref_configuration] ON 

INSERT [dbo].[ref_configuration] ([config_id], [ptext], [pvalue]) VALUES (1, N'autoot', N'False')
INSERT [dbo].[ref_configuration] ([config_id], [ptext], [pvalue]) VALUES (2, N'getgraceperiod', N'15')
INSERT [dbo].[ref_configuration] ([config_id], [ptext], [pvalue]) VALUES (3, N'nightdiffIn1', N'22:00')
INSERT [dbo].[ref_configuration] ([config_id], [ptext], [pvalue]) VALUES (4, N'nightdiffOut1', N'30:00')
INSERT [dbo].[ref_configuration] ([config_id], [ptext], [pvalue]) VALUES (5, N'nightdiffIn2', N'00:00')
INSERT [dbo].[ref_configuration] ([config_id], [ptext], [pvalue]) VALUES (6, N'nightdiffOut2', N'06:00')
INSERT [dbo].[ref_configuration] ([config_id], [ptext], [pvalue]) VALUES (7, N'minot', N'2')
INSERT [dbo].[ref_configuration] ([config_id], [ptext], [pvalue]) VALUES (1001, N'includeraceperiod', N'False')
SET IDENTITY_INSERT [dbo].[ref_configuration] OFF
SET IDENTITY_INSERT [dbo].[ref_day_type] ON 

INSERT [dbo].[ref_day_type] ([ref_day_type_id], [date_type_code], [date_type_name], [ot_multiplier], [nightdif_multiplier1], [nightdif_multiplier2]) VALUES (1, N'RWD', N'Regular Working Day', CAST(1.25 AS Numeric(18, 2)), CAST(0.10 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)))
INSERT [dbo].[ref_day_type] ([ref_day_type_id], [date_type_code], [date_type_name], [ot_multiplier], [nightdif_multiplier1], [nightdif_multiplier2]) VALUES (2, N'RD', N'Rest Day', CAST(1.69 AS Numeric(18, 2)), CAST(1.30 AS Numeric(18, 2)), CAST(0.10 AS Numeric(18, 2)))
INSERT [dbo].[ref_day_type] ([ref_day_type_id], [date_type_code], [date_type_name], [ot_multiplier], [nightdif_multiplier1], [nightdif_multiplier2]) VALUES (3, N'SH', N'Special Holiday', CAST(1.69 AS Numeric(18, 2)), CAST(1.30 AS Numeric(18, 2)), CAST(0.10 AS Numeric(18, 2)))
INSERT [dbo].[ref_day_type] ([ref_day_type_id], [date_type_code], [date_type_name], [ot_multiplier], [nightdif_multiplier1], [nightdif_multiplier2]) VALUES (4, N'SHRD', N'Special Holiday + Rest Day', CAST(1.95 AS Numeric(18, 2)), CAST(1.50 AS Numeric(18, 2)), CAST(0.10 AS Numeric(18, 2)))
INSERT [dbo].[ref_day_type] ([ref_day_type_id], [date_type_code], [date_type_name], [ot_multiplier], [nightdif_multiplier1], [nightdif_multiplier2]) VALUES (5, N'RH', N'Regular Holiday', CAST(2.60 AS Numeric(18, 2)), CAST(2.00 AS Numeric(18, 2)), CAST(0.10 AS Numeric(18, 2)))
INSERT [dbo].[ref_day_type] ([ref_day_type_id], [date_type_code], [date_type_name], [ot_multiplier], [nightdif_multiplier1], [nightdif_multiplier2]) VALUES (6, N'RHRD', N'Regular Holiday + Rest Day', CAST(3.38 AS Numeric(18, 2)), CAST(2.60 AS Numeric(18, 2)), CAST(0.10 AS Numeric(18, 2)))
INSERT [dbo].[ref_day_type] ([ref_day_type_id], [date_type_code], [date_type_name], [ot_multiplier], [nightdif_multiplier1], [nightdif_multiplier2]) VALUES (7, N'DH', N'Double Holiday', CAST(3.90 AS Numeric(18, 2)), CAST(3.30 AS Numeric(18, 2)), CAST(0.10 AS Numeric(18, 2)))
INSERT [dbo].[ref_day_type] ([ref_day_type_id], [date_type_code], [date_type_name], [ot_multiplier], [nightdif_multiplier1], [nightdif_multiplier2]) VALUES (8, N'DHRD', N'Double Holiday + Rest Day', CAST(5.07 AS Numeric(18, 2)), CAST(3.90 AS Numeric(18, 2)), CAST(0.10 AS Numeric(18, 2)))
SET IDENTITY_INSERT [dbo].[ref_day_type] OFF
SET IDENTITY_INSERT [dbo].[ref_department] ON 

INSERT [dbo].[ref_department] ([ref_department_id], [department_name]) VALUES (1, N'NONE')
INSERT [dbo].[ref_department] ([ref_department_id], [department_name]) VALUES (2, N'IT')
INSERT [dbo].[ref_department] ([ref_department_id], [department_name]) VALUES (3, N'dsfsdf')
INSERT [dbo].[ref_department] ([ref_department_id], [department_name]) VALUES (4, N'TETE')
SET IDENTITY_INSERT [dbo].[ref_department] OFF
SET IDENTITY_INSERT [dbo].[ref_department_approver] ON 

INSERT [dbo].[ref_department_approver] ([ref_department_approver_id], [ref_department_id], [employee_id], [ordering], [date_deleted]) VALUES (13, 4, 2, 1, NULL)
INSERT [dbo].[ref_department_approver] ([ref_department_approver_id], [ref_department_id], [employee_id], [ordering], [date_deleted]) VALUES (14, 3, 2, 1, NULL)
SET IDENTITY_INSERT [dbo].[ref_department_approver] OFF
SET IDENTITY_INSERT [dbo].[ref_group] ON 

INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (1, N'Payroll', N'/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (2, N'Company 1', N'/1/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (3, N'Company 2', N'/2/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (4, N'Company 3', N'/3/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (5, N'C1 Branch 1', N'/1/1/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (6, N'C1 Branch 2', N'/1/2/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (7, N'C1 Branch 3', N'/1/3/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (8, N'B1 HR Department', N'/1/1/1/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (9, N'B1 IT Department', N'/1/1/2/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (10, N'B1 TTTT Department', N'/1/1/3/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (11, N'Company 4', N'/4/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (12, N'Company 5', N'/5/', NULL, 1)
INSERT [dbo].[ref_group] ([group_id], [name], [level], [date_deleted], [is_enable]) VALUES (14, N'Charlie Dept', N'/1/1/4/', NULL, 1)
SET IDENTITY_INSERT [dbo].[ref_group] OFF
SET IDENTITY_INSERT [dbo].[ref_leave_type] ON 

INSERT [dbo].[ref_leave_type] ([ref_leave_type_id], [ref_leave_type_name], [ref_leave_type_code], [with_pay]) VALUES (1, N'Sick Leave with Pay', N'SLWP', 1)
INSERT [dbo].[ref_leave_type] ([ref_leave_type_id], [ref_leave_type_name], [ref_leave_type_code], [with_pay]) VALUES (2, N'Vacation Leave with Pay', N'VLWP', 1)
INSERT [dbo].[ref_leave_type] ([ref_leave_type_id], [ref_leave_type_name], [ref_leave_type_code], [with_pay]) VALUES (3, N'Sick Leave without Pay', N'SLWOP', 0)
INSERT [dbo].[ref_leave_type] ([ref_leave_type_id], [ref_leave_type_name], [ref_leave_type_code], [with_pay]) VALUES (4, N'Vacation Leave without Pay', N'VLWOP', 0)
INSERT [dbo].[ref_leave_type] ([ref_leave_type_id], [ref_leave_type_name], [ref_leave_type_code], [with_pay]) VALUES (5, N'Official Business', N'OB', 1)
INSERT [dbo].[ref_leave_type] ([ref_leave_type_id], [ref_leave_type_name], [ref_leave_type_code], [with_pay]) VALUES (6, N'Maternity Leave with Pay', N'MLWP', 1)
INSERT [dbo].[ref_leave_type] ([ref_leave_type_id], [ref_leave_type_name], [ref_leave_type_code], [with_pay]) VALUES (7, N'Remote Work', N'RW', 0)
INSERT [dbo].[ref_leave_type] ([ref_leave_type_id], [ref_leave_type_name], [ref_leave_type_code], [with_pay]) VALUES (8, N'Emergency Leave', N'EL', 0)
INSERT [dbo].[ref_leave_type] ([ref_leave_type_id], [ref_leave_type_name], [ref_leave_type_code], [with_pay]) VALUES (9, N'Half Day', N'HD', 0)
SET IDENTITY_INSERT [dbo].[ref_leave_type] OFF
SET IDENTITY_INSERT [dbo].[ref_overtime_type] ON 

INSERT [dbo].[ref_overtime_type] ([ref_overtime_type_id], [ref_overtime_type_name]) VALUES (1, N'WITHPAY')
INSERT [dbo].[ref_overtime_type] ([ref_overtime_type_id], [ref_overtime_type_name]) VALUES (2, N'BANK')
SET IDENTITY_INSERT [dbo].[ref_overtime_type] OFF
SET IDENTITY_INSERT [dbo].[ref_pagibig] ON 

INSERT [dbo].[ref_pagibig] ([ref_pagibig_id], [name], [salary_from], [salary_to], [employee_contribution], [employer_contribution], [flat_rate], [date_deleted]) VALUES (1, N'1500 and Below', CAST(0.00 AS Decimal(18, 2)), CAST(1500.00 AS Decimal(18, 2)), CAST(0.01 AS Decimal(18, 2)), CAST(0.02 AS Decimal(18, 2)), 0, NULL)
INSERT [dbo].[ref_pagibig] ([ref_pagibig_id], [name], [salary_from], [salary_to], [employee_contribution], [employer_contribution], [flat_rate], [date_deleted]) VALUES (2, N'1500 and Below', CAST(1501.00 AS Decimal(18, 2)), CAST(100000.00 AS Decimal(18, 2)), CAST(0.02 AS Decimal(18, 2)), CAST(0.02 AS Decimal(18, 2)), 0, NULL)
SET IDENTITY_INSERT [dbo].[ref_pagibig] OFF
SET IDENTITY_INSERT [dbo].[ref_pay_type] ON 

INSERT [dbo].[ref_pay_type] ([ref_pay_type_id], [ref_pay_type_name]) VALUES (1, N'SEMI_MONTHLY')
INSERT [dbo].[ref_pay_type] ([ref_pay_type_id], [ref_pay_type_name]) VALUES (2, N'MONTHLY')
INSERT [dbo].[ref_pay_type] ([ref_pay_type_id], [ref_pay_type_name]) VALUES (3, N'DAILY')
INSERT [dbo].[ref_pay_type] ([ref_pay_type_id], [ref_pay_type_name]) VALUES (4, N'WEEKLY')
SET IDENTITY_INSERT [dbo].[ref_pay_type] OFF
SET IDENTITY_INSERT [dbo].[ref_payroll_cutoff] ON 

INSERT [dbo].[ref_payroll_cutoff] ([ref_payroll_cutoff_id], [pay_date], [cutoff_date_start], [cutoff_date_end], [is_processed], [date_deleted], [government], [cutoff]) VALUES (2, CAST(N'2019-02-15T00:00:00.000' AS DateTime), CAST(N'2019-09-01T00:00:00.000' AS DateTime), CAST(N'2019-09-15T00:00:00.000' AS DateTime), 0, CAST(N'2019-09-15T00:00:00.000' AS DateTime), 0, 1)
INSERT [dbo].[ref_payroll_cutoff] ([ref_payroll_cutoff_id], [pay_date], [cutoff_date_start], [cutoff_date_end], [is_processed], [date_deleted], [government], [cutoff]) VALUES (3, CAST(N'2019-09-30T00:00:00.000' AS DateTime), CAST(N'2019-09-16T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 0, CAST(N'2019-09-15T00:00:00.000' AS DateTime), 1, 2)
INSERT [dbo].[ref_payroll_cutoff] ([ref_payroll_cutoff_id], [pay_date], [cutoff_date_start], [cutoff_date_end], [is_processed], [date_deleted], [government], [cutoff]) VALUES (4, CAST(N'2020-03-04T11:38:01.167' AS DateTime), CAST(N'2020-01-16T00:00:00.000' AS DateTime), CAST(N'2020-01-31T00:00:00.000' AS DateTime), 0, NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[ref_payroll_cutoff] OFF
SET IDENTITY_INSERT [dbo].[ref_payroll_details_type] ON 

INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (1, N'BASIC SALARY', N'BASICSALARY', NULL, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (2, N'TRANSPORT ALLOWANCE', N'TRANSPORTALLOWANCE', NULL, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (3, N'MEAL ALLOWANCE', N'MEALALLOWANCE', NULL, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (4, N'TARDINESS', N'TARDINESS', NULL, 0, 0, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (5, N'REST DAY ADJ', N'REST DAY ADJ', NULL, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (6, N'REST DAY OT ADJ', N'REST DAY OT ADJ', NULL, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (7, N'ABSENCE', N'ABSENCE', NULL, 0, 0, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (8, N'WITHHOLDING TAX', N'WITHHOLDING TAX', NULL, 0, 0, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (9, N'SSSCONTRIBUTION', N'SSSCONTRIBUTION', NULL, 0, 0, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (10, N'PHILHEALTHCONTRIBUTION', N'PHILHEALTHCONTRIBUTION', NULL, 0, 0, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (11, N'HDMFCONTRIBUTION', N'HDMFCONTRIBUTION', NULL, 0, 0, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (12, N'SSSCONTRIBUTIONCOM', N'SSSCONTRIBUTIONCOM', NULL, 0, 0, 1)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (13, N'PHILHEALTHCONTRIBUTIONCOM', N'PHILHEALTHCONTRIBUTIONCOM', NULL, 0, 0, 1)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (14, N'HDMFCONTRIBUTIONCOM', N'HDMFCONTRIBUTIONCOM', NULL, 0, 0, 1)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (15, N'SH', N'Special Holiday Overtime', 3, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (16, N'SHRD', N'Special Holiday + Rest Day Overtime', 4, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (17, N'RH', N'Regular Holiday Overtime', 5, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (18, N'RHRD', N'Regular Holiday + Rest Day Overtime', 6, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (19, N'DH', N'Double Holiday Overtime', 7, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (20, N'DHRD', N'Double Holiday + Rest Day Overtime', 8, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (21, N'RWD', N'Regular Working Day Overtime', 1, 1, 1, 0)
INSERT [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id], [ref_payroll_details_type_name], [ref_payroll_details_type_code], [ref_day_type_id], [earnings], [taxable], [company_contribution]) VALUES (22, N'RD', N'Rest Day Overtime', 2, 1, 1, 0)
SET IDENTITY_INSERT [dbo].[ref_payroll_details_type] OFF
SET IDENTITY_INSERT [dbo].[ref_philhealth] ON 

INSERT [dbo].[ref_philhealth] ([ref_philhealth_id], [name], [salary_from], [salary_to], [employee_contribution], [employer_contribution], [flat_rate], [date_deleted]) VALUES (1, N'40,000 and UP', CAST(40000.00 AS Decimal(18, 2)), CAST(1000000.00 AS Decimal(18, 2)), CAST(550.00 AS Decimal(18, 2)), CAST(550.00 AS Decimal(18, 2)), 1, NULL)
SET IDENTITY_INSERT [dbo].[ref_philhealth] OFF
SET IDENTITY_INSERT [dbo].[ref_shift] ON 

INSERT [dbo].[ref_shift] ([ref_shift_id], [shift_name], [shift_in], [shift_out], [break_in], [break_out], [break_hour], [required_hour], [nd_start], [nd_end], [nd_start2], [nd_end2], [grace_period], [include_grace_period], [is_auto_ot], [is_nd], [date_deleted]) VALUES (1, N'12PM-10PM', N'12:00', N'22:00', N'18:00', N'19:00', CAST(1.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), N'22:00', N'30:00', NULL, NULL, CAST(0.00 AS Decimal(18, 2)), 0, 1, 1, NULL)
INSERT [dbo].[ref_shift] ([ref_shift_id], [shift_name], [shift_in], [shift_out], [break_in], [break_out], [break_hour], [required_hour], [nd_start], [nd_end], [nd_start2], [nd_end2], [grace_period], [include_grace_period], [is_auto_ot], [is_nd], [date_deleted]) VALUES (2, N'8PM-5AM', N'20:00', N'29:00', N'18:00', N'19:00', CAST(1.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), N'22:00', N'30:00', NULL, NULL, CAST(0.00 AS Decimal(18, 2)), 0, 1, 1, NULL)
INSERT [dbo].[ref_shift] ([ref_shift_id], [shift_name], [shift_in], [shift_out], [break_in], [break_out], [break_hour], [required_hour], [nd_start], [nd_end], [nd_start2], [nd_end2], [grace_period], [include_grace_period], [is_auto_ot], [is_nd], [date_deleted]) VALUES (3, N'1AM-10AM', N'01:00', N'10:00', N'18:00', N'19:11', CAST(1.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), N'22:00', N'30:00', NULL, NULL, CAST(0.00 AS Decimal(18, 2)), 0, 1, 1, NULL)
INSERT [dbo].[ref_shift] ([ref_shift_id], [shift_name], [shift_in], [shift_out], [break_in], [break_out], [break_hour], [required_hour], [nd_start], [nd_end], [nd_start2], [nd_end2], [grace_period], [include_grace_period], [is_auto_ot], [is_nd], [date_deleted]) VALUES (4, N'10AM-8PM', N'10:00', N'20:00', N'12:00', N'13:00', NULL, CAST(45.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), 0, 1, 0, NULL)
INSERT [dbo].[ref_shift] ([ref_shift_id], [shift_name], [shift_in], [shift_out], [break_in], [break_out], [break_hour], [required_hour], [nd_start], [nd_end], [nd_start2], [nd_end2], [grace_period], [include_grace_period], [is_auto_ot], [is_nd], [date_deleted]) VALUES (5, N'8AM-6PM', N'08:00', N'18:00', N'12:00', N'13:00', NULL, CAST(45.00 AS Decimal(18, 2)), N'22:00', N'22:00', NULL, NULL, CAST(15.00 AS Decimal(18, 2)), 1, 1, 1, NULL)
INSERT [dbo].[ref_shift] ([ref_shift_id], [shift_name], [shift_in], [shift_out], [break_in], [break_out], [break_hour], [required_hour], [nd_start], [nd_end], [nd_start2], [nd_end2], [grace_period], [include_grace_period], [is_auto_ot], [is_nd], [date_deleted]) VALUES (6, N'1AM-10AM', N'01:00', N'10:00', N'05:00', N'06:00', NULL, CAST(45.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), 0, 1, 0, NULL)
INSERT [dbo].[ref_shift] ([ref_shift_id], [shift_name], [shift_in], [shift_out], [break_in], [break_out], [break_hour], [required_hour], [nd_start], [nd_end], [nd_start2], [nd_end2], [grace_period], [include_grace_period], [is_auto_ot], [is_nd], [date_deleted]) VALUES (7, N'12PM-10PM', N'11:11', N'11:11', N'11:11', N'11:11', NULL, CAST(11.00 AS Decimal(18, 2)), NULL, NULL, NULL, NULL, CAST(0.00 AS Decimal(18, 2)), 0, 1, 0, CAST(N'2020-03-11T09:51:09.837' AS DateTime))
SET IDENTITY_INSERT [dbo].[ref_shift] OFF
SET IDENTITY_INSERT [dbo].[ref_shift_detail] ON 

INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (1, 1, N'MON', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (2, 1, N'TUE', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (3, 1, N'WED', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (4, 1, N'THU', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (5, 1, N'FRI', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (6, 1, N'SAT', CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (7, 1, N'SUN', CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (8, 2, N'MON', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (9, 2, N'TUE', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (10, 2, N'WED', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (11, 2, N'THU', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (12, 2, N'FRI', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (13, 2, N'SAT', CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (14, 2, N'SUN', CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (15, 3, N'MON', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (16, 3, N'TUE', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (17, 3, N'WED', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (18, 3, N'THU', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (19, 3, N'FRI', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (20, 3, N'SAT', CAST(9.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (21, 3, N'SUN', CAST(9.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (22, 4, N'MON', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (23, 4, N'TUE', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (24, 4, N'WED', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (25, 4, N'THU', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (26, 4, N'FRI', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (27, 4, N'SAT', CAST(9.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (28, 4, N'SUN', CAST(9.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (30, 5, N'MON', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (31, 5, N'TUE', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (32, 5, N'WED', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (33, 5, N'THU', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (34, 5, N'FRI', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (35, 5, N'SAT', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (36, 5, N'SUN', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (38, 6, N'MON', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (39, 6, N'TUE', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (40, 6, N'WED', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (41, 6, N'THU', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (42, 6, N'FRI', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (43, 6, N'SAT', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (44, 6, N'SUN', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (45, 7, N'MON', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (46, 7, N'TUE', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (47, 7, N'WED', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (48, 7, N'THU', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (49, 7, N'FRI', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (50, 7, N'SAT', CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (51, 7, N'SUN', CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (52, 7, N'MON', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (53, 7, N'TUE', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (54, 7, N'WED', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (55, 7, N'THU', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (56, 7, N'FRI', CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (57, 7, N'SAT', CAST(0.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[ref_shift_detail] ([ref_shift_detail_id], [ref_shift_id], [day], [required_hour], [rest_day]) VALUES (58, 7, N'SUN', CAST(0.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[ref_shift_detail] OFF
SET IDENTITY_INSERT [dbo].[ref_sss] ON 

INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (1, N'BELOW – 2,250', CAST(0.00 AS Decimal(18, 2)), CAST(2250.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(80.00 AS Decimal(18, 2)), CAST(160.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (2, N'2,250 – 2,749.99', CAST(2250.00 AS Decimal(18, 2)), CAST(2749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (3, N'2,750 – 3,249.99', CAST(2750.00 AS Decimal(18, 2)), CAST(3249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), CAST(240.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (4, N'3,250 – 3,749.99', CAST(3250.00 AS Decimal(18, 2)), CAST(3749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(140.00 AS Decimal(18, 2)), CAST(280.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (5, N'3,750 – 4,249.99', CAST(3750.00 AS Decimal(18, 2)), CAST(4249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(160.00 AS Decimal(18, 2)), CAST(320.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (6, N'4250 – 4,749.99', CAST(4250.00 AS Decimal(18, 2)), CAST(4749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(180.00 AS Decimal(18, 2)), CAST(360.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (7, N'4,750 – 5249.99', CAST(4750.00 AS Decimal(18, 2)), CAST(5249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), CAST(400.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (8, N'5,250 – 5,749.99', CAST(5250.00 AS Decimal(18, 2)), CAST(5749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(220.00 AS Decimal(18, 2)), CAST(440.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (9, N'5,750 – 6,249.99', CAST(5750.00 AS Decimal(18, 2)), CAST(6249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(240.00 AS Decimal(18, 2)), CAST(480.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (10, N'6,250 – 6,749.99', CAST(6250.00 AS Decimal(18, 2)), CAST(6749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(260.00 AS Decimal(18, 2)), CAST(520.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (11, N'6,750 – 7,249.99', CAST(6750.00 AS Decimal(18, 2)), CAST(7249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(280.00 AS Decimal(18, 2)), CAST(560.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (12, N'7,250 – 7,749.99', CAST(7250.00 AS Decimal(18, 2)), CAST(7749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (13, N'7,750 – 8,249.99', CAST(7750.00 AS Decimal(18, 2)), CAST(8249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(320.00 AS Decimal(18, 2)), CAST(640.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (14, N'8,250 – 8,749.99', CAST(8250.00 AS Decimal(18, 2)), CAST(8749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(340.00 AS Decimal(18, 2)), CAST(680.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (15, N'8.750 – 9,249.99', CAST(8750.00 AS Decimal(18, 2)), CAST(9249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(360.00 AS Decimal(18, 2)), CAST(720.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (16, N'9,250 – 9,749.99', CAST(9250.00 AS Decimal(18, 2)), CAST(9749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(380.00 AS Decimal(18, 2)), CAST(760.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (17, N'9,750 – 10,249.99', CAST(9750.00 AS Decimal(18, 2)), CAST(10249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(400.00 AS Decimal(18, 2)), CAST(800.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (18, N'10,250 – 10,749.99', CAST(10250.00 AS Decimal(18, 2)), CAST(10749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(420.00 AS Decimal(18, 2)), CAST(840.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (19, N'10,760 – 11,249.99', CAST(10760.00 AS Decimal(18, 2)), CAST(11249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(440.00 AS Decimal(18, 2)), CAST(880.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (20, N'11,250 – 11,749.99', CAST(11250.00 AS Decimal(18, 2)), CAST(11749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(460.00 AS Decimal(18, 2)), CAST(920.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (21, N'11,750 – 12,249.99', CAST(11750.00 AS Decimal(18, 2)), CAST(12249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(480.00 AS Decimal(18, 2)), CAST(960.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (22, N'12,250 – 12,749.99', CAST(12250.00 AS Decimal(18, 2)), CAST(12749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (23, N'12,760 – 13,249.99', CAST(12760.00 AS Decimal(18, 2)), CAST(13249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(520.00 AS Decimal(18, 2)), CAST(1040.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (24, N'13,250 – 13,749.99', CAST(13250.00 AS Decimal(18, 2)), CAST(13749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(540.00 AS Decimal(18, 2)), CAST(1080.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (25, N'13,750 – 14,249.99', CAST(13750.00 AS Decimal(18, 2)), CAST(14249.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(560.00 AS Decimal(18, 2)), CAST(1120.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (26, N'14,250 – 14,749.99', CAST(14250.00 AS Decimal(18, 2)), CAST(14749.99 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(580.00 AS Decimal(18, 2)), CAST(1160.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (27, N'14,750 – 15,249.99', CAST(14750.00 AS Decimal(18, 2)), CAST(15249.99 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), CAST(1200.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (28, N'15,250 – 15,749.99', CAST(15250.00 AS Decimal(18, 2)), CAST(15749.99 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(620.00 AS Decimal(18, 2)), CAST(1240.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (29, N'15,750 – 16,249.99', CAST(15750.00 AS Decimal(18, 2)), CAST(16249.99 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(640.00 AS Decimal(18, 2)), CAST(1280.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (30, N'16,250 – 16,749.99', CAST(16250.00 AS Decimal(18, 2)), CAST(16749.99 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(660.00 AS Decimal(18, 2)), CAST(1320.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (31, N'16,750 – 17,249.99', CAST(16750.00 AS Decimal(18, 2)), CAST(17249.99 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(680.00 AS Decimal(18, 2)), CAST(1360.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (32, N'17,250 – 17,749.99', CAST(17250.00 AS Decimal(18, 2)), CAST(17749.99 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(700.00 AS Decimal(18, 2)), CAST(1400.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (33, N'17,750 – 18,249.99', CAST(17750.00 AS Decimal(18, 2)), CAST(18249.99 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(720.00 AS Decimal(18, 2)), CAST(1440.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (34, N'18,250 – 18,749.99', CAST(18250.00 AS Decimal(18, 2)), CAST(18749.99 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(740.00 AS Decimal(18, 2)), CAST(1480.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (35, N'18,750 – 19,249.99', CAST(18750.00 AS Decimal(18, 2)), CAST(19249.99 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(760.00 AS Decimal(18, 2)), CAST(1520.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (36, N'19,250 – 19,749.99', CAST(19250.00 AS Decimal(18, 2)), CAST(19749.99 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(780.00 AS Decimal(18, 2)), CAST(1560.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ref_sss] ([ref_sss_id], [name], [salary_from], [salary_to], [monthly_salary_credit], [employee_share], [employer_share], [date_deleted]) VALUES (37, N'19,750 and above', CAST(19750.00 AS Decimal(18, 2)), CAST(100000.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), CAST(800.00 AS Decimal(18, 2)), CAST(1600.00 AS Decimal(18, 2)), NULL)
SET IDENTITY_INSERT [dbo].[ref_sss] OFF
SET IDENTITY_INSERT [dbo].[ref_status] ON 

INSERT [dbo].[ref_status] ([ref_status_id], [ref_status_name]) VALUES (1, N'FOR APPROVAL')
INSERT [dbo].[ref_status] ([ref_status_id], [ref_status_name]) VALUES (2, N'APPROVED')
INSERT [dbo].[ref_status] ([ref_status_id], [ref_status_name]) VALUES (3, N'DISAPPROVED')
INSERT [dbo].[ref_status] ([ref_status_id], [ref_status_name]) VALUES (4, N'CANCELLED')
SET IDENTITY_INSERT [dbo].[ref_status] OFF
SET IDENTITY_INSERT [dbo].[request_dtr] ON 

INSERT [dbo].[request_dtr] ([request_dtr_id], [employee_id], [ref_shift_id], [shift_date], [time_in], [time_out], [reason], [ref_status_id], [approver_remark], [approver_id], [approver_date], [date_deleted], [group_id]) VALUES (1, 2, 1, CAST(N'2020-04-30T00:00:00.000' AS DateTime), N'12:00', N'22:00', N'asdasfsdf', 1, NULL, 5, NULL, NULL, 1)
INSERT [dbo].[request_dtr] ([request_dtr_id], [employee_id], [ref_shift_id], [shift_date], [time_in], [time_out], [reason], [ref_status_id], [approver_remark], [approver_id], [approver_date], [date_deleted], [group_id]) VALUES (2, 2, 1, CAST(N'2020-04-29T00:00:00.000' AS DateTime), N'12:00', N'22:00', N'ewrwer', 1, NULL, 5, NULL, NULL, 1)
INSERT [dbo].[request_dtr] ([request_dtr_id], [employee_id], [ref_shift_id], [shift_date], [time_in], [time_out], [reason], [ref_status_id], [approver_remark], [approver_id], [approver_date], [date_deleted], [group_id]) VALUES (3, 2, 1, CAST(N'2020-04-28T00:00:00.000' AS DateTime), N'12:00', N'22:00', N'qweqwe', 1, NULL, 5, NULL, NULL, 8)
SET IDENTITY_INSERT [dbo].[request_dtr] OFF
SET IDENTITY_INSERT [dbo].[request_leave] ON 

INSERT [dbo].[request_leave] ([request_leave_id], [employee_id], [leave_date], [ref_leave_type_id], [leave_day], [ref_department_id], [reason], [ref_status_id], [approver_id], [approver_date], [approver_remark], [date_deleted], [group_id]) VALUES (1, 2, CAST(N'2020-01-31T00:00:00.000' AS DateTime), 2, CAST(1.00 AS Numeric(3, 2)), 1, N'TEST', 2, 2, CAST(N'2020-03-05T00:21:44.060' AS DateTime), N'TEST', NULL, 1)
INSERT [dbo].[request_leave] ([request_leave_id], [employee_id], [leave_date], [ref_leave_type_id], [leave_day], [ref_department_id], [reason], [ref_status_id], [approver_id], [approver_date], [approver_remark], [date_deleted], [group_id]) VALUES (2, 2, CAST(N'2020-04-01T00:00:00.000' AS DateTime), 1, CAST(1.00 AS Numeric(3, 2)), 1, N'asdasd', 1, 2, NULL, N'', NULL, 8)
SET IDENTITY_INSERT [dbo].[request_leave] OFF
SET IDENTITY_INSERT [dbo].[request_overtime] ON 

INSERT [dbo].[request_overtime] ([request_overtime_id], [employee_id], [overtime_date], [ref_overtime_type_id], [time_in], [time_out], [ref_department_id], [reason], [ref_status_id], [approver_remark], [approver_id], [approver_date], [date_deleted], [group_id]) VALUES (1, 2, CAST(N'2020-01-29T00:00:00.000' AS DateTime), 1, N'22:00', N'30:00', 1, N'TEST', 2, N'Good', 2, CAST(N'2020-03-08T11:09:29.490' AS DateTime), NULL, 1)
INSERT [dbo].[request_overtime] ([request_overtime_id], [employee_id], [overtime_date], [ref_overtime_type_id], [time_in], [time_out], [ref_department_id], [reason], [ref_status_id], [approver_remark], [approver_id], [approver_date], [date_deleted], [group_id]) VALUES (2, 2, CAST(N'2020-04-01T00:00:00.000' AS DateTime), 1, N'12:00', N'22:00', 1, N'sasdasd', 1, NULL, 2, NULL, NULL, 8)
SET IDENTITY_INSERT [dbo].[request_overtime] OFF
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([role_id], [role_name], [date_deleted]) VALUES (1, N'ADMIN', NULL)
SET IDENTITY_INSERT [dbo].[role] OFF
SET IDENTITY_INSERT [dbo].[role_menu] ON 

INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (1, 0, 1, N'Settings', N'nav-icon fas fa-business-time', N'', N'', 1, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (2, 1, 1, N'Employee', N'far fa-circle nav-icon', N'Employee', N'Index', 2, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (3, 1, 1, N'Shift', N'far fa-circle nav-icon', N'RefShift', N'Index', 3, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (4, 1, 1, N'Payroll Cutoff', N'far fa-circle nav-icon', N'RefPayrollCutoff', N'Index', 4, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (5, 1, 1, N'Role', N'far fa-circle nav-icon', N'Role', N'Index', 5, 0, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (6, 1, 1, N'Leave Type', N'far fa-circle nav-icon', N'RefLeaveType', N'Index', 6, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (7, 1, 1, N'Employee Leave Bal', N'far fa-circle nav-icon', N'EmployeeBalance', N'Index', 7, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (8, 1, 1, N'Calendar Activity', N'far fa-circle nav-icon', N'RefCalendarActivity', N'Index', 8, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (9, 1, 1, N'Groupings', N'far fa-circle nav-icon', N'RefGroup', N'Index', 9, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (10, 0, 1, N'Timesheet', N'nav-icon fas fa-business-time', N'', N'', 10, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (11, 10, 1, N'Processing', N'far fa-circle nav-icon', N'EmployeeTimesheet', N'Process', 11, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (12, 10, 1, N'DTR', N'far fa-circle nav-icon', N'EmployeeTimesheet', N'Index', 12, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (13, 0, 1, N'Request', N'nav-icon fas fa-chalkboard-teacher', N'', N'', 13, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (14, 13, 1, N'Overtime', N'far fa-circle nav-icon', N'RequestOvertime', N'Index', 14, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (15, 13, 1, N'Leave', N'far fa-circle nav-icon', N'RequestLeave', N'Index', 15, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (16, 13, 1, N'DTR Correction', N'far fa-circle nav-icon', N'RequestDTR', N'Index', 16, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (17, 0, 1, N'Approval', N'nav-icon fas fa-file-alt', N'', N'', 17, 0, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (18, 17, 1, N'Leave Calendar', N'far fa-circle nav-icon', N'Calendar', N'Index', 18, 0, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (19, 17, 1, N'Leave', N'far fa-circle nav-icon', N'RequestLeave', N'Approval', 19, 0, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (20, 17, 1, N'Overtime', N'far fa-circle nav-icon', N'RequestOvertime', N'Approval', 20, 0, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (21, 17, 1, N'DTR Correction', N'far fa-circle nav-icon', N'RequestDTR', N'Approval', 21, 0, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (22, 0, 1, N'Report', N'nav-icon fas fa-file-alt', N'', N'', 22, 1, NULL)
INSERT [dbo].[role_menu] ([role_menu_id], [role_menu_parent_id], [role_id], [display_name], [display_icon], [controller_name], [action_name], [ordering], [is_enable], [date_deleted]) VALUES (23, 22, 1, N'DTR Report', N'far fa-circle nav-icon', N'EmployeeTimesheet', N'TimesheetReport', 23, 1, NULL)
SET IDENTITY_INSERT [dbo].[role_menu] OFF
SET IDENTITY_INSERT [dbo].[role_permission] ON 

INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (1, 1, 1, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (2, 1, 15, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (3, 1, 14, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (4, 1, 13, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (5, 1, 12, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (6, 1, 11, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (7, 1, 10, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (8, 1, 16, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (9, 1, 9, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (10, 1, 7, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (11, 1, 6, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (12, 1, 5, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (13, 1, 4, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (14, 1, 3, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (15, 1, 2, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (16, 1, 8, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (17, 1, 17, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (18, 1, 18, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (19, 1, 19, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (20, 1, 20, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (21, 1, 21, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (22, 1, 22, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (23, 1, 23, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (24, 1, 24, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (25, 1, 27, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (26, 1, 28, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (27, 1, 29, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (28, 1, 30, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (29, 1, 31, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (30, 1, 32, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (31, 1, 33, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (32, 1, 34, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (33, 1, 35, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (34, 1, 36, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (35, 1, 37, 1)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (36, 1, 25, 0)
INSERT [dbo].[role_permission] ([role_permission_id], [role_id], [permission_id], [is_enable]) VALUES (37, 1, 26, 1)
SET IDENTITY_INSERT [dbo].[role_permission] OFF
/****** Object:  Index [IX_FK_employee_ref_department]    Script Date: 4/12/2020 9:07:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_employee_ref_department] ON [dbo].[employee]
(
	[ref_department_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_employee_ref_shift]    Script Date: 4/12/2020 9:07:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_employee_ref_shift] ON [dbo].[employee]
(
	[ref_shift_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_employee_timesheet_employee]    Script Date: 4/12/2020 9:07:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_employee_timesheet_employee] ON [dbo].[employee_timesheet]
(
	[employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_employee_timesheet_ref_day_type]    Script Date: 4/12/2020 9:07:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_employee_timesheet_ref_day_type] ON [dbo].[employee_timesheet]
(
	[ref_day_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_employee_timesheet_ref_shift]    Script Date: 4/12/2020 9:07:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_employee_timesheet_ref_shift] ON [dbo].[employee_timesheet]
(
	[ref_shift_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [LevelUnique]    Script Date: 4/12/2020 9:07:21 AM ******/
ALTER TABLE [dbo].[ref_group] ADD  CONSTRAINT [LevelUnique] UNIQUE NONCLUSTERED 
(
	[level] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_ref_shift_detail_ref_shift]    Script Date: 4/12/2020 9:07:21 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_ref_shift_detail_ref_shift] ON [dbo].[ref_shift_detail]
(
	[ref_shift_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[employee] ADD  CONSTRAINT [DF_employee_ref_pay_type_id]  DEFAULT ((1)) FOR [ref_pay_type_id]
GO
ALTER TABLE [dbo].[employee] ADD  CONSTRAINT [DF_employee_ref_role_id]  DEFAULT ((1)) FOR [role_id]
GO
ALTER TABLE [dbo].[employee_group] ADD  CONSTRAINT [DF_employee_group_approver_all]  DEFAULT ((0)) FOR [approver_all]
GO
ALTER TABLE [dbo].[employee_group] ADD  CONSTRAINT [DF_employee_group_ot_approver]  DEFAULT ((0)) FOR [ot_approver]
GO
ALTER TABLE [dbo].[employee_group] ADD  CONSTRAINT [DF_employee_group_ob_approver]  DEFAULT ((0)) FOR [ob_approver]
GO
ALTER TABLE [dbo].[employee_group] ADD  CONSTRAINT [DF_employee_group_leave_approver]  DEFAULT ((0)) FOR [leave_approver]
GO
ALTER TABLE [dbo].[employee_group] ADD  CONSTRAINT [DF_employee_group_dtr_approver]  DEFAULT ((0)) FOR [dtr_approver]
GO
ALTER TABLE [dbo].[payroll_config] ADD  CONSTRAINT [DF_payroll_config_config_value]  DEFAULT ((0)) FOR [config_value]
GO
ALTER TABLE [dbo].[payroll_details] ADD  CONSTRAINT [DF_payroll_details_amount]  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[payroll_earning_deduction] ADD  CONSTRAINT [DF_payroll_earning_deduction_cutoff]  DEFAULT ((1)) FOR [cutoff]
GO
ALTER TABLE [dbo].[payroll_earning_deduction] ADD  CONSTRAINT [DF_payroll_earning_deduction_amount]  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[payroll_earning_deduction] ADD  CONSTRAINT [DF_payroll_earning_deduction_recurring]  DEFAULT ((0)) FOR [recurring]
GO
ALTER TABLE [dbo].[ref_bir] ADD  CONSTRAINT [DF_ref_bir_salary_from]  DEFAULT ((0)) FOR [salary_from]
GO
ALTER TABLE [dbo].[ref_bir] ADD  CONSTRAINT [DF_ref_bir_salary_to]  DEFAULT ((0)) FOR [salary_to]
GO
ALTER TABLE [dbo].[ref_bir] ADD  CONSTRAINT [DF_ref_bir_add_tax]  DEFAULT ((0)) FOR [add_tax]
GO
ALTER TABLE [dbo].[ref_bir] ADD  CONSTRAINT [DF_Table_1_subtract_tax]  DEFAULT ((0)) FOR [subtract_tax_over]
GO
ALTER TABLE [dbo].[ref_bir] ADD  CONSTRAINT [DF_ref_bir_multiplier]  DEFAULT ((0)) FOR [multiplier]
GO
ALTER TABLE [dbo].[ref_day_type] ADD  CONSTRAINT [DF_ref_day_type_ot_multiplier]  DEFAULT ((0)) FOR [ot_multiplier]
GO
ALTER TABLE [dbo].[ref_day_type] ADD  CONSTRAINT [DF_ref_day_type_nightdif_multiplier1]  DEFAULT ((0)) FOR [nightdif_multiplier1]
GO
ALTER TABLE [dbo].[ref_day_type] ADD  CONSTRAINT [DF_ref_day_type_nightdif_multiplier2]  DEFAULT ((0)) FOR [nightdif_multiplier2]
GO
ALTER TABLE [dbo].[ref_group] ADD  CONSTRAINT [DF_ref_group_is_enable_1]  DEFAULT ((1)) FOR [is_enable]
GO
ALTER TABLE [dbo].[ref_leave_type] ADD  CONSTRAINT [DF_ref_leave_type_with_pay]  DEFAULT ((0)) FOR [with_pay]
GO
ALTER TABLE [dbo].[ref_payroll_cutoff] ADD  CONSTRAINT [DF_ref_payroll_cutoff_pay_date]  DEFAULT (getdate()) FOR [pay_date]
GO
ALTER TABLE [dbo].[ref_payroll_cutoff] ADD  CONSTRAINT [DF_ref_payroll_cutoff_is_processed]  DEFAULT ((0)) FOR [is_processed]
GO
ALTER TABLE [dbo].[ref_payroll_cutoff] ADD  CONSTRAINT [DF_ref_payroll_cutoff_government]  DEFAULT ((0)) FOR [government]
GO
ALTER TABLE [dbo].[ref_payroll_cutoff] ADD  CONSTRAINT [DF_ref_payroll_cutoff_cutoff]  DEFAULT ((1)) FOR [cutoff]
GO
ALTER TABLE [dbo].[ref_payroll_details_type] ADD  CONSTRAINT [DF_ref_payroll_details_type_earnings]  DEFAULT ((0)) FOR [earnings]
GO
ALTER TABLE [dbo].[ref_payroll_details_type] ADD  CONSTRAINT [DF_ref_payroll_details_type_taxable]  DEFAULT ((0)) FOR [taxable]
GO
ALTER TABLE [dbo].[ref_payroll_details_type] ADD  CONSTRAINT [DF_ref_payroll_details_type_company_contribution]  DEFAULT ((0)) FOR [company_contribution]
GO
ALTER TABLE [dbo].[ref_shift] ADD  CONSTRAINT [DF_ref_shift_grace_period]  DEFAULT ((0)) FOR [grace_period]
GO
ALTER TABLE [dbo].[ref_shift] ADD  CONSTRAINT [DF_ref_shift_include_grace_period]  DEFAULT ((0)) FOR [include_grace_period]
GO
ALTER TABLE [dbo].[ref_shift] ADD  CONSTRAINT [DF_ref_shift_is_ot]  DEFAULT ((1)) FOR [is_auto_ot]
GO
ALTER TABLE [dbo].[ref_shift] ADD  CONSTRAINT [DF_ref_shift_is_nd]  DEFAULT ((0)) FOR [is_nd]
GO
ALTER TABLE [dbo].[request_dtr] ADD  CONSTRAINT [DF_request_dtr_group_id]  DEFAULT ((1)) FOR [group_id]
GO
ALTER TABLE [dbo].[request_leave] ADD  CONSTRAINT [DF_request_leave_group_id]  DEFAULT ((1)) FOR [group_id]
GO
ALTER TABLE [dbo].[request_overtime] ADD  CONSTRAINT [DF_request_overtime_group_id]  DEFAULT ((1)) FOR [group_id]
GO
ALTER TABLE [dbo].[role_menu] ADD  CONSTRAINT [DF_role_menu_role_menu_parent_id]  DEFAULT ((0)) FOR [role_menu_parent_id]
GO
ALTER TABLE [dbo].[role_menu] ADD  CONSTRAINT [DF_role_menu_is_enable]  DEFAULT ((1)) FOR [is_enable]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_employee] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employee] ([employee_id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_employee]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_ref_department] FOREIGN KEY([ref_department_id])
REFERENCES [dbo].[ref_department] ([ref_department_id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_ref_department]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_ref_pay_type] FOREIGN KEY([ref_pay_type_id])
REFERENCES [dbo].[ref_pay_type] ([ref_pay_type_id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_ref_pay_type]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_ref_shift] FOREIGN KEY([ref_shift_id])
REFERENCES [dbo].[ref_shift] ([ref_shift_id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_ref_shift]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_role]
GO
ALTER TABLE [dbo].[employee_balance]  WITH CHECK ADD  CONSTRAINT [FK_employee_balance_employee] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employee] ([employee_id])
GO
ALTER TABLE [dbo].[employee_balance] CHECK CONSTRAINT [FK_employee_balance_employee]
GO
ALTER TABLE [dbo].[employee_balance]  WITH CHECK ADD  CONSTRAINT [FK_employee_balance_ref_leave_type] FOREIGN KEY([ref_leave_type_id])
REFERENCES [dbo].[ref_leave_type] ([ref_leave_type_id])
GO
ALTER TABLE [dbo].[employee_balance] CHECK CONSTRAINT [FK_employee_balance_ref_leave_type]
GO
ALTER TABLE [dbo].[employee_balance_transaction]  WITH CHECK ADD  CONSTRAINT [FK_employee_balance_transaction_employee] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employee] ([employee_id])
GO
ALTER TABLE [dbo].[employee_balance_transaction] CHECK CONSTRAINT [FK_employee_balance_transaction_employee]
GO
ALTER TABLE [dbo].[employee_balance_transaction]  WITH CHECK ADD  CONSTRAINT [FK_employee_balance_transaction_employee_balance] FOREIGN KEY([employee_balance_id])
REFERENCES [dbo].[employee_balance] ([employee_balance_id])
GO
ALTER TABLE [dbo].[employee_balance_transaction] CHECK CONSTRAINT [FK_employee_balance_transaction_employee_balance]
GO
ALTER TABLE [dbo].[employee_group]  WITH CHECK ADD  CONSTRAINT [FK_employee_group_employee] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employee] ([employee_id])
GO
ALTER TABLE [dbo].[employee_group] CHECK CONSTRAINT [FK_employee_group_employee]
GO
ALTER TABLE [dbo].[employee_group]  WITH CHECK ADD  CONSTRAINT [FK_employee_group_ref_group] FOREIGN KEY([group_id])
REFERENCES [dbo].[ref_group] ([group_id])
GO
ALTER TABLE [dbo].[employee_group] CHECK CONSTRAINT [FK_employee_group_ref_group]
GO
ALTER TABLE [dbo].[employee_timesheet]  WITH CHECK ADD  CONSTRAINT [FK_employee_timesheet_employee] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employee] ([employee_id])
GO
ALTER TABLE [dbo].[employee_timesheet] CHECK CONSTRAINT [FK_employee_timesheet_employee]
GO
ALTER TABLE [dbo].[employee_timesheet]  WITH CHECK ADD  CONSTRAINT [FK_employee_timesheet_ref_day_type] FOREIGN KEY([ref_day_type_id])
REFERENCES [dbo].[ref_day_type] ([ref_day_type_id])
GO
ALTER TABLE [dbo].[employee_timesheet] CHECK CONSTRAINT [FK_employee_timesheet_ref_day_type]
GO
ALTER TABLE [dbo].[employee_timesheet]  WITH CHECK ADD  CONSTRAINT [FK_employee_timesheet_ref_shift] FOREIGN KEY([ref_shift_id])
REFERENCES [dbo].[ref_shift] ([ref_shift_id])
GO
ALTER TABLE [dbo].[employee_timesheet] CHECK CONSTRAINT [FK_employee_timesheet_ref_shift]
GO
ALTER TABLE [dbo].[payroll]  WITH CHECK ADD  CONSTRAINT [FK_payroll_payroll] FOREIGN KEY([payroll_id])
REFERENCES [dbo].[payroll] ([payroll_id])
GO
ALTER TABLE [dbo].[payroll] CHECK CONSTRAINT [FK_payroll_payroll]
GO
ALTER TABLE [dbo].[payroll_details]  WITH CHECK ADD  CONSTRAINT [FK_payroll_details_payroll] FOREIGN KEY([payroll_id])
REFERENCES [dbo].[payroll] ([payroll_id])
GO
ALTER TABLE [dbo].[payroll_details] CHECK CONSTRAINT [FK_payroll_details_payroll]
GO
ALTER TABLE [dbo].[payroll_earning_deduction]  WITH CHECK ADD  CONSTRAINT [FK_payroll_earning_deduction_ref_payroll_details_type] FOREIGN KEY([ref_payroll_details_type_id])
REFERENCES [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id])
GO
ALTER TABLE [dbo].[payroll_earning_deduction] CHECK CONSTRAINT [FK_payroll_earning_deduction_ref_payroll_details_type]
GO
ALTER TABLE [dbo].[ref_calendar_activity]  WITH CHECK ADD  CONSTRAINT [FK_ref_calendar_activity_ref_day_type] FOREIGN KEY([ref_day_type_id])
REFERENCES [dbo].[ref_day_type] ([ref_day_type_id])
GO
ALTER TABLE [dbo].[ref_calendar_activity] CHECK CONSTRAINT [FK_ref_calendar_activity_ref_day_type]
GO
ALTER TABLE [dbo].[ref_department_approver]  WITH CHECK ADD  CONSTRAINT [FK_ref_department_approver_employee] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employee] ([employee_id])
GO
ALTER TABLE [dbo].[ref_department_approver] CHECK CONSTRAINT [FK_ref_department_approver_employee]
GO
ALTER TABLE [dbo].[ref_department_approver]  WITH CHECK ADD  CONSTRAINT [FK_ref_department_approver_ref_department] FOREIGN KEY([ref_department_id])
REFERENCES [dbo].[ref_department] ([ref_department_id])
GO
ALTER TABLE [dbo].[ref_department_approver] CHECK CONSTRAINT [FK_ref_department_approver_ref_department]
GO
ALTER TABLE [dbo].[ref_pay_type]  WITH CHECK ADD  CONSTRAINT [FK_ref_pay_type_ref_pay_type] FOREIGN KEY([ref_pay_type_id])
REFERENCES [dbo].[ref_pay_type] ([ref_pay_type_id])
GO
ALTER TABLE [dbo].[ref_pay_type] CHECK CONSTRAINT [FK_ref_pay_type_ref_pay_type]
GO
ALTER TABLE [dbo].[ref_shift_detail]  WITH CHECK ADD  CONSTRAINT [FK_ref_shift_detail_ref_shift] FOREIGN KEY([ref_shift_id])
REFERENCES [dbo].[ref_shift] ([ref_shift_id])
GO
ALTER TABLE [dbo].[ref_shift_detail] CHECK CONSTRAINT [FK_ref_shift_detail_ref_shift]
GO
ALTER TABLE [dbo].[request_dtr]  WITH CHECK ADD  CONSTRAINT [FK_request_dtr_employee] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employee] ([employee_id])
GO
ALTER TABLE [dbo].[request_dtr] CHECK CONSTRAINT [FK_request_dtr_employee]
GO
ALTER TABLE [dbo].[request_dtr]  WITH CHECK ADD  CONSTRAINT [FK_request_dtr_ref_shift] FOREIGN KEY([ref_shift_id])
REFERENCES [dbo].[ref_shift] ([ref_shift_id])
GO
ALTER TABLE [dbo].[request_dtr] CHECK CONSTRAINT [FK_request_dtr_ref_shift]
GO
ALTER TABLE [dbo].[request_dtr]  WITH CHECK ADD  CONSTRAINT [FK_request_dtr_ref_status] FOREIGN KEY([ref_status_id])
REFERENCES [dbo].[ref_status] ([ref_status_id])
GO
ALTER TABLE [dbo].[request_dtr] CHECK CONSTRAINT [FK_request_dtr_ref_status]
GO
ALTER TABLE [dbo].[request_leave]  WITH CHECK ADD  CONSTRAINT [FK_request_leave_employee] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employee] ([employee_id])
GO
ALTER TABLE [dbo].[request_leave] CHECK CONSTRAINT [FK_request_leave_employee]
GO
ALTER TABLE [dbo].[request_leave]  WITH CHECK ADD  CONSTRAINT [FK_request_leave_ref_department] FOREIGN KEY([ref_department_id])
REFERENCES [dbo].[ref_department] ([ref_department_id])
GO
ALTER TABLE [dbo].[request_leave] CHECK CONSTRAINT [FK_request_leave_ref_department]
GO
ALTER TABLE [dbo].[request_leave]  WITH CHECK ADD  CONSTRAINT [FK_request_leave_ref_leave_type] FOREIGN KEY([ref_leave_type_id])
REFERENCES [dbo].[ref_leave_type] ([ref_leave_type_id])
GO
ALTER TABLE [dbo].[request_leave] CHECK CONSTRAINT [FK_request_leave_ref_leave_type]
GO
ALTER TABLE [dbo].[request_leave]  WITH CHECK ADD  CONSTRAINT [FK_request_leave_ref_status] FOREIGN KEY([ref_status_id])
REFERENCES [dbo].[ref_status] ([ref_status_id])
GO
ALTER TABLE [dbo].[request_leave] CHECK CONSTRAINT [FK_request_leave_ref_status]
GO
ALTER TABLE [dbo].[request_overtime]  WITH CHECK ADD  CONSTRAINT [FK_request_overtime_employee] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employee] ([employee_id])
GO
ALTER TABLE [dbo].[request_overtime] CHECK CONSTRAINT [FK_request_overtime_employee]
GO
ALTER TABLE [dbo].[request_overtime]  WITH CHECK ADD  CONSTRAINT [FK_request_overtime_ref_department] FOREIGN KEY([ref_department_id])
REFERENCES [dbo].[ref_department] ([ref_department_id])
GO
ALTER TABLE [dbo].[request_overtime] CHECK CONSTRAINT [FK_request_overtime_ref_department]
GO
ALTER TABLE [dbo].[request_overtime]  WITH CHECK ADD  CONSTRAINT [FK_request_overtime_ref_overtime_type] FOREIGN KEY([ref_overtime_type_id])
REFERENCES [dbo].[ref_overtime_type] ([ref_overtime_type_id])
GO
ALTER TABLE [dbo].[request_overtime] CHECK CONSTRAINT [FK_request_overtime_ref_overtime_type]
GO
ALTER TABLE [dbo].[request_overtime]  WITH CHECK ADD  CONSTRAINT [FK_request_overtime_ref_status] FOREIGN KEY([ref_status_id])
REFERENCES [dbo].[ref_status] ([ref_status_id])
GO
ALTER TABLE [dbo].[request_overtime] CHECK CONSTRAINT [FK_request_overtime_ref_status]
GO
ALTER TABLE [dbo].[role]  WITH CHECK ADD  CONSTRAINT [FK_role_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[role] CHECK CONSTRAINT [FK_role_role]
GO
ALTER TABLE [dbo].[role_permission]  WITH CHECK ADD  CONSTRAINT [FK_role_permission_permission] FOREIGN KEY([permission_id])
REFERENCES [dbo].[permission] ([permission_id])
GO
ALTER TABLE [dbo].[role_permission] CHECK CONSTRAINT [FK_role_permission_permission]
GO
ALTER TABLE [dbo].[role_permission]  WITH CHECK ADD  CONSTRAINT [FK_role_permission_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[role_permission] CHECK CONSTRAINT [FK_role_permission_role]
GO
USE [master]
GO
ALTER DATABASE [PayrollDB] SET  READ_WRITE 
GO
