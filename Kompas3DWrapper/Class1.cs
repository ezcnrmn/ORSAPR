using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;

namespace Kompas3DWrapper
{
    public class Class1
    {
        public KompasObject Kompas { get; set; }
        
        public void OpenKompas()
        {
            try
            {
                if (Kompas == null)
                {
                    var type = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    Kompas = (KompasObject)Activator.CreateInstance(type);
                }

                if (Kompas == null) return;
                Kompas.Visible = true;
                Kompas.ActivateControllerAPI();
            }
            catch
            {
                Kompas = null;
                if (Kompas == null)
                {
                    var type = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    Kompas = (KompasObject)Activator.CreateInstance(type);
                }

                if (Kompas == null) return;
                Kompas.Visible = true;
                Kompas.ActivateControllerAPI();
            }
        }
    }
}
