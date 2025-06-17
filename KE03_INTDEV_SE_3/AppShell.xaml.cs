using KE03_INTDEV_SE_3.Views;
namespace KE03_INTDEV_SE_3
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(UpdateDeliveryPage), typeof(UpdateDeliveryPage));
        }
    }
}
