CREATE TABLE [dbo].[ref_payroll_cutoff] (
    [ref_payroll_cutoff_id] INT      IDENTITY (1, 1) NOT NULL,
    [pay_date]              DATETIME CONSTRAINT [DF_ref_payroll_cutoff_pay_date] DEFAULT (getdate()) NOT NULL,
    [cutoff_date_start]     DATETIME NOT NULL,
    [cutoff_date_end]       DATETIME NOT NULL,
    [is_processed]          BIT      CONSTRAINT [DF_ref_payroll_cutoff_is_processed] DEFAULT ((0)) NOT NULL,
    [date_deleted]          DATETIME NULL,
    [government]            BIT      CONSTRAINT [DF_ref_payroll_cutoff_government] DEFAULT ((0)) NOT NULL,
    [cutoff]                INT      CONSTRAINT [DF_ref_payroll_cutoff_cutoff] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ref_payroll_cutoff] PRIMARY KEY CLUSTERED ([ref_payroll_cutoff_id] ASC)
);

