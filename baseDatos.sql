USE [master]
GO
/****** Object:  Database [academia]    Script Date: 29/05/2017 16:55:34 ******/
CREATE DATABASE [academia]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'academia', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\academia.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'academia_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\academia_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [academia] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [academia].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [academia] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [academia] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [academia] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [academia] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [academia] SET ARITHABORT OFF 
GO
ALTER DATABASE [academia] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [academia] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [academia] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [academia] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [academia] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [academia] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [academia] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [academia] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [academia] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [academia] SET  ENABLE_BROKER 
GO
ALTER DATABASE [academia] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [academia] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [academia] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [academia] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [academia] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [academia] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [academia] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [academia] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [academia] SET  MULTI_USER 
GO
ALTER DATABASE [academia] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [academia] SET DB_CHAINING OFF 
GO
ALTER DATABASE [academia] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [academia] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [academia] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'academia', N'ON'
GO
USE [academia]
GO
/****** Object:  Table [dbo].[alumnos_inscripciones]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[alumnos_inscripciones](
	[id_inscripcion] [int] IDENTITY(1,1) NOT NULL,
	[id_alumno] [int] NOT NULL,
	[id_curso] [int] NOT NULL,
	[condicion] [varchar](50) NULL,
	[nota] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_inscripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[condiciones]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[condiciones](
	[id_condicion] [int] IDENTITY(1,1) NOT NULL,
	[condicion] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cursos]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cursos](
	[id_curso] [int] IDENTITY(1,1) NOT NULL,
	[id_materia] [int] NULL,
	[anio_calendario] [int] NULL,
	[cupo] [int] NULL,
	[denominacion] [varchar](300) NULL,
 CONSTRAINT [PK_cursos] PRIMARY KEY CLUSTERED 
(
	[id_curso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[docentes_cursos]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[docentes_cursos](
	[id_dictado] [int] NOT NULL,
	[id_curso] [int] NULL,
	[id_docente] [int] NULL,
	[cargo] [int] NULL,
 CONSTRAINT [PK_docentes_cursos] PRIMARY KEY CLUSTERED 
(
	[id_dictado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[especialidades]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[especialidades](
	[id_especialidad] [int] IDENTITY(1,1) NOT NULL,
	[desc_especialidad] [varchar](50) NULL,
 CONSTRAINT [PK_especialidades] PRIMARY KEY CLUSTERED 
(
	[id_especialidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[materias]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[materias](
	[id_materia] [int] IDENTITY(1,1) NOT NULL,
	[desc_materia] [varchar](50) NULL,
	[hs_semanales] [int] NULL,
	[hs_totales] [int] NULL,
	[id_plan] [int] NULL,
 CONSTRAINT [PK_materias] PRIMARY KEY CLUSTERED 
(
	[id_materia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[modulos]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[modulos](
	[id_modulo] [int] IDENTITY(1,1) NOT NULL,
	[desc_modulo] [varchar](50) NULL,
	[ejecuta] [varchar](50) NULL,
 CONSTRAINT [PK_modulos] PRIMARY KEY CLUSTERED 
(
	[id_modulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[modulos_usuarios]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[modulos_usuarios](
	[id_modulo_usuario] [int] NOT NULL,
	[id_modulo] [int] NULL,
	[id_usuario] [int] NULL,
	[alta] [bit] NOT NULL,
	[baja] [bit] NULL,
	[modificacion] [bit] NULL,
	[consulta] [bit] NULL,
 CONSTRAINT [PK_modulos_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_modulo_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[personas]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[personas](
	[id_persona] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL,
	[direccion] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[fecha_nac] [datetime] NULL,
	[legajo] [int] NULL,
	[id_tipo_persona] [int] NULL,
	[id_plan] [int] NULL,
 CONSTRAINT [PK_personas] PRIMARY KEY CLUSTERED 
(
	[id_persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[planes]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[planes](
	[id_plan] [int] IDENTITY(1,1) NOT NULL,
	[desc_plan] [varchar](50) NULL,
	[id_especialidad] [int] NULL,
 CONSTRAINT [PK_planes] PRIMARY KEY CLUSTERED 
(
	[id_plan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipo_persona]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipo_persona](
	[id_tipo_persona] [int] IDENTITY(1,1) NOT NULL,
	[desc_tipo_persona] [varchar](50) NULL,
 CONSTRAINT [PK_tipo_persona] PRIMARY KEY CLUSTERED 
(
	[id_tipo_persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 29/05/2017 16:55:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre_usuario] [varchar](50) NULL,
	[clave] [varchar](50) NULL,
	[habilitado] [bit] NULL,
	[cambia_clave] [bit] NULL,
	[id_persona] [int] NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[alumnos_inscripciones] ON 

INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (1, 1, 1, N'Aprobado', 7)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (2, 1, 3, N'Inscripto', 0)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (3, 3, 4, N'Inscripto', 0)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (4, 1, 4, N'Inscripto', 0)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (5, 1, 10, N'Inscripto', 0)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (6, 1, 15, N'Inscripto', 0)
SET IDENTITY_INSERT [dbo].[alumnos_inscripciones] OFF
SET IDENTITY_INSERT [dbo].[cursos] ON 

INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (1, 1, 2017, 29, N'29')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (2, 3, 2017, 678, N'678')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (3, 2, 2017, 26, N'26')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (4, 2, 2017, 8, N'8')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (5, 1, 2016, 70, N'70')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (6, 3, 2016, 60, N'401')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (7, 3, 2016, 60, N'402')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (8, 3, 2016, 55, N'403')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (9, 2, 2016, 60, N'401')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (10, 2, 2016, 59, N'59')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (11, 2, 2016, 55, N'403')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (12, 2, 2017, 60, N'401')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (13, 2, 2017, 60, N'402')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (14, 2, 2017, 55, N'403')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (15, 1, 2017, 59, N'59')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (16, 1, 2017, 60, N'402')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [anio_calendario], [cupo], [denominacion]) VALUES (17, 1, 2017, 55, N'403')
SET IDENTITY_INSERT [dbo].[cursos] OFF
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (1, 1, 2, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (2, 2, 2, 2)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (3, 5, 2, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (4, 15, 2, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (5, 16, 2, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (6, 17, 2, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (7, 3, 6, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (8, 4, 6, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (9, 9, 6, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (10, 10, 6, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (11, 11, 6, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (12, 12, 6, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (13, 13, 6, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (14, 14, 6, 1)
SET IDENTITY_INSERT [dbo].[especialidades] ON 

INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (1, N'ISI')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (2, N'IM')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (3, N'IE')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (5, N'IQ')
SET IDENTITY_INSERT [dbo].[especialidades] OFF
SET IDENTITY_INSERT [dbo].[materias] ON 

INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (1, N'NET', 12, 24, 1)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (2, N'JAVA', 12, 24, 1)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (3, N'ADR', 12, 24, 1)
SET IDENTITY_INSERT [dbo].[materias] OFF
SET IDENTITY_INSERT [dbo].[personas] ON 

INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [id_tipo_persona], [id_plan]) VALUES (1, N'Nicolas', N'Giordano', N'Zeballos y Santiago', N'miemail@gmail.com', N'346323459', CAST(N'1994-06-22 00:00:00.000' AS DateTime), 40467, 2, 1)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [id_tipo_persona], [id_plan]) VALUES (2, N'Sebastián', N'Rocco', N'1º de Mayo 1500', N'sebarocco@gmail.com', N'34132345', CAST(N'1890-04-21 00:00:00.000' AS DateTime), 492221, 1, 1)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [id_tipo_persona], [id_plan]) VALUES (3, N'Admin', N'Admin', N'Admin', N'Admin', N'00000000', CAST(N'1999-02-01 00:00:00.000' AS DateTime), 0, 5, 1)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [id_tipo_persona], [id_plan]) VALUES (4, N'Leonardo', N'Peretti', N'', N'', N'21312312', CAST(N'1994-01-30 00:00:00.000' AS DateTime), 40425, 2, 8)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [id_tipo_persona], [id_plan]) VALUES (5, N'Clauida', N'Seveso', N'España 1233', N'cseveso@hotmail.com', N'304030', CAST(N'1994-03-01 00:00:00.000' AS DateTime), 111222, 3, 1)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [id_tipo_persona], [id_plan]) VALUES (6, N'Martin', N'Comparetto', N'España 1111', N'martin@hotmail.com', N'341023552', CAST(N'1980-11-11 00:00:00.000' AS DateTime), 12, 1, 22)
SET IDENTITY_INSERT [dbo].[personas] OFF
SET IDENTITY_INSERT [dbo].[planes] ON 

INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (1, N'PLAN 2008', 1)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (2, N'PLAN 2015', 1)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (3, N'PLAN 2008', 2)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (6, N'PLAN 2015', 2)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (7, N'PLAN 2015', 5)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (8, N'PLAN 2015', 5)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (17, N'PLAN 2020', 1)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (18, N'PLAN 2020', 2)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (19, N'PLAN 2020', 3)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (20, N'PLAN 2020', 5)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (21, N'PLAN 2008', 3)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (22, N'PLAN 2015', 3)
SET IDENTITY_INSERT [dbo].[planes] OFF
SET IDENTITY_INSERT [dbo].[tipo_persona] ON 

INSERT [dbo].[tipo_persona] ([id_tipo_persona], [desc_tipo_persona]) VALUES (1, N'Profesor')
INSERT [dbo].[tipo_persona] ([id_tipo_persona], [desc_tipo_persona]) VALUES (2, N'Alumno')
INSERT [dbo].[tipo_persona] ([id_tipo_persona], [desc_tipo_persona]) VALUES (3, N'Libre')
INSERT [dbo].[tipo_persona] ([id_tipo_persona], [desc_tipo_persona]) VALUES (4, N'Libre')
INSERT [dbo].[tipo_persona] ([id_tipo_persona], [desc_tipo_persona]) VALUES (5, N'Admin')
SET IDENTITY_INSERT [dbo].[tipo_persona] OFF
SET IDENTITY_INSERT [dbo].[usuarios] ON 

INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [cambia_clave], [id_persona]) VALUES (1, N'alumno', N'alumno', 1, NULL, 1)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [cambia_clave], [id_persona]) VALUES (2, N'profesorNET', N'profesorNET', 1, NULL, 2)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [cambia_clave], [id_persona]) VALUES (3, N'admin', N'admin', 1, NULL, 3)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [cambia_clave], [id_persona]) VALUES (4, N'inhabilitado', N'inhabilitado', 0, NULL, 4)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [cambia_clave], [id_persona]) VALUES (6, N'recepcionista', N'recepcionista', 1, NULL, 5)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [cambia_clave], [id_persona]) VALUES (7, N'profesorJAVA', N'profesorJAVA', 1, NULL, 6)
SET IDENTITY_INSERT [dbo].[usuarios] OFF
ALTER TABLE [dbo].[alumnos_inscripciones]  WITH CHECK ADD  CONSTRAINT [FK_alumnos_inscripciones_cursos] FOREIGN KEY([id_curso])
REFERENCES [dbo].[cursos] ([id_curso])
GO
ALTER TABLE [dbo].[alumnos_inscripciones] CHECK CONSTRAINT [FK_alumnos_inscripciones_cursos]
GO
ALTER TABLE [dbo].[alumnos_inscripciones]  WITH CHECK ADD  CONSTRAINT [FK_alumnos_inscripciones_personas] FOREIGN KEY([id_alumno])
REFERENCES [dbo].[personas] ([id_persona])
GO
ALTER TABLE [dbo].[alumnos_inscripciones] CHECK CONSTRAINT [FK_alumnos_inscripciones_personas]
GO
ALTER TABLE [dbo].[cursos]  WITH CHECK ADD  CONSTRAINT [FK_cursos_materias] FOREIGN KEY([id_materia])
REFERENCES [dbo].[materias] ([id_materia])
GO
ALTER TABLE [dbo].[cursos] CHECK CONSTRAINT [FK_cursos_materias]
GO
ALTER TABLE [dbo].[docentes_cursos]  WITH CHECK ADD  CONSTRAINT [FK_docentes_cursos_cursos] FOREIGN KEY([id_curso])
REFERENCES [dbo].[cursos] ([id_curso])
GO
ALTER TABLE [dbo].[docentes_cursos] CHECK CONSTRAINT [FK_docentes_cursos_cursos]
GO
ALTER TABLE [dbo].[docentes_cursos]  WITH CHECK ADD  CONSTRAINT [FK_docentes_cursos_personas] FOREIGN KEY([id_docente])
REFERENCES [dbo].[personas] ([id_persona])
GO
ALTER TABLE [dbo].[docentes_cursos] CHECK CONSTRAINT [FK_docentes_cursos_personas]
GO
ALTER TABLE [dbo].[materias]  WITH CHECK ADD  CONSTRAINT [FK_materias_planes] FOREIGN KEY([id_plan])
REFERENCES [dbo].[planes] ([id_plan])
GO
ALTER TABLE [dbo].[materias] CHECK CONSTRAINT [FK_materias_planes]
GO
ALTER TABLE [dbo].[modulos_usuarios]  WITH CHECK ADD  CONSTRAINT [FK_modulos_usuarios_modulos] FOREIGN KEY([id_modulo])
REFERENCES [dbo].[modulos] ([id_modulo])
GO
ALTER TABLE [dbo].[modulos_usuarios] CHECK CONSTRAINT [FK_modulos_usuarios_modulos]
GO
ALTER TABLE [dbo].[modulos_usuarios]  WITH CHECK ADD  CONSTRAINT [FK_modulos_usuarios_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[modulos_usuarios] CHECK CONSTRAINT [FK_modulos_usuarios_usuarios]
GO
ALTER TABLE [dbo].[personas]  WITH CHECK ADD  CONSTRAINT [FK_personas_planes] FOREIGN KEY([id_plan])
REFERENCES [dbo].[planes] ([id_plan])
GO
ALTER TABLE [dbo].[personas] CHECK CONSTRAINT [FK_personas_planes]
GO
ALTER TABLE [dbo].[personas]  WITH CHECK ADD  CONSTRAINT [FK_personas_tipo_persona] FOREIGN KEY([id_tipo_persona])
REFERENCES [dbo].[tipo_persona] ([id_tipo_persona])
GO
ALTER TABLE [dbo].[personas] CHECK CONSTRAINT [FK_personas_tipo_persona]
GO
ALTER TABLE [dbo].[planes]  WITH CHECK ADD  CONSTRAINT [FK_planes_especialidades] FOREIGN KEY([id_especialidad])
REFERENCES [dbo].[especialidades] ([id_especialidad])
GO
ALTER TABLE [dbo].[planes] CHECK CONSTRAINT [FK_planes_especialidades]
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_personas] FOREIGN KEY([id_persona])
REFERENCES [dbo].[personas] ([id_persona])
GO
ALTER TABLE [dbo].[usuarios] CHECK CONSTRAINT [FK_usuarios_personas]
GO
USE [master]
GO
ALTER DATABASE [academia] SET  READ_WRITE 
GO
