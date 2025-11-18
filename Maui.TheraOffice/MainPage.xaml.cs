using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
        private void PatientsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//PatientMainView");
        }
        private void PhysiciansClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//PhysicianMainView");
        }
        private void AppointmentClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//AppointmentMainView");
        }
    }

}
