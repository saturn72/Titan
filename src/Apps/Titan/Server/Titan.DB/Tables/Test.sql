CREATE TABLE [dbo].[Test]
(
	[Id]			BIGINT			IDENTITY(1,1)	Primary Key	NOT NULL, 
	[CreatedOnUtc]	DATETIME2		NOT NULL,
	[UpdatedOnUtc]	DATETIME2		NULL,
	[DeletedOnUtc]	DATETIME2		NULL,
	[Deleted]		BIT				NOT NULL	
)
