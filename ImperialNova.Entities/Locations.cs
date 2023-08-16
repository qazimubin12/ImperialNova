using System.ComponentModel.DataAnnotations;

namespace ImperialNova.Entities
{
    public class Locations
    {
        [Key]
        public int _id { get; set; }
        public string _LocationName { get; set; }
    }
}
