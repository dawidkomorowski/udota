using System.Collections.ObjectModel;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UDota.WindowsApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            NavigationMenuItems = new ObservableCollection<NavigationViewItem>();

            _mainViewModel = new MainViewModel();
            BindTeamsToNavigationView(_mainViewModel.Teams);
        }

        private ObservableCollection<NavigationViewItem> NavigationMenuItems { get; }

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

        private void AddTeamNavigationViewItem_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () => { _mainViewModel.AddTeam(); });
        }

        private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var navigationOptions = new FrameNavigationOptions
            {
                TransitionInfoOverride = args.RecommendedNavigationTransitionInfo,
                IsNavigationStackEnabled = false
            };

            ContentFrame.NavigateToType(typeof(AddTeamPage), args.InvokedItem, navigationOptions);
        }
    }
}