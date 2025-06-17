using KE03_INTDEV_SE_3.Models;
using System.Text;
using System.Text.Json;

namespace KE03_INTDEV_SE_3
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            statusPicker.ItemsSource = Enum
                .GetValues(typeof(DeliveryStateEnum))
                .Cast<DeliveryStateEnum>()
                .ToList();
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
                State = selectedStatus
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://51.137.100.120:5000");

            client.DefaultRequestHeaders.Add("ApiKey", "c9e6f1b1-ee5d-4538-8f61-50bf57a9f42b");

            var response = await client.PostAsync("/api/DeliveryStates/UpdateDeliveryState", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Gelukt", "Status verstuurd!", "OK");
            }
            else
            {
                await DisplayAlert("Fout", $"Statuscode: {response.StatusCode}", "OK");
            }
        }
    }
}
