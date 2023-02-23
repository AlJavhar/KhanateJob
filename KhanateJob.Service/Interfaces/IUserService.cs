using KhanateJob.Domain.Entities;
using KhanateJob.Service.Dtos;
using KhanateJob.Service.Helpers;

namespace KhanateJob.Service.Interfaces;

public interface IUserService
{
    Task<Response<Users>>CreateAsync(UserCreationDto value);
    Task<Response<Users>> UpdateAsync(long id, UserCreationDto value);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<Users>> GetByIdAsync(long id);
    Task<Response<List<Users>>> GetAllAsync();
}
