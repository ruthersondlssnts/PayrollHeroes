CREATE TABLE [dbo].[employee] (
    [employee_id]          INT             IDENTITY (1, 1) NOT NULL,
    [employee_serial]      VARCHAR (20)    NULL,
    [username]             VARCHAR (50)    NULL,
    [password]             VARCHAR (50)    NULL,
    [last_name]            VARCHAR (200)   NULL,
    [first_name]           VARCHAR (200)   NULL,
    [middle_name]          VARCHAR (200)   NULL,
    [email_address]        VARCHAR (200)   NULL,
    [gender]               VARCHAR (10)    NULL,
    [contact_number]       VARCHAR (100)   NULL,
    [sss]                  VARCHAR (50)    NULL,
    [pagibig]              VARCHAR (50)    NULL,
    [philhealth]           VARCHAR (50)    NULL,
    [marital_status]       VARCHAR (20)    NULL,
    [date_hire]            DATETIME        NULL,
    [date_regular]         DATETIME        NULL,
    [date_resign]          DATETIME        NULL,
    [ref_employee_type_id] INT             CONSTRAINT [DF_employee_ref_employee_type_id] DEFAULT ((1)) NOT NULL,
    [ref_shift_id]         INT             NOT NULL,
    [ref_pay_type_id]      INT             CONSTRAINT [DF_employee_ref_pay_type_id] DEFAULT ((1)) NOT NULL,
    [role_id]              INT             CONSTRAINT [DF_employee_ref_role_id] DEFAULT ((1)) NOT NULL,
    [ref_department_id]    INT             NOT NULL,
    [fingerprint]          VARCHAR (50)    NULL,
    [basic_pay]            DECIMAL (18, 2) NULL,
    [date_deleted]         DATETIME        NULL,
    CONSTRAINT [PK_employees] PRIMARY KEY CLUSTERED ([employee_id] ASC),
    CONSTRAINT [FK_employee_employee] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[employee] ([employee_id]),
    CONSTRAINT [FK_employee_ref_department] FOREIGN KEY ([ref_department_id]) REFERENCES [dbo].[ref_department] ([ref_department_id]),
    CONSTRAINT [FK_employee_ref_employee_type] FOREIGN KEY ([ref_employee_type_id]) REFERENCES [dbo].[ref_employee_type] ([ref_employee_type_id]),
    CONSTRAINT [FK_employee_ref_pay_type] FOREIGN KEY ([ref_pay_type_id]) REFERENCES [dbo].[ref_pay_type] ([ref_pay_type_id]),
    CONSTRAINT [FK_employee_ref_shift] FOREIGN KEY ([ref_shift_id]) REFERENCES [dbo].[ref_shift] ([ref_shift_id]),
    CONSTRAINT [FK_employee_role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[role] ([role_id])
);




GO
CREATE NONCLUSTERED INDEX [IX_FK_employee_ref_department]
    ON [dbo].[employee]([ref_department_id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_employee_ref_shift]
    ON [dbo].[employee]([ref_shift_id] ASC);

