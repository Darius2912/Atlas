create procedure RET_ALL_USUARIO_PR
as
begin 
   select  Id, Created, Name, LastName, Password, Email, BirthDate, Status, Rol 
   from tblUsuario;

end