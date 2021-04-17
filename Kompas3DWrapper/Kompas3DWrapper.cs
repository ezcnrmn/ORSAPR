using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6Constants3D;
using Kompas6API5;

namespace KompasWrapper
{
    public class Kompas3DWrapper
    {
        public KompasObject kompas { get; set; }

        public ksPart iPart { get; set; } = null;

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
