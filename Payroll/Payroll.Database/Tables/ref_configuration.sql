CREATE TABLE [dbo].[ref_configuration] (
    [config_id] INT          IDENTITY (1, 1) NOT NULL,
    [ptext]     VARCHAR (50) NOT NULL,
    [pvalue]    VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ref_configuration] PRIMARY KEY CLUSTERED ([config_id] ASC)
);

