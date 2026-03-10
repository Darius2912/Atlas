CREATE PROCEDURE RET_ALL_PRESTAMOS_PR
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Created, Updated, Isbn, UsuarioId, FechaPrestamo, FechaLimite, FechaDevolucion, Estado
    FROM tblPrestamo;
END;
GO
