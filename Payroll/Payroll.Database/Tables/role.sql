CREATE TABLE [dbo].[role] (
    [role_id]      INT          IDENTITY (1, 1) NOT NULL,
    [role_name]    VARCHAR (50) NULL,
    [date_deleted] DATETIME     NULL,
    CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED ([role_id] ASC),
    CONSTRAINT [FK_role_role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[role] ([role_id])
);

