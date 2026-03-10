CREATE PROCEDURE RET_PRESTAMO_BY_ID_PR
    @P_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Created, Updated, Isbn, UsuarioId, FechaPrestamo, FechaLimite, FechaDevolucion, Estado
    FROM tblPrestamo
    WHERE Id = @P_Id;
END;
GO
