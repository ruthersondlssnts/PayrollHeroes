CREATE TABLE [dbo].[request_leave] (
    [request_leave_id]  INT            IDENTITY (1, 1) NOT NULL,
    [employee_id]       INT            NOT NULL,
    [leave_date]        DATETIME       NOT NULL,
    [ref_leave_type_id] INT            NOT NULL,
    [leave_day]         NUMERIC (3, 2) NOT NULL,
    [ref_department_id] INT            NOT NULL,
    [reason]            VARCHAR (500)  NOT NULL,
    [ref_status_id]     INT            NOT NULL,
    [approver_id]       INT            NULL,
    [approver_date]     DATETIME       NULL,
    [approver_remark]   VARCHAR (500)  NULL,
    [date_deleted]      DATETIME       NULL,
    [group_id]          INT            CONSTRAINT [DF_request_leave_group_id] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_request_leave] PRIMARY KEY CLUSTERED ([request_leave_id] ASC),
    CONSTRAINT [FK_request_leave_employee] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[employee] ([employee_id]),
    CONSTRAINT [FK_request_leave_ref_department] FOREIGN KEY ([ref_department_id]) REFERENCES [dbo].[ref_department] ([ref_department_id]),
    CONSTRAINT [FK_request_leave_ref_leave_type] FOREIGN KEY ([ref_leave_type_id]) REFERENCES [dbo].[ref_leave_type] ([ref_leave_type_id]),
    CONSTRAINT [FK_request_leave_ref_status] FOREIGN KEY ([ref_status_id]) REFERENCES [dbo].[ref_status] ([ref_status_id])
);

