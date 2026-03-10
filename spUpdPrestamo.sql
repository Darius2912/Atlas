CREATE PROCEDURE UPD_PRESTAMO_PR
    @P_Id INT,
    @P_Isbn VARCHAR(20),
    @P_UsuarioId INT,
    @P_FechaPrestamo DATE,
    @P_FechaLimite DATE,
    @P_FechaDevolucion DATE = NULL,
    @P_Estado NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE tblPrestamo
    SET Isbn           = @P_Isbn,
        UsuarioId      = @P_UsuarioId,
        FechaPrestamo  = @P_FechaPrestamo,
        FechaLimite    = @P_FechaLimite,
        FechaDevolucion= @P_FechaDevolucion,
        Estado         = @P_Estado,
        Updated        = GETDATE()
    WHERE Id = @P_Id;
END;
GO
