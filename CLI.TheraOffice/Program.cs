using Library.TheraOffice.Models;
using Library.TheraOffice.Services;

namespace CLI.TheraOffice
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to TheraOffice");
            var runApp = true;
            do
            {
                Console.WriteLine("C. Create a 1) Patient, 2) Physician, or 3) Appointment");
                Console.WriteLine("R. List all 1) Patient, 2) Physician, or 3) Appointment");
                Console.WriteLine("U. Update a 1) Patient, 2) Physician, or 3) Appointment");
                Console.WriteLine("D. Delete a 1) Patient, 2) Physician, or 3) Appointment");
                Console.WriteLine("Q. Quit");
                
                var choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "c":
                    case "C":
                        {
                            Console.WriteLine("Choose (1, 2, or 3): ");
                            var option = Console.ReadLine();
                            switch (option)
                            {
                                case "1":
                                    var patient = new Patient();
                                    Console.WriteLine("Name: ");
                                    patient.Name = Console.ReadLine();

                                    Console.WriteLine("Address: ");
                                    patient.Address = Console.ReadLine();

                                    Console.WriteLine("Birthday: ");
                                    if(DateOnly.TryParse(Console.ReadLine() ?? "1/1/0001", out DateOnly date))
                                    {
                                        patient.Birthday = date;
                                    }

                                    Console.WriteLine("Race: ");
                                    patient.Race = Console.ReadLine();

                                    Console.WriteLine("Gender: ");
                                    patient.Gender = Console.ReadLine();

                                    PatientServiceProxy.Current.AddOrUpdate(patient);
                                    break;
                                case "2":
                                    var physician = new Physician();
                                    Console.WriteLine("Name: ");
                                    physician.Name = Console.ReadLine();

                                    Console.WriteLine("License #: ");
                                    physician.LicenseNumber = Console.ReadLine();

                                    Console.WriteLine("Graduation Date: ");
                                    if (DateOnly.TryParse(Console.ReadLine() ?? "1/1/0001", out DateOnly gradDate))
                                    {
                                        physician.GraduationDate = gradDate;
                                    }

                                    PhysicianServiceProxy.Current.AddOrUpdate(physician);
                                    break;
                                case "3":
                                    var appt = new Appointment();
                                    Console.WriteLine("Patient Id: ");
                                    if(int.TryParse(Console.ReadLine() ?? "-1", out int patientId)){
                                        appt.PatientId = patientId;
                                    }

                                    Console.WriteLine("Physician Id: ");
                                    if (int.TryParse(Console.ReadLine() ?? "-1", out int physicianId)){
                                        appt.PhysicianId = physicianId;
                                    }

                                    Console.WriteLine("Start Time: ");
                                    if(DateTime.TryParse(Console.ReadLine() ?? "1/1/0001 12:00:00 AM", out DateTime startTime))
                                    {
                                        appt.StartTime = startTime;
                                    }
                                    Console.WriteLine("End Time: ");
                                    if(DateTime.TryParse(Console.ReadLine() ?? "1/1/0001 12:00:00 AM", out DateTime endTime))
                                    {
                                        appt.EndTime = endTime;
                                    }

                                    AppointmentServiceProxy.Current.CreateAppointment(appt);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case "r":
                    case "R":
                        {
                            Console.WriteLine("Choose (1, 2, or 3): ");
                            var option = Console.ReadLine();
                            switch (option)
                            {
                                case "1":
                                    foreach(var patient in PatientServiceProxy.Current.Patients.Values)
                                    {
                                        Console.WriteLine(patient);
                                    }
                                    break;
                                case "2":
                                    foreach (var physician in PhysicianServiceProxy.Current.Physicians.Values)
                                    {
                                        Console.WriteLine(physician);
                                    }
                                    break;
                                case "3":
                                    foreach (var appt in AppointmentServiceProxy.Current.Appointments.Values)
                                    {
                                        Console.WriteLine(appt);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case "u":
                    case "U":
                        {
                            Console.WriteLine("Choose (1, 2, or 3): ");
                            var option = Console.ReadLine();
                            switch (option)
                            {
                                case "1":
                                    {
                                        Console.WriteLine("Select Patient to Update(Id): ");
                                        if (int.TryParse(Console.ReadLine() ?? "-1", out int patientId))
                                        {
                                            var patientToUpdate = PatientServiceProxy.Current.Patients.Values.
                                                                                      Where(p => p != null).
                                                                                      FirstOrDefault(p => (p?.Id ?? -1) == patientId);
                                            if(patientToUpdate != null)
                                            {
                                                Console.WriteLine("Name: ");
                                                patientToUpdate.Name = Console.ReadLine();

                                                Console.WriteLine("Address: ");
                                                patientToUpdate.Address = Console.ReadLine();

                                                Console.WriteLine("Birthday: ");
                                                if (DateOnly.TryParse(Console.ReadLine() ?? "1/1/0001", out DateOnly date))
                                                {
                                                    patientToUpdate.Birthday = date;
                                                }

                                                Console.WriteLine("Race: ");
                                                patientToUpdate.Race = Console.ReadLine();

                                                Console.WriteLine("Gender: ");
                                                patientToUpdate.Gender = Console.ReadLine(); Console.WriteLine("Name:");

                                                PatientServiceProxy.Current.AddOrUpdate(patientToUpdate);
                                            }
                                        }
                                            break;
                                    }
                                case "2":
                                    {
                                        Console.WriteLine("Select Physician to Update(Id): ");
                                        if (int.TryParse(Console.ReadLine() ?? "-1", out int physicianId))
                                        {
                                            var physicianToUpdate = PhysicianServiceProxy.Current.Physicians.Values.
                                                                                          Where(p => p != null).
                                                                                          FirstOrDefault(p => (p?.Id ?? -1) == physicianId);
                                            if (physicianToUpdate != null)
                                            {
                                                Console.WriteLine("Name: ");
                                                physicianToUpdate.Name = Console.ReadLine();

                                                Console.WriteLine("License #: ");
                                                physicianToUpdate.LicenseNumber = Console.ReadLine();

                                                Console.WriteLine("Graduation Date: ");
                                                if (DateOnly.TryParse(Console.ReadLine() ?? "1/1/0001", out DateOnly gradDate))
                                                {
                                                    physicianToUpdate.GraduationDate = gradDate;
                                                }

                                                PhysicianServiceProxy.Current.AddOrUpdate(physicianToUpdate);
                                            }
                                        }
                                            break;
                                    }
                                case "3":
                                    {
                                        Console.WriteLine("Select Appointment to Update(Id): ");
                                        if (int.TryParse(Console.ReadLine() ?? "-1", out int apptId))
                                        {
                                            var apptToUpdate = AppointmentServiceProxy.Current.Appointments.Values.
                                                                                          Where(p => p != null).
                                                                                          FirstOrDefault(p => (p?.Id ?? -1) == apptId);
                                            if (apptToUpdate != null)
                                            {
                                                Console.WriteLine("Patient Id: ");
                                                if (int.TryParse(Console.ReadLine() ?? "-1", out int patientId))
                                                {
                                                    apptToUpdate.PatientId = patientId;
                                                }

                                                Console.WriteLine("Physician Id: ");
                                                if (int.TryParse(Console.ReadLine() ?? "-1", out int physicianId))
                                                {
                                                    apptToUpdate.PhysicianId = physicianId;
                                                }

                                                Console.WriteLine("Start Time: ");
                                                apptToUpdate.StartTime = DateTime.Parse(Console.ReadLine() ?? "1/1/0001");

                                                Console.WriteLine("End Time: ");
                                                apptToUpdate.EndTime = DateTime.Parse(Console.ReadLine() ?? "1/1/0001");

                                                AppointmentServiceProxy.Current.CreateAppointment(apptToUpdate);
                                            }
                                        }
                                            break;
                                    }
                                default:
                                    break;
                            }
                        }
                        break;
                    case "d":
                    case "D":
                        {
                            Console.WriteLine("Choose (1, 2, or 3): ");
                            var option = Console.ReadLine();
                            switch (option)
                            {
                                case "1":
                                    Console.WriteLine("Select Patient to Delete(Id): ");
                                    if (int.TryParse(Console.ReadLine() ?? "-1", out int patientId))
                                    {
                                        PatientServiceProxy.Current.Delete(patientId);
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("Select Physician to Delete(Id): ");
                                    if (int.TryParse(Console.ReadLine() ?? "-1", out int physicianId))
                                    {
                                        PhysicianServiceProxy.Current.Delete(physicianId);
                                    }
                                    break;
                                case "3":
                                    Console.WriteLine("Select Appointment to Delete(Id): ");
                                    if (int.TryParse(Console.ReadLine() ?? "-1", out int apptId))
                                    {
                                        AppointmentServiceProxy.Current.DeleteAppointment(apptId);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case "q":
                    case "Q":
                        {
                            runApp = false;
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid Input Must be C, R, U, D, or Q");
                        break;
                }

            } while (runApp);
        }
    }
}
