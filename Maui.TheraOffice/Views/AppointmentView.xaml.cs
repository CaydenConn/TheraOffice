using Library.TheraOffice.Models;
using Maui.TheraOffice.ViewModels;
using System.Reflection.Metadata;

namespace Maui.TheraOffice.Views;

[QueryProperty(nameof(AppointmentId), "AppointmentId")]
public partial class AppointmentView : ContentPage
{
    public AppointmentView()
    {
        InitializeComponent();
    }
    public int AppointmentId { get; set; }
    private void InlineEditClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.Refresh();
    }
    private void SearchClickedPatient(object sender, EventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.Refresh();
    }
    private void SortSearchClickedPatient(object sender, EventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.SortSearchPatient();
    }

    private void SortTypeClickedPatient(object sender, EventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.SortTypeChangedPatient();
    }
    private void SearchClickedPhysician(object sender, EventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.Refresh();
    }
    private void SortSearchClickedPhysician(object sender, EventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.SortSearchPhysician();
    }

    private void SortTypeClickedPhysician(object sender, EventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.SortTypeChangedPhysician();
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AppointmentMainView");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        //Add the Appointment
        (BindingContext as AppointmentViewModel)?.AddOrUpdate();

        //Return to AppointmentMain
        Shell.Current.GoToAsync("//AppointmentMainView");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (AppointmentId == 0)
        {
            BindingContext = new AppointmentViewModel();
        }
        else
        {
            BindingContext = new AppointmentViewModel(AppointmentId);
        }
        //(BindingContext as AppointmentViewModel)?.Refresh();
    }

}