using NUnit.Framework;
using UDota.WindowsApp.MainWindow;

namespace UDota.WindowsApp.UnitTests.MainWindow
{
    public class MainViewModelTests
    {
        [Test]
        public void OnViewReady_ShouldNavigateToGetStartedPage()
        {
            // Arrange
            var mainViewModel = new MainViewModel();

            object? actualSender = null;
            NavigationRequestedEventArgs? actualArgs = null;
            mainViewModel.NavigationRequested += (sender, args) =>
            {
                actualSender = sender;
                actualArgs = args;
            };

            // Act
            mainViewModel.OnViewReady();

            // Assert
            Assert.That(actualSender, Is.EqualTo(mainViewModel));
            Assert.That(actualArgs, Is.Not.Null);
            Assert.That(actualArgs!.NavigationRequest.TargetPage, Is.EqualTo(MainViewPage.GetStarted));
        }

        [Test]
        public void OpenAddTeamPage_ShouldRequestNavigationToAddTeamPage_WhenItIsNotCurrentPage()
        {
            // Arrange
            var mainViewModel = new MainViewModel();

            object? actualSender = null;
            NavigationRequestedEventArgs? actualArgs = null;
            mainViewModel.NavigationRequested += (sender, args) =>
            {
                actualSender = sender;
                actualArgs = args;
            };

            // Act
            mainViewModel.OpenAddTeamPage();

            // Assert
            Assert.That(actualSender, Is.EqualTo(mainViewModel));
            Assert.That(actualArgs, Is.Not.Null);
            Assert.That(actualArgs!.NavigationRequest.TargetPage, Is.EqualTo(MainViewPage.AddTeam));
        }

        [Test]
        public void OpenAddTeamPage_ShouldNotRequestNavigationToAddTeamPage_WhenItIsCurrentPage()
        {
            // Arrange
            var mainViewModel = new MainViewModel();
            mainViewModel.OpenAddTeamPage();

            var navigationRequested = false;
            mainViewModel.NavigationRequested += (_, _) => { navigationRequested = true; };

            // Act
            mainViewModel.OpenAddTeamPage();

            // Assert
            Assert.That(navigationRequested, Is.False);
        }
    }
}