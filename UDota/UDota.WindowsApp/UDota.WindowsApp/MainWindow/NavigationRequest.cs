namespace UDota.WindowsApp.MainWindow
{
    public sealed record NavigationRequest
    {
        public MainViewPage TargetPage { get; init; }
    }
}