using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.DataBase.Models
{
    public class TblLocation
    {
        public Guid LocationID { get; set; }

        public string LocationName { get; set; } = null!;
       

    }
}
