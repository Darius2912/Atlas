CREATE PROCEDURE CRE_USUARIO_PR
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

    INSERT INTO Usuarios (Created, Name, LastName, Password, Email, BirthDate, Status, Rol)
    VALUES (GETDATE(), @P_Name, @P_LastName, @P_Password, @P_Email, @P_BirthDate, @P_Status, @P_Rol);
END;
GO
