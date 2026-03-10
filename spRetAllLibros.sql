CREATE PROCEDURE RET_ALL_LIBROS_PR
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Created, Updated, Isbn, Titulo, Autor, Categoria, Copias, Disponibles
    FROM tblLibro;
END;
GO