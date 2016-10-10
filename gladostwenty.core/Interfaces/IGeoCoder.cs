using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gladostwenty.core.Models;

namespace gladostwenty.core.Interfaces
{
    public interface IGeoCoder
    {
        Task<string> GetCityFromLocation(GeoLocation location);
    }
}