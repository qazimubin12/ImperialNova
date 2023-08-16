﻿using Microsoft.AspNet.Identity.EntityFramework;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImperialNova.Entities;

namespace ImperialNova.Database
{
    public class DSContext :IdentityDbContext<User>,IDisposable
    {
        public DSContext() : base("ImperialNovaConnectionStrings")
        {

        }

        public static DSContext Create()
        {
            return new DSContext();
        }



        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Inventory> inventories { get; set; }
        public DbSet<InventoryBackups> inventorybackups { get; set; }
        public DbSet<InventoryTemporary> inventorytemporary { get; set; }
        public DbSet<DeliveryForm> deliveryform { get; set; }
        public DbSet<Locations> locations { get; set; }



    }
}
