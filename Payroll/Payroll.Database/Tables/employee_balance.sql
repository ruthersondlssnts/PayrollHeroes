CREATE TABLE [dbo].[employee_balance] (
    [employee_balance_id] INT             IDENTITY (1, 1) NOT NULL,
    [employee_id]         INT             NOT NULL,
    [ref_leave_type_id]   INT             NOT NULL,
    [acquire_date]        DATETIME        NOT NULL,
    [expire_date]         DATETIME        NOT NULL,
    [quantity]            DECIMAL (18, 2) NOT NULL,
    [remarks]             VARCHAR (200)   NULL,
    [date_deleted]        DATETIME        NULL,
    CONSTRAINT [PK_employee_balance] PRIMARY KEY CLUSTERED ([employee_balance_id] ASC),
    CONSTRAINT [FK_employee_balance_employee] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[employee] ([employee_id]),
    CONSTRAINT [FK_employee_balance_ref_leave_type] FOREIGN KEY ([ref_leave_type_id]) REFERENCES [dbo].[ref_leave_type] ([ref_leave_type_id])
);

