using System;
using System.Collections.ObjectModel;

namespace UDota.WindowsApp
{
    public sealed class MainViewModel
    {
        public ObservableCollection<string> Teams { get; }

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

        public void AddTeam()
        {
            Teams.Add($"Team {Guid.NewGuid()}");
        }
    }
}