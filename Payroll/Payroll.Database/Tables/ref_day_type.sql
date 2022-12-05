CREATE TABLE [dbo].[ref_day_type] (
    [ref_day_type_id]      INT             IDENTITY (1, 1) NOT NULL,
    [date_type_code]       VARCHAR (50)    NULL,
    [date_type_name]       VARCHAR (50)    NULL,
    [ot_multiplier]        NUMERIC (18, 2) CONSTRAINT [DF_ref_day_type_ot_multiplier] DEFAULT ((0)) NOT NULL,
    [ot8_multiplier]       NUMERIC (18, 2) CONSTRAINT [DF_ref_day_type_ot8_multipplier] DEFAULT ((0)) NOT NULL,
    [nightdif_multiplier1] NUMERIC (18, 2) CONSTRAINT [DF_ref_day_type_nightdif_multiplier1] DEFAULT ((0)) NOT NULL,
    [nightdif_multiplier2] NUMERIC (18, 2) CONSTRAINT [DF_ref_day_type_nightdif_multiplier2] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ref_day_type] PRIMARY KEY CLUSTERED ([ref_day_type_id] ASC)
);



