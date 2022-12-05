CREATE TABLE [dbo].[role_menu] (
    [role_menu_id]        INT           IDENTITY (1, 1) NOT NULL,
    [role_menu_parent_id] INT           CONSTRAINT [DF_role_menu_role_menu_parent_id] DEFAULT ((0)) NOT NULL,
    [role_id]             INT           NOT NULL,
    [display_name]        VARCHAR (100) NULL,
    [display_icon]        VARCHAR (100) NULL,
    [controller_name]     VARCHAR (100) NULL,
    [action_name]         VARCHAR (100) NULL,
    [ordering]            INT           NOT NULL,
    [is_enable]           BIT           CONSTRAINT [DF_role_menu_is_enable] DEFAULT ((1)) NOT NULL,
    [date_deleted]        DATETIME      NULL,
    CONSTRAINT [PK_role_menu] PRIMARY KEY CLUSTERED ([role_menu_id] ASC)
);

