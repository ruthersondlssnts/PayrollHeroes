CREATE TABLE [dbo].[ref_department_approver] (
    [ref_department_approver_id] INT      IDENTITY (1, 1) NOT NULL,
    [ref_department_id]          INT      NOT NULL,
    [employee_id]                INT      NOT NULL,
    [ordering]                   INT      NOT NULL,
    [date_deleted]               DATETIME NULL,
    CONSTRAINT [PK_ref_department_approver] PRIMARY KEY CLUSTERED ([ref_department_approver_id] ASC),
    CONSTRAINT [FK_ref_department_approver_employee] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[employee] ([employee_id]),
    CONSTRAINT [FK_ref_department_approver_ref_department] FOREIGN KEY ([ref_department_id]) REFERENCES [dbo].[ref_department] ([ref_department_id])
);

