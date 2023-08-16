using ImperialNova.Database;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ImperialNova.Services
{
    public class ProductServices
    {
        public List<Product> ExportProducts(DateTime start, DateTime end)
        {
            using (var context = new DSContext())
            {
                var data = context.products.Where(c => c._ExportDate >= start && c._ExportDate <= end).ToList();
                data.Reverse();
                return data;
            }
        }
        public List<string> GetProductNames()
        {
            using (var context = new DSContext())
            {
                var data = context.products.Select(c => c._Name).ToList();
                data.Reverse();
                return data;
            }
        }
        public List<Product> GetProducts()
        {
            using (var context = new DSContext())
            {
                var data = context.products.ToList();
                data.Reverse();
                return data;
            }
        }


        public Entities.Product GetProductById(int id)
        {
            using (var context = new DSContext())
            {
                return context.products.Find(id);

            }
        }

        public void CreateProduct(Product Product)
        {
            using (var context = new DSContext())
            {
                context.products.Add(Product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product Product)
        {
            using (var context = new DSContext())
            {
                context.Entry(Product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteProduct(int id)
        {
            var data = GetProductById(id);

            using (var context = new DSContext())
            {
                context.products.Remove(data);
                context.SaveChanges();
            }
        }
    }
}

