using System.Text;
using Microsoft.UI.Xaml;
using UDota.CoreLib.OpenDota;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UDota.WindowsApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var odc = new OpenDotaClient();
            var players = await odc.SearchPlayers("Dakki");

            var sb = new StringBuilder();
            foreach (var player in players)
            {
                sb.Append("Player name: ");
                sb.AppendLine(player.Name);
            }

            textBlock.Text = sb.ToString();
        }
    }
}