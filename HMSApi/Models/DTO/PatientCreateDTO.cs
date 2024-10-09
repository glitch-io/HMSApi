namespace HMSApi.Models.DTO
{
    public class PatientCreateDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string MedicalHistory { get; set; }
        public PatientType PatientType { get; set; }

        // For Inpatient
        public DateTime? AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }

        // For Outpatient
        public DateTime? AppointmentDate { get; set; }

        // Image file (received from form)
        public IFormFile Image { get; set; }
    }

}
