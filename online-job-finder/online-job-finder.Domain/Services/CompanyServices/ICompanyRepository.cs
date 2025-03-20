namespace online_job_finder.Domain.Services.CompanyServices;

public interface ICompanyRepository
{
    CompanyUsersViewModels CreateCompany(CompanyUsersViewModels reqModel);

    List<CompanyUsersViewModels> GetCompanies();

    CompanyUsersViewModels GetCompanyById(string userId);

}
