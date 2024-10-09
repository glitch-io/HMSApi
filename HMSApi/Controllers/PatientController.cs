using HMSApi.Models.DTO;
using HMSApi.Models;
using Microsoft.AspNetCore.Mvc;
using HMSApi.Context;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly HospitalContext _context;

    public PatientsController(HospitalContext context)
    {
        _context = context;
    }

    // POST: api/Patients
    [HttpPost]
    public async Task<ActionResult<Patient>> PostPatient([FromForm] PatientCreateDTO patientDto)
    {
        var patient = new Patient
        {
            Name = patientDto.Name,
            Age = patientDto.Age,
            Address = patientDto.Address,
            MedicalHistory = patientDto.MedicalHistory,
            PatientType = patientDto.PatientType,
            AdmissionDate = patientDto.AdmissionDate,
            DischargeDate = patientDto.DischargeDate,
            AppointmentDate = patientDto.AppointmentDate
        };

        if (patientDto.Image != null && patientDto.Image.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await patientDto.Image.CopyToAsync(memoryStream);
                patient.Image = memoryStream.ToArray();
            }
        }

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientID }, patient);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient(int id)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
        {
            return NotFound();
        }

        return patient;
    }

    // GET: api/Patients/{id}/image
    [HttpGet("{id}/image")]
    public async Task<IActionResult> GetPatientImage(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null || patient.Image == null)
        {
            return NotFound();
        }

        return File(patient.Image, "image/jpeg");
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
    {
        return await _context.Patients.ToListAsync();
    }

    // PUT: api/Patients/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(int id, [FromForm] PatientCreateDTO patientDto)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return NotFound();
        }

        patient.Name = patientDto.Name;
        patient.Age = patientDto.Age;
        patient.Address = patientDto.Address;
        patient.MedicalHistory = patientDto.MedicalHistory;
        patient.PatientType = patientDto.PatientType;
        patient.AdmissionDate = patientDto.AdmissionDate;
        patient.DischargeDate = patientDto.DischargeDate;
        patient.AppointmentDate = patientDto.AppointmentDate;

        if (patientDto.Image != null && patientDto.Image.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await patientDto.Image.CopyToAsync(memoryStream);
                patient.Image = memoryStream.ToArray();
            }
        }

        _context.Entry(patient).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Patients.Any(e => e.PatientID == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Patients/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return NotFound();
        }

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
