using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.Services.CompanyServices
{
    public interface ICompanyRepository
    {
        CompanyUsersViewModels CreateCompany(CompanyUsersViewModels reqModel);

        List<CompanyUsersViewModels> GetCompanies();

        CompanyUsersViewModels GetCompanyById(string userId);

    }
}
