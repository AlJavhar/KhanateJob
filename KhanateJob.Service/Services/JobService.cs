using KhanateJob.DAL.Configurations;
using KhanateJob.DAL.IRepositories;
using KhanateJob.Domain.Entities;
using KhanateJob.Service.DTOs;
using KhanateJob.Service.Helpers;
using KhanateJob.Service.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace KhanateJob.Service.Services;

public class JobService : IJobService
{
    private readonly IGenericRepository<JobTables> genericRepository;
    
    public JobService(IGenericRepository<JobTables> genericRepository)
    {
        this.genericRepository = genericRepository;
      
    }
    public async Task<Response<JobTables>> CreateAsync(JobTableCreationDto value)
    {
        var models = await this.genericRepository.GetAllAsync();
        var model = models.FirstOrDefault(x => x.JobName == value.JobName);
        if (model is not null)
        {
            await genericRepository.UpdateAsync(model.Id, model);

            return new Response<JobTables>()
            {
                StatusCode = 403,
                Message = "Job already exists",
                Value = null
            };
        }

        var mappedModel = new JobTables()
        {
           Categories = value.Categories,
           JobName = value.JobName,
           City = value.City,
           Salary = value.Salary,
           HowToBecomeOne = value.HowToBecomeOne,
           Salary_Info = value.Salary_Info,
           WhatTheyDo = value.WhatTheyDo
        };

        var result = await this.genericRepository.CreatAsync(mappedModel);

        return new Response<JobTables>()
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
                Message = "Job is not found",
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

    public async Task<Response<List<JobTables>>> GetAllAsync()
    {
        var result = await this.genericRepository.GetAllAsync();
        return new Response<List<JobTables>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<JobTables>> GetByIdAsync(long id)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<JobTables>()
            {
                StatusCode = 404,
                Message = "Job is not found",
                Value = null
            };

        return new Response<JobTables>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = model
        };
    }

    public async Task<Response<JobTables>> UpdateAsync(long id, JobTableCreationDto value)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<JobTables>()
            {
                StatusCode = 404,
                Message = "Job is not found",
                Value = null
            };

        var mappedModel = new JobTables()
        {
            Categories = value.Categories,
            JobName = value.JobName,
            City = value.City,
            Salary = value.Salary,
            HowToBecomeOne = value.HowToBecomeOne,
            Salary_Info = value.Salary_Info,
            WhatTheyDo = value.WhatTheyDo
        };

        var result = await this.genericRepository.UpdateAsync(id, mappedModel);
        return new Response<JobTables>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }
}
