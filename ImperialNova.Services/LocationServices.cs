using ImperialNova.Database;
using ImperialNova.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ImperialNova.Services
{
    public class locationservices
    {
      
        public List<string> GetLocationsNames()
        {
            using (var context = new DSContext())
            {
                var data = context.locations.Select(c => c._LocationName).ToList();
                data.Reverse();
                return data;
            }
        }
        public List<Locations> Getlocations()
        {
            using (var context = new DSContext())
            {
                var data = context.locations.ToList();
                data.Reverse();
                return data;
            }
        }


        public Entities.Locations GetLocationsById(int id)
        {
            using (var context = new DSContext())
            {
                return context.locations.Find(id);

            }
        }

        public void CreateLocations(Locations Locations)
        {
            using (var context = new DSContext())
            {
                context.locations.Add(Locations);
                context.SaveChanges();
            }
        }

        public void UpdateLocations(Locations Locations)
        {
            using (var context = new DSContext())
            {
                context.Entry(Locations).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteLocations(int id)
        {
            var data = GetLocationsById(id);

            using (var context = new DSContext())
            {
                context.locations.Remove(data);
                context.SaveChanges();
            }
        }
    }
}
