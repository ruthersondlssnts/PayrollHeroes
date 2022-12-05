CREATE TABLE [dbo].[payroll_config] (
    [payroll_config_id] INT          IDENTITY (1, 1) NOT NULL,
    [config_name]       VARCHAR (50) NULL,
    [config_value]      INT          CONSTRAINT [DF_payroll_config_config_value] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_payroll_config] PRIMARY KEY CLUSTERED ([payroll_config_id] ASC)
);

