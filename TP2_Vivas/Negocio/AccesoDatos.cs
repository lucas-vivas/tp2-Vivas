﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        public SqlDataReader lector { get; set; }
        public SqlConnection conexion { get; }
        public SqlCommand comando { get; set; }
        public  AccesoDatos()
        {
            conexion = new SqlConnection("data source= DESKTOP-F294LK5; initial catalog=CATALOGO_DB; integrated security=sspi");
            comando = new SqlCommand();
            comando.Connection = conexion;
        }
        public void setearQuery(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        public void setearSP(string sp)
        {

        }
        public void agregarParametro(string nombre,object valor)
        {
            comando.Parameters.AddWithValue(nombre,valor);
        }
        public void cerrarConexion()
        {
            conexion.Close();
        }
        public void ejecutarLector()
        {
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        internal void ejecutarAccion()
        {
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
