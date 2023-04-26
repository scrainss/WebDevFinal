using AdvancedWevDevFinal.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AdvancedWevDevFinal.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
}