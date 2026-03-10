using DataAccess.DAO;
using Entities_DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class LibroCrudFactory : CrudFactory
    {
        public LibroCrudFactory()
        {
            sqlDAO = sqlDAO.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var libro = baseDTO as Libro;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "CRE_LIBRO_PR";

            sqlOperation.AddStringParam("P_Isbn", libro.Isbn);
            sqlOperation.AddStringParam("P_Titulo", libro.Titulo);
            sqlOperation.AddStringParam("P_Autor", libro.Autor);
            sqlOperation.AddStringParam("P_Categoria", libro.Categoria);
            sqlOperation.AddIntParam("P_Copias", libro.Copias);
            sqlOperation.AddIntParam("P_Disponibles", libro.Disponibles);

            sqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var libro = baseDTO as Libro;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "DEL_LIBRO_PR";

            sqlOperation.AddIntParam("P_Id", libro.Id);

            sqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstResults = new List<T>();
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_ALL_LIBROS_PR";

            var lstResult = sqlDAO.ExecuteQueryProcedure(operation);

            foreach (var item in lstResult)
            {
                var libro = BuildLibro(item);
                lstResults.Add((T)Convert.ChangeType(libro, typeof(T)));
            }
            return lstResults;
        }

        public override T RetrieveById<T>(int id)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_LIBRO_BY_ID_PR";
            operation.AddIntParam("P_Id", id);

            var lstResults = sqlDAO.ExecuteQueryProcedure(operation);

            if (lstResults.Count > 0)
            {
                var libro = BuildLibro(lstResults[0]);
                return (T)Convert.ChangeType(libro, typeof(T));
            }
            return default(T);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var libro = baseDTO as Libro;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "UPD_LIBRO_PR";

            sqlOperation.AddIntParam("P_Id", libro.Id);
            sqlOperation.AddStringParam("P_Isbn", libro.Isbn);
            sqlOperation.AddStringParam("P_Titulo", libro.Titulo);
            sqlOperation.AddStringParam("P_Autor", libro.Autor);
            sqlOperation.AddStringParam("P_Categoria", libro.Categoria);
            sqlOperation.AddIntParam("P_Copias", libro.Copias);
            sqlOperation.AddIntParam("P_Disponibles", libro.Disponibles);

            sqlDAO.ExecuteProcedure(sqlOperation);
        }

        // Construye el DTO Libro a partir de la fila devuelta por BD
        private Libro BuildLibro(Dictionary<string, object> row)
        {
            return new Libro()
            {
                Id = (int)row["Id"],
                Created = (DateTime)row["Created"],
                Isbn = (string)row["Isbn"],
                Titulo = (string)row["Titulo"],
                Autor = (string)row["Autor"],
                Categoria = (string)row["Categoria"],
                Copias = (int)row["Copias"],
                Disponibles = (int)row["Disponibles"]
            };
        }
    }
}
