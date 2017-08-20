using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;

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


        /*
         * Metodo que devuelve un listado con nombre, apellido y dni de todos los profesores
         * Parametros: pDni: dni del profe a buscar
         * Retorno: List<Object>
         * */
        public List<Object> BuscarProfePorDni(int pDni)
        {
            using (var db = new JJSSEntities())
            {
                if (pDni == 0)
                {
                    var profesores = from profe in db.profesor
                                     select new
                                     {
                                         alu_dni=profe.dni,
                                         alu_nombre=profe.nombre,
                                         alu_apellido=profe.apellido
                                     };
                    return profesores.ToList<Object>();
                }
                else
                {
                    var profesores = from profe in db.profesor
                                     where profe.dni==pDni
                                     select new
                                     {
                                         alu_dni = profe.dni,
                                         alu_nombre = profe.nombre,
                                         alu_apellido = profe.apellido
                                     };
                    return profesores.ToList<Object>();
                }
                
            }
        }

       

        /*Método que permite eliminar profe
         * 
         * Parametros: 
         *              pDni : Entero numero de DNI del profe a eliminar
         *  Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         */
        public string EliminarProfesor(int pDni)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    profesor profeABorrar = ObtenerProfesorPorDNI(pDni);
                    db.profesor.Attach(profeABorrar);
                    db.profesor.Remove(profeABorrar);
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
         * Permite modificar algunos datos de un profesor
         * Parametros: pNombre : String nombre del profesor
         *              pApellido : String apellido del profesor
         *              pDni : Entero numero de DNI del profesor
         * Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         * 
         */
        public string Modificarprofesor(int pDni, string pNombre, string pApellido)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    //+ se tienen que poder modificar mas datos
                    
                    profesor profesorModificar = ObtenerProfesorPorDNI(pDni);
                    if (profesorModificar == null) return "NO";
                    profesorModificar.apellido = pApellido;
                    profesorModificar.nombre = pNombre;
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
         * Permite modificar los datos de contacto de un profesor
         * Parametros: pDni : Entero numero de DNI del profesor
         *              pTelefono : Entero numero de telefono del profesor
         *              pMail : String mail del profesor
         *              pCalle: string calle del profesor
         *              pnumero: entero nullable numero de la calle
         *              pdpto: string nombre del departament
         *              ppido: entero nullable piso del departamentp
         *              pidciudad: entero que representa el id de la ciudad del profesor
         *              pTelEmergencia : Entero numero de telefono de emergencia del profesor
         * Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              NO: no encontro el profesor
         * 
         */
        public string ModificarProfesor(string pCalle, string pDepto, int? pNumero, int? pPiso, int pTelefono, int pTelUrgencia, string pMail, int pDni, int pIdCiudad)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    profesor profesorModificar = ObtenerProfesorPorDNI(pDni);
                    if (profesorModificar == null) return "NO";
                    profesorModificar.telefono = pTelefono;
                    profesorModificar.telefono_emergencia = pTelUrgencia;
                    profesorModificar.mail = pMail;

                    //busco la direccion 
                    var direccionProfesor = from dir in db.direccion
                                          join alu in db.profesor on dir.id_direccion equals alu.id_direccion
                                          where alu.dni == pDni
                                          select dir;

                    direccion direccionModificar = direccionProfesor.FirstOrDefault();

                    if (direccionModificar == null)//no tenia direccion direccion
                    {
                        if (pCalle.CompareTo("") != 0 && pNumero != null) //cargo una direcicon, entonces creo una
                        {
                            direccion nuevaDireccion;
                            nuevaDireccion = new direccion
                            {
                                calle1 = pCalle,
                                departamento = pDepto,
                                numero = pNumero,
                                piso = pPiso,
                                id_ciudad = pIdCiudad

                            };
                            db.direccion.Add(nuevaDireccion);
                            profesorModificar.direccion = nuevaDireccion;
                        }
                    }
                    else //tenia direccion, entonces la modifico
                    {
                        direccionModificar.calle1 = pCalle;
                        direccionModificar.departamento = pDepto;
                        direccionModificar.numero = pNumero;
                        direccionModificar.piso = pPiso;
                        direccionModificar.id_ciudad = pIdCiudad;
                    }

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
         * Permite obtener los datos de un profesor a partir de su usuario
         * Parametros: pIdUsuario: entero que representa el id de un usuario
         * Retornos: profesorencontrado
         *          null: si no encuentra ninguno
         * 
         */
        public profesor ObtenerProfesorPorIdUsuario(int pIdUsuario)
        {
            using (var db = new JJSSEntities())
            {
                var profesorEncontrado = from usuario in db.seguridad_usuario
                                       join alu in db.profesor on usuario.id_usuario equals alu.id_usuario
                                       where usuario.id_usuario == pIdUsuario
                                       select alu;
                return profesorEncontrado.FirstOrDefault();
            }
        }


        /*
         * Permite obtener la direccion de un profesor a partir del id profesor
         * Parametros: pIdUsuario: entero que representa el id de un profesor
         * Retornos: datatable con la direccion
         *          null: si no encuentra ninguno
         * 
         */
        public DataTable ObtenerDireccionProfesor(int pIdProfe)
        {
            using (var db = new JJSSEntities())
            {
                var direccionEncontrada = from dir in db.direccion
                                          join pro in db.profesor on dir.id_direccion equals pro.id_direccion
                                          join ciu in db.ciudad on dir.id_ciudad equals ciu.id_ciudad
                                          where pro.id_profesor == pIdProfe
                                          select new
                                          {
                                              calle = dir.calle1,
                                              numero = dir.numero,
                                              depto = dir.departamento,
                                              piso = dir.piso,
                                              idCiudad = dir.id_ciudad,
                                              idProvincia = ciu.id_provincia
                                          };
                return modUtilidadesTablas.ToDataTable(direccionEncontrada.ToList());
            }
        }

    }
}
