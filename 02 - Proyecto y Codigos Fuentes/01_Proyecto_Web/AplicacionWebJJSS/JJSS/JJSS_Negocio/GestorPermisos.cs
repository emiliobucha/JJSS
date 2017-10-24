using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing.Drawing2D;
using System.Net.Sockets;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    public class GestorPermisos
    {


        /*
        * Metodo que permite obtener los permisos de un usuario
        * Parametros:  pId : long id del usuario
        *             
        * Retornos: DataTable de permisos de usuario
        * 
        */
        public DataTable ObtenerPermisos(long pId)
        {
            using (var db = new JJSSEntities())
            {
                try
                {
                    var usuario = db.seguridad_usuario.Find(pId);


                    if (usuario == null)
                    {
                        return null;
                    }

                    if (usuario.baja_logica == 0) throw new Exception("Usuario inhabilitado");

                    var permisosUsuario = from x in
                        (from op in db.seguridad_opcion
                         join gop in db.seguridad_grupoxopcion on op.clave_opcion equals gop.clave_opcion
                         join gus in db.seguridad_usuarioxgrupo on gop.id_grupo equals gus.id_grupo
                         where gus.id_usuario == usuario.id_usuario
                         select new
                         {
                             perm_clave = op.clave_opcion,
                             perm_nombre = op.nombre,
                             perm_ejecutar = gop.ejecutar,
                             perm_ver = gop.ver,
                             perm_modificar = gop.modificar,
                             perm_agregar = gop.agregar,
                             perm_eliminar = gop.eliminar

                         })
                                          group x by new
                                          {
                                              x.perm_clave,
                                              x.perm_nombre
                                          } into g

                                          select new
                                          {
                                              perm_clave = g.Key.perm_clave,
                                              perm_nombre = g.Key.perm_nombre,
                                              perm_ejecutar = g.Max(x => x.perm_ejecutar),
                                              perm_ver = g.Max(x => x.perm_ver),
                                              perm_modificar = g.Max(x => x.perm_modificar),
                                              perm_agregar = g.Max(x => x.perm_agregar),
                                              perm_eliminar = g.Max(x => x.perm_eliminar)
                                          };

                    return permisosUsuario.ToList().ToDataTable();
                }
                catch (Exception ex)
                {

                    return null;
                }

            }
        }


        public string AgregarGrupo(long pId, long pIdGrupo)
        {
            using (var db = new JJSSEntities())
            {
                try
                {
                    var Usuario = db.seguridad_usuario.Find(pId);


                    if (Usuario == null) throw new Exception("Usuario no encontrado");


                    if (Usuario.baja_logica == 0) throw new Exception("Usuario inhabilitado");


                    var nuevoGrupo = db.seguridad_grupo.Find(pIdGrupo);

                    if (nuevoGrupo == null) throw new Exception("Grupo inexistente");

                    var usuarioxGrupo = from uxg in db.seguridad_usuarioxgrupo
                                        where uxg.id_usuario == Usuario.id_usuario &&
                                              uxg.id_grupo == nuevoGrupo.id_grupo
                                        select uxg;

                    if (usuarioxGrupo.ToList().Count > 0) throw new Exception("Grupo ya asignado");


                    var nuevoUsuarioxGrupo = new seguridad_usuarioxgrupo()
                    {
                        id_usuario = Usuario.id_usuario,
                        id_grupo = nuevoGrupo.id_grupo,
                        seguridad_grupo = nuevoGrupo,
                        seguridad_usuario = Usuario

                    };

                    db.seguridad_usuarioxgrupo.Add(nuevoUsuarioxGrupo);
                    db.SaveChanges();

                    return "";

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }


            }
        }

        public string QuitarGrupo(long pId, long pIdGrupo)
        {
            using (var db = new JJSSEntities())
            {
                try
                {
                    var Usuario = db.seguridad_usuario.Find(pId);


                    if (Usuario == null) throw new Exception("Usuario no encontrado");


                    if (Usuario.baja_logica == 0) throw new Exception("Usuario inhabilitado");


                    var nuevoGrupo = db.seguridad_grupo.Find(pIdGrupo);

                    if (nuevoGrupo == null) throw new Exception("Grupo inexistente");

                    var usuarioxGrupo = from uxg in db.seguridad_usuarioxgrupo
                                        where uxg.id_usuario == Usuario.id_usuario &&
                                              uxg.id_grupo == nuevoGrupo.id_grupo
                                        select uxg;

                    if (usuarioxGrupo.ToList().Count == 0) throw new Exception("Asignación de Grupo Inexistente");

                    db.seguridad_usuarioxgrupo.Remove(usuarioxGrupo.ToList()[0]);
                    db.SaveChanges();

                    return "";

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }


            }
        }

        public string NuevoGrupo(string pNombre, List<seguridad_grupoxopcion> pGrupoxOpcion)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {

                    if (string.IsNullOrEmpty(pNombre)) throw new Exception("Nombre de grupo invalido");
                    var grupo = from grp in db.seguridad_grupo
                                where grp.nombre == pNombre
                                select grp;

                    if (grupo.ToList().Count > 0) throw new Exception("Nombre de grupo ya existente");

                    var nuevoGrupo = new seguridad_grupo()
                    {
                        nombre = pNombre
                    };

                    db.seguridad_grupo.Add(nuevoGrupo);
                    db.SaveChanges();

                    if (pGrupoxOpcion == null || pGrupoxOpcion.Count == 0) throw new Exception("No hay opciones seleccionadas");

                    foreach (var nuevaOpcion in pGrupoxOpcion)
                    {
                        var opcion = db.seguridad_opcion.Find(nuevaOpcion.clave_opcion);
                        if (opcion == null) throw new Exception("Opción Invalida");

                        nuevaOpcion.id_grupo = nuevoGrupo.id_grupo;
                        nuevaOpcion.seguridad_grupo = nuevoGrupo;

                        db.seguridad_grupoxopcion.Attach(nuevaOpcion);


                    }

                    db.SaveChanges();

                    transaction.Commit();
                    return "";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }

            }
        }

        public string ActualizarGrupo(long pId, string pNombre, List<seguridad_grupoxopcion> pGrupoxOpcion)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    var grupo = db.seguridad_grupo.Find(pId);

                    if (grupo == null) throw new Exception("No existe el grupo");

                    if (!string.IsNullOrEmpty(pNombre))
                    {
                        grupo.nombre = pNombre;
                    }

                    if (pGrupoxOpcion == null || pGrupoxOpcion.Count == 0) throw new Exception("No hay opciones seleccionadas");

                    foreach (var nuevaOpcion in pGrupoxOpcion)
                    {
                        var opcion = db.seguridad_opcion.Find(nuevaOpcion.clave_opcion);
                        if (opcion == null) throw new Exception("Opción invalida");

                        db.seguridad_grupoxopcion.Attach(nuevaOpcion);

                    }

                    db.SaveChanges();

                    transaction.Commit();
                    return "";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }

            }
        }


        public List<seguridad_grupoxopcion> getGrupoxOpciones(long pId)
        {

            using (var db = new JJSSEntities())
            {
                var grupoxOpciones = from gop in db.seguridad_grupoxopcion
                                     where gop.id_grupo == pId
                                     select gop;

                return grupoxOpciones.ToList();

            }

        }

        public List<seguridad_grupo> obtenerListaGrupos()
        {
            using (var db = new JJSSEntities())
            {

                return db.seguridad_grupo.ToList();
            }
        }

        public DataTable obtenerTablaGrupos(long pId)
        {
            using (var db = new JJSSEntities())
            {
                var grupos = from gru in db.seguridad_grupo
                             join gxu in db.seguridad_usuarioxgrupo on gru.id_grupo equals gxu.id_grupo
                             where gxu.id_usuario == pId
                             select gru;
                return grupos.ToList().ToDataTable();

            }
        }


    }
}
