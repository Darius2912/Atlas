CREATE TABLE [dbo].[tblPrestamo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NULL,
	[Isbn] [varchar](20) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[FechaPrestamo] [date] NOT NULL,
	[FechaLimite] [date] NOT NULL,
	[FechaDevolucion] [date] NULL,
	[Estado] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblPrestamo]  WITH CHECK ADD  CONSTRAINT [FK_Prestamo_Libro] FOREIGN KEY([Isbn])
REFERENCES [dbo].[tblLibro] ([Isbn])
GO

ALTER TABLE [dbo].[tblPrestamo] CHECK CONSTRAINT [FK_Prestamo_Libro]
GO

ALTER TABLE [dbo].[tblPrestamo]  WITH CHECK ADD  CONSTRAINT [FK_Prestamo_Usuario] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[tblUsuario] ([Id])
GO

ALTER TABLE [dbo].[tblPrestamo] CHECK CONSTRAINT [FK_Prestamo_Usuario]
GO
