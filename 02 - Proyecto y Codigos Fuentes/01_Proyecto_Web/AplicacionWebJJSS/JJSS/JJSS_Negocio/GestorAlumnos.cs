using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
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

        public string RegistrarAlumno(string pNombre, string pApellido, DateTime? pFechaNacimiento, int? pIdFaja, int? pIdCategoria, short? pSexo, int pDni, int? pTelefono, string pMail, int? pIdDireccion, int? pTelEmergencia)
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
                        //direccion
                        fecha_ingreso = DateTime.Today,
                        //foto_perfil = pFotoPerfil,
                        telefono_emergencia = pTelEmergencia
                    };
                    db.alumno.Add(nuevoAlumno);
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
