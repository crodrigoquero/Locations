using Locations.API.Data;
using Locations.API.Models;
using Locations.API.Repository.iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.API.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _db;

        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public bool CreateNationalPark(Location nationalPark)
        {
            _db.Locations.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(Location nationalPark)
        {
            _db.Locations.Remove(nationalPark);
            return Save();
        }

        public Location GetNationalPark(int nationalParkId)
        {
            return _db.Locations.FirstOrDefault(a => a.Id == nationalParkId);

        }

        public ICollection<Location> GetNationalParks()
        {
            return _db.Locations.OrderBy(a => a.Name).ToList();
        }

        public bool NationalParkExist(string name)
        {
            bool value = _db.Locations.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExist(int id)
        {
            return _db.Locations.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(Location nationalPark)
        {
            _db.Locations.Update(nationalPark);
            return Save();
        }
    }
}
