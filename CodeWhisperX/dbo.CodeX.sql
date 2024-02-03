CREATE TABLE [dbo].[CodeX] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CodeQuestion] NVARCHAR (MAX) NULL,
    [CodeAnswer]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_CodeX] PRIMARY KEY CLUSTERED ([Id] ASC)
);

