CREATE TABLE [dbo].[permission] (
    [permission_id]   INT          IDENTITY (1, 1) NOT NULL,
    [permission_name] VARCHAR (50) NULL,
    [permission_code] VARCHAR (50) NULL,
    [is_enable]       BIT          NOT NULL,
    CONSTRAINT [PK_permission] PRIMARY KEY CLUSTERED ([permission_id] ASC)
);

