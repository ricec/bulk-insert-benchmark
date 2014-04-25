USE [master]
GO

IF EXISTS(SELECT NULL FROM sys.databases WHERE name = 'BulkCopyTest')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'BulkCopyTest'
	ALTER DATABASE [BulkCopyTest] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [BulkCopyTest]
END
GO

IF EXISTS(SELECT NULL FROM sys.databases WHERE name = 'XmlInsertTest')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'XmlInsertTest'
	ALTER DATABASE [XmlInsertTest] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [XmlInsertTest]
END
GO

IF EXISTS(SELECT NULL FROM sys.databases WHERE name = 'StandardInsertTest')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'StandardInsertTest'
	ALTER DATABASE [StandardInsertTest] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [StandardInsertTest]
END
GO

CREATE DATABASE [BulkCopyTest]
CREATE DATABASE [XmlInsertTest]
CREATE DATABASE [StandardInsertTest]
GO

USE [BulkCopyTest]
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Depth] [int] NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [XmlInsertTest]
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Depth] [int] NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE PROCEDURE [dbo].[AddItems]
	@xml as XML
AS
BEGIN
	insert into Items(Name, Width, Height, Depth)
select
	T.c.value('./Name[1]','varchar(50)'),
	T.c.value('./Width[1]', 'int'),
	T.c.value('./Height[1]', 'int'),
	T.c.value('./Depth[1]', 'int')
from @XML.nodes('/ArrayOfItem/Item') t(c)
END
GO

USE [StandardInsertTest]
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Depth] [int] NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO