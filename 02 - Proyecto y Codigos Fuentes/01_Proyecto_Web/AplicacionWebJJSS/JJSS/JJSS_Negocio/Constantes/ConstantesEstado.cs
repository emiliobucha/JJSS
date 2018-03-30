using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Constantes
{
    public class ConstantesEstado
    {
        public const int TORNEO_INSCRIPCION_ABIERTA = 1;
        public const int TORNEO_IN_SCRIPCION_CERRADA = 2;
        public const int TORNEO_EN_CURSO = 3;
        public const int TORNEO_FINALIZADO = 4;
        public const int TORNEO_SUSPENDIDO = 13;
        public const int TORNEO_CANCELADO = 14;

        public const int RESERVA_RESERVADO = 5;
        public const int RESERVA_CANCELADO = 6;
        public const int RESERVA_RETIRADO = 7;

        public const int ALUMNOS_CREADO = 8;
        public const int ALUMNOS_ACTIVO = 9;
        public const int ALUMNOS_MOROSO = 10;
        public const int ALUMNOS_INACTIVO = 11;
        public const int ALUMNOS_DE_BAJA = 12;


    }
}

