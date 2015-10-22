using PymeTamFinal.Contratos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Repositorios.DatatablesBase
{
    public class DataTablesBase<ViewModel, DbModel> : IGeneradorDataTables<ViewModel, DbModel> where ViewModel : class where DbModel : class
    {
        public virtual List<string[]> generarListaDeArreglo(IEnumerable<ViewModel> model)
        {
            throw new NotImplementedException();
        }

        public virtual List<ViewModel> modeloTabla(IQueryable<DbModel> dbModel,string busqueda)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<ViewModel> OrdenaResultados(IEnumerable<ViewModel> model, int idCol, string orden)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<ViewModel> PaginarResultados(int inicio, int toma, IEnumerable<ViewModel> model)
        {
            throw new NotImplementedException();
        }
    }
}
