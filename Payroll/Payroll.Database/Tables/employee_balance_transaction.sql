CREATE TABLE [dbo].[employee_balance_transaction] (
    [employee_balance_transaction_id] INT             IDENTITY (1, 1) NOT NULL,
    [employee_id]                     INT             NOT NULL,
    [employee_balance_id]             INT             NOT NULL,
    [requested_date]                  DATETIME        NOT NULL,
    [approved_date]                   DATETIME        NOT NULL,
    [quantity]                        DECIMAL (18, 2) NOT NULL,
    [date_deleted]                    DATETIME        NULL,
    CONSTRAINT [PK_employee_balance_transaction] PRIMARY KEY CLUSTERED ([employee_balance_transaction_id] ASC),
    CONSTRAINT [FK_employee_balance_transaction_employee] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[employee] ([employee_id]),
    CONSTRAINT [FK_employee_balance_transaction_employee_balance] FOREIGN KEY ([employee_balance_id]) REFERENCES [dbo].[employee_balance] ([employee_balance_id])
);

