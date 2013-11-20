CREATE TABLE [dbo].[Comments] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [PostId]  INT            NOT NULL,
    [Author]  NVARCHAR (255) NOT NULL,
    [Content] TEXT           NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comments_Posts] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([Id])
);

