using AdvancedWevDevFinal.Models.Entities;

namespace AdvancedWevDevFinal.Models.ViewModels;

public class EditDoctorVM
{
    public Patient? Patient { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Specialization Specialization { get; set; }
    public Doctor GetDoctorInstance()
    {
        return new Doctor
        {
            Id = this.Id,
            FirstName = this.FirstName, 
            LastName = this.LastName,
            Specialization = this.Specialization
        };
    }
}