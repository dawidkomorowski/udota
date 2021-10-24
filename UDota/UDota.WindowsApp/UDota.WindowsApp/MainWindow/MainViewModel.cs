using System;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace UDota.WindowsApp.MainWindow
{
    public sealed partial class MainViewModel : ObservableObject
    {
        private MainViewPage _currentPage;
        [ObservableProperty] private string _selectedTeam;

        public MainViewModel()
        {
            Teams = new ObservableCollection<string>();
        }

        public ObservableCollection<string> Teams { get; }

        public event EventHandler<NavigationRequestedEventArgs>? NavigationRequested;

        public void OnViewReady()
        {
            Navigate(new NavigationRequest
            {
                TargetPage = MainViewPage.GetStarted
            });
        }

        public void OpenAddTeamPage()
        {
            if (_currentPage == MainViewPage.AddTeam) return;

            Navigate(new NavigationRequest
            {
                TargetPage = MainViewPage.AddTeam
            });
        }

        public void AddTeam()
        {
            Teams.Add($"Team {Guid.NewGuid()}");
        }

        private void Navigate(NavigationRequest navigationRequest)
        {
            _currentPage = navigationRequest.TargetPage;
            NavigationRequested?.Invoke(this, new NavigationRequestedEventArgs(navigationRequest));
        }
    }

    public sealed class NavigationRequestedEventArgs : EventArgs
    {
        public NavigationRequestedEventArgs(NavigationRequest navigationRequest)
        {
            NavigationRequest = navigationRequest;
        }

        public NavigationRequest NavigationRequest { get; }
    }
}