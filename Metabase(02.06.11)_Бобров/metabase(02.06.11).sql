USE [master]
GO
/****** Object:  Database [MetaBase]    Script Date: 06/02/2011 22:49:29 ******/
CREATE DATABASE [MetaBase] ON  PRIMARY 
( NAME = N'MetaBase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\MetaBase.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MetaBase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\MetaBase_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MetaBase] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MetaBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MetaBase] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [MetaBase] SET ANSI_NULLS OFF
GO
ALTER DATABASE [MetaBase] SET ANSI_PADDING OFF
GO
ALTER DATABASE [MetaBase] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [MetaBase] SET ARITHABORT OFF
GO
ALTER DATABASE [MetaBase] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [MetaBase] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [MetaBase] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [MetaBase] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [MetaBase] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [MetaBase] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [MetaBase] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [MetaBase] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [MetaBase] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [MetaBase] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [MetaBase] SET  DISABLE_BROKER
GO
ALTER DATABASE [MetaBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [MetaBase] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [MetaBase] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [MetaBase] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [MetaBase] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [MetaBase] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [MetaBase] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [MetaBase] SET  READ_WRITE
GO
ALTER DATABASE [MetaBase] SET RECOVERY FULL
GO
ALTER DATABASE [MetaBase] SET  MULTI_USER
GO
ALTER DATABASE [MetaBase] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [MetaBase] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'MetaBase', N'ON'
GO
USE [MetaBase]
GO
/****** Object:  Table [dbo].[TStrValues]    Script Date: 06/02/2011 22:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TStrValues](
	[metaobject_id] [bigint] NOT NULL,
	[atr_id] [bigint] NOT NULL,
	[value] [nchar](1000) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TMetaObjectTypes]    Script Date: 06/02/2011 22:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TMetaObjectTypes](
	[id_type] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
 CONSTRAINT [PK_TMetaObjectTypes] PRIMARY KEY CLUSTERED 
(
	[id_type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TListValues]    Script Date: 06/02/2011 22:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TListValues](
	[metaobject_id] [bigint] NOT NULL,
	[atr_id] [bigint] NOT NULL,
	[value] [binary](500) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TIdValues]    Script Date: 06/02/2011 22:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIdValues](
	[metaobject_id] [bigint] NOT NULL,
	[atr_id] [bigint] NOT NULL,
	[value] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TFloatValues]    Script Date: 06/02/2011 22:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TFloatValues](
	[metaobject_id] [bigint] NOT NULL,
	[atr_id] [bigint] NOT NULL,
	[value] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBinValues]    Script Date: 06/02/2011 22:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBinValues](
	[metaobject_id] [bigint] NOT NULL,
	[atr_id] [bigint] NOT NULL,
	[value] [binary](500) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBigintValues]    Script Date: 06/02/2011 22:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBigintValues](
	[metaobject_id] [bigint] NOT NULL,
	[atr_id] [bigint] NOT NULL,
	[value] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAttributes]    Script Date: 06/02/2011 22:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAttributes](
	[id_atr] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
	[atr_type] [nchar](50) NOT NULL,
 CONSTRAINT [PK_TAttributes] PRIMARY KEY CLUSTERED 
(
	[id_atr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAtr_Type]    Script Date: 06/02/2011 22:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAtr_Type](
	[attr_id] [bigint] NOT NULL,
	[type_id] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SaveValue]    Script Date: 06/02/2011 22:49:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SaveValue]
	@metaObj_id bigint,
	@attr_id bigint,
	@value	sql_variant
AS
BEGIN
	DECLARE @attrType	nchar(20)
	--получаем имя типа атрибута
	SET @attrType=(SELECT atr_type FROM TAttributes WHERE id_atr=@attr_id)
	/*создаем у объекта атрибут значения NULL*/
	
	if(@attrType='String')
	begin
		UPDATE TStrValues 
		SET value=cast(value as varchar(MAX)) 
		WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 
	else if(@attrType='Bigint')
	begin
		UPDATE TBigintValues SET value=cast(value as bigint) WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 
	else if(@attrType='Double')
	begin
		UPDATE TFloatValues SET value=cast(value as float) WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 
	else if(@attrType='List')
	begin
		UPDATE TListValues SET value=cast(value as binary(500)) WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 
	else if(@attrType='Id')
	begin
		UPDATE TIdValues SET value=cast(value as bigint) WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end		
	else if(@attrType='Binary')
	begin
		UPDATE TBinValues SET value=cast(value as binary) WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 

END
GO
/****** Object:  StoredProcedure [dbo].[LoadValue]    Script Date: 06/02/2011 22:49:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LoadValue]
	@metaObj_id bigint,
	@attrName	nchar(50)
AS
BEGIN
	DECLARE @attr_id bigint,
			@attrType	nchar(20)
	/*получаем id атрибута*/
	SET @attr_id=(SELECT id_atr FROM TAttributes WHERE name=@attrName)
	--получаем имя типа атрибута
	SET @attrType=(SELECT atr_type FROM TAttributes WHERE name=@attrName)
	/*создаем у объекта атрибут значения NULL*/
	
	if(@attrType='String')
	begin
		SELECT value,@attr_id FROM TStrValues WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 
	else if(@attrType='Bigint')
	begin
		SELECT value,@attr_id FROM TBigintValues WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 
	else if(@attrType='Double')
	begin
		SELECT value,@attr_id FROM TFloatValues WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 
	else if(@attrType='List')
	begin
		SELECT value,@attr_id FROM TListValues WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 
	else if(@attrType='Id')
	begin
		SELECT value,@attr_id FROM TIdValues WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 
	else if(@attrType='Binary')
	begin
		SELECT value,@attr_id FROM TBinValues WHERE metaobject_id=@metaObj_id and atr_id=@attr_id
	end 

END
GO
/****** Object:  StoredProcedure [dbo].[CreateValue]    Script Date: 06/02/2011 22:49:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateValue]
	@metaObj_id bigint,
	@attrName	nchar(50)
	
AS
BEGIN
	DECLARE @attr_id bigint,
			@attrType	nchar(20)
	/*получаем id атрибута*/
	SET @attr_id=(SELECT id_atr FROM TAttributes WHERE name=@attrName)
	--получаем имя типа атрибута
	SET @attrType=(SELECT atr_type FROM TAttributes WHERE name=@attrName)
	/*создаем у объекта атрибут значения NULL*/
	
	if(@attrType='String')
	begin
		INSERT INTO TStrValues(metaobject_id,atr_id,value)
		VALUES(@metaObj_id,@attr_id,'')
	end 
	else if(@attrType='Bigint')
	begin
		INSERT INTO TBigintValues(metaobject_id,atr_id,value)
		VALUES(@metaObj_id,@attr_id,0)
	end 
	else if(@attrType='Double')
	begin
		INSERT INTO TFloatValues(metaobject_id,atr_id,value)
		VALUES(@metaObj_id,@attr_id,0)
	end 
	else if(@attrType='List')
	begin
		INSERT INTO TListValues(metaobject_id,atr_id,value)
		VALUES(@metaObj_id,@attr_id,0)
	end 
	else if(@attrType='Id')
	begin
		INSERT INTO TIdValues(metaobject_id,atr_id,value)
		VALUES(@metaObj_id,@attr_id,0)
	end 
	else if(@attrType='Binary')
	begin
		INSERT INTO TBinValues(metaobject_id,atr_id,value)
		VALUES(@metaObj_id,@attr_id,0)
	end 
	SELECT @attr_id
END
GO
/****** Object:  Table [dbo].[TMetaObjects]    Script Date: 06/02/2011 22:49:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TMetaObjects](
	[id_metaobject] [bigint] NOT NULL,
	[type_id] [bigint] NOT NULL,
	[stridentifier] [nchar](50) NULL,
 CONSTRAINT [PK_TMetaObjects] PRIMARY KEY CLUSTERED 
(
	[id_metaobject] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddNewType]    Script Date: 06/02/2011 22:49:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddNewType]
	@typeName nchar(50)
AS
BEGIN
	if not exists (SELECT * FROM TMetaObjectTypes t WHERE t.name=@typeName)
	begin
		INSERT INTO TMetaObjectTypes(name) 
		VALUES(@typeName)
	end
END
GO
/****** Object:  StoredProcedure [dbo].[AddNewAttr]    Script Date: 06/02/2011 22:49:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddNewAttr]
	@attrName nchar(50),
	@attrType nchar(50),
	@typeName nchar(50)
AS
BEGIN
	DECLARE @typeId bigint,
			@attrId bigint
	/*если нет такого атрибута, то добавляем его*/
	if not exists(SELECT * FROM TAttributes WHERE name=@attrName)
	begin
		INSERT INTO TAttributes (name,atr_type)
			VALUES(@attrName,@attrType)
	end
	
	/*получаем id типа*/
	SET @typeId=(SELECT id_type FROM TMetaObjectTypes WHERE name=@typeName)
	/*получаем id атрибута*/
	SET @attrId=(SELECT id_atr FROM TAttributes WHERE name=@attrName)
	/*добавляем атрибут к типу*/
	INSERT INTO TAtr_Type(type_id,attr_id)
	VALUES (@typeId,@attrId)
END
GO
/****** Object:  StoredProcedure [dbo].[CreateNewMetaObj]    Script Date: 06/02/2011 22:49:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateNewMetaObj]
	@MOTypeName nchar(50),--имя типа метаобъекта
	@StrIdentifier nchar(50)--строковый идентификатор
AS
BEGIN
	DECLARE @id bigint,
			@typeId bigint
	if(@StrIdentifier!='')
	begin
		--проверяем, нет ли в базе уже метаобъктов с таким строковым идентификатором
		--если есть, ничего не создаем
		if exists (SELECT * FROM TMetaObjects WHERE stridentifier=@StrIdentifier)
		begin
			SELECT -1
			return
		end
	end
	--получаем свободный id
	SET @id=( SELECT max(id_metaobject) FROM MetaBase.dbo.TMetaObjects)
	if @id is null 
		SET @id=0
	SET @id=@id+1
	--получаем id типа метаобъекта
	SET @typeId=(SELECT id_type FROM TMetaObjectTypes WHERE name=@MOTypeName)
	--создаем новую записть о метаобъекте
	INSERT INTO TMetaObjects(id_metaobject,type_id,stridentifier) VALUES(@id,@typeId,@StrIdentifier)
	--возвращаем id нового метаобъекта
	SELECT @id
	return 
END
GO
/****** Object:  Default [DF_TIdValues_value]    Script Date: 06/02/2011 22:49:32 ******/
ALTER TABLE [dbo].[TIdValues] ADD  CONSTRAINT [DF_TIdValues_value]  DEFAULT ((0)) FOR [value]
GO
/****** Object:  Default [DF_TFloatValues_value]    Script Date: 06/02/2011 22:49:32 ******/
ALTER TABLE [dbo].[TFloatValues] ADD  CONSTRAINT [DF_TFloatValues_value]  DEFAULT ((0.0)) FOR [value]
GO
/****** Object:  Default [DF_TBigintValues_value]    Script Date: 06/02/2011 22:49:32 ******/
ALTER TABLE [dbo].[TBigintValues] ADD  CONSTRAINT [DF_TBigintValues_value]  DEFAULT ((0)) FOR [value]
GO
/****** Object:  ForeignKey [FK_TAtr_Type_TAttributes]    Script Date: 06/02/2011 22:49:32 ******/
ALTER TABLE [dbo].[TAtr_Type]  WITH CHECK ADD  CONSTRAINT [FK_TAtr_Type_TAttributes] FOREIGN KEY([attr_id])
REFERENCES [dbo].[TAttributes] ([id_atr])
GO
ALTER TABLE [dbo].[TAtr_Type] CHECK CONSTRAINT [FK_TAtr_Type_TAttributes]
GO
/****** Object:  ForeignKey [FK_TAtr_Type_TMetaObjectTypes]    Script Date: 06/02/2011 22:49:32 ******/
ALTER TABLE [dbo].[TAtr_Type]  WITH CHECK ADD  CONSTRAINT [FK_TAtr_Type_TMetaObjectTypes] FOREIGN KEY([type_id])
REFERENCES [dbo].[TMetaObjectTypes] ([id_type])
GO
ALTER TABLE [dbo].[TAtr_Type] CHECK CONSTRAINT [FK_TAtr_Type_TMetaObjectTypes]
GO
/****** Object:  ForeignKey [FK_TMetaObjects_TMetaObjectTypes]    Script Date: 06/02/2011 22:49:41 ******/
ALTER TABLE [dbo].[TMetaObjects]  WITH CHECK ADD  CONSTRAINT [FK_TMetaObjects_TMetaObjectTypes] FOREIGN KEY([type_id])
REFERENCES [dbo].[TMetaObjectTypes] ([id_type])
GO
ALTER TABLE [dbo].[TMetaObjects] CHECK CONSTRAINT [FK_TMetaObjects_TMetaObjectTypes]
GO
