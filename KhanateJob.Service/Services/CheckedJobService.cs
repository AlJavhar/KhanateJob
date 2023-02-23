using KhanateJob.DAL.IRepositories;
using KhanateJob.DAL.Repositories;
using KhanateJob.Domain.Entities;
using KhanateJob.Service.DTOs;
using KhanateJob.Service.Helpers;
using KhanateJob.Service.Interfaces;

namespace KhanateJob.Service.Services;

public class CheckedJobService : IChekedJobService
{
    private readonly IGenericRepository<CheckedJobs> genericRepository;

    public CheckedJobService(IGenericRepository<CheckedJobs> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<Response<CheckedJobs>> CreateAsync(CheckedJobCreationDto value)
    {
        var models = await this.genericRepository.GetAllAsync();

        var model = models.FirstOrDefault(x => (x.UserId == value.UserId && x.JobId == value.JobId));
        if (model is not null)
        {
            await genericRepository.UpdateAsync(model.Id, model);

            return new Response<CheckedJobs>()
            {
                StatusCode = 403,
                Message = "Already exists",
                Value = null
            };
        }

        var mappedModel = new CheckedJobs()
        {
           JobId = value.JobId,
           UserId = value.UserId,
           ResumeId = value.ResumeId,
           Request = value.Request
        };

        var result = await this.genericRepository.CreatAsync(mappedModel);

        return new Response<CheckedJobs>()
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
                Message = "Not found",
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

    public async Task<Response<List<CheckedJobs>>> GetAllAsync()
    {
        var result = await this.genericRepository.GetAllAsync();
        return new Response<List<CheckedJobs>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<CheckedJobs>> UpdateAsync(long id, CheckedJobCreationDto value)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<CheckedJobs>()
            {
                StatusCode = 404,
                Message = "Not found",
                Value = null
            };

        var mappedModel = new CheckedJobs()
        {
            UserId = value.UserId,
            JobId = value.JobId,
            ResumeId = value.ResumeId,
            Request = value.Request
        };

        var result = await this.genericRepository.UpdateAsync(id, mappedModel);
        return new Response<CheckedJobs>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }
}
