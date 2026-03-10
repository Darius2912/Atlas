CREATE PROCEDURE CRE_LIBRO_PR
    @P_Isbn VARCHAR(20),
    @P_Titulo NVARCHAR(200),
    @P_Autor NVARCHAR(200),
    @P_Categoria NVARCHAR(100),
    @P_Copias INT,
    @P_Disponibles INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO tblLibro (Created, Isbn, Titulo, Autor, Categoria, Copias, Disponibles)
    VALUES (GETDATE(), @P_Isbn, @P_Titulo, @P_Autor, @P_Categoria, @P_Copias, @P_Disponibles);
END;
GO
