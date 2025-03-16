using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.ViewModels
{
    public class CompanyUsersViewModels
    {
 
        //Company
        public Guid CompanyProfilesId { get; set; }

        public Guid UserId { get; set; }

        public Guid LocationId { get; set; }

        public string CompanyName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Website { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Description { get; set; }

        public int Version { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        //User
        public virtual UsersViewModels User { get; set; } = null!;


    }
}
