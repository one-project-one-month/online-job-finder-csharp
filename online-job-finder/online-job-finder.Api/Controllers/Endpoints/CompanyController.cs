namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Recruiters")]
[Route("api/recruiter")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    [HttpPost]
    public IActionResult CreateCompany(CompanyUsersViewModels reqModel)
    {
        var item = _companyRepository.CreateCompany(reqModel);
        return Ok(item);
    }

    [HttpGet]
    public IActionResult GetCompanies()
    {
        var items = _companyRepository.GetCompanies();
        return Ok(items);
    }

    [HttpGet("UserId")]
    public IActionResult GetCompany(string UserId)
    {
        var items = _companyRepository.GetCompanyById(UserId);
        return Ok(items);
    }


}
