using Microsoft.EntityFrameworkCore;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels.JobCategories;
using online_job_finder.Domain.ViewModels.Roles;
using online_job_finder.Domain.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.Services.JobCategoriesServices
{
    public class JobCategoryRepository: IJobCategoryRepository
    {
        private readonly AppDbContext _db;

        public JobCategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public TblJobCategory CreateJobCategory(JobsCategoriesViewModels jobsCategories)
        {

            TblJobCategory jobcategoriesModel = JobCategoriesMapping(jobsCategories);


        
            _db.TblJobCategories.Add(jobcategoriesModel);
            _db.SaveChanges();

            return jobcategoriesModel;
        }

        public List<JobsCategoriesViewModels> GetJobCategories()
        {
            var model = _db.TblJobCategories
                .AsNoTracking()                            
                .ToList();

            var jobCategoriesViewModels = model.Select(JobsCategoriesViewModelsMapping).ToList();

            return jobCategoriesViewModels;
        }

        public JobsCategoriesViewModels? GetJobCategory(string id)
        {
            var model = _db.TblJobCategories
                .AsNoTracking()
                .FirstOrDefault(x => x.JobCategoryID.ToString() == id
                );

            if (model is null) { return null; }

            var mappingModel = JobsCategoriesViewModelsMapping(model); 

            return mappingModel;
        }

        public JobsCategoriesViewModels? UpdateJobCategory(string id, JobsCategoriesViewModels jobCategories)
        {
            var item = _db.TblJobCategories
                .AsNoTracking()
                .FirstOrDefault(x => x.JobCategoryID.ToString() == id
                );
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(jobCategories.CategoryName))
            {
                item.CategoryName = jobCategories.CategoryName;
            }

            

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            var model = JobsCategoriesViewModelsMapping(item);

            return model;
        }

        public JobsCategoriesViewModels? PatchJobCategory(string id, JobsCategoriesViewModels jobsCategory)
        {
            var item = _db.TblJobCategories
                .AsNoTracking()
                .FirstOrDefault(x => x.JobCategoryID.ToString() == id
                );
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(jobsCategory.CategoryName))
            {
                item.CategoryName = jobsCategory.CategoryName;
            }

           

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            var model = JobsCategoriesViewModelsMapping(item);
            return jobsCategory;
        }

        public bool? DeleteJobCategory(string id)
        {
            var item = _db.TblJobCategories
                .AsNoTracking()
                .FirstOrDefault(x => x.JobCategoryID.ToString() == id
                );
            if (item is null)
            {
                return null;
            }

            
            

            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();

            return result > 0;
        }


        private static TblJobCategory JobCategoriesMapping(JobsCategoriesViewModels jobsCategories)
        {
            return new TblJobCategory
            {
                JobCategoryID = Guid.NewGuid(),
                CategoryName = jobsCategories.CategoryName              
            };
        }


        private static JobsCategoriesViewModels JobsCategoriesViewModelsMapping(TblJobCategory jobCategory)
        {
            return new JobsCategoriesViewModels
            {

                CategoryName = jobCategory.CategoryName,
            
            };
        }

        public UsersViewModels CreateJobCategory(UsersViewModels user)
        {
            throw new NotImplementedException();
        }
    }
}
