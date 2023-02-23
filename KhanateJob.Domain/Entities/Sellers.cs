using KhanateJob.Domain.Commons;

namespace KhanateJob.Domain.Entities;

public class Sellers : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
