CREATE TABLE [dbo].[employee_group] (
    [employee_id]    INT NOT NULL,
    [group_id]       INT NOT NULL,
    [approver_all]   BIT CONSTRAINT [DF_employee_group_approver_all] DEFAULT ((0)) NOT NULL,
    [ot_approver]    BIT CONSTRAINT [DF_employee_group_ot_approver] DEFAULT ((0)) NOT NULL,
    [ob_approver]    BIT CONSTRAINT [DF_employee_group_ob_approver] DEFAULT ((0)) NOT NULL,
    [leave_approver] BIT CONSTRAINT [DF_employee_group_leave_approver] DEFAULT ((0)) NOT NULL,
    [dtr_approver]   BIT CONSTRAINT [DF_employee_group_dtr_approver] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_employee_group] PRIMARY KEY CLUSTERED ([employee_id] ASC),
    CONSTRAINT [FK_employee_group_employee] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[employee] ([employee_id]),
    CONSTRAINT [FK_employee_group_ref_group] FOREIGN KEY ([group_id]) REFERENCES [dbo].[ref_group] ([group_id])
);

