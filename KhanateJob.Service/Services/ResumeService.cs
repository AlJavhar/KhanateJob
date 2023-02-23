using KhanateJob.DAL.IRepositories;
using KhanateJob.DAL.Repositories;
using KhanateJob.Domain.Entities;
using KhanateJob.Service.DTOs;
using KhanateJob.Service.Helpers;
using KhanateJob.Service.Interfaces;
using System.Reflection;

namespace KhanateJob.Service.Services;

public class ResumeService : IResumeService
{
 
    private readonly IGenericRepository<Resumes> genericRepository;
    private readonly IGenericRepository<CheckedJobs> genericRepositoryChecked;
    private readonly IGenericRepository<JobTables> genericRepositoryJobTables;

    public ResumeService
        (
        IGenericRepository<Resumes> genericRepository, 
        IGenericRepository<CheckedJobs> genericRepositoryChecked,
        IGenericRepository<JobTables> genericRepositoryJobTables
        )
    {
        this.genericRepository = genericRepository;
        this.genericRepositoryChecked = genericRepositoryChecked;
        this.genericRepositoryJobTables = genericRepositoryJobTables;
    }

    public async Task<Response<Resumes>> CreateAsync(ResumeCreationDto value)
    {
        var models = await this.genericRepository.GetAllAsync();

        var mappedModel = new Resumes()
        {
            UserId = value.UserId,
            Activities = value.Activities,
            Education = value.Education,
            Experince = value.Experince,
            Objective = value.Objective,
            Salary = value.Salary,
            JobTableId = value.JobTableId
        };

        var result = await this.genericRepository.CreatAsync(mappedModel);

        return new Response<Resumes>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<bool>> DeleteAsync(long id)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "Resume is not found",
                Value = false
            };

        await this.genericRepository.DeleteAsnyc(id);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async Task<Response<Resumes>> GetSuitableResume(long id)
    {
        var job = await genericRepositoryJobTables.GetByIdAsync(id);

        var resumes = await genericRepository.GetAllAsync();
        if(job is not null)
        {
            foreach (var resume in resumes)
            {
                if (resume.JobTableId == job.Id && resume.Salary <= job.Salary)
                {
                    CheckedJobs checkedJobs = new CheckedJobs()
                    {
                        UserId = resume.UserId,
                        JobId = job.Id,
                        ResumeId = resume.Id,
                        Request = Domain.Enums.RequestStatus.Accepted
                    };

                   await genericRepositoryChecked.CreatAsync(checkedJobs);

                    return new Response<Resumes>()
                    {
                        StatusCode = 200,
                        Message = "Success",
                        Value = resume
                    };
                    
                }
            }
        }
        return new Response<Resumes>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = null
        };

    }

    public async Task<Response<List<Resumes>>> GetAllAsync()
    {
        var result = await this.genericRepository.GetAllAsync();
        return new Response<List<Resumes>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<Resumes>> GetByIdAsync(long id)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Resumes>()
            {
                StatusCode = 404,
                Message = "Resume is not found",
                Value = null
            };

        return new Response<Resumes>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = model
        };
    }

    public async Task<Response<Resumes>> UpdateAsync(long id, ResumeCreationDto value)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Resumes>()
            {
                StatusCode = 404,
                Message = "Resume is not found",
                Value = null
            };

        var mappedModel = new Resumes()
        {
            UserId = value.UserId,
            Activities = value.Activities,
            Education = value.Education,
            Experince = value.Experince,
            Objective = value.Objective,
            Salary = value.Salary,
            JobTableId = value.JobTableId
        };

        var result = await this.genericRepository.UpdateAsync(id, mappedModel);
        return new Response<Resumes>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }
}
