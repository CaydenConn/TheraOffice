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
    public class PhysicianMainViewModel : INotifyPropertyChanged
    {
        public PhysicianMainViewModel()
        {
            InlinePhysician = new PhysicianViewModel();
            IsInlineCardVisible = false;
            InlineCardVisibleText = '▼';
            SortSearchIcon = '▲';
            IsAscending = true;
            SortSearchType = "Id";
        }
        public ObservableCollection<PhysicianViewModel?> Physicians
        {
            get
            {
                int.TryParse(Query, out var queryId);

                if (isAscending)
                {
                    if (SortSearchType == "Id")
                    {
                        return new ObservableCollection<PhysicianViewModel?>
                        (PhysicianServiceProxy
                        .Current
                        .Physicians
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == queryId) || (p?.LicenseNumber == Query) || (p?.Specialization == Query))
                        .OrderBy(p => p?.Id)
                        .Select(p => new PhysicianViewModel(p))
                        );
                    }
                    else
                    {
                        return new ObservableCollection<PhysicianViewModel?>
                        (PhysicianServiceProxy
                        .Current
                        .Physicians
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == queryId) || (p?.LicenseNumber == Query) || (p?.Specialization == Query))
                        .OrderBy(p => p?.Name)
                        .Select(p => new PhysicianViewModel(p))
                        );
                    }

                }
                else
                {
                    if (SortSearchType == "Id")
                    {
                        return new ObservableCollection<PhysicianViewModel?>
                        (PhysicianServiceProxy
                        .Current
                        .Physicians
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                                || (p?.Id == queryId) || (p?.LicenseNumber == Query) || (p?.Specialization == Query))
                        .OrderByDescending(p => p?.Id)
                        .Select(p => new PhysicianViewModel(p))
                        );
                    }
                    else
                    {
                        return new ObservableCollection<PhysicianViewModel?>
                        (PhysicianServiceProxy
                        .Current
                        .Physicians
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == queryId) || (p?.LicenseNumber == Query) || (p?.Specialization == Query))
                        .OrderByDescending(p => p?.Name)
                        .Select(p => new PhysicianViewModel(p))
                        );
                    }

                }

            }
        }
        public PhysicianViewModel? SelectedPhysician { get; set; }
        public PhysicianViewModel? InlinePhysician { get; set; }

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
                NotifyPropertyChanged(nameof(Physicians));
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
                NotifyPropertyChanged(nameof(Physicians));
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
                if (isAscending != value)
                {
                    isAscending = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Physicians));
            }
        }
        public void Delete()
        {
            if (SelectedPhysician == null)
            {
                return;
            }
            PhysicianServiceProxy.Current.Delete(SelectedPhysician?.Model?.Id ?? 0);
            var appointmentsToDelete = AppointmentServiceProxy.Current.Appointments.Values
                .Where(appt => appt.Physician?.Id == SelectedPhysician?.Model?.Id)
                .Select(appt => appt.Id)
                .ToList();

            foreach (var apptId in appointmentsToDelete)
            {
                AppointmentServiceProxy.Current.Delete(apptId);
            }
            Refresh();
        }
        public void AddInlinePhysician()
        {
            PhysicianServiceProxy.Current.AddOrUpdate(InlinePhysician?.Model);
            NotifyPropertyChanged(nameof(Physicians));

            InlinePhysician = new PhysicianViewModel();
            NotifyPropertyChanged(nameof(InlinePhysician));
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
            NotifyPropertyChanged(nameof(Physicians));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
