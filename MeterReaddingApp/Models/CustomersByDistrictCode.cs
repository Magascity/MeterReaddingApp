using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaddingApp.Models
{
    public class CustomersByDistrictCode
    {
       public long RecordId { get; set; }

       public string? CustomerName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? CustomerAddress { get; set; }
        public string? CustReference { get; set; } 
    }
}
