using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using UDota.WindowsApp.AddTeam;
using UDota.WindowsApp.GetStarted;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UDota.WindowsApp.MainWindow
{
    public sealed partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            NavigationMenuItems = new ObservableCollection<NavigationViewItem>();

            _mainViewModel = new MainViewModel();
            _mainViewModel.NavigationRequested += MainViewModel_OnNavigationRequested;
            BindTeamsToNavigationView(_mainViewModel.Teams);

            _mainViewModel.OnViewReady();
        }

        private ObservableCollection<NavigationViewItem> NavigationMenuItems { get; }

        private void MainViewModel_OnNavigationRequested(object? sender, NavigationRequestedEventArgs e)
        {
            var sourcePageType = e.NavigationRequest.TargetPage switch
            {
                MainViewPage.GetStarted => typeof(GetStartedPage),
                MainViewPage.AddTeam => typeof(AddTeamPage),
                MainViewPage.TeamDetails => throw new NotImplementedException(),
                // ReSharper disable once NotResolvedInText
                _ => throw new ArgumentOutOfRangeException("e.NavigationRequest.TargetPage",
                    $"Unknown {nameof(MainViewPage)} = {e.NavigationRequest.TargetPage}.")
            };

            var navigationOptions = new FrameNavigationOptions
            {
                TransitionInfoOverride = new EntranceNavigationTransitionInfo(),
                IsNavigationStackEnabled = false
            };

            ContentFrame.NavigateToType(sourcePageType, null, navigationOptions);
        }

        private void BindTeamsToNavigationView(ObservableCollection<string> teams)
        {
            void RecreateNavigationMenuItems()
            {
                NavigationMenuItems.Clear();

                foreach (var team in teams)
                {
                    var navigationViewItem = new NavigationViewItem { Content = team };
                    NavigationMenuItems.Add(navigationViewItem);
                }

                NavigationMenuItems.Add(CreateAddTeamNavigationViewItem());
            }

            teams.CollectionChanged += (_, _) => RecreateNavigationMenuItems();

            RecreateNavigationMenuItems();
        }

        private NavigationViewItem CreateAddTeamNavigationViewItem()
        {
            var navigationViewItem = new NavigationViewItem
            {
                Icon = new SymbolIcon(Symbol.Add),
                SelectsOnInvoked = false,
                Content = "Add Team"
            };

            navigationViewItem.Tapped += AddTeamNavigationViewItem_OnTapped;

            return navigationViewItem;
        }

        private void AddTeamNavigationViewItem_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            _mainViewModel.OpenAddTeamPage();
        }

        private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            //var navigationOptions = new FrameNavigationOptions
            //{
            //    TransitionInfoOverride = args.RecommendedNavigationTransitionInfo,
            //    IsNavigationStackEnabled = false
            //};

            //ContentFrame.NavigateToType(typeof(AddTeamPage), args.InvokedItem, navigationOptions);
        }
    }
}