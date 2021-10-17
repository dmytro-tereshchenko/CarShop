using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [DisplayName("Manufacturer")]
        public int? ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
    }
}
