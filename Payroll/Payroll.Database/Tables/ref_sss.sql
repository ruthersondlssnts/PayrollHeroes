CREATE TABLE [dbo].[ref_sss] (
    [ref_sss_id]            INT             IDENTITY (1, 1) NOT NULL,
    [name]                  VARCHAR (50)    NULL,
    [salary_from]           DECIMAL (18, 2) NULL,
    [salary_to]             DECIMAL (18, 2) NULL,
    [monthly_salary_credit] DECIMAL (18, 2) NULL,
    [employee_share]        DECIMAL (18, 2) NULL,
    [employer_share]        DECIMAL (18, 2) NULL,
    [date_deleted]          DATETIME        NULL,
    CONSTRAINT [PK_ref_sss] PRIMARY KEY CLUSTERED ([ref_sss_id] ASC)
);

