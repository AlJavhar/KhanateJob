using KhanateJob.Domain.Entities;
using KhanateJob.Service.DTOs;
using KhanateJob.Service.Helpers;

namespace KhanateJob.Service.Interfaces;

public interface IChekedJobService
{
    Task<Response<CheckedJobs>> CreateAsync(CheckedJobCreationDto value);
    Task<Response<CheckedJobs>> UpdateAsync(long id, CheckedJobCreationDto value);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<List<CheckedJobs>>> GetAllAsync();
    
}
