using Library.TheraOffice.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.TheraOffice.ViewModels
{
    public class PatientMainViewModel : INotifyPropertyChanged
    {
        public PatientMainViewModel() {
            InlinePatient = new PatientViewModel();
            IsInlineCardVisible = false;
            InlineCardVisibleText = '▼';
            SortSearchIcon = '▲';
            IsAscending = true;
            SortSearchType = "Id";
        }
        public ObservableCollection<PatientViewModel?> Patients
        {
            get
            {
                int.TryParse(Query, out var queryId);

                if (isAscending)
                {
                    if (SortSearchType == "Id")
                    {
                        return new ObservableCollection<PatientViewModel?>
                        (PatientServiceProxy
                        .Current
                        .Patients
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == queryId))
                        .OrderBy(p => p?.Id)
                        .Select(p => new PatientViewModel(p))
                        );
                    }
                    else
                    {
                        return new ObservableCollection<PatientViewModel?>
                        (PatientServiceProxy
                        .Current
                        .Patients
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == queryId))
                        .OrderBy(p => p?.Name)
                        .Select(p => new PatientViewModel(p))
                        );
                    }
                        
                }
                else
                {
                    if(SortSearchType == "Id")
                    {
                        return new ObservableCollection<PatientViewModel?>
                        (PatientServiceProxy
                        .Current
                        .Patients
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                                || (p?.Id == queryId))
                        .OrderByDescending(p => p?.Id)
                        .Select(p => new PatientViewModel(p))
                        );
                    }
                    else
                    {
                        return new ObservableCollection<PatientViewModel?>
                        (PatientServiceProxy
                        .Current
                        .Patients
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == queryId))
                        .OrderByDescending(p => p?.Name)
                        .Select(p => new PatientViewModel(p))
                        );
                    }
                        
                }
                
            }
        }
        public PatientViewModel? SelectedPatient { get; set; }
        public PatientViewModel? InlinePatient { get; set; }

        public string? Query { get; set; }


        private bool isInlineCardVisible;
        public bool IsInlineCardVisible
        {
            get
            {
                return isInlineCardVisible;
            }
            set
            {
                if (isInlineCardVisible != value)
                {
                    isInlineCardVisible = value;
                }
                NotifyPropertyChanged();
            }
        }
        private char inlineCardVisibleText;
        public char InlineCardVisibleText
        {
            get
            {
                return inlineCardVisibleText;
            }
            set
            {
                if (inlineCardVisibleText != value)
                {
                    inlineCardVisibleText = value;
                }
                NotifyPropertyChanged();
            }
        }
        private string sortSearchType;
        public string SortSearchType
        {
            get
            {
                return sortSearchType;
            }
            set
            {
                if (sortSearchType != value)
                {
                    sortSearchType = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Patients));
            }
        }
        private char sortSearchIcon;
        public char SortSearchIcon
        {
            get
            {
                return sortSearchIcon;
            }
            set
            {
                if (sortSearchIcon != value)
                {
                    sortSearchIcon = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Patients));
            }
        }
        private bool isAscending;
        public bool IsAscending
        {
            get
            {
                return isAscending;
            }
            set
            {
                if(isAscending != value)
                {
                    isAscending = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Patients));
            }
        }
        public void Delete()
        {
            if (SelectedPatient == null)
            {
                return;
            }
            PatientServiceProxy.Current.Delete(SelectedPatient?.Model?.Id ?? 0);
            Refresh();
        }
        public void AddInlineBlog()
        {
            PatientServiceProxy.Current.AddOrUpdate(InlinePatient?.Model);
            NotifyPropertyChanged(nameof(Patients));

            InlinePatient = new PatientViewModel();
            NotifyPropertyChanged(nameof(InlinePatient));
        }
        public void ExpandCard()
        {
            IsInlineCardVisible = !IsInlineCardVisible;
            InlineCardVisibleText = InlineCardVisibleText == '▼' ? '-' : '▼';
        }
        public void SortSearch()
        {
            IsAscending = !IsAscending;
            SortSearchIcon = IsAscending ? '▲' : '▼';
        }
        public void SortTypeChanged()
        {
            SortSearchType = SortSearchType == "Id" ? "Name" : "Id";
        }
        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Patients));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
