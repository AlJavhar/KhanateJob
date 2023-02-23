using KhanateJob.Domain.Entities;
using KhanateJob.Service.DTOs;
using KhanateJob.Service.Helpers;

namespace KhanateJob.Service.Interfaces;

public interface IJobService
{ 
    Task<Response<JobTables>> CreateAsync(JobTableCreationDto value);
    Task<Response<JobTables>> UpdateAsync(long id, JobTableCreationDto value);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<JobTables>> GetByIdAsync(long id);
    Task<Response<List<JobTables>>> GetAllAsync();
}
