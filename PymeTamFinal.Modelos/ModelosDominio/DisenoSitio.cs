using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("DisenoSitio")]
    public class DisenoSitio
    {
        [Key]
        public int idDiseno { get; set; }
        public string nombreDiseno { get; set; }
        public bool usarArchivosCss { get; set; }
        public ICollection<archivosEstilo> archivosEstilo { get; set; }
        /// <summary>
        /// Para modificaciónes basicas de diseño para bootstrap
        /// </summary>
        public string colorBaseMenu { get; set; }
        public string colorTextoMenu { get; set; }
        public string colorBordeMenu { get; set; }
        public string colorTexoHoverMenu { get; set; }
        public bool usarContenedoresFluidos { get; set; }

    }
}
