USE [master]
GO
/****** Object:  Database [BaseConocimiento]    Script Date: 6/2/2017 12:56:07 AM ******/
CREATE DATABASE [BaseConocimiento] ON  PRIMARY 
( NAME = N'BaseConocimiento', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\BaseConocimiento.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BaseConocimiento_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\BaseConocimiento_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BaseConocimiento] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BaseConocimiento].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BaseConocimiento] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BaseConocimiento] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BaseConocimiento] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BaseConocimiento] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BaseConocimiento] SET ARITHABORT OFF 
GO
ALTER DATABASE [BaseConocimiento] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BaseConocimiento] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BaseConocimiento] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BaseConocimiento] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BaseConocimiento] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BaseConocimiento] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BaseConocimiento] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BaseConocimiento] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BaseConocimiento] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BaseConocimiento] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BaseConocimiento] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BaseConocimiento] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BaseConocimiento] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BaseConocimiento] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BaseConocimiento] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BaseConocimiento] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BaseConocimiento] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BaseConocimiento] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BaseConocimiento] SET  MULTI_USER 
GO
ALTER DATABASE [BaseConocimiento] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BaseConocimiento] SET DB_CHAINING OFF 
GO
USE [BaseConocimiento]
GO
/****** Object:  Table [dbo].[acomulador]    Script Date: 6/2/2017 12:56:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[acomulador](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_palabras] [int] NOT NULL,
	[id_base_conocimiento] [int] NOT NULL,
 CONSTRAINT [PK_acomulador] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[baseconocimiento]    Script Date: 6/2/2017 12:56:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[baseconocimiento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[palabra] [nchar](50) NOT NULL,
	[peso] [int] NOT NULL,
 CONSTRAINT [PK_baseconocimiento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[palabras]    Script Date: 6/2/2017 12:56:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[palabras](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[palabra] [nchar](100) NOT NULL,
 CONSTRAINT [PK_palabras] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblUsuarios]    Script Date: 6/2/2017 12:56:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblUsuarios](
	[Id_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [nvarchar](100) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TblUsuarios] PRIMARY KEY CLUSTERED 
(
	[Id_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[acomulador]  WITH CHECK ADD  CONSTRAINT [FK_acomulador_base_conocimiento] FOREIGN KEY([id_base_conocimiento])
REFERENCES [dbo].[baseconocimiento] ([id])
GO
ALTER TABLE [dbo].[acomulador] CHECK CONSTRAINT [FK_acomulador_base_conocimiento]
GO
ALTER TABLE [dbo].[acomulador]  WITH CHECK ADD  CONSTRAINT [FK_acomulador_palabras] FOREIGN KEY([id_palabras])
REFERENCES [dbo].[palabras] ([id])
GO
ALTER TABLE [dbo].[acomulador] CHECK CONSTRAINT [FK_acomulador_palabras]
GO
USE [master]
GO
ALTER DATABASE [BaseConocimiento] SET  READ_WRITE 
GO
