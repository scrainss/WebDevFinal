using AdvancedWevDevFinal.Models.Entities;

namespace AdvancedWevDevFinal.Models.ViewModels;

public class CreateDoctorVM
{
    public Patient? Patient { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Specialization Specialization { get; set; }

    public Doctor GetDiagnosisInstance()
    {
        return new Doctor
        {
            Id = 0,
            FirstName = this.FirstName,
            LastName = this.LastName,
            Specialization = this.Specialization
            
        };
    }
}