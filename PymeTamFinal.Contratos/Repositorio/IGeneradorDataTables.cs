using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Contratos.Repositorio
{
    public interface IGeneradorDataTables<ViewModel, DbModel> where ViewModel :class where DbModel :class
    {
        List<string[]> generarListaDeArreglo(IEnumerable<ViewModel> model);
        IEnumerable<ViewModel> OrdenaResultados(IEnumerable<ViewModel> model, int idCol,string orden);
        IEnumerable<ViewModel> PaginarResultados(int inicio,int toma,IEnumerable<ViewModel>model);
        List<ViewModel> modeloTabla(IQueryable<DbModel>dbModel,string busqueda);
    }
}
