using Library.TheraOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.TheraOffice.Services
{
    public class PatientServiceProxy
    {
        private Dictionary<int, Patient> patients;
        
        private PatientServiceProxy() { 
            patients = new Dictionary<int, Patient>();
        }

        private static PatientServiceProxy? instance;
        private static object instanceLock = new object();
    
        public static PatientServiceProxy Current 
        { 
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PatientServiceProxy();
                    }
                }
                return instance;
            } 
        }

        public Dictionary<int, Patient> Patients { get { return patients; } }

        public Patient? GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            if (patients.TryGetValue(id, out var patient))
            {
                return patient;
            }
            return null;
        }
        
        public Patient? AddOrUpdate(Patient? patient)
        {
            if (patient == null) return null;
            // If the Id is not a valid Id find the next valid Id and set it

            if(patient.Birthday == DateTime.MinValue)
            {
                //Console.WriteLine("Invalid Birth Date");
                //return null;
            }

            if (patient.Id <= 0)
            {
                var maxId = -1;
                if (patients.Any())
                {
                    maxId = patients.Keys.Max();
                }
                else
                {
                    maxId = 0;
                }
                maxId += 1;
                patient.Id = maxId;
            }

            // If the Dictionary already has the key, update it, else add it to the dictionary
            if (patients.ContainsKey(patient.Id))
            {
                patients[patient.Id] = patient;
            }
            else
            {
                patients.Add(patient.Id, patient);
            }
            return patient;
        }

        public Patient? Delete(int id)
        {
            if (!patients.ContainsKey(id)) { return null; }
            var deletedPatient = patients[id];
            patients.Remove(id);

            return deletedPatient;
        }
    }
}
