namespace KhanateJob.Service.DTOs;

public class ResumeCreationDto
{
    public long UserId { get; set; }
    public string Objective { get; set; }
    public string Education { get; set; }
    public string Experince { get; set; }
    public string Activities { get; set; }
    public decimal Salary { get; set; }
    public long JobTableId { get; set; }
}
