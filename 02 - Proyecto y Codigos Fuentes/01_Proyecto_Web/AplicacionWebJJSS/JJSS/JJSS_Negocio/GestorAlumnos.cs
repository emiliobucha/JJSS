using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using JJSS_Negocio.Resultados;
using JJSS_Negocio.Constantes;

namespace JJSS_Negocio
{

    /*
     * Clase que nos permite gestionar alumnos
     */
    public class GestorAlumnos
    {

        /*
         * Método que nos permite obtener a un alumno buscandolo por DNI
         * Parámetros:
         *              pDni:entero que indica el dni del alumno a buscar
         * Retornos:
         *              Alumno encontrado, o si no estaba devuelve null
         */
        public alumno ObtenerAlumnoPorDNI(string pDni)
        {
            using (var db = new JJSSEntities())
            {
                var alumnoEncontrado = from alu in db.alumno
                                       where alu.dni == pDni && alu.baja_logica == 1
                                       select alu;
                return alumnoEncontrado.FirstOrDefault();
            }
        }

        /*
        * Método que nos permite obtener a un alumno buscandolo por DNI
        * Parámetros:
        *              pDni:entero que indica el dni del alumno a buscar
        * Retornos:
        *              Alumno encontrado, o si no estaba devuelve null
        */
        public alumno ObtenerAlumnoPorDNITipo(int pTipo, string pDni)
        {
            using (var db = new JJSSEntities())
            {
                var alumnoEncontrado = from alu in db.alumno
                    where alu.dni == pDni && alu.baja_logica == 1 && alu.id_tipo_documento == pTipo
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
         *              pCalle: string calle del alumno
         *              pnumero: entero nullable numero de la calle
         *              pdpto: string nombre del departament
         *              ppido: entero nullable piso del departamentp
         *              pidciudad: entero que representa el id de la ciudad del alumno
         *              pTelEmergencia : Entero numero de telefono de emergencia del alumno
         *              pimagen : arregl de bytes que representa la foto de perfil
         *  Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              Alumno existente
         *          
         * 
         */
        public string RegistrarAlumno(string pNombre, string pApellido, DateTime? pFechaNacimiento,
            short? pSexo, int pTipo, string pDni, long pTelefono, string pMail, long pTelEmergencia, byte[] pImagen,
            string pCalle, int? pNumero, string pDpto, int? pPiso, int? pIdCiudad, string pTorre, int pNacionalidad)
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
                    string iduser = nuevoUsuario.GenerarNuevoUsuario(login, pDni, 3, pMail, nombreUsuario);
                    int idUsuario;
                    if (int.TryParse(iduser, out idUsuario) == false)
                    {
                        return iduser;
                    }
                    seguridad_usuario usuario = db.seguridad_usuario.Find(idUsuario);


                    if (ObtenerAlumnoPorDNITipo(pTipo,pDni) != null)
                    {
                        throw new Exception("El alumno ya existe");
                    }
                    alumno nuevoAlumno;

                    ciudad ciudadElegida = db.ciudad.Find(pIdCiudad);

                    //+ crear direccion vacia
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
                            torre = pTorre,

                        };
                        db.direccion.Add(nuevaDireccion);

                        nuevoAlumno = new alumno()
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
                            baja_logica = 1,
                            id_estado = ConstantesEstado.ALUMNOS_CREADO,
                            id_tipo_documento = pTipo,
                            id_pais = pNacionalidad
                        };
                    }
                    else //no ingresa direccion
                    {
                        nuevoAlumno = new alumno()
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
                            baja_logica = ConstatesBajaLogica.BAJA_LOGICA,
                            id_estado = ConstantesEstado.ALUMNOS_CREADO,
                            id_tipo_documento = pTipo,
                            id_pais = pNacionalidad
                        };
                    }

                    db.alumno.Add(nuevoAlumno);

                    db.SaveChanges();


                    byte[] arrayImagen = pImagen;
                    if (arrayImagen.Length > 7000)
                    {
                        arrayImagen = new byte[0];
                    }

                    string imagenUrl = modUtilidades.SaveImage(pImagen, pNombre, "alumnos");



                    alumno_imagen nuevoAlumno_imagen = new alumno_imagen()
                    {
                        id_alumno = nuevoAlumno.id_alumno,
                        imagen = arrayImagen,
                        imagen_url = imagenUrl
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


        /*Método que busca un alumno
         * 
         * 
         *  Retornos: List<Object>
         *              "" : Transaccion Correcta
         *              null : Error en la transaccion
         *          
         * 
         */
        public List<alumno> BuscarAlumno()
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                try
                {
                    var alumnosPorApellido = from alumno in db.alumno

                                             where alumno.baja_logica == 1
                                             orderby alumno.apellido
                                             select alumno;
                    return alumnosPorApellido.ToList<alumno>();


                }
                catch (Exception ex)
                {
                    sReturn = ex.Message;
                    return null;
                }
            }
        }

        public List<AlumnoConEstado> BuscarAlumnoConEstado(int[] filtroEstados, string filtroApellido, string filtroDni)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                try
                {
                    int filtroEstado0 = filtroEstados[0];
                    int filtroEstado1 = filtroEstados[1];
                    int filtroEstado2 = filtroEstados[2];
                    int filtroEstado3 = filtroEstados[3];
                    int filtroEstado4 = filtroEstados[4];
                    if (string.IsNullOrEmpty(filtroDni))
                    {
                        var alumnosPorApellido = from alumno in db.alumno
                                                 join est in db.estado on alumno.id_estado equals est.id_estado
                                                 where
                                                 (alumno.id_estado == filtroEstado0 || alumno.id_estado == filtroEstado1 || alumno.id_estado == filtroEstado2 ||
                                                 alumno.id_estado == filtroEstado3 || alumno.id_estado == filtroEstado4)
                                                 && alumno.apellido.StartsWith(filtroApellido)
                                                 orderby alumno.apellido
                                                 select new AlumnoConEstado
                                                 {
                                                     alu_apellido = alumno.apellido,
                                                     alu_nombre = alumno.nombre,
                                                     alu_dni = alumno.dni,
                                                     alu_estado = est.nombre,
                                                     alu_id_estado = est.id_estado,
                                                 };
                        return alumnosPorApellido.ToList();
                    }
                    else
                    {
                        var alumnosPorApellido = from alumno in db.alumno
                                                 join est in db.estado on alumno.id_estado equals est.id_estado
                                                 where
                                                 (alumno.id_estado == filtroEstado0 || alumno.id_estado == filtroEstado1 || alumno.id_estado == filtroEstado2 ||
                                                 alumno.id_estado == filtroEstado3 || alumno.id_estado == filtroEstado4)
                                                 && alumno.apellido.StartsWith(filtroApellido)
                                                 && alumno.dni == filtroDni
                                                 orderby alumno.apellido
                                                 select new AlumnoConEstado
                                                 {
                                                     alu_apellido = alumno.apellido,
                                                     alu_nombre = alumno.nombre,
                                                     alu_dni = alumno.dni,
                                                     alu_estado = est.nombre,
                                                     alu_id_estado = est.id_estado,
                                                 };
                        return alumnosPorApellido.ToList<AlumnoConEstado>();
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
        public string EliminarAlumno(string pDni)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    var alumnoEncontrado = from alu in db.alumno
                                           where alu.dni == pDni
                                           select alu;
                    alumno alumnoBorrar = alumnoEncontrado.FirstOrDefault();

                    if (alumnoBorrar == null) throw new Exception("El alumno no existe");
                    alumnoBorrar.baja_logica = 0;
                    alumnoBorrar.id_estado = ConstantesEstado.ALUMNOS_DE_BAJA;
                    db.SaveChanges();

                    seguridad_usuario usuario = (from usu in db.seguridad_usuario
                                                 where usu.id_usuario == alumnoBorrar.id_usuario
                                                 select usu).FirstOrDefault();
                    if (usuario == null) throw new Exception("El usuario no existe");
                    usuario.baja_logica = 0;
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
         * Permite modificar los datos personales de un alumno
         * Parametros: pNombre : String nombre del alumno
         *              pApellido : String apellido del alumno
         *              pDni : Entero numero de DNI del alumno
         *              pFecha: DateTime fecha de nacimiento dle alumno
         *              pSexo: short sexo del alumno
         * Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              NO: no encontro el alumno
         * 
         */
        public string ModificarAlumno(int pTipo, string pDni, string pNombre, string pApellido, DateTime? pFecha, short? pSexo, string pUsuario, int? pPais)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    var alumnoModificar = db.alumno.FirstOrDefault(x => x.dni == pDni && x.id_tipo_documento == pTipo);
                    
                    if (alumnoModificar == null) throw new Exception("El usuario no existe");
                    alumnoModificar.apellido = pApellido;
                    alumnoModificar.nombre = pNombre;
                    if (pFecha != null) alumnoModificar.fecha_nacimiento = pFecha;
                    if (pSexo != null) alumnoModificar.sexo = pSexo;
                    if (pPais != null)
                        alumnoModificar.id_pais = pPais;

                    db.SaveChanges();

                    var usuarioEncontrado =
                        db.seguridad_usuario.FirstOrDefault(x => x.id_usuario == alumnoModificar.id_usuario);
                    if (usuarioEncontrado != null)
                    {
                        usuarioEncontrado.nombre = alumnoModificar.nombre + " " + alumnoModificar.apellido;
                        if (string.IsNullOrEmpty(pUsuario))
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
                    return ex.Message;
                }
            }
        }


        /*
         * Permite modificar los datos de contacto de un alumno
         * Parametros: pDni : Entero numero de DNI del alumno
         *              pTelefono : Entero numero de telefono del alumno
         *              pMail : String mail del alumno
         *              pCalle: string calle del alumno
         *              pnumero: entero nullable numero de la calle
         *              pdpto: string nombre del departament
         *              ppido: entero nullable piso del departamentp
         *              pidciudad: entero que representa el id de la ciudad del alumno
         *              pTelEmergencia : Entero numero de telefono de emergencia del alumno
         * Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              NO: no encontro el alumno
         * 
         */
        public string ModificarAlumnoContacto(string pCalle, string pDepto, int? pNumero, int? pPiso, long pTelefono, long pTelUrgencia, string pMail, int pTipo, string pDni, int? pIdCiudad, string pTorre)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    var alumnoEncontrado = from alu in db.alumno
                                           where alu.dni == pDni && alu.id_tipo_documento == pTipo
                                           select alu;
                    alumno alumnoModificar = alumnoEncontrado.FirstOrDefault();

                    if (alumnoModificar == null) throw new Exception("El usuario no existe");
                    alumnoModificar.telefono = pTelefono;
                    alumnoModificar.telefono_emergencia = pTelUrgencia;
                    alumnoModificar.mail = pMail;

                    //busco la direccion 
                    var direccionAlumno = from dir in db.direccion
                                          join alu in db.alumno on dir.id_direccion equals alu.id_direccion
                                          where alu.dni == pDni
                                          select dir;

                    direccion direccionModificar = direccionAlumno.FirstOrDefault();

                    var usuarioEncontrado =
                        db.seguridad_usuario.FirstOrDefault(x => x.id_usuario == alumnoModificar.id_usuario);
                    if (usuarioEncontrado != null)
                    {
                        usuarioEncontrado.nombre = alumnoModificar.nombre + " " + alumnoModificar.apellido;

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
                                torre = pTorre,

                            };
                            db.direccion.Add(nuevaDireccion);

                            //alumnoModificar.direccion = nuevaDireccion;
                            alumnoModificar.id_direccion = nuevaDireccion.id_direccion;
                            db.SaveChanges();

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
                    return ex.Message;
                }
            }
        }

        /*
         * Permite obtener los datos de un alumno a partir de su usuario
         * Parametros: pIdUsuario: entero que representa el id de un usuario
         * Retornos: alumnoencontrado
         *          null: si no encuentra ninguno
         * 
         */
        public alumno ObtenerAlumnoPorIdUsuario(int pIdUsuario)
        {
            using (var db = new JJSSEntities())
            {
                var alumnoEncontrado = from usuario in db.seguridad_usuario
                                       join alu in db.alumno on usuario.id_usuario equals alu.id_usuario
                                       where usuario.id_usuario == pIdUsuario && alu.baja_logica == 1
                                       select alu;
                return alumnoEncontrado.FirstOrDefault();
            }
        }

        /*
         * Permite obtener la direccion de un alumno a partir del id alumno
         * Parametros: pIdUsuario: entero que representa el id de un usuario
         * Retornos: alumnoencontrado
         *          null: si no encuentra ninguno
         * 
         */
        public DireccionAlumno ObtenerDireccionAlumno(int pIdAlumno)
        {
            using (var db = new JJSSEntities())
            {
                var direccionEncontrada = from dir in db.direccion
                                          join alu in db.alumno on dir.id_direccion equals alu.id_direccion
                                          join ciu in db.ciudad on dir.id_ciudad equals ciu.id_ciudad
                                          where alu.id_alumno == pIdAlumno
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


        /*
         * Metodo que busca la faja que tiene el alumno para un tipo clase
         * Parametros:  pDniAlumno : entero- representa el dni del alumno
         *              pIDTipoClase: entero - representa el id del tipo de clase
         * Retornos:    faja encontrada
         *              null
         * 
         */
        public faja ObtenerFajaAlumno(int pIdAlumno, int pIdTipoClase)
        {

            using (var db = new JJSSEntities())
            {
                var fajaEncontrada = from alu in db.alumno
                                     join axf in db.alumnoxfaja on alu.id_alumno equals axf.id_alumno
                                     join faj in db.faja on axf.id_faja equals faj.id_faja
                                     where alu.id_alumno == pIdAlumno && faj.id_tipo_clase == pIdTipoClase && alu.baja_logica == 1 && axf.actual == 1
                                     select faj;
                return fajaEncontrada.FirstOrDefault();
            }

        }



        /*
         * Metodo que asigna una faja a un alumno
         * Parametros:  pDniAlumno : entero- representa el dni del alumno
         *              pIDFaja: entero - representa el id de la faja
         * Retornos:    "": transaccion correcta
         *              ex.message: error en la BD
         * 
         */
        public string AsignarFaja(string pDniAlumno, int pIDFaja)
        {
            alumno alu = ObtenerAlumnoPorDNI(pDniAlumno);
            DateTime fechaActual = DateTime.Today.Date;
            using (var db = new JJSSEntities())
            {
                try
                {

                    alumnoxfaja nuevoAxF;
                    nuevoAxF = new alumnoxfaja
                    {
                        fecha = fechaActual,
                        id_alumno = alu.id_alumno,
                        id_faja = pIDFaja
                    };

                    db.alumnoxfaja.Add(nuevoAxF);
                    db.SaveChanges();
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string CambiarFotoPerfil(string pDni, byte[] pImagen)
        {

            using (var db = new JJSSEntities())
            {
                try

                {
                    var alumno = ObtenerAlumnoPorDNI(pDni);
                    var arrayImagen = pImagen;

                    var alumnoImagen = db.alumno_imagen.FirstOrDefault(imag => imag.id_alumno == alumno.id_alumno);

                    if (alumnoImagen == null)
                    {
                        alumnoImagen = new alumno_imagen()
                        {
                            id_alumno = alumno.id_alumno,
                            imagen = pImagen
                        };
                        if (arrayImagen.Length > 7000)
                        {
                            arrayImagen = new byte[0];
                        }
                      
                        var imagenUrl = modUtilidades.SaveImage(pImagen, alumno.dni + alumno.nombre, "alumnos");

                        alumnoImagen.imagen = arrayImagen;
                        alumnoImagen.imagen_url = imagenUrl;
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

                            var imagenUrl = modUtilidades.SaveImage(pImagen, alumno.dni + alumno.nombre, "alumnos");

                            alumnoImagen.imagen = arrayImagen;
                            alumnoImagen.imagen_url = imagenUrl;
                            db.SaveChanges();
                        }

                    }

                    if (alumno != null)
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


        public alumno_imagen ObtenerImagenPerfil(int pId)
        {
            using (var db = new JJSSEntities())
            {
                try
                {

                    var alumnoImagen = db.alumno_imagen.FirstOrDefault(imag => imag.id_alumno == pId);

                    return alumnoImagen;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
