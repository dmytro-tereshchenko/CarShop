using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class IndexCarViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public PageInfo PageInfo { get; set; }
        public string SelectedManufacturer { get; set; }
    }
}
