using System;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace UDota.WindowsApp
{
    public sealed partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private string _selectedTeam;

        public MainViewModel()
        {
            Teams = new ObservableCollection<string>();
            var random = new Random();
            var numberOfTeams = random.Next(2, 8);
            for (var i = 0; i < numberOfTeams; i++)
            {
                Teams.Add($"Team {i}");
            }
        }

        public ObservableCollection<string> Teams { get; }

        public void AddTeam()
        {
            Teams.Add($"Team {Guid.NewGuid()}");
        }
    }
}