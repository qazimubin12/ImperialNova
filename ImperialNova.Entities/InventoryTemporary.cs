﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ImperialNova.Entities
{
    public class InventoryTemporary
    {
        [Key]
        public int _Id { get; set; }
        public int _ProductId { get; set; }
        public string _Name { get; set; }
        public string _Size { get; set; }
        public string _Color { get; set; }
        public decimal _Cost { get; set; }
        public decimal _RetailPrice { get; set; }
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
        public string _Action { get; set; }

        public string _UserId { get; set; }
        public string _UserFullName { get; set; }
        public string _UserEmail { get; set; }
    }
}
