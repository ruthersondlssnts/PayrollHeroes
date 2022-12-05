CREATE TABLE [dbo].[ref_payroll_status] (
    [ref_payroll_status_id] INT          IDENTITY (1, 1) NOT NULL,
    [payroll_status_name]   VARCHAR (50) NULL,
    CONSTRAINT [PK_ref_payroll_status] PRIMARY KEY CLUSTERED ([ref_payroll_status_id] ASC)
);

