CREATE TABLE [dbo].[Posts] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Author]  NVARCHAR (255) NOT NULL,
    [Content] TEXT           NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

