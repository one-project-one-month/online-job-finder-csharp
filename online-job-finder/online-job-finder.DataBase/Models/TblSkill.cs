using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.DataBase.Models
{
    public class TblSkill
    {
        public Guid SkillID { get; set; }

        public string SkillName { get; set; } = null!;
    }
}
