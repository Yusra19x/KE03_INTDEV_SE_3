namespace KE03_INTDEV_SE_3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //Gebruik de Appshell als rootpagina van de applicatie
            return new Window(new AppShell());
        }
    }
}