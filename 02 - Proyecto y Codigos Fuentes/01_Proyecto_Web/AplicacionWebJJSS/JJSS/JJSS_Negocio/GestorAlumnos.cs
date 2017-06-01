using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{

    /*
     * Clase que nos permite gestionar alumnos
     */
    public class GestorAlumnos
    {


        public alumno ObtenerAlumnoPorDNI(int pDni)
        {
            using (var db = new JJSSEntities())
            {
                var alumnoEncontrado = from alu in db.alumno
                                       where alu.dni == pDni
                                       select alu;
                return alumnoEncontrado.FirstOrDefault();
            }
        }

        /*Método que permite crear un nuevo alumno
         * Valida si el alumno ya fue creado comparando por DNI
         * 
         * Parametros: 
         *              pNombre : String nombre del alumno
         *              pApellido : String apellido del alumno
         *              pFechaNacimiento : DateTime fecha de nacimiento del alumno
         *              pIdFaja : Entero id de la faja del alumno
         *              pIdCategoria : Entero id de la categoria a la que pertenece el alumno
         *              pSexo : Short 0 Mujer 1 Hombre
         *              pDni : Entero numero de DNI del alumno
         *              pTelefono : Entero numero de telefono del alumno
         *              pMail : String mail del alumno
         *              pIdDireccion : Entero id de la direccion del alumno
         *              pTelEmergencia : Entero numero de telefono de emergencia del alumno
         *  Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              Alumno existente
         *          
         * 
         */
        public string RegistrarAlumno(string pNombre, string pApellido, DateTime? pFechaNacimiento, int? pIdFaja, int? pIdCategoria, 
            short? pSexo, int pDni, int? pTelefono, string pMail, int? pIdDireccion, int? pTelEmergencia, byte[] pImagen, 
            string pCalle, int? pNumero, string pDpto, int? pPiso)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    faja fajaElegida = db.faja.Find(pIdFaja);
                    //+Rever categorias en la BD
                    //categoria catElegida = db.categoria.Find(pIdCategoria);

                    if (ObtenerAlumnoPorDNI(pDni) != null)
                    {
                        return "Alumno existente";
                    }
                    alumno nuevoAlumno;
                    direccion nuevaDireccion;

                    
                    ciudad ciudadElegida = db.ciudad.Find(1);

                    nuevaDireccion = new direccion()
                    {
                        calle1 = pCalle,
                        departamento = pDpto,
                        numero = pNumero,
                        piso = pPiso,
                        ciudad = ciudadElegida
                    };

                    nuevoAlumno = new alumno()
                    {
                        nombre = pNombre,
                        apellido = pApellido,
                        fecha_nacimiento = pFechaNacimiento,
                        faja = fajaElegida,
                        //categoria
                        sexo = pSexo,
                        dni = pDni,
                        telefono = pTelefono,
                        mail = pMail,
                        direccion=nuevaDireccion,
                        fecha_ingreso = DateTime.Today,
                        telefono_emergencia = pTelEmergencia
                    };

                    db.alumno.Add(nuevoAlumno);
                    db.direccion.Add(nuevaDireccion);
                    db.SaveChanges();
                    alumno_imagen nuevoAlumno_imagen = new alumno_imagen()
                    {
                        id_alumno = nuevoAlumno.id_alumno,
                        imagen = pImagen
                    };
                    db.alumno_imagen.Add(nuevoAlumno_imagen);
                    db.SaveChanges();
                        
                    transaction.Commit();
                    return sReturn;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }

        }


        /*Método que busca un alumno con filtro de apellido
         * 
         * Parametros: 
         *              
         *              pApellido : String filtro para buscar alumnos
         *  Retornos: List<Object>
         *              "" : Transaccion Correcta
         *              null : Error en la transaccion
         *          
         * 
         */
        public List<Object> BuscarAlumnoPorApellido(int pDni)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                try
                {
                    if (pDni == 0) //sin filtro
                    {
                        var alumnosPorApellido = from alumno in db.alumno
                                                 select new
                                                 {
                                                     alu_nombre = alumno.nombre,
                                                     alu_apellido = alumno.apellido,
                                                     alu_dni = alumno.dni,
                                                 };
                        return alumnosPorApellido.ToList<Object>();
                    }
                    else //con filtro de apellido
                    {
                        var alumnosPorApellido = from alumno in db.alumno
                                                 where alumno.dni == pDni
                                                 select new
                                                 {
                                                     alu_nombre = alumno.nombre,
                                                     alu_apellido = alumno.apellido,
                                                     alu_dni = alumno.dni,
                                                 };
                        return alumnosPorApellido.ToList<Object>();
                    }
                    
                }
                catch (Exception ex)
                {
                    sReturn = ex.Message;
                    return null;
                }
            }
        }

        /*Método que permite eliminar alumno
         * 
         * Parametros: 
         *              pDni : Entero numero de DNI del alumno a eliminar
         *  Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *          
         * 
         */
        public string EliminarAlumno(int pDni)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    alumno alumnoBorrar = ObtenerAlumnoPorDNI(pDni);
                    db.alumno.Attach(alumnoBorrar);
                    db.alumno.Remove(alumnoBorrar);
                    db.SaveChanges();
                    transaction.Commit();
                    return sReturn;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }
    }
}
