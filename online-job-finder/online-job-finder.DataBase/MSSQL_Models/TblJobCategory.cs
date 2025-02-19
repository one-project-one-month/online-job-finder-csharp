using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.DataBase.Models
{
    public partial class TblJobCategory
    {
        public Guid JobCategoryID { get; set; }

        public string CategoryName { get; set; } = null!;

        public string SoftDelete { get; set; } = null!;
    }
}