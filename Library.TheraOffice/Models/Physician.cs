using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TheraOffice.Models
{
    public class Physician
    {
        public string? Name {  get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime GraduationDate { get; set; } = DateTime.Today;
        public string? Specialization { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{Id}. Name: {Name} - {LicenseNumber} - {Specialization}";
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
