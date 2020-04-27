using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;

namespace Negocio
{
   public class NegocioCategoria
    {
        public List<Categoria> listar()
        {
            List<Categoria> listado = new List<Categoria>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "data source=DESKTOP-F294LK5; initial catalog=CATALOGO_DB; integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Id, Descripcion from CATEGORIAS";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.ID = lector.GetInt32(0);
                    aux.Descripcion = lector.GetString(1);

                    listado.Add(aux);
                }

                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}

