using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;
using JJSS_Negocio.Resultados;

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
        public profesor ObtenerProfesorPorDNI(string pDni)
        {
            using (var db = new JJSSEntities())
            {
                var profeEncontrado = from profe in db.profesor
                                      where profe.dni == pDni
                                      select profe;
                return profeEncontrado.FirstOrDefault();
            }
        }


        /*
         * Método que nos permite obtener a un profesor buscandolo por DNI
         * Parámetros:
         *              pDni:entero que indica el dni del profesor a buscar
         * Retornos:
         *              profesor encontrado, o si no estaba devuelve null
         */
        public profesor ObtenerProfesorPorDNITipo(int pTipo, string pDni)
        {
            using (var db = new JJSSEntities())
            {
                var profeEncontrado = from profe in db.profesor
                    where profe.dni == pDni && profe.id_tipo_documento == pTipo
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
        public string RegistrarProfesor(string pNombre, string pApellido, DateTime? pFechaNacimiento, 
            short? pSexo,int pTipo, string pDni, long pTelefono, string pMail, long pTelEmergencia, byte[] pImagen,
            string pCalle, int? pNumero, string pDpto, int? pPiso, int? pIdCiudad, string pTorre)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    GestorUsuarios nuevoUsuario = new GestorUsuarios();
                    string nombreUsuario = pNombre + " " + pApellido;
                    string login = nuevoUsuario.GenerarLogin(pNombre, pApellido);
                    string iduser = nuevoUsuario.GenerarNuevoUsuario(login, pDni, 2, pMail, nombreUsuario);
                    int idUsuario;
                    if (int.TryParse(iduser, out idUsuario) == false)
                    {
                        return iduser;
                    }
                    seguridad_usuario usuario = db.seguridad_usuario.Find(idUsuario);

                    
                    if (ObtenerProfesorPorDNITipo(pTipo,pDni) != null)
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
                            calle = pCalle,
                            departamento = pDpto,
                            numero = pNumero,
                            piso = pPiso,
                            ciudad = ciudadElegida,
                            torre=pTorre
                            
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
                            seguridad_usuario = usuario,
                            id_tipo_documento = pTipo
                           
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
                            seguridad_usuario = usuario,
                            id_tipo_documento = pTipo
                        };
                    }

                    db.profesor.Add(nuevoProfesor);
                    
                    db.SaveChanges();


                    byte[] arrayImagen = pImagen;
                    if (arrayImagen.Length > 7000)
                    {
                        arrayImagen = new byte[0];
                    }

                    string imagenUrl = modUtilidades.SaveImage(pImagen, pNombre, "profesores");

                    profesor_imagen nuevoProfesor_imagen = new profesor_imagen()
                    {
                        id_profesor = nuevoProfesor.id_profesor,
                        imagen = arrayImagen,
                        imagen_url = imagenUrl
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
         * Método que devuelve un profe segun su id
         * Parametros: pIDProfe: entero que representa el id del tipo de clase a buscar
         * Retorno: profe
         *          null
         */
        public profesor ObtenerProfesorPorID(int pIDProfe)
        {
            using (var db = new JJSSEntities())
            {
                return db.profesor.Find(pIDProfe);
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
        public string EliminarProfesor(string pDni)
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
        public string ModificarProfesor(int pTipo, string pDni, string pNombre, string pApellido, string pUsuario, int? pPais, 
            DateTime? pFechaNacimiento, short? pSexo)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    //+ se tienen que poder modificar mas datos

                    profesor profesorModificar = db.profesor.FirstOrDefault(x => x.dni == pDni && x.id_tipo_documento == pTipo);

                    if (profesorModificar == null) throw new Exception("El usuario no existe");
                    profesorModificar.apellido = pApellido;
                    profesorModificar.nombre = pNombre;
                    if (pFechaNacimiento != null) profesorModificar.fecha_nacimiento = pFechaNacimiento;
                    if (pSexo != null) profesorModificar.sexo = pSexo;
                    if (pPais != null)
                        profesorModificar.id_pais = pPais;
                    db.SaveChanges();

                    var usuarioEncontrado =
                        db.seguridad_usuario.FirstOrDefault(x => x.id_usuario == profesorModificar.id_usuario);
                    if (usuarioEncontrado != null)
                    {
                        usuarioEncontrado.nombre = profesorModificar.nombre + " " + profesorModificar.apellido;
                        if (!string.IsNullOrEmpty(pUsuario))
                        {
                            usuarioEncontrado.login = pUsuario;
                        }

                    }
                    db.SaveChanges();

                    transaction.Commit();
                    return sReturn;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
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
        public string ModificarProfesorContacto(string pCalle, string pDepto, int? pNumero, int? pPiso, long pTelefono, long pTelUrgencia, 
            string pMail, int pTipo, string pDni, int? pIdCiudad, string pTorre)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    profesor profesorModificar = db.profesor.FirstOrDefault(x => x.dni == pDni && x.id_tipo_documento == pTipo);
                    if (profesorModificar == null) throw new Exception("El usuario no existe");
                    profesorModificar.telefono = pTelefono;
                    profesorModificar.telefono_emergencia = pTelUrgencia;
                    profesorModificar.mail = pMail;

                    //busco la direccion 
                    var direccionProfesor = from dir in db.direccion
                                          join alu in db.profesor on dir.id_direccion equals alu.id_direccion
                                          where alu.dni == pDni && alu.id_tipo_documento == pTipo
                                          select dir;

                    direccion direccionModificar = direccionProfesor.FirstOrDefault();

                    var usuarioEncontrado =
                        db.seguridad_usuario.FirstOrDefault(x => x.id_usuario == profesorModificar.id_usuario);
                    if (usuarioEncontrado != null)
                    {
                        usuarioEncontrado.nombre = profesorModificar.nombre + " " + profesorModificar.apellido;
 

                    }




                    if (direccionModificar == null)//no tenia direccion direccion
                    {
                        if (pCalle.CompareTo("") != 0 && pNumero != null) //cargo una direcicon, entonces creo una
                        {
                            direccion nuevaDireccion;
                            nuevaDireccion = new direccion
                            {
                                calle = pCalle,
                                departamento = pDepto,
                                numero = pNumero,
                                piso = pPiso,
                                id_ciudad = pIdCiudad,
                                torre=pTorre,
                            };
                            db.direccion.Add(nuevaDireccion);
                            profesorModificar.direccion = nuevaDireccion;
                        }
                    }
                    else //tenia direccion, entonces la modifico
                    {
                        direccionModificar.calle = pCalle;
                        direccionModificar.departamento = pDepto;
                        direccionModificar.numero = pNumero;
                        direccionModificar.piso = pPiso;
                        direccionModificar.id_ciudad = pIdCiudad;
                        direccionModificar.torre = pTorre;
                    }

                    db.SaveChanges();
                    transaction.Commit();
                    return sReturn;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
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
                                          select new DireccionAlumno
                                          {
                                              calle = dir.calle,
                                              numero = dir.numero,
                                              depto = dir.departamento,
                                              piso = dir.piso,
                                              idCiudad = dir.id_ciudad,
                                              idProvincia = ciu.id_provincia,
                                              torre=dir.torre,
                                          };
                return modUtilidadesTablas.ToDataTable(direccionEncontrada.ToList());
            }
        }

        public DireccionAlumno ObtenerDireccionProfesor2(int pIdProfe)
        {
            using (var db = new JJSSEntities())
            {
                var direccionEncontrada = from dir in db.direccion
                                          join pro in db.profesor on dir.id_direccion equals pro.id_direccion
                                          join ciu in db.ciudad on dir.id_ciudad equals ciu.id_ciudad
                                          where pro.id_profesor == pIdProfe
                                          select new DireccionAlumno
                                          {
                                              calle = dir.calle,
                                              numero = dir.numero,
                                              depto = dir.departamento,
                                              piso = dir.piso,
                                              idCiudad = dir.id_ciudad,
                                              idProvincia = ciu.id_provincia,
                                              torre = dir.torre,
                                          };
                return direccionEncontrada.FirstOrDefault();
            }
        }

        public string CambiarFotoPerfil(int pTipo, string pDni, byte[] pImagen)
        {






            using (var db = new JJSSEntities())
            {
                try

                {
                    var profesor = ObtenerProfesorPorDNITipo(pTipo, pDni);
                    var arrayImagen = pImagen;

                    var profesorImagen = db.profesor_imagen.FirstOrDefault(imag => imag.id_profesor == profesor.id_profesor);

                    if (profesorImagen == null)
                    {
                        profesorImagen = new profesor_imagen()
                        {
                            id_profesor = profesor.id_profesor,
                            imagen = pImagen
                        };
                        if (arrayImagen.Length > 7000)
                        {
                            arrayImagen = new byte[0];
                        }

                        var imagenUrl = modUtilidades.SaveImage(pImagen, profesor.dni + profesor.nombre, "profesores");

                        profesorImagen.imagen = arrayImagen;
                        profesorImagen.imagen_url = imagenUrl;
                        db.SaveChanges();

                    }
                    else
                    {
                        if (pImagen != null && pImagen.Length > 0)
                        {
                            arrayImagen = pImagen;
                            if (arrayImagen.Length > 7000)
                            {
                                arrayImagen = new byte[0];
                            }

                            var imagenUrl = modUtilidades.SaveImage(pImagen, profesor.dni + profesor.nombre, "profesores");

                            profesorImagen.imagen = arrayImagen;
                            profesorImagen.imagen_url = imagenUrl;
                            db.SaveChanges();
                        }

                    }




                    if (profesor != null)
                    {
                        db.SaveChanges();
                        return "";
                    }
                    else
                    {
                        throw new Exception("El usuario no existe");
                    }



                }
                catch (Exception)
                {
                    throw new Exception("El usuario no existe");
                }
            }
        }


        public profesor_imagen ObtenerImagenPerfil(int pID)
        {
            using (var db = new JJSSEntities())
            {
                try
                {

                    var profesorImagen = db.profesor_imagen.FirstOrDefault(imag => imag.id_profesor == pID);

                    return profesorImagen;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

    }
}
