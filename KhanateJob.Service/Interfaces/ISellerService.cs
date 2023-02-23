using KhanateJob.Domain.Entities;
using KhanateJob.Service.DTOs;
using KhanateJob.Service.Helpers;

namespace KhanateJob.Service.Interfaces;

public interface ISellerService
{
    Task<Response<Sellers>> CreateAsync(SellerCreationDto value);
    Task<Response<Sellers>> UpdateAsync(long id, SellerCreationDto value);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<Sellers>> GetByIdAsync(long id);
    Task<Response<List<Sellers>>> GetAllAsync();
}
