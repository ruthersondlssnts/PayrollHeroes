CREATE TABLE [dbo].[ref_shift_detail] (
    [ref_shift_detail_id] INT             IDENTITY (1, 1) NOT NULL,
    [ref_shift_id]        INT             NOT NULL,
    [day]                 VARCHAR (10)    NULL,
    [required_hour]       DECIMAL (18, 2) NULL,
    [rest_day]            BIT             NOT NULL,
    CONSTRAINT [PK_ref_shift_detail] PRIMARY KEY CLUSTERED ([ref_shift_detail_id] ASC),
    CONSTRAINT [FK_ref_shift_detail_ref_shift] FOREIGN KEY ([ref_shift_id]) REFERENCES [dbo].[ref_shift] ([ref_shift_id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ref_shift_detail_ref_shift]
    ON [dbo].[ref_shift_detail]([ref_shift_id] ASC);

