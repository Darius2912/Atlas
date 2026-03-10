create procedure DEL_USUARIO_PR
@P_Id int
AS
Begin
Delete from tblUsuario where Id = @P_Id;
End