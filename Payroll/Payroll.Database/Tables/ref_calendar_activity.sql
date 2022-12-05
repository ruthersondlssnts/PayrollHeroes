CREATE TABLE [dbo].[ref_calendar_activity] (
    [ref_calendar_activity_id] INT           IDENTITY (1, 1) NOT NULL,
    [work_date]                DATETIME      NOT NULL,
    [ref_day_type_id]          INT           NOT NULL,
    [description]              VARCHAR (200) NULL,
    [date_deleted]             DATETIME      NULL,
    CONSTRAINT [PK_ref_calendar_activity] PRIMARY KEY CLUSTERED ([ref_calendar_activity_id] ASC),
    CONSTRAINT [FK_ref_calendar_activity_ref_day_type] FOREIGN KEY ([ref_day_type_id]) REFERENCES [dbo].[ref_day_type] ([ref_day_type_id])
);

