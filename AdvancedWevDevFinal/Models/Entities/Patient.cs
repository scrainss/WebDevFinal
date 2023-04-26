using System.ComponentModel.DataAnnotations;
using AdvancedWevDevFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedWevDevFinal.Models.Entities;

public class Patient
{
    public int Id { get; set; }
    [StringLength(20)]
    public string FirstName { get; set; }
    [StringLength(20)]
    public string LastName { get; set; }
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }

    public ICollection<Doctor> Doctors { get; set; }
        = new List<Doctor>();
}