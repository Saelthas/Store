USE [master]
GO
/****** Object:  Database [Store]    Script Date: 4/11/2022 01:01:34 ******/
CREATE DATABASE [Store]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Store', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Store.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Store_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Store_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Store] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Store].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Store] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Store] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Store] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Store] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Store] SET ARITHABORT OFF 
GO
ALTER DATABASE [Store] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Store] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Store] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Store] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Store] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Store] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Store] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Store] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Store] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Store] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Store] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Store] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Store] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Store] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Store] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Store] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Store] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Store] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Store] SET  MULTI_USER 
GO
ALTER DATABASE [Store] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Store] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Store] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Store] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Store] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Store] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Store] SET QUERY_STORE = OFF
GO
USE [Store]
GO
/****** Object:  Table [dbo].[ProductEntry]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductEntry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Supplier] [varchar](25) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductEntry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductEntryDetail]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductEntryDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdEntry] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IdProduct] [int] NOT NULL,
 CONSTRAINT [PK_ProductEntryDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Name] [varchar](25) NULL,
	[Description] [varchar](50) NOT NULL,
	[Stock] [int] NOT NULL,
	[Price] [decimal](12, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleDetail]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSale] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IdProduct] [int] NOT NULL,
 CONSTRAINT [PK_SaleDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubTotal] [decimal](15, 2) NOT NULL,
	[Tax] [decimal](7, 2) NOT NULL,
	[Total] [decimal](15, 2) NOT NULL,
	[Status] [nvarchar](1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductEntryDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductEntryDetail_ProductEntry] FOREIGN KEY([IdEntry])
REFERENCES [dbo].[ProductEntry] ([Id])
GO
ALTER TABLE [dbo].[ProductEntryDetail] CHECK CONSTRAINT [FK_ProductEntryDetail_ProductEntry]
GO
ALTER TABLE [dbo].[ProductEntryDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductEntryDetail_Products] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Products] ([id])
GO
ALTER TABLE [dbo].[ProductEntryDetail] CHECK CONSTRAINT [FK_ProductEntryDetail_Products]
GO
ALTER TABLE [dbo].[SaleDetail]  WITH CHECK ADD  CONSTRAINT [FK_SaleDetail_Products] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Products] ([id])
GO
ALTER TABLE [dbo].[SaleDetail] CHECK CONSTRAINT [FK_SaleDetail_Products]
GO
ALTER TABLE [dbo].[SaleDetail]  WITH CHECK ADD  CONSTRAINT [FK_SaleDetail_Sales] FOREIGN KEY([IdSale])
REFERENCES [dbo].[Sales] ([Id])
GO
ALTER TABLE [dbo].[SaleDetail] CHECK CONSTRAINT [FK_SaleDetail_Sales]
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Inserta un nuevo producto a la tabla productos.>
-- =============================================
CREATE PROCEDURE [dbo].[AddProduct](
@Code varchar(10),
@Name varchar(25),
@Description varchar(50),
--@Stock int,
@Price decimal(12,2)
)
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO [dbo].[Products]
           (   [Code]
			  ,[Name]
			  ,[Description]
			  ,[Stock]
			  ,[Price]
			  ,[CreatedDate]
			  ,[UpdateDate])
     VALUES
           (@Code
		   ,@Name
           ,@Description
		   ,0
		   ,@Price
           ,GETDATE()
		   ,GETDATE())
	SELECT @@IDENTITY AS [@@IDENTITY]
	SET NOCOUNT OFF

END
GO
/****** Object:  StoredProcedure [dbo].[AddSale]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Inserta un nuevo producto a la tabla productos.>
-- =============================================
CREATE   PROCEDURE [dbo].[AddSale](
@Subtotal decimal(15,2),
@Tax decimal(7,2),
@Total decimal(15,2)
)
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO [dbo].[Sales]
           ([SubTotal]
           ,[Tax]
           ,[Total]
           ,[Status]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           (@Subtotal
		   ,@Tax
           ,@Total
		   ,'P'
           ,GETDATE()
		   ,GETDATE())
	SELECT @@IDENTITY AS [@@IDENTITY]
	SET NOCOUNT OFF

END
GO
/****** Object:  StoredProcedure [dbo].[AddSaleDetail]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Inserta un nuevo detalle de venta a la tabla ventas.>
-- =============================================
CREATE   PROCEDURE [dbo].[AddSaleDetail](
@IdSale int,
@Quantity int,
@IdProduct int)
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO [dbo].[SaleDetail]
           ([IdSale]
           ,[Quantity]
           ,[IdProduct])
     VALUES
           (@IdSale
		   ,@Quantity
           ,@IdProduct)
	SELECT @@IDENTITY AS [@@IDENTITY]
	SET NOCOUNT OFF

END
GO
/****** Object:  StoredProcedure [dbo].[CreateDetailEntry]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,02-11-2022>
-- Description:	<Description,,Inserta un nuevo detalle entrada de compra de productos.>
-- =============================================
CREATE   PROCEDURE [dbo].[CreateDetailEntry](
@IdEntry int,
@Quantity int,
@IdProduct int)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[ProductEntryDetail]
           ([IdEntry]
           ,[Quantity]
           ,[IdProduct])
     VALUES
           (@IdEntry
           ,@Quantity
           ,@IdProduct);
   SELECT scope_identity() as [@@IDENTITY];
   	SET NOCOUNT OFF;
END
GO
/****** Object:  StoredProcedure [dbo].[CreateEntry]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,02-11-2022>
-- Description:	<Description,,Inserta una nueva entrada de compra de productos.>
-- =============================================
CREATE   PROCEDURE [dbo].[CreateEntry](
@Supplier varchar(25))
AS
BEGIN
	SET NOCOUNT ON;
   --DECLARE @ProductEntryId int;
   INSERT INTO [dbo].[ProductEntry]
           ([Supplier]
           ,[CreatedDate])
     VALUES
           (@Supplier
           ,GETDATE());
   SELECT scope_identity() as [@@IDENTITY];
   	SET NOCOUNT OFF;
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Elimina un producto x ID>
-- =============================================
CREATE   PROCEDURE [dbo].[DeleteProduct](@id int)
AS
BEGIN

	SET NOCOUNT ON;
	Delete
	from Products
	where id=@id;
	SELECT @@ROWCOUNT AS [@@ROWCOUNT]
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteSale]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Elimina una venta x ID>
-- =============================================
CREATE     PROCEDURE [dbo].[DeleteSale](@id int)
AS
BEGIN
DECLARE @ROWCOUNT INT
	SET NOCOUNT ON;
	
	Delete
	from Sales
	where id=@id;
	set @ROWCOUNT =(SELECT  @@ROWCOUNT )
	Delete
	from SaleDetail
	where IdSale=@id;
	select @ROWCOUNT as [@@ROWCOUNT]
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Obtiene todas los productos registrados>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllProducts]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Select [p].[id]
      ,[p].[Code]
      ,[p].[Name]
      ,[p].[Description]
      ,[p].[Stock]
      ,[p].[Price]
      ,[p].[CreatedDate]
      ,[p].[UpdateDate]
	from Products p
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllSales]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Obtiene todas las ventas registradas>
-- =============================================
CREATE   PROCEDURE [dbo].[GetAllSales]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [Id]
      ,[SubTotal]
      ,[Tax]
      ,[Total]
      ,[Status]
      ,[CreatedDate]
      ,[UpdatedDate]
	FROM [dbo].[Sales]
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetDetailSaleById]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Obtiene una venta x ID>
-- =============================================
Create   PROCEDURE [dbo].[GetDetailSaleById](@idSale int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [Id]
      ,[IdSale]
      ,[Quantity]
      ,[IdProduct]
  FROM [dbo].[SaleDetail]
	where IdSale=@idSale
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetProduct]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Obtiene un producto x ID>
-- =============================================
CREATE PROCEDURE [dbo].[GetProduct](@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Select 
		[p].[id]
      ,[p].[Code]
      ,[p].[Name]
      ,[p].[Description]
      ,[p].[Stock]
      ,[p].[Price]
      ,[p].[CreatedDate]
      ,[p].[UpdateDate]
	from Products p
	where id=@id
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[GetSaleById]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Obtiene una venta x ID>
-- =============================================
CREATE     PROCEDURE [dbo].[GetSaleById](@id int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [Id]
      ,[SubTotal]
      ,[Tax]
      ,[Total]
      ,[Status]
      ,[CreatedDate]
      ,[UpdatedDate]
  FROM [dbo].[Sales]
	where id=@id
	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Modifica un productos.>
-- =============================================
CREATE   PROCEDURE [dbo].[UpdateProduct](
@Id int,
@Code varchar(10),
@Name varchar(25),
@Description varchar(50),
--@Stock int,
@Price decimal(12,2))
AS
BEGIN

	SET NOCOUNT ON;
	update [dbo].[Products]
    Set 
	Code=@Code,
	Name=@Name,
	Description=@Description,
	UpdateDate=GETDATE(),
	Price=@Price
	where id=@Id
	SELECT @@ROWCOUNT AS [@@ROWCOUNT]
	SET NOCOUNT OFF

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStatusSale]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,>
-- Description:	<Description,, Modifica el estado de una venta.>
-- =============================================
CREATE     PROCEDURE [dbo].[UpdateStatusSale](
@Id int,
@Status varchar(1)
)
AS
BEGIN

	SET NOCOUNT ON;
	update [dbo].[Sales]
    Set 
	Status=@Status
	where id=@Id
	SELECT @@ROWCOUNT AS [@@ROWCOUNT]
	SET NOCOUNT OFF

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStockProduct]    Script Date: 4/11/2022 01:01:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ruben Aruzamen>
-- Create date: <Create Date,,02-11-2022>
-- Description:	<Description,, Modifica el campo stock de productos.>
-- =============================================
CREATE     PROCEDURE [dbo].[UpdateStockProduct](
@Id int,
@Stock int)
AS
BEGIN

	SET NOCOUNT ON;
	update [dbo].[Products]
    Set 
	Stock=Stock+@Stock
	where id=@Id
	SELECT @@ROWCOUNT AS [@@ROWCOUNT]
	SET NOCOUNT OFF

END
GO
USE [master]
GO
ALTER DATABASE [Store] SET  READ_WRITE 
GO
