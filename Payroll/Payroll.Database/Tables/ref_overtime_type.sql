CREATE TABLE [dbo].[ref_overtime_type] (
    [ref_overtime_type_id]   INT           IDENTITY (1, 1) NOT NULL,
    [ref_overtime_type_name] VARCHAR (100) NULL,
    CONSTRAINT [PK_ref_overtime_type] PRIMARY KEY CLUSTERED ([ref_overtime_type_id] ASC)
);

