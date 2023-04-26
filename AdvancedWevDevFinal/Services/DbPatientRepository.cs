using AdvancedWevDevFinal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedWevDevFinal.Services;

public class DbPatientRepository : IPatientRepository
{
    //injection of DbContext
    private readonly ApplicationDbContext _db;

    public DbPatientRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    //create patient
    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        await _db.Patients.AddAsync(patient);
        await _db.SaveChangesAsync();
        return patient;
    }

    //create diagnosis by diagnosis id for a patient by patientId
    public async Task<Doctor> CreateDoctorAsync(int patientId, Doctor doctor)
    {
        var patient = await ReadPatientAsync(patientId);
        if (patient != null)
        {
            patient.Doctors.Add(doctor);
            doctor.Patient = patient;
            await _db.SaveChangesAsync();
        }
        return doctor;
    }

    //read patient by id
    public async Task<Patient?> ReadPatientAsync(int id)
    {
        var patient = await _db.Patients.FindAsync(id);
        if (patient != null)
        {
            _db.Entry(patient)
              .Collection(p => p.Doctors)
              .Load();
        }
        return patient;
    }

    //read diagnosis by id
    public async Task<Doctor?> ReadDoctorAsync(int id)
    {
        var doctor = await _db.Doctors.FindAsync(id);
        if (doctor != null)
        {
            _db.Entry(doctor);
        }
        return doctor;
    }

    //read all method, providing a list of patients and their diagnoses
    public async Task<ICollection<Patient>> ReadAllAsync()
    {
        return await _db.Patients
               .Include(p => p.Doctors)
               .ToListAsync();
    }

    //update patient by id
    public async Task UpdatePatientAsync(int patientId, Patient patient)
    {
        if (patient != null)
        {
            var patientToUpdate = await ReadPatientAsync(patientId);
            if (patientToUpdate != null)
            {
                patientToUpdate.FirstName = patient.FirstName;
                patientToUpdate.LastName = patient.LastName;
                patientToUpdate.Birthday = patient.Birthday;
                await _db.SaveChangesAsync();
            }
        }
    }

    //update doctor by patientId
    public async Task UpdateDoctorAsync(int patientId, Doctor updatedDoctor)
    {
        var patient = await ReadPatientAsync(patientId);
        if (patient != null)
        {
            var doctorToUpdate = patient.Doctors.FirstOrDefault(b => b.Id == updatedDoctor.Id);
            if (doctorToUpdate != null)
            {
                doctorToUpdate.FirstName = updatedDoctor.FirstName;
                doctorToUpdate.LastName = updatedDoctor.LastName;
                doctorToUpdate.Specialization = updatedDoctor.Specialization;
                
                await _db.SaveChangesAsync();
            }
        }
    }

    //delete patient by id
    public async Task DeletePatientAsync(int patientId)
    {
        var patientToDelete = await ReadPatientAsync(patientId);
        if (patientToDelete != null)
        {
            _db.Patients.Remove(patientToDelete);
            await _db.SaveChangesAsync();
        }
    }
    //delete diagnosis by id for a patient specified by id
    public async Task DeleteDoctorAsync(int patientId, int doctorId)
    {
        var patient = await ReadPatientAsync(patientId);
        if (patient != null)
        {
            var doctor = patient.Doctors
               .FirstOrDefault(d => d.Id == doctorId);
            if (doctor != null)
            {
                patient.Doctors.Remove(doctor);
                await _db.SaveChangesAsync();
            }
        }

    }
}
