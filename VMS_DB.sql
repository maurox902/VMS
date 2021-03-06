SET NOCOUNT ON
GO
USE MASTER
GO
IF EXISTS (SELECT * FROM sysdatabases WHERE NAME='VMS_DB')
		DROP DATABASE VMS_DB
GO
DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(FILENAME, 1, CHARINDEX(N'master.mdf', LOWER(FILENAME)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE VMS_DB
  ON PRIMARY (NAME = N''VMS_DB'', FILENAME = N''' + @device_directory + N'vms_db.mdf'')
  LOG ON (NAME = N''VMS_DB_log'',  FILENAME = N''' + @device_directory + N'vms_db.ldf'')')
GO
ALTER DATABASE [VMS_DB] SET COMPATIBILITY_LEVEL = 140
GO
USE [VMS_DB]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type in (N'U'))
ALTER TABLE [dbo].[users] DROP CONSTRAINT IF EXISTS [FK_users_role]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[seat]') AND type in (N'U'))
ALTER TABLE [dbo].[seat] DROP CONSTRAINT IF EXISTS [FK_seat_types]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[seat]') AND type in (N'U'))
ALTER TABLE [dbo].[seat] DROP CONSTRAINT IF EXISTS [FK_seat_module]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[reservation]') AND type in (N'U'))
ALTER TABLE [dbo].[reservation] DROP CONSTRAINT IF EXISTS [FK_reservation_users]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[reservation]') AND type in (N'U'))
ALTER TABLE [dbo].[reservation] DROP CONSTRAINT IF EXISTS [FK_reservation_seat]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[reservation]') AND type in (N'U'))
ALTER TABLE [dbo].[reservation] DROP CONSTRAINT IF EXISTS [FK_reservation_schedule]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[reservation]') AND type in (N'U'))
ALTER TABLE [dbo].[reservation] DROP CONSTRAINT IF EXISTS [FK_reservation_reservation_types]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[preferences]') AND type in (N'U'))
ALTER TABLE [dbo].[preferences] DROP CONSTRAINT IF EXISTS [FK_preferences_users]
GO
/****** Object:  Index [UK_username]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP INDEX IF EXISTS [UK_username] ON [dbo].[users]
GO
/****** Object:  Index [UK_reservation_date]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP INDEX IF EXISTS [UK_reservation_date] ON [dbo].[reservation]
GO
/****** Object:  Table [dbo].[users]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP TABLE IF EXISTS [dbo].[users]
GO
/****** Object:  Table [dbo].[seat_types]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP TABLE IF EXISTS [dbo].[seat_types]
GO
/****** Object:  Table [dbo].[seat]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP TABLE IF EXISTS [dbo].[seat]
GO
/****** Object:  Table [dbo].[schedule]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP TABLE IF EXISTS [dbo].[schedule]
GO
/****** Object:  Table [dbo].[role]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP TABLE IF EXISTS [dbo].[role]
GO
/****** Object:  Table [dbo].[reservation_types]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP TABLE IF EXISTS [dbo].[reservation_types]
GO
/****** Object:  Table [dbo].[reservation]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP TABLE IF EXISTS [dbo].[reservation]
GO
/****** Object:  Table [dbo].[preferences]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP TABLE IF EXISTS [dbo].[preferences]
GO
/****** Object:  Table [dbo].[module]    Script Date: 6/6/2022 11:35:22 AM ******/
DROP TABLE IF EXISTS [dbo].[module]
GO
/****** Object:  Table [dbo].[module]    Script Date: 6/6/2022 11:35:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[module]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[module](
	[module_id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_module] PRIMARY KEY CLUSTERED 
(
	[module_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[preferences]    Script Date: 6/6/2022 11:35:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[preferences]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[preferences](
	[preference_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[detail] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_preferences] PRIMARY KEY CLUSTERED 
(
	[preference_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[reservation]    Script Date: 6/6/2022 11:35:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[reservation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[reservation](
	[reservaction_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[status] [int] NOT NULL,
	[reservation_date] [date] NOT NULL,
	[schedule_id] [int] NOT NULL,
	[seat_id] [int] NOT NULL,
	[reservation_type_id] [int] NOT NULL,
	[detail] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_reservation] PRIMARY KEY CLUSTERED 
(
	[reservaction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[reservation_types]    Script Date: 6/6/2022 11:35:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[reservation_types]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[reservation_types](
	[reservation_type_id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_reservation_types] PRIMARY KEY CLUSTERED 
(
	[reservation_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[role]    Script Date: 6/6/2022 11:35:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[role]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[role](
	[rol_id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[rol_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[schedule]    Script Date: 6/6/2022 11:35:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[schedule]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[schedule](
	[schedule_id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_schedule] PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[seat]    Script Date: 6/6/2022 11:35:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[seat]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[seat](
	[seat_id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
	[module_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_seat] PRIMARY KEY CLUSTERED 
(
	[seat_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[seat_types]    Script Date: 6/6/2022 11:35:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[seat_types]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[seat_types](
	[type_id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_types] PRIMARY KEY CLUSTERED 
(
	[type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[users]    Script Date: 6/6/2022 11:35:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](100) NOT NULL,
	[password] [nvarchar](200) NOT NULL,
	[role_id] [int] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [UK_reservation_date]    Script Date: 6/6/2022 11:35:23 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[reservation]') AND name = N'UK_reservation_date')
CREATE UNIQUE NONCLUSTERED INDEX [UK_reservation_date] ON [dbo].[reservation]
(
	[reservation_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_username]    Script Date: 6/6/2022 11:35:23 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND name = N'UK_username')
CREATE UNIQUE NONCLUSTERED INDEX [UK_username] ON [dbo].[users]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_preferences_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[preferences]'))
ALTER TABLE [dbo].[preferences]  WITH CHECK ADD  CONSTRAINT [FK_preferences_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_preferences_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[preferences]'))
ALTER TABLE [dbo].[preferences] CHECK CONSTRAINT [FK_preferences_users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_reservation_reservation_types]') AND parent_object_id = OBJECT_ID(N'[dbo].[reservation]'))
ALTER TABLE [dbo].[reservation]  WITH CHECK ADD  CONSTRAINT [FK_reservation_reservation_types] FOREIGN KEY([reservation_type_id])
REFERENCES [dbo].[reservation_types] ([reservation_type_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_reservation_reservation_types]') AND parent_object_id = OBJECT_ID(N'[dbo].[reservation]'))
ALTER TABLE [dbo].[reservation] CHECK CONSTRAINT [FK_reservation_reservation_types]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_reservation_schedule]') AND parent_object_id = OBJECT_ID(N'[dbo].[reservation]'))
ALTER TABLE [dbo].[reservation]  WITH CHECK ADD  CONSTRAINT [FK_reservation_schedule] FOREIGN KEY([schedule_id])
REFERENCES [dbo].[schedule] ([schedule_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_reservation_schedule]') AND parent_object_id = OBJECT_ID(N'[dbo].[reservation]'))
ALTER TABLE [dbo].[reservation] CHECK CONSTRAINT [FK_reservation_schedule]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_reservation_seat]') AND parent_object_id = OBJECT_ID(N'[dbo].[reservation]'))
ALTER TABLE [dbo].[reservation]  WITH CHECK ADD  CONSTRAINT [FK_reservation_seat] FOREIGN KEY([seat_id])
REFERENCES [dbo].[seat] ([seat_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_reservation_seat]') AND parent_object_id = OBJECT_ID(N'[dbo].[reservation]'))
ALTER TABLE [dbo].[reservation] CHECK CONSTRAINT [FK_reservation_seat]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_reservation_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[reservation]'))
ALTER TABLE [dbo].[reservation]  WITH CHECK ADD  CONSTRAINT [FK_reservation_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_reservation_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[reservation]'))
ALTER TABLE [dbo].[reservation] CHECK CONSTRAINT [FK_reservation_users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_seat_module]') AND parent_object_id = OBJECT_ID(N'[dbo].[seat]'))
ALTER TABLE [dbo].[seat]  WITH CHECK ADD  CONSTRAINT [FK_seat_module] FOREIGN KEY([module_id])
REFERENCES [dbo].[module] ([module_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_seat_module]') AND parent_object_id = OBJECT_ID(N'[dbo].[seat]'))
ALTER TABLE [dbo].[seat] CHECK CONSTRAINT [FK_seat_module]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_seat_types]') AND parent_object_id = OBJECT_ID(N'[dbo].[seat]'))
ALTER TABLE [dbo].[seat]  WITH CHECK ADD  CONSTRAINT [FK_seat_types] FOREIGN KEY([type_id])
REFERENCES [dbo].[seat_types] ([type_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_seat_types]') AND parent_object_id = OBJECT_ID(N'[dbo].[seat]'))
ALTER TABLE [dbo].[seat] CHECK CONSTRAINT [FK_seat_types]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_users_role]') AND parent_object_id = OBJECT_ID(N'[dbo].[users]'))
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([rol_id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_users_role]') AND parent_object_id = OBJECT_ID(N'[dbo].[users]'))
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_role]
GO
USE [master]
GO
ALTER DATABASE [VMS_DB] SET READ_WRITE 
GO
USE [VMS_DB]
GO