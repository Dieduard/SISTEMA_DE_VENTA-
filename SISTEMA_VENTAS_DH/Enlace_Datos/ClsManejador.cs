using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region LIBRERIAS AGREGADAS PARA PODER USAR USAR SQL SERVER EN VISUAL STUDIO
using System.Data;
using System.Data.SqlClient; /* AGREGARMOS LAS SIGUIENTES LIBRERIAS PARA TODO EL FUNCIONAMIENTO DE SQL*/
#endregion

namespace Enlace_Datos
{
    public class ClsManejador
    {
        #region AQUI TENEMOS EL LINK DE CONEXION CON LA BASE DE DATOS 
        // DECLARACION DE LA VARIABLE DE CONEXION AL SERVIDOR DE LA BASE DE DATOS VETERINARIA
        public SqlConnection conexion = new SqlConnection("Server=.;DataBase=BD_Veterinaria;Integrated Security=SSPI");
        #endregion

        #region ABRIR Y CERRA SESSION HACIA LA BASE DE DATOS
        //METODO PARA ABRIR CONEXION A LA BASE DE DATOS.
        void abrir_conexion()
        {
            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
            }
        }
        // METODO PARA CERRAR CONEXION
        void cerrar_conexion()
        {
            if (conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }
        #endregion

        #region METODO QUE LOS PERMITE EJECUTAR LOS PROCEDIMIENTOS ALMACENADOS DE LA BASE DE DATOS
        public void ejecutar_SP(string NombreSP, List<ClsParametro> lst) // METODO PARA EJECUTAR PROC INSERT/DELETE/UPDATE
        {
            SqlCommand cmd;  //CON ESTE PODEMOS USAR LOS COMANDOS SQL
            try
            {
                abrir_conexion();  // APERTURAMOS LA CONEXION 
                cmd = new SqlCommand(NombreSP, conexion);       // FORMA DE EJECUTAR LOS PROCEDIMEINTOS
                cmd.CommandType = CommandType.StoredProcedure; //  ESPECIFICAMOS EL TIPO DE COMANDO QUE SERA PARA PROCEDIMIENTO ALMACENADO

                // PARA PODER RECORRER TODOS LOS ELEMENTOS HACEMOS ESTE FOR
                if (lst != null) // VALIDACION PARA VER SI NUESTRA LISTA ESTA VACIA
                {
                    for (int i = 0; i < lst.Count; i++) // RECORRIENDO DESDE CERO AL NUMERO MAYOR
                    {
                        if (lst[i].Direccion == ParameterDirection.Input) // AQUI VEMOS SI EL PARAMETRO ES DE ENTRADA Y LE MANDAMOS LOS PARAMETROS
                        {
                            cmd.Parameters.AddWithValue(lst[i].Nombre, lst[i].Valor);
                        }
                        if (lst[i].Direccion == ParameterDirection.Output)// AQUI VEMOS SI EL PARAMETRO ES DE SALIDA Y LE MANDAMOS LOS PARAMETROS
                        {
                            cmd.Parameters.Add(lst[i].Nombre, lst[i].TipoDato, lst[i].Tamaño).Direction = ParameterDirection.Output;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    // AQUI RECUPERAMOS EL VALOR DE SALIDA COM ESTE FOR
                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (lst[i].Direccion == ParameterDirection.Output)
                        {
                            lst[i].Valor = cmd.Parameters[i].Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cerrar_conexion();
        }
        #endregion

        #region METODO QUE LOS PERMITE GENERAR NUESTRA LISTA GENERICA
        public DataTable Listado(String NombreSP, List<ClsParametro> lst)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da; // ESTE PERMITE EJECUTAR TODO PROCEDIMIENTO ALMACENADO Y LE MANDA LOS DATOS A DATATABLE.
            try
            {
                abrir_conexion();
                da = new SqlDataAdapter(NombreSP, conexion);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (lst != null) // VALIDACION PARA VER SI LA LISTA ESTA VACIA.
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        da.SelectCommand.Parameters.AddWithValue(lst[i].Nombre, lst[i].Valor);
                    }
                }
                da.Fill(dt); // AQUI ESTAMOS LLENANDO TODO LO QUE ES NUESTRO DATATABLE CON EL COMANDO FILL
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cerrar_conexion();
            return dt;
        }
        #endregion
    }
}
