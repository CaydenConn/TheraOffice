using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice.Views;

public partial class PhysicianMainView : ContentPage
{
    public PhysicianMainView()
    {
        InitializeComponent();
        BindingContext = new PhysicianMainViewModel();
    }
    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PhysicianView?PhysicianId=0");
    }
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianMainViewModel)?.Delete();
    }
    private void EditClicked(object sender, EventArgs e)
    {
        var selectedId = (BindingContext as PhysicianMainViewModel)?.SelectedPhysician?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"//PhysicianView?PhysicianId={selectedId}");
    }
    private void InlineEditClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianMainViewModel)?.Refresh();
    }
    private void InlineAddClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianMainViewModel)?.AddInlineBlog();
    }

    private void ExpandCardClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianMainViewModel)?.ExpandCard();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianMainViewModel)?.Refresh();
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as PhysicianMainViewModel)?.Refresh();
    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void SortSearchClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianMainViewModel)?.SortSearch();
    }

    private void SortTypeClicked(object sender, EventArgs e)
    {
        (BindingContext as PhysicianMainViewModel)?.SortTypeChanged();
    }
}