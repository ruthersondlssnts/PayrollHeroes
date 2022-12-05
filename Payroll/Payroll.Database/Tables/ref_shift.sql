CREATE TABLE [dbo].[ref_shift] (
    [ref_shift_id]         INT             IDENTITY (1, 1) NOT NULL,
    [shift_name]           VARCHAR (50)    NULL,
    [shift_in]             VARCHAR (5)     NOT NULL,
    [shift_out]            VARCHAR (5)     NOT NULL,
    [break_in]             VARCHAR (5)     NOT NULL,
    [break_out]            VARCHAR (5)     NOT NULL,
    [break_hour]           DECIMAL (18, 2) NULL,
    [required_hour]        DECIMAL (18, 2) NULL,
    [nd_start]             VARCHAR (5)     NULL,
    [nd_end]               VARCHAR (5)     NULL,
    [nd_start2]            VARCHAR (5)     NULL,
    [nd_end2]              VARCHAR (5)     NULL,
    [grace_period]         DECIMAL (18, 2) CONSTRAINT [DF_ref_shift_grace_period] DEFAULT ((0)) NOT NULL,
    [include_grace_period] BIT             CONSTRAINT [DF_ref_shift_include_grace_period] DEFAULT ((0)) NOT NULL,
    [is_auto_ot]           BIT             CONSTRAINT [DF_ref_shift_is_ot] DEFAULT ((1)) NOT NULL,
    [is_nd]                BIT             CONSTRAINT [DF_ref_shift_is_nd] DEFAULT ((0)) NOT NULL,
    [date_deleted]         DATETIME        NULL,
    CONSTRAINT [PK_ref_shift] PRIMARY KEY CLUSTERED ([ref_shift_id] ASC)
);

