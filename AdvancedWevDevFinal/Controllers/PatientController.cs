using AdvancedWevDevFinal.Services;
using Microsoft.AspNetCore.Mvc;
using AdvancedWevDevFinal.Models.ViewModels;

namespace AdvancedWevDevFinal.Controllers;

public class PatientController : Controller
{
    //
    private readonly IPatientRepository _patientRepo;

    public PatientController(IPatientRepository patientRepo)
    {
        _patientRepo = patientRepo;
    }

    public async Task<IActionResult> Index()
    {
        var patients = await _patientRepo.ReadAllAsync();
        return View(patients);
    }

    public async Task<IActionResult> Details(int id)
    {
        var patient = await _patientRepo.ReadPatientAsync(id);
        if (patient == null)
        {
            return RedirectToAction("Index");
        }
        return View(patient);
    }

    public IActionResult Create()
    {
        return View();
    }
    //POST for create patient
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePatientVM createPatientVM)
    {
        if (ModelState.IsValid)
        {
            await _patientRepo.CreatePatientAsync(createPatientVM.GetPatientInstance());
            return RedirectToAction("Index", "Patient");
        }
        return View(createPatientVM);
    }
    //GET edit for Patient
    public async Task<IActionResult> Edit(int patientId)
    {
        var patient = await _patientRepo.ReadPatientAsync(patientId);
        if (patient == null)
        {
            return RedirectToAction("Index", "Patient");
        }
        var model = new EditPatientVM
        {
            //Patient = patient,
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthday = patient.Birthday,
            Doctors = patient.Doctors
        };
        return View(model);
    }

    //POST for edit patient
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int patientId, EditPatientVM editPatientVM)
    {
        if (ModelState.IsValid)
        {
            var patient = editPatientVM.GetPatientInstance();
            await _patientRepo.UpdatePatientAsync(patientId, patient);
            return RedirectToAction("Details", "Patient", new { id = patientId });
        }

        return RedirectToAction("Index", "Patient");
    }

    //post for edit diagnosis
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int patientId, EditDoctorVM doctorVM)
    {
        if (ModelState.IsValid)
        {
            var doctor = doctorVM.GetDoctorInstance();
            await _patientRepo.UpdateDoctorAsync(patientId, doctor);
            return RedirectToAction("Details", "Patient", new { id = patientId });
        }
        doctorVM.Patient = await _patientRepo.ReadPatientAsync(patientId);
        return View(doctorVM);
    }

    //GET for patient delete
    public async Task<IActionResult> Delete(int id)
    {
        var patient = await _patientRepo.ReadPatientAsync(id);
        if (patient == null)
        {
            return RedirectToAction("Index", "Patient");
        }
        var model = new DeletePatientVM
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthday = patient.Birthday,
            Doctors = patient.Doctors
        };
        return View(model);
    }

    //POST for patient delete
    [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int patientId)
    {
        await _patientRepo.DeletePatientAsync(patientId);
        return RedirectToAction("Index", "Patient");
    }
}
