using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region LIBRERIAS AGREGADAS PARA EL FUNCIONAMIENTO Y USO DE SQL EN VISUAL STUDIO
using System.Data;
using System.Data.SqlClient;
#endregion
namespace Enlace_Datos
{
    public class ClsParametro
    {
        // PARAMETROS A USAR NOMBRE - VALOR - TIPO DATOS  Y EL TAMAÑO DEL DATO CAPTURADO
        // SON CARACTERISTICAS O ATRIBUTOS QUE TODO PARAMETRO POSEE Y ESOS  VIENEN DE LOS ATRIBUTOS ENVIADOS DESDE LA BD.

        #region PARAMETROS USADOS EN LA CONEXION A LA BASE DE DATOS
        // AQUI SE AGREGAMOS LOS PARAMETROS GENERALES QUE TIENE UN OBJETO EN TODOS LOS PARAMETROS HECHOS
        // LOS PARAMETROS SON LOS ATRIBUTOS O CARACTERISTICAS QUE UN OBJETO TIENE
        public String Nombre { get; set; }
        public Object Valor { get; set; }
        public SqlDbType TipoDato { get; set; }     // Permite Manejar tipo datoS SQL SERVER
        public Int32 Tamaño { get; set; }
        public ParameterDirection Direccion { get; set; }
        #endregion

        // CONSTRUCTORES DE ENTRADA - SALIDA DE LOS DATOS DE LA BASE DE DATOS 

        #region AQUI TENEMOS EL CONSTRUCTOR DE ENTRADA DE LOS DATOS 
        // Constructor de Entrada
        public ClsParametro(string ObjNombre, Object objValor)
        {
            Nombre = ObjNombre;
            Valor = objValor;
            Direccion = ParameterDirection.Input;
        }
        #endregion

        #region AQUI TENEMOS EL CONSTRUCTOR DE SALIDA DE LOS DATOS
        // Constructor de Salida
        public ClsParametro(string objNombre, Object objValor, SqlDbType objTipoDato, ParameterDirection objDireccion, Int32 objTamaño)
        {
            Nombre = objNombre;
            TipoDato = objTipoDato;
            Tamaño = objTamaño;
            Direccion = ParameterDirection.Output;
        }
        #endregion

    }
}
