CREATE PROCEDURE CRE_PRESTAMO_PR
    @P_Isbn VARCHAR(20),
    @P_UsuarioId INT,
    @P_FechaPrestamo DATE,
    @P_FechaLimite DATE,
    @P_Estado NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO tblPrestamo (Created, Isbn, UsuarioId, FechaPrestamo, FechaLimite, Estado)
    VALUES (GETDATE(), @P_Isbn, @P_UsuarioId, @P_FechaPrestamo, @P_FechaLimite, @P_Estado);
END;
GO
