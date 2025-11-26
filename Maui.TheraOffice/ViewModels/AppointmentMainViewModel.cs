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
    public class AppointmentMainViewModel : INotifyPropertyChanged
    {
        public AppointmentMainViewModel()
        {
            InlineAppointment = new AppointmentViewModel();
            IsInlineCardVisible = false;
            InlineCardVisibleText = '▼';
            SortSearchIcon = '▲';
            IsAscending = true;
            SortSearchType = "Id";
        }
        public ObservableCollection<AppointmentViewModel?> Appointments
        {
            get
            {
                int.TryParse(Query, out var queryId);

                if (isAscending)
                {
                    if (SortSearchType == "Id")
                    {
                        return new ObservableCollection<AppointmentViewModel?>
                        (AppointmentServiceProxy
                        .Current
                        .Appointments
                        .Values
                        .Where(a => (a?.Patient?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                              || (a?.Physician?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                              || (a?.Id == queryId))
                        .OrderBy(a => a?.Id)
                        .Select(a => new AppointmentViewModel(a))
                        );
                    }
                    else
                    {
                        return new ObservableCollection<AppointmentViewModel?>
                        (AppointmentServiceProxy
                        .Current
                        .Appointments
                        .Values
                        .Where(a => (a?.Patient?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                              || (a?.Physician?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                              || (a?.Id == queryId))
                        .OrderBy(a => a?.StartTime)
                        .Select(a => new AppointmentViewModel(a))
                        );
                    }

                }
                else
                {
                    if (SortSearchType == "Id")
                    {
                        return new ObservableCollection<AppointmentViewModel?>
                        (AppointmentServiceProxy
                        .Current
                        .Appointments
                        .Values
                        .Where(a => (a?.Patient?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                                || (a?.Physician?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                                || (a?.Id == queryId))
                        .OrderByDescending(a => a?.Id)
                        .Select(a => new AppointmentViewModel(a))
                        );
                    }
                    else
                    {
                        return new ObservableCollection<AppointmentViewModel?>
                        (AppointmentServiceProxy
                        .Current
                        .Appointments
                        .Values
                        .Where(a => (a?.Patient?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                                || (a?.Physician?.Name?.ToUpper().Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                                || (a?.Id == queryId))
                        .OrderByDescending(a => a?.StartTime)
                        .Select(a => new AppointmentViewModel(a))
                        );
                    }

                }

            }
        }
        public AppointmentViewModel? SelectedAppointment { get; set; }
        public AppointmentViewModel? InlineAppointment { get; set; }

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
                NotifyPropertyChanged(nameof(Appointments));
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
                NotifyPropertyChanged(nameof(Appointments));
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
                NotifyPropertyChanged(nameof(Appointments));
            }
        }
        public void Delete()
        {
            if (SelectedAppointment == null)
            {
                return;
            }
            AppointmentServiceProxy.Current.Delete(SelectedAppointment?.Model?.Id ?? 0);
            Refresh();
        }
        public void AddInlineAppointment()
        {
            AppointmentServiceProxy.Current.AddOrUpdate(InlineAppointment?.Model);
            NotifyPropertyChanged(nameof(Appointments));

            InlineAppointment = new AppointmentViewModel();
            NotifyPropertyChanged(nameof(InlineAppointment));
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
            SortSearchType = SortSearchType == "Id" ? "Time" : "Id";
        }
        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Appointments));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
