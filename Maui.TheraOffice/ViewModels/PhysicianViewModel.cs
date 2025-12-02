using Library.TheraOffice.Models;
using Library.TheraOffice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.TheraOffice.ViewModels
{
    public class PhysicianViewModel
    {
        public PhysicianViewModel()
        {
            Model = new Physician();
            SetUpCommands();
        }
        public PhysicianViewModel(Physician? model)
        {
            Model = model;
            SetUpCommands();
        }
        public PhysicianViewModel(int PhysicianId)
        {
            Model = PhysicianServiceProxy.Current.Physicians[PhysicianId];
            SetUpCommands();
        }
        public void AddOrUpdate()
        {
            PhysicianServiceProxy.Current.AddOrUpdate(Model);
        }
        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PhysicianViewModel));
        }
        private void DoDelete()
        {
            if (Model?.Id > 0)
            {
                PhysicianServiceProxy.Current.Delete(Model.Id);
                var appointmentsToDelete = AppointmentServiceProxy.Current.Appointments.Values
                    .Where(appt => appt.Physician?.Id == Model?.Id)
                    .Select(appt => appt.Id)
                    .ToList();

                foreach (var apptId in appointmentsToDelete)
                {
                    AppointmentServiceProxy.Current.Delete(apptId);
                }
                Shell.Current.GoToAsync("//PhysicianMainView");
            }
        }

        private void DoEdit(PhysicianViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPhysicianId = pvm?.Model?.Id ?? 0;
            Shell.Current.GoToAsync($"//PhysicianView?PhysicianId={selectedPhysicianId}");
        }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
        public Physician? Model { get; set; }
        private Color? displayBackgroundColor = default;
        public Color DisplayBackgroundColor
        {
            get
            {
                if (displayBackgroundColor != default) { return displayBackgroundColor; }
                if (Model == null) { return Colors.Transparent; }

                switch (Model.Specialization)
                {
                    case "Pediatrics":
                        return Colors.LightBlue;
                    case "Gynecology":
                        return Colors.LightGoldenrodYellow;
                    case "Psychiatry":
                        return Colors.LightCoral;
                    default:
                        return Colors.Transparent;
                }
            
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
