using KE03_INTDEV_SE_3.Views;
namespace KE03_INTDEV_SE_3
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Registreer de route, deze wordt aangeroepen vanuit de OverviewOrder.xaml.cs
            Routing.RegisterRoute(nameof(UpdateDeliveryPage), typeof(UpdateDeliveryPage));
        }
    }
}
