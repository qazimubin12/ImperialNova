using ImperialNova.Database;
using ImperialNova.Entities;
using System.Data.Entity;
using System.Linq;

namespace ImperialNova.Services
{
    public class DeliveryFormServices
    {
        public void CreateDeliveryForm(DeliveryForm DeliveryForm)
        {
            using (var context = new DSContext())
            {
                context.deliveryform.Add(DeliveryForm);
                context.SaveChanges();
            }
        }

        public void UpdateDeliveryForm(DeliveryForm DeliveryForm)
        {
            using (var context = new DSContext())
            {
                context.Entry(DeliveryForm).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public DeliveryForm GetLastEnteredDeliveryForm()
        {
            using (var context = new DSContext())
            {
                var lastEnteredForm = context.deliveryform.OrderByDescending(c => c._id).FirstOrDefault();
                return lastEnteredForm;
            }
        }
        public DeliveryForm GetFormById(int id)
        {
            using (var context = new DSContext())
            {
                return context.deliveryform.Find(id);

            }
        }
    }
}
