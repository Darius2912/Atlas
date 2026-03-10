create procedure RET_USUARIO_BY_ID_PR
@P_Id int
as
begin 
   select  Id, Created, Name, LastName, Password, Email, BirthDate, Status, Rol 
   from tblUsuario
   where Id = @P_Id;
end;