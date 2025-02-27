using MauiApp17.Service;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MauiApp17.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private Profile _profile = new Profile();

        public ProfileViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            LoadProfile();
            SaveCommand = new Command(async () => await SaveProfile());
        }

        // Constructor for design time
        public ProfileViewModel()
        {
            _databaseService = null;
            SaveCommand = new Command(async () => await Task.CompletedTask);
        }

        public string Name
        {
            get => _profile?.Name ?? string.Empty;
            set
            {
                if (_profile.Name != value)
                {
                    _profile.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Surname
        {
            get => _profile?.Surname ?? string.Empty;
            set
            {
                if (_profile.Surname != value)
                {
                    _profile.Surname = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EmailAddress
        {
            get => _profile?.EmailAddress ?? string.Empty;
            set
            {
                if (_profile.EmailAddress != value)
                {
                    _profile.EmailAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Bio
        {
            get => _profile?.Bio ?? string.Empty;
            set
            {
                if (_profile.Bio != value)
                {
                    _profile.Bio = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command SaveCommand { get; }

        private async void LoadProfile()
        {
            if (_databaseService != null)
            {
                var loadedProfile = await _databaseService.GetProfileAsync(1);
                if (loadedProfile != null)
                {
                    _profile = loadedProfile;
                }
                else
                {
                    _profile = new Profile();
                }

                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Surname));
                OnPropertyChanged(nameof(EmailAddress));
                OnPropertyChanged(nameof(Bio));
            }
        }

        private async Task SaveProfile()
        {
            if (_databaseService != null)
            {
                await _databaseService.SaveProfileAsync(_profile);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}