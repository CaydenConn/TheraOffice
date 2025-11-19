using Library.TheraOffice.Models;
using Maui.TheraOffice.ViewModels;
using System.Reflection.Metadata;

namespace Maui.TheraOffice.Views;

[QueryProperty(nameof(PhysicianId), "PhysicianId")]
public partial class PhysicianView : ContentPage
{
    public PhysicianView()
    {
        InitializeComponent();
    }
    public int PhysicianId { get; set; }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PhysicianMainView");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        //Add the Physician
        (BindingContext as PhysicianViewModel)?.AddOrUpdate();

        //Return to PhysicianMain
        Shell.Current.GoToAsync("//PhysicianMainView");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (PhysicianId == 0)
        {
            BindingContext = new PhysicianViewModel();
        }
        else
        {
            BindingContext = new PhysicianViewModel(PhysicianId);
        }
    }
}