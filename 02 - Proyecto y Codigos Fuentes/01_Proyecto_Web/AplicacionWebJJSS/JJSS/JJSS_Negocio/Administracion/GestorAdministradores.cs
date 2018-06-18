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
    public class GestorAdministradores
    {


        /*
        * Método que nos permite obtener a un administrador buscandolo por DNI
        * Parámetros:
        *              pDni:entero que indica el dni del alumno a buscar
        * Retornos:
        *              administrador encontrado, o si no estaba devuelve null
        */
        public administrador ObtenerAdminPorDNITipo(int pTipo, string pDni)
        {
            using (var db = new JJSSEntities())
            {
                return db.administrador.FirstOrDefault(x => x.id_tipo_documento == pTipo && x.dni == pDni);
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
        public string RegistrarAdmin(string pNombre, string pApellido, DateTime? pFechaNacimiento,
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


                    if (ObtenerAdminPorDNITipo(pTipo,pDni) != null)
                    {
                        throw new Exception("El alumno ya existe");
                    }
                    administrador nuevoAdmin;

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

                        nuevoAdmin = new administrador()
                        {
                            nombre = pNombre,
                            apellido = pApellido,
                            fecha_nacimiento = pFechaNacimiento,
                            sexo = pSexo,
                            dni = pDni,
                            telefono = pTelefono,
                            mail = pMail,
                            direccion = nuevaDireccion,
                            telefono_emergencia = pTelEmergencia,
                            seguridad_usuario = usuario,
                            baja_logica = 1,
                            id_tipo_documento = pTipo,
                            id_pais = pNacionalidad
                        };
                    }
                    else //no ingresa direccion
                    {
                        nuevoAdmin = new administrador()
                        {
                            nombre = pNombre,
                            apellido = pApellido,
                            fecha_nacimiento = pFechaNacimiento,
                            sexo = pSexo,
                            dni = pDni,
                            telefono = pTelefono,
                            mail = pMail,
                            telefono_emergencia = pTelEmergencia,
                            seguridad_usuario = usuario,
                            baja_logica = ConstatesBajaLogica.BAJA_LOGICA,
                            id_tipo_documento = pTipo,
                            id_pais = pNacionalidad
                        };
                    }

                    db.administrador.Add(nuevoAdmin);

                    db.SaveChanges();


                    byte[] arrayImagen = pImagen;
                    if (arrayImagen.Length > 7000)
                    {
                        arrayImagen = new byte[0];
                    }

                    string imagenUrl = modUtilidades.SaveImage(pImagen, pNombre, "admins");



                    administrador_imagen administrador_imagen = new administrador_imagen()
                    {
                        id_administrador = nuevoAdmin.id_admin,
                        imagen = arrayImagen,
                        imagen_url = imagenUrl
                    }; 
                    db.administrador_imagen.Add(administrador_imagen);
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
         * Permite modificar los datos personales de un admin
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
        public string ModificarAdmin(int pTipo, string pDni, string pNombre, string pApellido, DateTime? pFecha, short? pSexo, string pUsuario, int? pPais)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    var adminModificar = db.administrador.FirstOrDefault(x => x.dni == pDni && x.id_tipo_documento == pTipo);
                    
                    if (adminModificar == null) throw new Exception("El usuario no existe");
                    adminModificar.apellido = pApellido;
                    adminModificar.nombre = pNombre;
                    if (pFecha != null) adminModificar.fecha_nacimiento = pFecha;
                    if (pSexo != null) adminModificar.sexo = pSexo;
                    if (pPais != null) adminModificar.id_pais = pPais;

                    db.SaveChanges();

                    var usuarioEncontrado =
                        db.seguridad_usuario.FirstOrDefault(x => x.id_usuario == adminModificar.id_usuario);
                    if (usuarioEncontrado != null)
                    {
                        usuarioEncontrado.nombre = adminModificar.nombre + " " + adminModificar.apellido;
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
                    throw ex;
                }
            }
        }


        /*
         * Permite modificar los datos de contacto de un admin
         * Parametros: pDni : Entero numero de DNI del admin
         *              pTelefono : Entero numero de telefono del admin
         *              pMail : String mail del admin
         *              pCalle: string calle del admin
         *              pnumero: entero nullable numero de la calle
         *              pdpto: string nombre del departament
         *              ppido: entero nullable piso del departamentp
         *              pidciudad: entero que representa el id de la ciudad del admin
         *              pTelEmergencia : Entero numero de telefono de emergencia del admin
         * Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              NO: no encontro el admin
         * 
         */
        public string ModificarAdminContacto(string pCalle, string pDepto, int? pNumero, int? pPiso, long pTelefono, long pTelUrgencia, string pMail, int pTipo, string pDni, int? pIdCiudad, string pTorre)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    var adminCheck =
                        db.administrador.FirstOrDefault(x => x.id_tipo_documento == pTipo && x.dni == pDni);

                    if (adminCheck == null) throw new Exception("El usuario no existe");
                    adminCheck.telefono = pTelefono;
                    adminCheck.telefono_emergencia = pTelUrgencia;
                    adminCheck.mail = pMail;

                    //busco la direccion 
                    var direccionModificar = db.direccion.FirstOrDefault(x => x.id_direccion == adminCheck.id_direccion);

                    var usuarioEncontrado =
                        db.seguridad_usuario.FirstOrDefault(x => x.id_usuario == adminCheck.id_usuario);
                    if (usuarioEncontrado != null)
                    {
                        usuarioEncontrado.nombre = adminCheck.nombre + " " + adminCheck.apellido;

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
                            db.SaveChanges();

                            adminCheck.id_direccion = nuevaDireccion.id_direccion;
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
        public administrador ObtenerAdminPorIdUsuario(int pIdUsuario)
        {
            using (var db = new JJSSEntities())
            {
                administrador admin = db.administrador.FirstOrDefault(x => x.id_usuario == pIdUsuario);
                return admin;
            }
        }

        /*
         * Permite obtener la direccion de un alumno a partir del id alumno
         * Parametros: pIdUsuario: entero que representa el id de un usuario
         * Retornos: alumnoencontrado
         *          null: si no encuentra ninguno
         * 
         */
        public DireccionAlumno ObtenerDireccionAdmin(int pIdAdmin)
        {
            using (var db = new JJSSEntities())
            {
                var direccionEncontrada = from dir in db.direccion
                                          join ad in db.administrador on dir.id_direccion equals ad.id_direccion
                                          join ciu in db.ciudad on dir.id_ciudad equals ciu.id_ciudad
                                          where ad.id_admin == pIdAdmin
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
                    var admin = ObtenerAdminPorDNITipo(pTipo,pDni);
                    var arrayImagen = pImagen;

                    var adminImagen = db.administrador_imagen.FirstOrDefault(imag => imag.id_administrador == admin.id_admin);

                    if (adminImagen == null)
                    {
                        adminImagen = new administrador_imagen()
                        {
                             id_administrador= admin.id_admin,
                            imagen = pImagen
                        };
                        if (arrayImagen.Length > 7000)
                        {
                            arrayImagen = new byte[0];
                        }
                      
                        var imagenUrl = modUtilidades.SaveImage(pImagen, admin.dni + admin.nombre, "admins");

                        adminImagen.imagen = arrayImagen;
                        adminImagen.imagen_url = imagenUrl;
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

                            var imagenUrl = modUtilidades.SaveImage(pImagen, admin.dni + admin.nombre, "admins");

                            adminImagen.imagen = arrayImagen;
                            adminImagen.imagen_url = imagenUrl;
                            db.SaveChanges();
                        }

                    }

                    if (admin != null)
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


        public administrador_imagen ObtenerImagenPerfil(int pId)
        {
            using (var db = new JJSSEntities())
            {
                try
                {

                    return db.administrador_imagen.FirstOrDefault(imag => imag.id_administrador == pId);

                    
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
