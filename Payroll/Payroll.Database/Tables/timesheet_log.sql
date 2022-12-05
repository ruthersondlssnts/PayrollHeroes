CREATE TABLE [dbo].[timesheet_log] (
    [timesheet_log_id] BIGINT       NOT NULL,
    [employee_serial]  VARCHAR (20) NULL,
    [datetime_in]      DATETIME     NULL,
    [datetime_out]     DATETIME     NULL,
    CONSTRAINT [PK_timesheet_log] PRIMARY KEY CLUSTERED ([timesheet_log_id] ASC)
);

