using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Domain
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Input manufacturer's name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Input manufacturer's location")]
        public string Location { get; set; }
    }
}
