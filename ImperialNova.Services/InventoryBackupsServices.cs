using ImperialNova.Database;
using ImperialNova.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ImperialNova.Services
{
    public class InventoryBackupsServices
    {
        public List<InventoryBackups> GetInventoryBackup()
        {
            using (var context = new DSContext())
            {
                var data = context.inventorybackups.ToList();
                data.Reverse();
                return data;
            }
        }
        public void CreateInventoryBackup(InventoryBackups inventoryBackups)
        {
            using (var context = new DSContext())
            {
                context.inventorybackups.Add(inventoryBackups);
                context.SaveChanges();
            }
        }
    }
}
