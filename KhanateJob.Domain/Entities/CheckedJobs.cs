using KhanateJob.Domain.Commons;
using KhanateJob.Domain.Enums;

namespace KhanateJob.Domain.Entities;

public class CheckedJobs : Auditable
{
    public long JobId { get; set; }
    public long UserId { get; set; }
    public long ResumeId { get; set; }
    public RequestStatus Request { get; set; } = RequestStatus.pending;
}
