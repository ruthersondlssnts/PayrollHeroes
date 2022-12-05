CREATE TABLE [dbo].[payroll_details] (
    [payroll_details_id]          INT             IDENTITY (1, 1) NOT NULL,
    [payroll_id]                  INT             NOT NULL,
    [ref_payroll_details_type_id] INT             NOT NULL,
    [qty]                         NUMERIC (18, 2) NULL,
    [amount]                      NUMERIC (18, 2) CONSTRAINT [DF_payroll_details_amount] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_payroll_details] PRIMARY KEY CLUSTERED ([payroll_details_id] ASC),
    CONSTRAINT [FK_payroll_details_payroll] FOREIGN KEY ([payroll_id]) REFERENCES [dbo].[payroll] ([payroll_id]),
    CONSTRAINT [FK_payroll_details_payroll_details] FOREIGN KEY ([payroll_details_id]) REFERENCES [dbo].[payroll_details] ([payroll_details_id]),
    CONSTRAINT [FK_payroll_details_ref_payroll_details_type] FOREIGN KEY ([ref_payroll_details_type_id]) REFERENCES [dbo].[ref_payroll_details_type] ([ref_payroll_details_type_id])
);



