CREATE TABLE [dbo].[ref_pagibig] (
    [ref_pagibig_id]        INT             IDENTITY (1, 1) NOT NULL,
    [name]                  VARCHAR (50)    NULL,
    [salary_from]           DECIMAL (18, 2) NULL,
    [salary_to]             DECIMAL (18, 2) NULL,
    [employee_contribution] DECIMAL (18, 2) NULL,
    [employer_contribution] DECIMAL (18, 2) NULL,
    [flat_rate]             BIT             NOT NULL,
    [date_deleted]          DATETIME        NULL,
    CONSTRAINT [PK_ref_pagibig] PRIMARY KEY CLUSTERED ([ref_pagibig_id] ASC)
);

