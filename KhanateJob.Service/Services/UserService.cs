using KhanateJob.DAL.IRepositories;
using KhanateJob.Domain.Entities;
using KhanateJob.Service.Dtos;
using KhanateJob.Service.DTOs;
using KhanateJob.Service.Helpers;
using KhanateJob.Service.Interfaces;

namespace KhanateJob.Service.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<Users> genericRepository;

    public UserService(IGenericRepository<Users> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<Response<Users>> CreateAsync(UserCreationDto value)
    {
        var models = await this.genericRepository.GetAllAsync();
        var model = models.FirstOrDefault(x => x.FirstName == value.FirstName);
        if (model is not null)
        {
            await genericRepository.UpdateAsync(model.Id, model);

            return new Response<Users>()
            {
                StatusCode = 403,
                Message = "Users already exists",
                Value = null
            };
        }

        var mappedModel = new Users()
        {
            FirstName = value.FirstName,
            LastName = value.LastName,
            Email = value.Email,
            Password = value.Password
        };

        var result = await this.genericRepository.CreatAsync(mappedModel);

        return new Response<Users>()
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
                Message = "Users is not found",
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

    public async Task<Response<List<Users>>> GetAllAsync()
    {
        var result = await this.genericRepository.GetAllAsync();
        return new Response<List<Users>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<Users>> GetByIdAsync(long id)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Users>()
            {
                StatusCode = 404,
                Message = "Users is not found",
                Value = null
            };

        return new Response<Users>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = model
        };
    }

    public async Task<Response<Users>> UpdateAsync(long id, UserCreationDto value)
    {
        var model = await this.genericRepository.GetByIdAsync(id);
        if (model is null)
            return new Response<Users>()
            {
                StatusCode = 404,
                Message = "Users is not found",
                Value = null
            };

        var mappedModel = new Users()
        {
            FirstName = value.FirstName,
            LastName = value.LastName,
            Email = value.Email,
            Password = value.Password
        };

        var result = await this.genericRepository.UpdateAsync(id, mappedModel);
        return new Response<Users>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }
}
