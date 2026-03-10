CREATE PROCEDURE UPD_LIBRO_PR
    @P_Id INT,
    @P_Isbn VARCHAR(20),
    @P_Titulo NVARCHAR(200),
    @P_Autor NVARCHAR(200),
    @P_Categoria NVARCHAR(100),
    @P_Copias INT,
    @P_Disponibles INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE tblLibro
    SET Isbn        = @P_Isbn,
        Titulo      = @P_Titulo,
        Autor       = @P_Autor,
        Categoria   = @P_Categoria,
        Copias      = @P_Copias,
        Disponibles = @P_Disponibles,
        Updated     = GETDATE()
    WHERE Id = @P_Id;
END;
GO
