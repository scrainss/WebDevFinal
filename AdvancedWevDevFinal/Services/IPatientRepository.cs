using AdvancedWevDevFinal.Models.Entities;

namespace AdvancedWevDevFinal.Services;

public interface IPatientRepository
{
    public Task<Patient> CreatePatientAsync(Patient patient);
    public Task<Patient> ReadPatientAsync(int id);
    public Task<ICollection<Patient>> ReadAllAsync();

    public Task UpdatePatientAsync(int patientId, Patient patient);
    public Task DeletePatientAsync(int patientId);


    public Task<Doctor> CreateDoctorAsync(int patientId, Doctor doctor);
    public Task<Doctor> ReadDoctorAsync(int id);
    public Task UpdateDoctorAsync(int patientId, Doctor doctor);
    public Task DeleteDoctorAsync(int patientId, int doctorId);

}