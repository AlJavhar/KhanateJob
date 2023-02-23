using KhanateJob.Domain.Entities;
using KhanateJob.Service.DTOs;
using KhanateJob.Service.Helpers;

namespace KhanateJob.Service.Interfaces;

public interface ILikeJobService
{
    Task<Response<LikeJobs>> CreateAsync(JobTableCreationDto value);
    Task<Response<LikeJobs>> UpdateAsync(long id, JobTableCreationDto value);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<LikeJobs>> GetByIdAsync(long id);
    Task<Response<List<LikeJobs>>> GetAllAsync();
}
