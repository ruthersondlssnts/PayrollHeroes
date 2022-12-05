CREATE TABLE [dbo].[ref_status] (
    [ref_status_id]   INT          IDENTITY (1, 1) NOT NULL,
    [ref_status_name] VARCHAR (50) NULL,
    CONSTRAINT [PK_ref_status] PRIMARY KEY CLUSTERED ([ref_status_id] ASC)
);

