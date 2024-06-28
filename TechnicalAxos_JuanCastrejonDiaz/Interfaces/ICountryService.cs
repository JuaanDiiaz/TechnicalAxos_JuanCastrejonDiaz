using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAxos_JuanCastrejonDiaz.Models;

namespace TechnicalAxos_JuanCastrejonDiaz.Interfaces
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountriesAsync();
    }
}
