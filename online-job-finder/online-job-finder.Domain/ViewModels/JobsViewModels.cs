using online_job_finder.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.ViewModels
{
    public class JobsViewModels
    {
        public Guid JobsId { get; set; }
       
        public string Title { get; set; } 
        public string CompanyName { get; set; }
        public string LocationName { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string? Requirements { get; set; } 
        public string? Industry { get; set; }

        public int NumOfPosts { get; set; }

        public decimal Salary { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public int Version { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDelete { get; set; }

       
    }
}
