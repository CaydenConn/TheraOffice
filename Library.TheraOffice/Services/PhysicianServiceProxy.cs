using Library.TheraOffice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TheraOffice.Services
{
    public class PhysicianServiceProxy
    {
        private Dictionary<int, Physician> physicians;
        private PhysicianServiceProxy()
        {
            physicians = new Dictionary<int, Physician>();
        }

        private static PhysicianServiceProxy? instance;
        private static object instanceLock = new object();

        public static PhysicianServiceProxy Current
        { 
            get
            {
                lock(instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new PhysicianServiceProxy();
                    }
                }
                return instance;
            } 
        }
        public Dictionary<int, Physician> Physicians { get { return physicians; } }
        public Physician? GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            if (physicians.TryGetValue(id, out var physician))
            {
                return physician;
            }
            return null;
        }
        public Physician? CreatePhysician(Physician physician)
        {
            if (physician == null) { return null; }
            
            if(!long.TryParse(physician.LicenseNumber ?? "-1", out long number))
            {
                Console.WriteLine("Invalid License Number, Must Only Contain Digits (0 - 9)");
                return null;
            }

            if(physician.GraduationDate == DateOnly.MinValue)
            {
                Console.WriteLine("Invalid Gradiation Date");
                return null;
            }
            if (physician.Id <= 0)
            {
                var maxId = -1;
                if (physicians.Any())
                {
                    maxId = physicians.Keys.Max();
                }
                else
                {
                    maxId = 0;
                }
                maxId += 1;
                physician.Id = maxId;
            }

            if (physicians.ContainsKey(physician.Id))
            {
                physicians[physician.Id] = physician;
            }
            else
            {
                physicians.Add(physician.Id, physician);
            }
            return physician;
        }

        public Physician? DeletePhysician(int id)
        {
            if (!physicians.ContainsKey(id)) { return null; }
            var deletedPhysician = physicians[id];
            physicians.Remove(id);
            var appointmentsToDelete = AppointmentServiceProxy.Current.Appointments.Values.
                                                    Where(appt => appt.PhysicianId == id).
                                                    Select(appt => appt.Id).
                                                    ToList();

            foreach (var apptId in appointmentsToDelete)
            {
                AppointmentServiceProxy.Current.Appointments.Remove(apptId);
            }

            return deletedPhysician;
        }
    }
}
