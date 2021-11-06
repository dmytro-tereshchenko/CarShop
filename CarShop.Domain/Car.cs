using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Domain
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Input model's name")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Input year's number")]
        [Range(1600, 2100, ErrorMessage = "Year have to be more than 1600")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Input description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Input car's price")]
        [Range(0, 99999999, ErrorMessage = "Price have to be between 0 and 99999999")]
        public double Price { get; set; }
        [DisplayName("Manufacturer")]
        public int? ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
    }
}
