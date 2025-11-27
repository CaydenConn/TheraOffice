using Library.TheraOffice.Models;
using Library.TheraOffice.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Bluetooth.Advertisement;

namespace Maui.TheraOffice.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        public AppointmentViewModel()
        {
            Model = new Appointment();

            SortSearchIconPatient = '▲';
            IsAscendingPatient = true;
            SortSearchTypePatient = "Id";

            SortSearchIconPhysician = '▲';
            IsAscendingPhysician = true;
            SortSearchTypePhysician = "Id";

            SetUpCommands();
        }
        public AppointmentViewModel(Appointment? model)
        {
            Model = model;

            SortSearchIconPatient = '▲';
            IsAscendingPatient = true;
            SortSearchTypePatient = "Id";

            SortSearchIconPhysician = '▲';
            IsAscendingPhysician = true;
            SortSearchTypePhysician = "Id";

            SetUpCommands();
        }
        public AppointmentViewModel(int AppointmentId)
        {
            Model = AppointmentServiceProxy.Current.Appointments[AppointmentId];

            SortSearchIconPatient = '▲';
            IsAscendingPatient = true;
            SortSearchTypePatient = "Id";

            SortSearchIconPhysician = '▲';
            IsAscendingPhysician = true;
            SortSearchTypePhysician = "Id";

            SetUpCommands();
        }


        // ****************************
        // * PATIENT LIST INFORMATION *
        // ****************************

        public string? QueryPatient { get; set; }
        private PatientViewModel? selectedPatient = null;
        public PatientViewModel? SelectedPatient
        {
            get
            {
                return selectedPatient; 
            }
            set
            {
                if (value != selectedPatient)
                {
                    selectedPatient = value;
                    Model.Patient = value?.Model;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Patients));
            }
        }
        private string sortSearchTypePatient;
        public string SortSearchTypePatient
        {
            get
            {
                return sortSearchTypePatient;
            }
            set
            {
                if (sortSearchTypePatient != value)
                {
                    sortSearchTypePatient = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Patients));
            }
        }
        private char sortSearchIconPatient;
        public char SortSearchIconPatient
        {
            get
            {
                return sortSearchIconPatient;
            }
            set
            {
                if (sortSearchIconPatient != value)
                {
                    sortSearchIconPatient = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Patients));
            }
        }
        private bool isAscendingPatient;
        public bool IsAscendingPatient
        {
            get
            {
                return isAscendingPatient;
            }
            set
            {
                if (isAscendingPatient != value)
                {
                    isAscendingPatient = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Patients));
            }
        }
        public void SortSearchPatient()
        {
            IsAscendingPatient = !IsAscendingPatient;
            SortSearchIconPatient = IsAscendingPatient ? '▲' : '▼';
        }
        public void SortTypeChangedPatient()
        {
            SortSearchTypePatient = SortSearchTypePatient == "Id" ? "Name" : "Id";
        }

        public ObservableCollection<PatientViewModel?> Patients
        {
            get
            {
                int.TryParse(QueryPatient, out var QueryPatientId);

                if (isAscendingPatient)
                {
                    if (SortSearchTypePatient == "Id")
                    {
                        return new ObservableCollection<PatientViewModel?>
                        (PatientServiceProxy
                        .Current
                        .Patients
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(QueryPatient?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == QueryPatientId))
                        .Where(p =>
                        {
                            return !AppointmentServiceProxy.Current.Appointments.Values
                            .Any(appt =>
                                appt.Patient?.Id == p?.Id
                                && Model.StartTime < appt.EndTime
                                && Model.EndTime > appt.StartTime
                            );
                        })
                        .OrderBy(p => (SelectedPatient != null && p?.Id == SelectedPatient?.Model?.Id ? 0 : 1))
                        .ThenBy(p => p?.Id)
                        .Select(p =>
                        {
                            var pvm = new PatientViewModel(p);
                            pvm.DisplayBackgroundColor = (SelectedPatient != null & SelectedPatient?.Model?.Id == p?.Id)
                                ? Colors.LightGreen
                                : (pvm.DisplayBackgroundColor ?? Colors.Transparent);
                            return pvm;
                        })
                        );
                    }
                    else
                    {
                        return new ObservableCollection<PatientViewModel?>
                        (PatientServiceProxy
                        .Current
                        .Patients
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(QueryPatient?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == QueryPatientId))
                        .Where(p =>
                        {
                            return !AppointmentServiceProxy.Current.Appointments.Values
                            .Any(appt =>
                                appt.Patient?.Id == p?.Id
                                && Model.StartTime < appt.EndTime
                                && Model.EndTime > appt.StartTime
                            );
                        })
                        .OrderBy(p => (SelectedPatient != null && p?.Id == SelectedPatient?.Model?.Id ? 0 : 1))
                        .ThenBy(p => p?.Name)
                        .Select(p =>
                        {
                            var pvm = new PatientViewModel(p);
                            pvm.DisplayBackgroundColor = (SelectedPatient != null & SelectedPatient?.Model?.Id == p?.Id)
                                ? Colors.LightGreen
                                : (pvm.DisplayBackgroundColor ?? Colors.Transparent);
                            return pvm;
                        })
                        );
                    }

                }
                else
                {
                    if (SortSearchTypePatient == "Id")
                    {
                        return new ObservableCollection<PatientViewModel?>
                        (PatientServiceProxy
                        .Current
                        .Patients
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(QueryPatient?.ToUpper() ?? string.Empty) ?? false)
                                || (p?.Id == QueryPatientId))
                        .Where(p =>
                        {
                            return !AppointmentServiceProxy.Current.Appointments.Values
                            .Any(appt =>
                                appt.Patient?.Id == p?.Id
                                && Model.StartTime < appt.EndTime
                                && Model.EndTime > appt.StartTime
                            );
                        })
                        .OrderBy(p => (SelectedPatient != null && p?.Id == SelectedPatient?.Model?.Id ? 0 : 1))
                        .ThenByDescending(p => p?.Id)
                        .Select(p =>
                        {
                            var pvm = new PatientViewModel(p);
                            pvm.DisplayBackgroundColor = (SelectedPatient != null & SelectedPatient?.Model?.Id == p?.Id)
                                ? Colors.LightGreen
                                : (pvm.DisplayBackgroundColor ?? Colors.Transparent);
                            return pvm;
                        })
                        );
                    }
                    else
                    {
                        return new ObservableCollection<PatientViewModel?>
                        (PatientServiceProxy
                        .Current
                        .Patients
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(QueryPatient?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == QueryPatientId))
                        .Where(p =>
                        {
                            return !AppointmentServiceProxy.Current.Appointments.Values
                            .Any(appt =>
                                appt.Patient?.Id == p?.Id
                                && Model.StartTime < appt.EndTime
                                && Model.EndTime > appt.StartTime
                            );
                        })
                        .OrderBy(p => (SelectedPatient != null && p?.Id == SelectedPatient?.Model?.Id ? 0 : 1))
                        .ThenByDescending(p => p?.Name)
                        .Select(p =>
                        {
                            var pvm = new PatientViewModel(p);
                            pvm.DisplayBackgroundColor = (SelectedPatient != null & SelectedPatient?.Model?.Id == p?.Id)
                                ? Colors.LightGreen
                                : (pvm.DisplayBackgroundColor ?? Colors.Transparent);
                            return pvm;
                        })
                        );
                    }

                }

            }
        }

        // ******************************
        // * PHYSICIAN LIST INFORMATION *
        // ******************************
        public string? QueryPhysician { get; set; }
        private PhysicianViewModel? selectedPhysician = null;
        public PhysicianViewModel? SelectedPhysician
        {
            get
            {
                return selectedPhysician;
            }
            set
            {
                if (value != selectedPhysician)
                {
                    selectedPhysician = value;
                    Model.Physician = value?.Model;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Physicians));
            }
        }
        private string sortSearchTypePhysician;
        public string SortSearchTypePhysician
        {
            get
            {
                return sortSearchTypePhysician;
            }
            set
            {
                if (sortSearchTypePhysician != value)
                {
                    sortSearchTypePhysician = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Physicians));
            }
        }
        private char sortSearchIconPhysician;
        public char SortSearchIconPhysician
        {
            get
            {
                return sortSearchIconPhysician;
            }
            set
            {
                if (sortSearchIconPhysician != value)
                {
                    sortSearchIconPhysician = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Physicians));
            }
        }
        private bool isAscendingPhysician;
        public bool IsAscendingPhysician
        {
            get
            {
                return isAscendingPhysician;
            }
            set
            {
                if (isAscendingPhysician != value)
                {
                    isAscendingPhysician = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Physicians));
            }
        }
        public void SortSearchPhysician()
        {
            IsAscendingPhysician = !IsAscendingPhysician;
            SortSearchIconPhysician = IsAscendingPhysician ? '▲' : '▼';
        }
        public void SortTypeChangedPhysician()
        {
            SortSearchTypePhysician = SortSearchTypePhysician == "Id" ? "Name" : "Id";
        }

        public ObservableCollection<PhysicianViewModel?> Physicians
        {
            get
            {
                int.TryParse(QueryPhysician, out var QueryPhysicianId);

                if (isAscendingPhysician)
                {
                    if (SortSearchTypePhysician == "Id")
                    {
                        return new ObservableCollection<PhysicianViewModel?>
                        (PhysicianServiceProxy
                        .Current
                        .Physicians
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(QueryPhysician?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == QueryPhysicianId))
                        .Where(p =>
                        {
                            return !AppointmentServiceProxy.Current.Appointments.Values
                            .Any(appt =>
                                appt.Physician?.Id == p?.Id
                                && Model.StartTime < appt.EndTime
                                && Model.EndTime > appt.StartTime
                            );
                        })
                        .OrderBy(p => (SelectedPhysician != null && p?.Id == SelectedPhysician?.Model?.Id ? 0 : 1))
                        .ThenBy(p => p?.Id)
                        .Select(p =>
                        {
                            var pvm = new PhysicianViewModel(p);
                            pvm.DisplayBackgroundColor = (SelectedPhysician != null & SelectedPhysician?.Model?.Id == p?.Id)
                                ? Colors.LightGreen
                                : (pvm.DisplayBackgroundColor ?? Colors.Transparent);
                            return pvm;
                        })
                        );
                    }
                    else
                    {
                        return new ObservableCollection<PhysicianViewModel?>
                        (PhysicianServiceProxy
                        .Current
                        .Physicians
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(QueryPhysician?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == QueryPhysicianId))
                        .Where(p =>
                        {
                            return !AppointmentServiceProxy.Current.Appointments.Values
                            .Any(appt =>
                                appt.Physician?.Id == p?.Id
                                && Model.StartTime < appt.EndTime
                                && Model.EndTime > appt.StartTime
                            );
                        })
                        .OrderBy(p => (SelectedPhysician != null && p?.Id == SelectedPhysician?.Model?.Id ? 0 : 1))
                        .ThenBy(p => p?.Name)
                        .Select(p =>
                        {
                            var pvm = new PhysicianViewModel(p);
                            pvm.DisplayBackgroundColor = (SelectedPhysician != null & SelectedPhysician?.Model?.Id == p?.Id)
                                ? Colors.LightGreen
                                : (pvm.DisplayBackgroundColor ?? Colors.Transparent);
                            return pvm;
                        })
                        );
                    }

                }
                else
                {
                    if (SortSearchTypePhysician == "Id")
                    {
                        return new ObservableCollection<PhysicianViewModel?>
                        (PhysicianServiceProxy
                        .Current
                        .Physicians
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(QueryPhysician?.ToUpper() ?? string.Empty) ?? false)
                                || (p?.Id == QueryPhysicianId))
                        .Where(p =>
                        {
                            return !AppointmentServiceProxy.Current.Appointments.Values
                            .Any(appt =>
                                appt.Physician?.Id == p?.Id
                                && Model.StartTime < appt.EndTime
                                && Model.EndTime > appt.StartTime
                            );
                        })
                        .OrderBy(p => (SelectedPhysician != null && p?.Id == SelectedPhysician?.Model?.Id ? 0 : 1))
                        .ThenByDescending(p => p?.Id)
                        .Select(p =>
                        {
                            var pvm = new PhysicianViewModel(p);
                            pvm.DisplayBackgroundColor = (SelectedPhysician != null & SelectedPhysician?.Model?.Id == p?.Id)
                                ? Colors.LightGreen
                                : (pvm.DisplayBackgroundColor ?? Colors.Transparent);
                            return pvm;
                        })
                        );
                    }
                    else
                    {
                        return new ObservableCollection<PhysicianViewModel?>
                        (PhysicianServiceProxy
                        .Current
                        .Physicians
                        .Values
                        .Where(p => (p?.Name?.ToUpper().Contains(QueryPhysician?.ToUpper() ?? string.Empty) ?? false)
                              || (p?.Id == QueryPhysicianId))
                        .Where(p =>
                        {
                            return !AppointmentServiceProxy.Current.Appointments.Values
                            .Any(appt =>
                                appt.Physician?.Id == p?.Id
                                && Model.StartTime < appt.EndTime
                                && Model.EndTime > appt.StartTime
                            );
                        })
                        .OrderBy(p => (SelectedPhysician != null && p?.Id == SelectedPhysician?.Model?.Id ? 0 : 1))
                        .ThenByDescending(p => p?.Name)
                        .Select(p =>
                        {
                            var pvm = new PhysicianViewModel(p);
                            pvm.DisplayBackgroundColor = (SelectedPhysician != null & SelectedPhysician?.Model?.Id == p?.Id)
                                ? Colors.LightGreen
                                : (pvm.DisplayBackgroundColor ?? Colors.Transparent);
                            return pvm;
                        })
                        );
                    }

                }

            }
        }

        // *************************
        // * DATE-TIME INFORMATION *
        // *************************

        public DateTime StartDate
        {
            get
            {
                return Model.StartTime;
            }
            set
            {
                if(value != Model?.StartTime)
                {
                    Model.StartTime = value;
                    Model.EndTime = value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Patients));
                NotifyPropertyChanged(nameof(Physicians));
            }
        }

        public TimeSpan StartTime
        {
            get
            {
                return Model.StartTime.TimeOfDay;
            }
            set
            {
                if (value != Model?.StartTime.TimeOfDay)
                {
                    Model.StartTime = Model.StartTime.Date + value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Patients));
                NotifyPropertyChanged(nameof(Physicians));
            }
        }
        public TimeSpan EndTime
        {
            get
            {
                return Model.EndTime.TimeOfDay;
            }
            set
            {
                if (value != Model?.EndTime.TimeOfDay)
                {
                    Model.EndTime = Model.EndTime.Date + value;
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Patients));
                NotifyPropertyChanged(nameof(Physicians));
            }
        }























        public void AddOrUpdate()
        {
            AppointmentServiceProxy.Current.AddOrUpdate(Model);
        }
        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((appt) => DoEdit(appt as AppointmentViewModel));
            CloseCommand = new Command((appt) => DoClose(appt as AppointmentViewModel));
        }
        private void DoDelete()
        {
            if (Model?.Id > 0)
            {
                AppointmentServiceProxy.Current.Delete(Model.Id);
                Shell.Current.GoToAsync("//AppointmentMainView");
            }
        }

        private void DoEdit(AppointmentViewModel? avm)
        {
            if (avm == null)
            {
                return;
            }
            var selectedAppointmentId = avm?.Model?.Id ?? 0;
            Shell.Current.GoToAsync($"//AppointmentView?AppointmentId={selectedAppointmentId}");
        }
        private void DoClose(AppointmentViewModel? avm)
        {
            if (avm == null)
            {
                return;
            }
            var selectedAppointmentId = avm?.Model?.Id ?? 0;
            Shell.Current.GoToAsync($"//AppointmentCloseView?AppointmentId={selectedAppointmentId}");
        }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
        public ICommand? CloseCommand { get; set; }
        public Appointment? Model { get; set; }
        public Color DisplayBackgroundColor
        {
            get
            {
                if (Model == null) { return Colors.Transparent; }
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                DateOnly appointmentDate = DateOnly.FromDateTime(Model.StartTime.Date);

                if(today == appointmentDate)
                {
                    return Colors.LightGreen;
                }else if (today.AddDays(7) >= appointmentDate)
                {
                    return Colors.LightYellow;
                }
                else
                {
                    return Colors.Transparent;
                }
            }
        }
        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Patients));
            NotifyPropertyChanged(nameof(Physicians));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
