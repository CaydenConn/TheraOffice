using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice.Views;

public partial class PatientMainView : ContentPage
{
    public PatientMainView()
    {
        InitializeComponent();
        BindingContext = new PatientMainViewModel();
    }
    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PatientView?patientId=0");
    }
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as PatientMainViewModel)?.Delete();
    }
    private void EditClicked(object sender, EventArgs e)
    {
        var selectedId = (BindingContext as PatientMainViewModel)?.SelectedPatient?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"//PatientView?patientId={selectedId}");
    }
    private void InlineEditClicked(object sender, EventArgs e)
    {
        (BindingContext as PatientMainViewModel)?.Refresh();
    }
    private void InlineAddClicked(object sender, EventArgs e)
    {
        (BindingContext as PatientMainViewModel)?.AddInlinePatient();
    }

    private void ExpandCardClicked(object sender, EventArgs e)
    {
        (BindingContext as PatientMainViewModel)?.ExpandCard();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as PatientMainViewModel)?.Refresh();
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as PatientMainViewModel)?.Refresh();
    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void SortSearchClicked(object sender, EventArgs e)
    {
        (BindingContext as PatientMainViewModel)?.SortSearch();
    }

    private void SortTypeClicked(object sender, EventArgs e)
    {
        (BindingContext as PatientMainViewModel)?.SortTypeChanged();
    }
}