using DataAccess.DAO;
using Entities_DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class PrestamoCrudFactory : CrudFactory
    {
        public PrestamoCrudFactory()
        {
            sqlDAO = sqlDAO.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var prestamo = baseDTO as Prestamo;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "CRE_PRESTAMO_PR";

            sqlOperation.AddStringParam("P_Isbn", prestamo.Isbn);
            sqlOperation.AddIntParam("P_UsuarioId", prestamo.UsuarioId);
            sqlOperation.AddDateTimeParam("P_FechaPrestamo", prestamo.FechaPrestamo);
            sqlOperation.AddDateTimeParam("P_FechaLimite", prestamo.FechaLimite);
            sqlOperation.AddStringParam("P_Estado", prestamo.Estado);

            sqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var prestamo = baseDTO as Prestamo;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "DEL_PRESTAMO_PR";

            sqlOperation.AddIntParam("P_Id", prestamo.Id);

            sqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstResults = new List<T>();
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_ALL_PRESTAMOS_PR";

            var lstResult = sqlDAO.ExecuteQueryProcedure(operation);

            foreach (var item in lstResult)
            {
                var prestamo = BuildPrestamo(item);
                lstResults.Add((T)Convert.ChangeType(prestamo, typeof(T)));
            }
            return lstResults;
        }

        public override T RetrieveById<T>(int id)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_PRESTAMO_BY_ID_PR";
            operation.AddIntParam("P_Id", id);

            var lstResults = sqlDAO.ExecuteQueryProcedure(operation);

            if (lstResults.Count > 0)
            {
                var prestamo = BuildPrestamo(lstResults[0]);
                return (T)Convert.ChangeType(prestamo, typeof(T));
            }
            return default(T);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var prestamo = baseDTO as Prestamo;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "UPD_PRESTAMO_PR";

            sqlOperation.AddIntParam("P_Id", prestamo.Id);
            sqlOperation.AddStringParam("P_Isbn", prestamo.Isbn);
            sqlOperation.AddIntParam("P_UsuarioId", prestamo.UsuarioId);
            sqlOperation.AddDateTimeParam("P_FechaPrestamo", prestamo.FechaPrestamo);
            sqlOperation.AddDateTimeParam("P_FechaLimite", prestamo.FechaLimite);
            sqlOperation.AddDateTimeParam("P_FechaDevolucion", prestamo.FechaDevolucion ?? (DateTime)System.Data.SqlTypes.SqlDateTime.Null);
            sqlOperation.AddStringParam("P_Estado", prestamo.Estado);

            sqlDAO.ExecuteProcedure(sqlOperation);
        }

        private Prestamo BuildPrestamo(Dictionary<string, object> row)
        {
            var prestamo = new Prestamo()
            {
                Id = (int)row["Id"],
                Created = (DateTime)row["Created"],
                Isbn = (string)row["Isbn"],
                UsuarioId = (int)row["UsuarioId"],
                FechaPrestamo = (DateTime)row["FechaPrestamo"],
                FechaLimite = (DateTime)row["FechaLimite"],
                FechaDevolucion = row["FechaDevolucion"] == DBNull.Value ? (DateTime?)null : (DateTime)row["FechaDevolucion"],
                Estado = (string)row["Estado"]
            };
            return prestamo;
        }

    }
}
