using Library.TheraOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TheraOffice.Services
{
    public class AppointmentServiceProxy
    {
        private Dictionary<int, Appointment> appointments;
        private AppointmentServiceProxy()
        {
            appointments = new Dictionary<int, Appointment>();
        }

        private static AppointmentServiceProxy? instance;
        private static object instanceLock = new object();

        public static AppointmentServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AppointmentServiceProxy();
                    }
                }
                return instance;
            }
        }
        public Dictionary<int, Appointment> Appointments { get { return appointments; } }
        public Appointment? GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            if (appointments.TryGetValue(id, out var appt))
            {
                return appt;
            }
            return null;
        }
        public Appointment? AddOrUpdate(Appointment? appt)
        {
            // Check if 'appt' parameter exists
            if (appt == null) return null;

            if (appt.StartTime == DateTime.MinValue || appt.EndTime == DateTime.MinValue)
            {
                Console.WriteLine("Error With Appointment Date or Time");
                return null;
            }

            // Makes sure the appointment has a valid Id
            if (appt.Id <= 0)
            {
                var maxId = -1;
                if (appointments.Any())
                {
                    maxId = appointments.Keys.Max();
                }
                else
                {
                    maxId = 0;
                }
                maxId += 1;
                appt.Id = maxId;
            }

            // Checks to see if the Date + Time fall within Business Hours
            if (appt.StartTime.Hour < 8 || (appt.EndTime.Hour > 17 && appt.EndTime.Minute >= 0) || appt.StartTime >= appt.EndTime)
            {
                Console.WriteLine("Appointment must be within business hours: 8am - 5pm");
                return null;
            }
            if (appt.StartTime.DayOfWeek == DayOfWeek.Saturday || appt.StartTime.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine("Appointment must be within business days: Monday - Friday");
                return null;
            }

            // Checks if appointment spans multiple days
            if (appt.StartTime.Date != appt.EndTime.Date)
            {
                Console.WriteLine("Cannot have an appointment spanning multiple days");
                return null;
            }

            // Checks to see if there is already an appointment at that given Date + Time
            bool overlap = appointments.Values.Any(existing => (existing?.Physician?.Id == appt?.Physician?.Id || existing?.Patient?.Id == appt?.Patient?.Id)
                                            && existing?.StartTime < appt?.EndTime
                                            && appt?.StartTime < existing?.EndTime);
            if (overlap)
            {
                Console.WriteLine("Physician is booked at this time");
                return null;
            }

            // Checks to see if there is already a room booked for this appointment
            bool roomOverlap = appointments.Values.Any(existing => existing?.RoomId == appt?.RoomId
                                            && existing?.StartTime < appt?.EndTime
                                            && appt?.StartTime < existing?.EndTime);
            if (roomOverlap)
            {
                Console.WriteLine("Room is booked at this time");
                return null;
            }

            // If the Appointment passess all the checks add it to the dictionary - Format: (Id, Appointment)
            if (appointments.ContainsKey(appt.Id))
            {
                appointments[appt.Id] = appt;
            }
            else
            {
                appointments.Add(appt.Id, appt);
            }
            return appt;
        }

        public Appointment? Delete(int id)
        {
            // If the Appointment id does not exist return null
            if (!appointments.ContainsKey(id)) { return null; }
            var deletedAppointment = appointments[id];
            appointments.Remove(id);
            return deletedAppointment;
        }
        
        public bool ListAppointments()
        {
            if (appointments == null) return false;

            foreach (var appt in appointments.Values)
            {
                Console.WriteLine("Appointments");
                Console.WriteLine(appt);
            }
            return true;
        }
    }
}
