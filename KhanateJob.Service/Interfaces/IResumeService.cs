using KhanateJob.Domain.Entities;
using KhanateJob.Service.DTOs;
using KhanateJob.Service.Helpers;

namespace KhanateJob.Service.Interfaces;

public interface IResumeService
{
    Task<Response<Resumes>> CreateAsync(ResumeCreationDto value);
    Task<Response<Resumes>> UpdateAsync(long id, ResumeCreationDto value);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<Resumes>> GetByIdAsync(long id);
    Task<Response<List<Resumes>>> GetAllAsync();
    Task<Response<Resumes>> GetSuitableResume(long id);
}
