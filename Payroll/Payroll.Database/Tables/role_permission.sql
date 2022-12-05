CREATE TABLE [dbo].[role_permission] (
    [role_permission_id] INT IDENTITY (1, 1) NOT NULL,
    [role_id]            INT NOT NULL,
    [permission_id]      INT NOT NULL,
    [is_enable]          BIT NOT NULL,
    CONSTRAINT [PK_role_permission] PRIMARY KEY CLUSTERED ([role_permission_id] ASC),
    CONSTRAINT [FK_role_permission_permission] FOREIGN KEY ([permission_id]) REFERENCES [dbo].[permission] ([permission_id]),
    CONSTRAINT [FK_role_permission_role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[role] ([role_id])
);

