using AdvancedWevDevFinal.Models.Entities;

namespace AdvancedWevDevFinal.Models.ViewModels;

public class CreatePatientVM
{
    public Patient? Patient { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public Patient GetPatientInstance()
    {
        return new Patient
        {
            Id = 0,
            FirstName = this.FirstName,
            LastName = this.LastName,
            Birthday = this.Birthday
        };
    }
}