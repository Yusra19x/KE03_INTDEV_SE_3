using KE03_INTDEV_SE_3.Models;
using System.Text;
using System.Text.Json;

namespace KE03_INTDEV_SE_3.Views
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            statusPicker.ItemsSource = new List<KeyValuePair<DeliveryStateEnum, string>>
    {
                new KeyValuePair<DeliveryStateEnum, string>(DeliveryStateEnum.InBehandeling, "In behandeling"),
                new KeyValuePair<DeliveryStateEnum, string>(DeliveryStateEnum.Onderweg, "Onderweg"),
                new KeyValuePair<DeliveryStateEnum, string>(DeliveryStateEnum.Bezorgd, "Bezorgd"),
                new KeyValuePair<DeliveryStateEnum, string>(DeliveryStateEnum.Geannuleerd, "Geannuleerd")
    };

            statusPicker.ItemDisplayBinding = new Binding("Value");
        }
        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            if (statusPicker.SelectedItem == null)
            {
                await DisplayAlert("Fout", "Selecteer een status aub.", "OK");
                return;
            }

            var selectedStatus = (DeliveryStateEnum)statusPicker.SelectedItem;

            var request = new DeliveryState
            {
                OrderId = 1,
                DeliveryServiceId = 1,
                State = selectedStatus,
                DateTime = DateTime.UtcNow,
                Order = new Order
                {
                    Id = 1,
                    OrderDate = DateTime.UtcNow,
                    CustomerId = 1,
                    Customer = new Customer
                    {
                        Id = 1,
                        Name = "Jan Jansen",
                        Address = "Kerkstraat 12",
                        Active = true
                    }
                },
                DeliveryService = new DeliveryService
                {
                    Id = 1,
                    Name = "DHL"
                }
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://51.137.100.120:5000");

            client.DefaultRequestHeaders.Add("ApiKey", "c9e6f1b1-ee5d-4538-8f61-50bf57a9f42b");

            var response = await client.PostAsync("/api/DeliveryStates/UpdateDeliveryState", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Gelukt", "Status verstuurd!", "OK");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Fout", $"Statuscode: {response.StatusCode}\nDetails: {errorContent}", "OK");
            }
        }
    }
}
