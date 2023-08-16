using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImperialNova.Entities
{
    public class Category
    {
        [Key]
        public int _Id { get; set; }
        [Required]
        public string _CName { get; set; }
        public string _Description { get; set; }
        [NotMapped]
        public List<int> productsList { get; set; }
    }
}
