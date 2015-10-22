﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Contratos.Repositorio
{
    public interface IGeneradorGraficas<T> where T :class
    {
        T generarGrafica(object data);
        double EpochMillis(DateTime fecha);
    }
}
