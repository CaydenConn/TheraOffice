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
        public int PhysicianId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public override string ToString()
        {
            return $"{Id}. Patient: {PatientServiceProxy.Current.Patients[PatientId].Name}({PatientId}) Physician: {PhysicianServiceProxy.Current.Physicians[PhysicianId].Name}({PhysicianId})\n   Time: {StartTime} - {EndTime}";
        }
    }
}
