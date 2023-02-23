using KhanateJob.DAL.IRepositories;
using KhanateJob.DAL.Repositories;
using KhanateJob.Domain.Entities;
using KhanateJob.Service.DTOs;
using KhanateJob.Service.Helpers;
using KhanateJob.Service.Interfaces;

namespace KhanateJob.Service.Services;

public class SellerService : ISellerService
{
    private readonly IGenericRepository<Sellers> genericRepository;

    public SellerService(IGenericRepository<Sellers> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<Response<Sellers>> CreateAsync(SellerCreationDto value)
    {
        var models = await this.genericRepository.GetAllAsync();
        var model = models.FirstOrDefault(x => x.FirstName == value.FirstName);
        if (model is not null)
        {
            await genericRepository.UpdateAsync(model.Id, model);

            return new Response<Sellers>()
            {
                StatusCode = 403,
                Message = "Seller already exists",
                Value = null
            };
        }

        var mappedModel = new Sellers()
        {
            FirstName = value.FirstName,
            LastName  = value.LastName,
            Email = value.Email,
            Password = value.Password
        };

        var result = await this.genericRepository.CreatAsync(mappedModel);

        return new Response<Sellers>()
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
                Message = "Seller is not found",
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

    public async Task<Response<List<Sellers>>> GetAllAsync()
    {
        var result = await this.genericRepository.GetAllAsync();
        return new Response<List<Sellers>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<Sellers>> GetByIdAsync(long id)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Sellers>()
            {
                StatusCode = 404,
                Message = "Seller is not found",
                Value = null
            };

        return new Response<Sellers>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = model
        };
    }

    public async Task<Response<Sellers>> UpdateAsync(long id, SellerCreationDto value)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Sellers>()
            {
                StatusCode = 404,
                Message = "Seller is not found",
                Value = null
            };

        var mappedModel = new Sellers()
        {
           FirstName = value.FirstName,
           LastName = value.LastName,
           Email = value.Email,
           Password = value.Password
        };

        var result = await this.genericRepository.UpdateAsync(id, mappedModel);
        return new Response<Sellers>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }
}
