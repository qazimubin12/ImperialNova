
using ImperialNova.Database;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ImperialNova.Services
{
    public class CategoryServices
    {
       
        public List<string> GetCategoryNames()
        {
            using (var context = new DSContext())
            {
                var data = context.categories.Select(c => c._CName).ToList();
                data.Reverse();
                return data;
            }
        }
        public Category GetCategoryInProducts(int Sentid)
        {
             using (var context = new DSContext())
            {
                var category =context.categories.FirstOrDefault(c => c._Id == Sentid);
                return category;

            }
        }
        public List<Category> GetCategorys()
        {
            using (var context = new DSContext())
            {
                var data = context.categories.ToList();
                data.Reverse();
                return data;
            }
        }


        public Entities.Category GetCategoryById(int id)
        {
            using (var context = new DSContext())
            {
                return context.categories.Find(id);

            }
        }

        public void CreateCategory(Category Category)
        {
            using (var context = new DSContext())
            {
                context.categories.Add(Category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Entities.Category Category)
        {
            using (var context = new DSContext())
            {
                context.Entry(Category).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteCategory(int id)
        {
            var data = GetCategoryById(id);

            using (var context = new DSContext())
            {
                context.categories.Remove(data);
                context.SaveChanges();
            }
        }
    }
}
