using Maui.TheraOffice.ViewModels;

namespace Maui.TheraOffice.Views;

public partial class PhysicianMainView : ContentPage
{
    public PhysicianMainView()
    {
        InitializeComponent();
        BindingContext = new PhysicianMainViewModel();
    }
    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}