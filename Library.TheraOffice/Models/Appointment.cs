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
        public DateTime StartTime { get; set; } = DateTime.Today;
        public DateTime EndTime { get; set; }
        public string? RoomId { get; set; }

        public override string ToString()
        {
            if (Patient == null || Physician == null)
            {
                return $"{Id}. {StartTime:MM/dd/yy} {StartTime:hh\\:mm} - {EndTime:hh\\:mm}: {PatientId} with {PhysicianId}";
            }

            return $"{Id}. {StartTime:MM/dd/yy} {StartTime:hh\\:mm} - {EndTime:hh\\:mm}: {Patient.Name}({Patient.Id}) with {Physician.Name}({Physician.Id})";
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
