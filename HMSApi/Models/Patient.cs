namespace HMSApi.Models
{

        public class Patient
        {
            public int PatientID { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Address { get; set; }
            public string MedicalHistory { get; set; }
            public PatientType PatientType { get; set; }

            public DateTime? AdmissionDate { get; set; }
            public DateTime? DischargeDate { get; set; }

            public DateTime? AppointmentDate { get; set; }

            public byte[] Image { get; set; }
        }
    public enum PatientType
    {
        Inpatient,
        Outpatient
    }

}

