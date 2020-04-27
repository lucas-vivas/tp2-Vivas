using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class NegocioArticulos
    {
        public List<Articulo>Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            Articulo aux;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexion = new SqlConnection();
            SqlDataReader lector;
            try
            {
                conexion.ConnectionString = "data source= DESKTOP-F294LK5; initial catalog=CATALOGO_DB; integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Id,Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio from ARTICULOS";
                comando.Connection = conexion;

                conexion.Open(); // abro la conexion   
                lector = comando.ExecuteReader();

                while (lector.Read())//Leo los campos de la base de datos y lo guardo en un objeto auxiliar
                {
                    aux = new Articulo();
                    aux.ID = lector.GetInt32(0);
                    aux.Codigo = lector["Codigo"].ToString();
                    aux.Nombre = lector["Nombre"].ToString();
                    aux.Descripcion = lector["Descripcion"].ToString();

                    aux.ID_Marca = new Marca();
                    aux.ID_Marca.ID = (int)lector["IdMarca"];
                    aux.ID_Marca.descripcion = lector["Descripcion"].ToString();

                    aux.Categoria = new Categoria();
                    aux.Categoria.ID = (int)lector["IdCategoria"];
                    aux.URL_Imagen = lector["ImagenUrl"].ToString();
                    aux.Precio = (decimal)lector["Precio"];
                    lista.Add(aux);//agrego a la lista la referencia
                }
              

                return lista;//devuelvo la lista
            }
            catch (Exception ex)
            {

                throw ex;// lanzo el error
            }
            finally
            {
                conexion.Close();//cierro la conexion con la base de datos
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearQuery("delete from ARTICULOS where Id=" + id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void agregar(Articulo nuevo)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            try
            {

                conexion.ConnectionString = "data source= DESKTOP-F294LK5; initial catalog=CATALOGO_DB; integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "insert into ARTICULOS (Codigo,Nombre,Descripcion,ImagenUrl,IdMarca,IdCategoria,Precio) values (@codigo,@Nombre,@Desc,@Imagen,@IdMarca,@IdCategoria,@Precio)";
                comando.Parameters.AddWithValue("@Codigo", nuevo.Codigo);
                comando.Parameters.AddWithValue("@Nombre", nuevo.Nombre);
                comando.Parameters.AddWithValue("@Desc", nuevo.Descripcion);
                comando.Parameters.AddWithValue("@Imagen", nuevo.URL_Imagen);
                comando.Parameters.AddWithValue("@IdMarca", nuevo.ID_Marca.ID);
                comando.Parameters.AddWithValue("@IdCategoria", nuevo.Categoria.ID);
                comando.Parameters.AddWithValue("@Precio", nuevo.Precio);
                //comando.Parameters.AddWithValue("@Estado", nuevo.Estado);

                comando.Connection = conexion;
                conexion.Open();
                comando.ExecuteNonQuery();
                
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

        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearQuery("update ARTICULOS set Codigo=@Codigo, Nombre=@Nombre, Descripcion=@Desc, IdMarca=@IdMarca, IdCategoria=@IdCategoria, ImagenUrl=@Imagen, Precio=@Precio where Id=@Id");
                datos.agregarParametro("@Id", articulo.ID);
                datos.agregarParametro("@Codigo", articulo.Codigo);
                datos.agregarParametro("@Nombre", articulo.Nombre);
                datos.agregarParametro("@Desc", articulo.Descripcion);
                datos.agregarParametro("@IdMarca", articulo.ID_Marca.ID);
                datos.agregarParametro("@IdCategoria", articulo.Categoria.ID);
                datos.agregarParametro("@Imagen", articulo.URL_Imagen);
                datos.agregarParametro("@Precio", articulo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Articulo>listar2()
        {
            List<Articulo> lista = new List<Articulo>();
            Articulo aux;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearQuery("select Id,Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio from ARTICULOS");
                datos.ejecutarLector();
                while(datos.lector.Read())
                {
                    aux = new Articulo();
                    aux.ID = datos.lector.GetInt32(0);
                    aux.Codigo = datos.lector["Codigo"].ToString();
                    aux.Nombre = datos.lector["Nombre"].ToString();
                    aux.Descripcion = datos.lector["Descripcion"].ToString();

                    aux.ID_Marca = new Marca();
                    aux.ID_Marca.ID = (int)datos.lector["IdMarca"];
                    aux.ID_Marca.descripcion = datos.lector["Descripcion"].ToString();

                    aux.Categoria = new Categoria();
                    aux.Categoria.ID = (int)datos.lector["IdCategoria"];
                    aux.Categoria.Descripcion = datos.lector["Descripcion"].ToString();
                    aux.URL_Imagen = datos.lector["ImagenUrl"].ToString();
                    aux.Precio = (decimal)datos.lector["Precio"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
