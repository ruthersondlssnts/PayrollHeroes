CREATE TABLE [dbo].[ref_department] (
    [ref_department_id] INT           IDENTITY (1, 1) NOT NULL,
    [department_name]   VARCHAR (100) NULL,
    CONSTRAINT [PK_ref_department] PRIMARY KEY CLUSTERED ([ref_department_id] ASC)
);

