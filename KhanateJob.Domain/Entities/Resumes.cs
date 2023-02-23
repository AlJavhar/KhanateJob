using KhanateJob.Domain.Commons;

namespace KhanateJob.Domain.Entities;

public class Resumes : Auditable
{
    public long UserId { get; set; }
    public string Objective { get; set; }
    public string Education { get; set; }
    public string Experince { get; set; }
    public string Activities { get; set; }
    public decimal Salary { get; set; }
    public long JobTableId { get; set; }

}
