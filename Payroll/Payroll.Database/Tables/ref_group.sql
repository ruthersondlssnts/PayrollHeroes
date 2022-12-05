CREATE TABLE [dbo].[ref_group] (
    [group_id]     INT                 IDENTITY (1, 1) NOT NULL,
    [name]         NVARCHAR (256)      NOT NULL,
    [level]        [sys].[hierarchyid] NULL,
    [date_deleted] DATETIME            NULL,
    [is_enable]    BIT                 CONSTRAINT [DF_ref_group_is_enable_1] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_group_id] PRIMARY KEY CLUSTERED ([group_id] ASC),
    CONSTRAINT [LevelUnique] UNIQUE NONCLUSTERED ([level] ASC)
);

