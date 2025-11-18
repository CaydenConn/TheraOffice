namespace Maui.TheraOffice.Views;

public partial class AppointmentMainView : ContentPage
{
	public AppointmentMainView()
	{
		InitializeComponent();
	}
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}