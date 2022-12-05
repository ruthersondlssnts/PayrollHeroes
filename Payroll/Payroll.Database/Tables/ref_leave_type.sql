CREATE TABLE [dbo].[ref_leave_type] (
    [ref_leave_type_id]   INT           IDENTITY (1, 1) NOT NULL,
    [ref_leave_type_name] VARCHAR (100) NULL,
    [ref_leave_type_code] VARCHAR (50)  NULL,
    [with_pay]            BIT           CONSTRAINT [DF_ref_leave_type_with_pay] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ref_leave_type] PRIMARY KEY CLUSTERED ([ref_leave_type_id] ASC)
);

