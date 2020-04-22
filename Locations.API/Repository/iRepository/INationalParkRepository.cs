using Locations.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.API.Repository.iRepository
{
    public interface INationalParkRepository
    {
        ICollection<Location> GetNationalParks();
        Location GetNationalPark(int nationalParkId);
        bool NationalParkExist(string name);
        bool NationalParkExist(int id);
        bool CreateNationalPark(Location nationalPark);
        bool UpdateNationalPark(Location nationalPark);
        bool DeleteNationalPark(Location nationalPark);

        bool Save();
    }
}
