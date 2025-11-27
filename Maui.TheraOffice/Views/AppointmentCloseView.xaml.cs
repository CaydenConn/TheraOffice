using Library.TheraOffice.Models;
using Maui.TheraOffice.ViewModels;
using System.Reflection.Metadata;

namespace Maui.TheraOffice.Views;

[QueryProperty(nameof(AppointmentId), "AppointmentId")]
public partial class AppointmentCloseView : ContentPage
{
    public AppointmentCloseView()
    {
        InitializeComponent();
    }

    public int AppointmentId { get; set; }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AppointmentMainView");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        //Delete the Appointment
        (BindingContext as AppointmentCloseViewModel)?.Delete();

        //Return to AppointmentMain
        Shell.Current.GoToAsync("//AppointmentMainView");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (AppointmentId == 0)
        {
            BindingContext = new AppointmentCloseViewModel();
        }
        else
        {
            BindingContext = new AppointmentCloseViewModel(AppointmentId);
        }
    }
}