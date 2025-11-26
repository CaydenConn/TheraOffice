using Library.TheraOffice.Models;
using Library.TheraOffice.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.TheraOffice.ViewModels
{
    public class PatientViewModel
    {
        public PatientViewModel() 
        {
            Model = new Patient();
            SetUpCommands();
        }
        public PatientViewModel(Patient? model)
        {
            Model = model;
            SetUpCommands();
        }
        public PatientViewModel(int patientId)
        {
            Model = PatientServiceProxy.Current.Patients[patientId];
            SetUpCommands();
        }
        public void AddOrUpdate()
        {
            PatientServiceProxy.Current.AddOrUpdate(Model);
        }
        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PatientViewModel));
        }
        private void DoDelete()
        {
            if (Model?.Id > 0)
            {
                PatientServiceProxy.Current.Delete(Model.Id);
                Shell.Current.GoToAsync("//PatientMainView");
            }
        }

        private void DoEdit(PatientViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPatientId = pvm?.Model?.Id ?? 0;
            Shell.Current.GoToAsync($"//PatientView?patientId={selectedPatientId}");
        }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
        public Patient? Model { get; set; }
        private Color? displayBackgroundColor = default;
        public Color DisplayBackgroundColor
        {
            get
            {
                if (displayBackgroundColor != default) { return displayBackgroundColor; }

                if (Model == null) { return Colors.Transparent; }

                int age = DateTime.Today.Year - Model.Birthday.Year;
                if (DateTime.Today < Model.Birthday.AddYears(age))
                {
                    age--;
                }

                return age < 18 ? Colors.LightBlue : Colors.LightGoldenrodYellow;
            }
            set
            {
                if (displayBackgroundColor != value)
                {
                    displayBackgroundColor = value;
                }
            }
        }
    }
}
