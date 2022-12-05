CREATE TABLE [dbo].[employee_role] (
    [employee_role_id] INT IDENTITY (1, 1) NOT NULL,
    [employee_id]      INT NOT NULL,
    [role_id]          INT NOT NULL,
    CONSTRAINT [PK_employee_role] PRIMARY KEY CLUSTERED ([employee_role_id] ASC)
);

