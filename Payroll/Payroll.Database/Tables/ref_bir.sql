CREATE TABLE [dbo].[ref_bir] (
    [ref_bir_id]        INT             IDENTITY (1, 1) NOT NULL,
    [ref_pay_type_id]   INT             NOT NULL,
    [salary_from]       NUMERIC (18, 2) CONSTRAINT [DF_ref_bir_salary_from] DEFAULT ((0)) NOT NULL,
    [salary_to]         NUMERIC (18, 2) CONSTRAINT [DF_ref_bir_salary_to] DEFAULT ((0)) NOT NULL,
    [add_tax]           NUMERIC (18, 2) CONSTRAINT [DF_ref_bir_add_tax] DEFAULT ((0)) NOT NULL,
    [subtract_tax_over] NUMERIC (18, 2) CONSTRAINT [DF_Table_1_subtract_tax] DEFAULT ((0)) NOT NULL,
    [multiplier]        NUMERIC (18, 2) CONSTRAINT [DF_ref_bir_multiplier] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ref_bir] PRIMARY KEY CLUSTERED ([ref_bir_id] ASC)
);

