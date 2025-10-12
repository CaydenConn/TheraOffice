namespace Maui.TheraOffice
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void AppointmentClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Appointments");
        }
    }

}
