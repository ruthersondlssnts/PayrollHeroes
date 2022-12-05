CREATE TABLE [dbo].[ref_pay_type] (
    [ref_pay_type_id]   INT          IDENTITY (1, 1) NOT NULL,
    [ref_pay_type_name] VARCHAR (20) NULL,
    CONSTRAINT [PK_ref_pay_type] PRIMARY KEY CLUSTERED ([ref_pay_type_id] ASC),
    CONSTRAINT [FK_ref_pay_type_ref_pay_type] FOREIGN KEY ([ref_pay_type_id]) REFERENCES [dbo].[ref_pay_type] ([ref_pay_type_id])
);

