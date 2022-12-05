CREATE TABLE [dbo].[payroll_earning_deduction] (
    [payroll_earning_deduction_id] INT             IDENTITY (1, 1) NOT NULL,
    [ref_employee_type_id]         INT             CONSTRAINT [DF_payroll_earning_deduction_ref_employee_type] DEFAULT ((1)) NOT NULL,
    [ref_payroll_details_type_id]  INT             NOT NULL,
    [exact_date]                   DATETIME        NULL,
    [cutoff]                       INT             CONSTRAINT [DF_payroll_earning_deduction_cutoff] DEFAULT ((1)) NOT NULL,
    [amount]                       NUMERIC (18, 2) CONSTRAINT [DF_payroll_earning_deduction_amount] DEFAULT ((0)) NOT NULL,
    [recurring]                    BIT             CONSTRAINT [DF_payroll_earning_deduction_recurring] DEFAULT ((0)) NOT NULL,
    [date_deleted]                 DATETIME        NULL,
    CONSTRAINT [PK_payroll_earning_deduction] PRIMARY KEY CLUSTERED ([payroll_earning_deduction_id] ASC),
    CONSTRAINT [FK_payroll_earning_deduction_ref_payroll_details_type] FOREIGN KEY ([ref_payroll_details_type_id]) REFERENCES [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id])
);



