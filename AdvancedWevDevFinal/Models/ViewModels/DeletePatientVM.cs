using AdvancedWevDevFinal.Models.Entities;

namespace AdvancedWevDevFinal.Models.ViewModels;

public class DeletePatientVM
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public ICollection<Doctor>? Doctors { get; set; }
}