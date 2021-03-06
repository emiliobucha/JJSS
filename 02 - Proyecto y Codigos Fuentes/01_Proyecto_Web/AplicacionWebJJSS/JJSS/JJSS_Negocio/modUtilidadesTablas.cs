﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

using System.Reflection;

namespace JJSS_Negocio
{   /*
    *Módulo statico que contiene metodos de utilidades para el manejo de tablas
    */
    public static class modUtilidadesTablas
    {
        /*
         * Metodo que retorna una datatable a partir de una coleccion generica como por ejemplo una List<T>
         */
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection, string tableName)
        {
            DataTable tbl = ToDataTable(collection);
            tbl.TableName = tableName;
            return tbl;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            PropertyInfo[] pia = t.GetProperties();
            object temp;
            DataRow dr;

            for (int i = 0; i < pia.Length; i++)
            {
                dt.Columns.Add(pia[i].Name, Nullable.GetUnderlyingType(pia[i].PropertyType) ?? pia[i].PropertyType);
                dt.Columns[i].AllowDBNull = true;
            }

            //Llenamos la tabla con la colección
            foreach (T item in collection)
            {
                dr = dt.NewRow();
                dr.BeginEdit();

                for (int i = 0; i < pia.Length; i++)
                {
                    temp = pia[i].GetValue(item, null);
                    if (temp == null || (temp.GetType().Name == "Char" && ((char)temp).Equals('\0')))
                    {
                        dr[pia[i].Name] = (object)DBNull.Value;
                    }
                    else
                    {
                        dr[pia[i].Name] = temp;
                    }
                }

                dr.EndEdit();
                dt.Rows.Add(dr);
            }
            return dt;
        }


        /*
         * concatena dos datatable uno a continuacion del otro
         */
        public static DataTable unirDataTable(DataTable dt1, DataTable dt2)
        {
            DataTable dt = new DataTable();

            dt = dt1.Clone();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                DataRow dr = dt1.Rows[i];
                dt.ImportRow(dr);
            }
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                DataRow dr = dt2.Rows[i];
                dt.ImportRow(dr);
            }


            return dt;
        }
    }
}

