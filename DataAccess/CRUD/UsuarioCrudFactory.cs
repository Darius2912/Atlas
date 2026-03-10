using DataAccess.DAO;
using Entities_DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class UsuarioCrudFactory : CrudFactory
    {
        public UsuarioCrudFactory()
        {
            sqlDAO = sqlDAO.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var usuario = baseDTO as Usuario;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "CRE_USUARIO_PR";

            sqlOperation.AddStringParam("P_Name", usuario.Name);
            sqlOperation.AddStringParam("P_LastName", usuario.LastName);
            sqlOperation.AddStringParam("P_Password", usuario.Password);
            sqlOperation.AddStringParam("P_Email", usuario.Email);
            sqlOperation.AddDateTimeParam("P_BirthDate", usuario.BirthDate);
            sqlOperation.AddStringParam("P_Status", usuario.Status);
            sqlOperation.AddStringParam("P_Rol", usuario.Rol);

            sqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var usuario = baseDTO as Usuario;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "DEL_USUARIO_PR";

            sqlOperation.AddIntParam("P_Id", usuario.Id);

            sqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstResults = new List<T>();
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_ALL_USUARIO_PR";

            var lstResult = sqlDAO.ExecuteQueryProcedure(operation);

            if (lstResult.Count > 0)
            {
                foreach (var item in lstResult)
                {
                    var usuario = BuildUsuario(item);
                    lstResults.Add((T)Convert.ChangeType(usuario, typeof(T)));
                }
            }
            return lstResults;
        }

        public override T RetrieveById<T>(int id)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_USUARIO_BY_ID_PR";

            operation.AddIntParam("P_Id", id);

            var lstResults = sqlDAO.ExecuteQueryProcedure(operation);

            if (lstResults.Count > 0)
            {
                var item = lstResults[0];
                var usuario = BuildUsuario(item);
                return (T)Convert.ChangeType(usuario, typeof(T));
            }
            return default(T);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var usuario = baseDTO as Usuario;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "UPD_USUARIO_PR";

            sqlOperation.AddIntParam("P_Id", usuario.Id);
            sqlOperation.AddStringParam("P_Name", usuario.Name);
            sqlOperation.AddStringParam("P_LastName", usuario.LastName);
            sqlOperation.AddStringParam("P_Password", usuario.Password);
            sqlOperation.AddStringParam("P_Email", usuario.Email);
            sqlOperation.AddDateTimeParam("P_BirthDate", usuario.BirthDate);
            sqlOperation.AddStringParam("P_Status", usuario.Status);
            sqlOperation.AddStringParam("P_Rol", usuario.Rol);

            sqlDAO.ExecuteProcedure(sqlOperation);
        }

        // Construye el DTO Usuario a partir de la fila devuelta por BD
        private Usuario BuildUsuario(Dictionary<string, object> row)
        {
            var usuario = new Usuario()
            {
                Id = (int)row["Id"],
                Created = (DateTime)row["Created"],
                Name = (string)row["Name"],
                LastName = (string)row["LastName"],
                Password = (string)row["Password"],
                Email = (string)row["Email"],
                BirthDate = (DateTime)row["BirthDate"],
                Status = (string)row["Status"],
                Rol = (string)row["Rol"]
            };
            return usuario;
        }
    }
}
