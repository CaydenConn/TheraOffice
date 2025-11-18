using Library.TheraOffice.Models;
using Maui.TheraOffice.ViewModels;
using System.Reflection.Metadata;

namespace Maui.TheraOffice.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
	}
    public int PatientId { get; set; }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PatientMainView");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        //Add the Patient
        (BindingContext as PatientViewModel)?.AddOrUpdate();

        //Return to PatientMain
        Shell.Current.GoToAsync("//PatientMainView");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (PatientId == 0)
        {
            BindingContext = new PatientViewModel();
        }
        else
        {
            BindingContext = new PatientViewModel(PatientId);
        }
    }
}