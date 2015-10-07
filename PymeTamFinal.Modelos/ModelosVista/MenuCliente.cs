using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public abstract class Menu
    {
        public string texto { get; set; }
        public string control { get; set; }
        public string accion { get; set; }
        public bool seleccionado { get; set; }
    }
    public class MenuCliente : Menu {

    }
    public class MenuSitio : Menu{
        public MenuSitio()
        {
            subMenu = new List<MenuSitio>();
        }
        public List<MenuSitio> subMenu { get; set; }
    }

}
