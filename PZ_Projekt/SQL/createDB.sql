USE [master]
GO

/****** Object:  Database [PZ_Projekt]    Script Date: 11.01.2020 11:25:24 ******/
DROP DATABASE [PZ_Projekt]
GO

/****** Object:  Database [PZ_Projekt]    Script Date: 11.01.2020 11:25:24 ******/
CREATE DATABASE [PZ_Projekt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PZ_Projekt', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PZ_Projekt.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PZ_Projekt_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PZ_Projekt_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PZ_Projekt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [PZ_Projekt] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PZ_Projekt] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PZ_Projekt] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PZ_Projekt] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PZ_Projekt] SET ARITHABORT OFF 
GO

ALTER DATABASE [PZ_Projekt] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [PZ_Projekt] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PZ_Projekt] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PZ_Projekt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PZ_Projekt] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PZ_Projekt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PZ_Projekt] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PZ_Projekt] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PZ_Projekt] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PZ_Projekt] SET  DISABLE_BROKER 
GO

ALTER DATABASE [PZ_Projekt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PZ_Projekt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PZ_Projekt] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PZ_Projekt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PZ_Projekt] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PZ_Projekt] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [PZ_Projekt] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PZ_Projekt] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [PZ_Projekt] SET  MULTI_USER 
GO

ALTER DATABASE [PZ_Projekt] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [PZ_Projekt] SET DB_CHAINING OFF 
GO

ALTER DATABASE [PZ_Projekt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [PZ_Projekt] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [PZ_Projekt] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [PZ_Projekt] SET QUERY_STORE = OFF
GO

ALTER DATABASE [PZ_Projekt] SET  READ_WRITE 
GO

