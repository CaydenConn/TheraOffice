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
    public class AppointmentCloseViewModel
    {
        public AppointmentCloseViewModel()
        {
            Model = new Appointment();
        }
        public AppointmentCloseViewModel(Appointment? model)
        {
            Model = model;
        }
        public AppointmentCloseViewModel(int apptId)
        {
            Model = AppointmentServiceProxy.Current.Appointments[apptId];
        }
        public void Delete()
        {
            AppointmentServiceProxy.Current.Delete(Model.Id);
        }

        public Appointment? Model { get; set; }
    }
}
