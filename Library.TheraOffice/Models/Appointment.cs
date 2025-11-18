using Library.TheraOffice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TheraOffice.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        public int PhysicianId { get; set; }
        public Physician? Physician { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public override string ToString()
        {
            if (Patient == null || Physician == null)
            {
                return $"{Id}. {StartTime}: {PatientId} with {PhysicianId} ends at {EndTime}";
            }

            return $"{Id}. {StartTime}: {Patient.Name}({Patient.Id}) with {Physician.Name}({Physician.Id}) ends at {EndTime}";
        }
        public string Display
        {
            get
            {
                return ToString();
            }
        }
    }
}
