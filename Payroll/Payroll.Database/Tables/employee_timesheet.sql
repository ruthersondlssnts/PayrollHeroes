CREATE TABLE [dbo].[employee_timesheet] (
    [employee_timesheet_id] BIGINT          IDENTITY (1, 1) NOT NULL,
    [employee_id]           INT             NOT NULL,
    [shift_date]            DATETIME        NULL,
    [ref_shift_id]          INT             NOT NULL,
    [ref_day_type_id]       INT             NOT NULL,
    [holiday_day_type_id]   INT             NULL,
    [required_hour]         DECIMAL (18, 2) NULL,
    [rendered_hour]         DECIMAL (18, 2) NULL,
    [time_in1]              VARCHAR (5)     NULL,
    [time_out1]             VARCHAR (5)     NULL,
    [time_in2]              VARCHAR (5)     NULL,
    [time_out2]             VARCHAR (5)     NULL,
    [ot_in]                 VARCHAR (5)     NULL,
    [ot_out]                VARCHAR (5)     NULL,
    [ot]                    DECIMAL (18, 2) NULL,
    [ot8]                   DECIMAL (18, 2) NULL,
    [night_dif]             DECIMAL (18, 2) NULL,
    [night_dif8]            DECIMAL (18, 2) NULL,
    [absent]                DECIMAL (18, 2) NULL,
    [late]                  DECIMAL (18, 2) NULL,
    [undertime]             DECIMAL (18, 2) NULL,
    [approve_leave]         DECIMAL (18, 2) NULL,
    [ref_leave_type_id]     INT             NULL,
    [payroll_process]       BIT             NOT NULL,
    CONSTRAINT [PK_employee_timesheet] PRIMARY KEY CLUSTERED ([employee_timesheet_id] ASC),
    CONSTRAINT [FK_employee_timesheet_employee] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[employee] ([employee_id]),
    CONSTRAINT [FK_employee_timesheet_ref_day_type] FOREIGN KEY ([ref_day_type_id]) REFERENCES [dbo].[ref_day_type] ([ref_day_type_id]),
    CONSTRAINT [FK_employee_timesheet_ref_shift] FOREIGN KEY ([ref_shift_id]) REFERENCES [dbo].[ref_shift] ([ref_shift_id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_employee_timesheet_employee]
    ON [dbo].[employee_timesheet]([employee_id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_employee_timesheet_ref_day_type]
    ON [dbo].[employee_timesheet]([ref_day_type_id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_employee_timesheet_ref_shift]
    ON [dbo].[employee_timesheet]([ref_shift_id] ASC);

