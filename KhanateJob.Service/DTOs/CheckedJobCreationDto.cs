using KhanateJob.Domain.Enums;

namespace KhanateJob.Service.DTOs;

public class CheckedJobCreationDto
{
    public long JobId { get; set; }
    public long UserId { get; set; }
    public long ResumeId { get; set; }
    public RequestStatus Request { get; set; } = RequestStatus.pending;
}
