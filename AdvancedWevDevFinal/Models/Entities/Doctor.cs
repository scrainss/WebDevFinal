using System.ComponentModel.DataAnnotations;

namespace AdvancedWevDevFinal.Models.Entities;

public class Doctor
{
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Specialization Specialization { get; set; }
    [StringLength(500)]

    public int PatientId { get; set; }
    public Patient? Patient { get; set; }
}