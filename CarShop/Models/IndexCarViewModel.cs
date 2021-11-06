using CarShop.Domain;
using System.Collections.Generic;


namespace CarShop.Models
{
    public class IndexCarViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public PageInfo PageInfo { get; set; }
        public string SelectedManufacturer { get; set; }
    }
}
