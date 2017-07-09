using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    /*
     * Clase que nos permite gestionar profesores
     */
    public class GestorProfesores
    {

        /*
         * Método que nos permite obtener a un profesor buscandolo por DNI
         * Parámetros:
         *              pDni:entero que indica el dni del profesor a buscar
         * Retornos:
         *              profesor encontrado, o si no estaba devuelve null
         */
        public profesor ObtenerProfesorPorDNI(int pDni)
        {
            using (var db = new JJSSEntities())
            {
                var profeEncontrado = from profe in db.profesor
                                      where profe.dni == pDni
                                      select profe;
                return profeEncontrado.FirstOrDefault();
            }
        }


        /*Método que permite crear un nuevo profe
         * Valida si el profe ya fue creado comparando por DNI
         * 
         * Parametros: 
         *              pNombre : String nombre del profe
         *              pApellido : String apellido del profesor
         *              pFechaNacimiento : DateTime fecha de nacimiento del profesor
         *              pIdFaja : Entero id de la faja del profesor
         *              pIdCategoria : Entero id de la categoria a la que pertenece el profesor
         *              pSexo : Short 0 Mujer 1 Hombre
         *              pDni : Entero numero de DNI del profesor
         *              pTelefono : Entero numero de telefono del profesor
         *              pMail : String mail del profesor
         *              pIdDireccion : Entero id de la direccion del profesor
         *              pTelEmergencia : Entero numero de telefono de emergencia del profesor
         *  Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              Profesor existente
         *          
         * 
         */
        public string RegistrarProfesor(string pNombre, string pApellido, DateTime? pFechaNacimiento, int? pIdFaja,
            short? pSexo, int pDni, int pTelefono, string pMail, int pTelEmergencia, byte[] pImagen,
            string pCalle, int? pNumero, string pDpto, int? pPiso, int pIdCiudad)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    GestorUsuarios nuevoUsuario = new GestorUsuarios();
                    string nombreUsuario = pNombre + " " + pApellido;
                    string login = pNombre.Substring(0, 1).ToLower();
                    login += pApellido.ToLower();
                    string iduser = nuevoUsuario.GenerarNuevoUsuario(login, pDni.ToString(), 2, pMail, nombreUsuario);
                    int idUsuario;
                    if (int.TryParse(iduser, out idUsuario) == false)
                    {
                        return iduser;
                    }
                    seguridad_usuario usuario = db.seguridad_usuario.Find(idUsuario);

                    faja fajaElegida = db.faja.Find(pIdFaja);
                    //+Rever categorias en la BD
                    //categoria catElegida = db.categoria.Find(pIdCategoria);

                    if (ObtenerProfesorPorDNI(pDni) != null)
                    {
                        return "Profesor existente";
                    }
                    profesor nuevoProfesor;


                    ciudad ciudadElegida = db.ciudad.Find(pIdCiudad);


                    if (pCalle != "" && pNumero != null) //si ingresa direccion
                    {
                        direccion nuevaDireccion;
                        nuevaDireccion = new direccion()
                        {
                            calle1 = pCalle,
                            departamento = pDpto,
                            numero = pNumero,
                            piso = pPiso,
                            ciudad = ciudadElegida
                        };
                        db.direccion.Add(nuevaDireccion);

                        nuevoProfesor = new profesor()
                        {
                            nombre = pNombre,
                            apellido = pApellido,
                            fecha_nacimiento = pFechaNacimiento,
                            faja = fajaElegida,
                            sexo = pSexo,
                            dni = pDni,
                            telefono = pTelefono,
                            mail = pMail,
                            direccion = nuevaDireccion,
                            fecha_ingreso = DateTime.Today,
                            telefono_emergencia = pTelEmergencia,
                            seguridad_usuario = usuario
                        };
                    }
                    else //no ingresa direccion
                    {
                        nuevoProfesor = new profesor()
                        {
                            nombre = pNombre,
                            apellido = pApellido,
                            fecha_nacimiento = pFechaNacimiento,
                            faja = fajaElegida,
                            sexo = pSexo,
                            dni = pDni,
                            telefono = pTelefono,
                            mail = pMail,
                            fecha_ingreso = DateTime.Today,
                            telefono_emergencia = pTelEmergencia,
                            seguridad_usuario = usuario
                        };
                    }

                    db.profesor.Add(nuevoProfesor);
                    
                    db.SaveChanges();
                    profesor_imagen nuevoProfesor_imagen = new profesor_imagen()
                    {
                        id_profesor = nuevoProfesor.id_profesor,
                        imagen = pImagen
                    };
                    db.profesor_imagen.Add(nuevoProfesor_imagen);
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

        /*
         * Método que devuelve un listado de todas los profesores cargadas
         * Retorno: List<profesor>
         *          Retorna toda lista de profesores
         */
        public List<profesor> ObtenerProfesores()
        {
            using (var db = new JJSSEntities())
            {
                return db.profesor.ToList();
            }
        }


    }
}
