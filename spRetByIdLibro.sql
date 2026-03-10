CREATE PROCEDURE RET_LIBRO_BY_ID_PR
    @P_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Created, Updated, Isbn, Titulo, Autor, Categoria, Copias, Disponibles
    FROM tblLibro
    WHERE Id = @P_Id;
END;
GO