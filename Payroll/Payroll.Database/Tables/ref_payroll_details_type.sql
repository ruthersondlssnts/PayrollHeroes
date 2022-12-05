CREATE TABLE [dbo].[ref_payroll_details_type] (
    [ref_payroll_details_type_id]   INT           IDENTITY (1, 1) NOT NULL,
    [ref_payroll_details_type_name] VARCHAR (100) NULL,
    [ref_payroll_details_type_code] VARCHAR (100) NULL,
    [ref_day_type_id]               INT           NULL,
    [earnings]                      BIT           CONSTRAINT [DF_ref_payroll_details_type_earnings] DEFAULT ((0)) NOT NULL,
    [taxable]                       BIT           CONSTRAINT [DF_ref_payroll_details_type_taxable] DEFAULT ((0)) NOT NULL,
    [company_contribution]          BIT           CONSTRAINT [DF_ref_payroll_details_type_company_contribution] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ref_payroll_details_type] PRIMARY KEY CLUSTERED ([ref_payroll_details_type_id] ASC)
);

