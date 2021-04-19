using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6Constants3D;
using Kompas6API5;

namespace KompasWrapper
{
    /// <summary>
    /// Класс для подключения к Компас-3D
    /// </summary>
    public class Kompas3DWrapper
    {
        /// <summary>
        /// Свойство Компас
        /// </summary>
        public KompasObject kompas { get; set; }

        /// <summary>
        /// Свойство Part
        /// </summary>
        public ksPart iPart { get; set; } = null;

        /// <summary>
        /// Функция запуска Компас-3D
        /// </summary>
        public void OpenKompas()
        {
            if (kompas == null)
            {
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                kompas = (KompasObject)Activator.CreateInstance(t);
            }

            if (kompas != null)
            {
                kompas.Visible = true;
                kompas.ActivateControllerAPI();
            }

            if (kompas != null)
            {
                ksDocument3D iDocument3D = (ksDocument3D)kompas.Document3D();

                iDocument3D.Create(false /*видимый*/, true /*деталь*/);
                iPart = (ksPart)iDocument3D.GetPart((short)Part_Type.pTop_Part);
            }
        }
    }
}
