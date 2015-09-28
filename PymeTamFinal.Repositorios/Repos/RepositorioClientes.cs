﻿using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.RepoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Repositorios.Repos
{
    public class RepositorioClientes : RepositorioBase<Cliente>
    {
        public RepositorioClientes(DataContext context ) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
