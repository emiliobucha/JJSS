﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;

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
        public alumno ObtenerAlumnoPorDNI(int pDni)
        {
            using (var db = new JJSSEntities())
            {
                var alumnoEncontrado = from alu in db.alumno
                                       where alu.dni == pDni && alu.baja_logica==1
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
                    string iduser = nuevoUsuario.GenerarNuevoUsuario(login, pDni.ToString(), 3, pMail, nombreUsuario);
                    int idUsuario;
                    if (int.TryParse(iduser, out idUsuario) == false)
                    {
                        return iduser;
                    }
                    seguridad_usuario usuario = db.seguridad_usuario.Find(idUsuario);


                    if (ObtenerAlumnoPorDNI(pDni) != null)
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
                            ciudad = ciudadElegida
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
                            baja_logica=1
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
                            baja_logica=1
                        };
                    }

                    db.alumno.Add(nuevoAlumno);

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


        /*Método que busca un alumno con filtro de dni
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
                    var alumnoEncontrado = from alu in db.alumno
                                           where alu.dni == pDni
                                           select alu;
                    alumno alumnoBorrar = alumnoEncontrado.FirstOrDefault();
                    
                    if (alumnoBorrar == null) throw new Exception("El alumno no existe");
                    alumnoBorrar.baja_logica = 0;
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
        public string ModificarAlumno(int pDni, string pNombre, string pApellido, DateTime? pFecha, short? pSexo)
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
                    alumno alumnoModificar = alumnoEncontrado.FirstOrDefault();

                    if (alumnoModificar == null) throw new Exception("El usuario no existe");
                    alumnoModificar.apellido = pApellido;
                    alumnoModificar.nombre = pNombre;
                    if (pFecha != null) alumnoModificar.fecha_nacimiento = pFecha;
                    if (pSexo != null) alumnoModificar.sexo = pSexo;


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
        public string ModificarAlumno(string pCalle, string pDepto, int? pNumero, int? pPiso, int pTelefono, int pTelUrgencia, string pMail, int pDni, int pIdCiudad)
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
                                id_ciudad = pIdCiudad

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
        public DataTable ObtenerDireccionAlumno(int pIdAlumno)
        {
            using (var db = new JJSSEntities())
            {
                var direccionEncontrada = from dir in db.direccion
                                          join alu in db.alumno on dir.id_direccion equals alu.id_direccion
                                          join ciu in db.ciudad on dir.id_ciudad equals ciu.id_ciudad
                                          where alu.id_alumno == pIdAlumno
                                          select new
                                          {
                                              calle = dir.calle,
                                              numero = dir.numero,
                                              depto = dir.departamento,
                                              piso = dir.piso,
                                              idCiudad = dir.id_ciudad,
                                              idProvincia = ciu.id_provincia
                                          };
                return modUtilidadesTablas.ToDataTable(direccionEncontrada.ToList());
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
                                     where alu.id_alumno == pIdAlumno && faj.id_tipo_clase == pIdTipoClase && alu.baja_logica == 1 && axf.actual==1
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
        public string AsignarFaja(int pDniAlumno, int pIDFaja)
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
    }
}
