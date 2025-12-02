using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice.Views;

public partial class AppointmentMainView : ContentPage
{
    public AppointmentMainView()
    {
        InitializeComponent();
        BindingContext = new AppointmentMainViewModel();
    }
    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AppointmentView?AppointmentId=0");
    }
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentMainViewModel)?.Delete();
    }
    private void EditClicked(object sender, EventArgs e)
    {
        var selectedId = (BindingContext as AppointmentMainViewModel)?.SelectedAppointment?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"//AppointmentView?AppointmentId={selectedId}");
    }
    private void InlineEditClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentMainViewModel)?.Refresh();
    }
    private void InlineAddClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentMainViewModel)?.AddInlineAppointment();
    }

    private void ExpandCardClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentMainViewModel)?.ExpandCard();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentMainViewModel)?.Refresh();
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as AppointmentMainViewModel)?.Refresh();
    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void SortSearchClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentMainViewModel)?.SortSearch();
    }

    private void SortTypeClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentMainViewModel)?.SortTypeChanged();
    }
}