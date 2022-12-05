CREATE TABLE [dbo].[request_overtime] (
    [request_overtime_id]  INT           IDENTITY (1, 1) NOT NULL,
    [employee_id]          INT           NOT NULL,
    [overtime_date]        DATETIME      NOT NULL,
    [ref_overtime_type_id] INT           NOT NULL,
    [time_in]              VARCHAR (5)   NOT NULL,
    [time_out]             VARCHAR (5)   NOT NULL,
    [ref_department_id]    INT           NOT NULL,
    [reason]               VARCHAR (500) NOT NULL,
    [ref_status_id]        INT           NOT NULL,
    [approver_remark]      VARCHAR (500) NULL,
    [approver_id]          INT           NOT NULL,
    [approver_date]        DATETIME      NULL,
    [date_deleted]         DATETIME      NULL,
    [group_id]             INT           CONSTRAINT [DF_request_overtime_group_id] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_request_overtime] PRIMARY KEY CLUSTERED ([request_overtime_id] ASC),
    CONSTRAINT [FK_request_overtime_employee] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[employee] ([employee_id]),
    CONSTRAINT [FK_request_overtime_ref_department] FOREIGN KEY ([ref_department_id]) REFERENCES [dbo].[ref_department] ([ref_department_id]),
    CONSTRAINT [FK_request_overtime_ref_overtime_type] FOREIGN KEY ([ref_overtime_type_id]) REFERENCES [dbo].[ref_overtime_type] ([ref_overtime_type_id]),
    CONSTRAINT [FK_request_overtime_ref_status] FOREIGN KEY ([ref_status_id]) REFERENCES [dbo].[ref_status] ([ref_status_id])
);

