using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RawData
    {
        public Guid MeasurementId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Raw { get; set; }
        public string Location { get; set; }
        public bool IsNew { get; set; }

        public RawData()
        {
            IsNew = false;
        }

        
    }
}
