using AdvancedWevDevFinal.Models.Entities;

namespace AdvancedWevDevFinal.Models.ViewModels;

public class DeleteDoctorVM
{
    public Patient? Patient { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Specialization Specialization { get; set; }
}
