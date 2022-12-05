CREATE TABLE [dbo].[ref_employee_type] (
    [ref_employee_type_id]   INT           IDENTITY (1, 1) NOT NULL,
    [ref_employee_type_name] VARCHAR (100) NULL,
    [date_deleted]           BIT           CONSTRAINT [DF_ref_employee_type_date_deleted] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ref_employee_type] PRIMARY KEY CLUSTERED ([ref_employee_type_id] ASC)
);

