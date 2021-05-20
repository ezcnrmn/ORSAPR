using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        public KompasObject KompasObject { get; set; }

        /// <summary>
        /// Свойство Part
        /// </summary>
        public ksPart KsPart { get; set; } = null;
        
        /// <summary>
        /// Функция запуска Компас-3D
        /// </summary>
        public void OpenKompas()
        {
            try
            {
                KompasObject = (KompasObject)Marshal.GetActiveObject
                    ("KOMPAS.Application.5");
            }
            catch
            {
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                KompasObject = (KompasObject)Activator.CreateInstance(t);
            }

            if (KompasObject != null)
            {
                KompasObject.Visible = true;
                KompasObject.ActivateControllerAPI();

                ksDocument3D iDocument3D = (ksDocument3D)KompasObject.Document3D();

                iDocument3D.Create(false /*видимый*/, true /*деталь*/);
                KsPart = (ksPart)iDocument3D.GetPart((short)Part_Type.pTop_Part);
            }
        }
    }
}
