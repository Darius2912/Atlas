using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess.DAO
{
    //Vamos usar el patron del singleton
    /*
     * Clase que se encarga de la comunicacion con la base de datos
     * Asegura que solo exista una unica instancia de la clase 
     */
    public class sqlDAO
    {
        //Paso 1: Crear una instancia privada de la misma clase
        private static sqlDAO instance;

        private string connectionString;

        //Paso 2: Redefinir el constructor, para convertirlo en privado
        private sqlDAO()   {

            connectionString = @"Data Source=DESKTOP-U50R978;Initial Catalog=Biblioteca;Integrated Security=True;Trust Server Certificate=True";
        }


        //Paso 3: Metodo que expone la instancia de la clase
        public static sqlDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new sqlDAO();
            }
            return instance;
        }

        //Metodo para ejecutar SP sin retorno de datos
        public void ExecuteProcedure(SqlOperation operation)
        {

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(operation.ProcedureName, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                //set de los parametros
                {
                    foreach (var param in operation.Parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    //Ejecutar el SP contra la base de datos
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Metodo para ejecutar SP que permiten el retorno de datos
        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation)
        {

            var lstResults = new List<Dictionary<string, object>>();

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(operation.ProcedureName, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                //set de los parametros
                {
                    foreach (var param in operation.Parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    //Ejecutar el SP contra la base de datos
                    conn.Open();

                    //Ejecucion del SP que retorna data desde la base de datos
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();

                            for (var index = 0; index < reader.FieldCount; index++)
                            {
                                var key = reader.GetName(index);
                                var value = reader.GetValue(index);

                                row[key] = value;
                            }
                            lstResults.Add(row);
                        }

                    }

                }

                return lstResults;
            }

        }

    }
}
