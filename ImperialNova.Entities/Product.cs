using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImperialNova.Entities
{

    public class Product
    {
        [Key]
        public int _Id { get; set; }
        [Required]
        public string _Name { get; set; }
        public string _Size { get; set; }
        public string _Color { get; set; }
        public decimal _Cost { get; set; }
        public decimal _RetailPrice { get; set;}
        public int _Quantity
        {
            get
            {
                return this._QuantityIn - this._QuantityOut;
            }
        }
        public int _QuantityIn { get; set; }
        public int _QuantityOut { get; set; } = 0;
        public string _Notes { get; set; }
        public DateTime _ExportDate { get; set; } = DateTime.Now;

        public int _CategoryId { get; set; }

    }
}
