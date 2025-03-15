using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.ViewModels
{
    public class ApplyJobViewModels
    {
        public Guid ApplicationsId { get; set; }

        public Guid JobsId { get; set; }

        public Guid ApplicantProfilesId { get; set; }

        public Guid ResumesId { get; set; }

        public string Status { get; set; } = null!;

        public int Version { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDelete { get; set; }
    }
}
