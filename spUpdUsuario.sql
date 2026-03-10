CREATE PROCEDURE UPD_USUARIO_PR
    @P_Id INT,
    @P_Name NVARCHAR(100),
    @P_LastName NVARCHAR(100),
    @P_Password NVARCHAR(200),
    @P_Email NVARCHAR(200),
    @P_BirthDate DATE,
    @P_Status NVARCHAR(50),
    @P_Rol NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE tblUsuario
    SET Name       = @P_Name,
        LastName   = @P_LastName,
        Password   = @P_Password,
        Email      = @P_Email,
        BirthDate  = @P_BirthDate,
        Status     = @P_Status,
        Rol        = @P_Rol,
        Updated    = GETDATE()
    WHERE Id = @P_Id;
END;
GO