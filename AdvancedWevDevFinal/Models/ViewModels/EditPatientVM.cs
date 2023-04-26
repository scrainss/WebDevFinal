using AdvancedWevDevFinal.Models.Entities;
using System.ComponentModel;

namespace AdvancedWevDevFinal.Models.ViewModels;

public class EditPatientVM
{
    public Patient? Patient { get; set; }
    public int Id { get; set; }
    [DisplayName("First Name")]
    public string? FirstName { get; set; }
    [DisplayName("Last Name")]
    public string? LastName { get; set; }
    [DisplayName("Birthday")]
    public DateTime Birthday { get; set; }

    public ICollection<Doctor>? Doctors { get; set; }

    public Patient GetPatientInstance()
    {
        return new Patient
        {
            Id = this.Id,
            FirstName = this.FirstName ?? string.Empty,
            LastName = this.LastName ?? string.Empty,
            Birthday = this.Birthday,
            Doctors = this.Doctors
        };
    }
}