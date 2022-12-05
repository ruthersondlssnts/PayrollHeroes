CREATE TABLE [dbo].[payroll] (
    [payroll_id]            INT             IDENTITY (1, 1) NOT NULL,
    [employee_id]           INT             NOT NULL,
    [ref_payroll_cutoff_id] INT             NOT NULL,
    [total_earnings]        NUMERIC (18, 2) NOT NULL,
    [total_deduction]       NUMERIC (18, 2) NOT NULL,
    [date_process]          DATETIME        NOT NULL,
    CONSTRAINT [PK_payroll] PRIMARY KEY CLUSTERED ([payroll_id] ASC),
    CONSTRAINT [FK_payroll_payroll] FOREIGN KEY ([payroll_id]) REFERENCES [dbo].[payroll] ([payroll_id])
);

