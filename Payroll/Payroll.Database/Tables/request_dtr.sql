CREATE TABLE [dbo].[request_dtr] (
    [request_dtr_id]  INT           IDENTITY (1, 1) NOT NULL,
    [employee_id]     INT           NOT NULL,
    [ref_shift_id]    INT           NOT NULL,
    [shift_date]      DATETIME      NOT NULL,
    [time_in]         VARCHAR (5)   NOT NULL,
    [time_out]        VARCHAR (5)   NOT NULL,
    [reason]          VARCHAR (500) NOT NULL,
    [ref_status_id]   INT           NOT NULL,
    [approver_remark] VARCHAR (500) NULL,
    [approver_id]     INT           NOT NULL,
    [approver_date]   DATETIME      NULL,
    [date_deleted]    DATETIME      NULL,
    [group_id]        INT           CONSTRAINT [DF_request_dtr_group_id] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_request_dtr] PRIMARY KEY CLUSTERED ([request_dtr_id] ASC),
    CONSTRAINT [FK_request_dtr_employee] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[employee] ([employee_id]),
    CONSTRAINT [FK_request_dtr_ref_shift] FOREIGN KEY ([ref_shift_id]) REFERENCES [dbo].[ref_shift] ([ref_shift_id]),
    CONSTRAINT [FK_request_dtr_ref_status] FOREIGN KEY ([ref_status_id]) REFERENCES [dbo].[ref_status] ([ref_status_id])
);

