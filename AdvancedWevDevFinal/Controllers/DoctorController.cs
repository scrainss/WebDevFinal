using AdvancedWevDevFinal.Models;
using AdvancedWevDevFinal.Models.ViewModels;
using AdvancedWevDevFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedWevDevFinal.Controllers;

//injection of patient repository
public class DoctorController : Controller
{
    private readonly IPatientRepository _patientRepo;

    public DoctorController(IPatientRepository patientRepo)
    {
        _patientRepo = patientRepo;
    }
    //GET create
    public async Task<IActionResult> Create([Bind(Prefix = "id")] int patientId)
    {
        var patient = await _patientRepo.ReadPatientAsync(patientId);
        if (patient == null)
        {
            return RedirectToAction("Index", "Patient");
        }
        var doctorVM = new CreateDoctorVM
        {
            Patient = patient
        };
        return View(doctorVM);
    }

    //POST create doctor
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int patientId, CreateDoctorVM doctorVM)
    {
        if (ModelState.IsValid)
        {
            var doctor = doctorVM.GetDiagnosisInstance();
            await _patientRepo.CreateDoctorAsync(patientId, doctor);
            return RedirectToAction("Index", "Patient");
        }
        doctorVM.Patient = await _patientRepo.ReadPatientAsync(patientId);
        return View(doctorVM);
    }


    //GET edit

    public async Task<IActionResult> Edit([Bind(Prefix = "id")] int patientId, int doctorId)
    {
        var patient = await _patientRepo.ReadPatientAsync(patientId);
        if (patient == null)
        {
            return RedirectToAction("Index", "Patient");
        }
        var doctor = patient.Doctors.FirstOrDefault(d => d.Id == doctorId);
        if (doctor == null)
        {
            return RedirectToAction("Details", "Patient", new { id = patientId });
        }
        var model = new EditDoctorVM
        {
            Patient = patient,
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Specialization = doctor.Specialization
        };
        return View(model);
    }

    //POST edit doctor
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int patientId, EditDoctorVM diagnosisVM)
    {
        if (ModelState.IsValid)
        {
            var doctor =
                diagnosisVM.GetDoctorInstance();
            await _patientRepo.UpdateDoctorAsync(
                patientId, doctor);
            return RedirectToAction("Details", "Patient", new { id = patientId });
        }
        diagnosisVM.Patient = await _patientRepo.ReadPatientAsync(patientId);
        return View(diagnosisVM);
    }

    //GET delete for diagnosis
    public async Task<IActionResult> Delete([Bind(Prefix = "id")] int patientId, int doctorId)
    {
        var patient = await _patientRepo.ReadPatientAsync(patientId);
        if (patient == null)
        {
            return RedirectToAction("Index", "Patient");
        }
        var doctor = patient.Doctors.FirstOrDefault(d => d.Id == doctorId);
        if (doctor == null)
        {
            return RedirectToAction("Details", "Patient", new { id = patientId });
        }
        var model = new DeleteDoctorVM
        {
            Patient = patient,
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Specialization = doctor.Specialization
        };
        return View(model);
    }

    //POST delete for diagnosis
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, int patientId)
    {
        await _patientRepo.DeleteDoctorAsync(patientId, id);
        return RedirectToAction("Details", "Patient", new { id = patientId });
    }
}
