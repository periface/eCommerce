﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("PreguntasFrecuentes")]
    public class PreguntasFrecuentes
    {
        [Key]
        public int idPregunta { get; set; }
        public string pregunta { get; set; }
        [AllowHtml]
        public string respuesta { get; set; }
        public DateTime fechaPublicacion { get; set; }
    }
}
