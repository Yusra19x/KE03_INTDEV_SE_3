using KE03_INTDEV_SE_3.Models;
using KE03_INTDEV_SE_3.Views;
using System.Text;
using System.Text.Json;

namespace KE03_INTDEV_SE_3.Views;

[QueryProperty(nameof(Order), "Order")]
public partial class UpdateDeliveryPage : ContentPage
{
    public Order Order { get; set; }

    public List<string> StatusOptions { get; set; } = Enum.GetNames(typeof(DeliveryStateEnum)).ToList();

    public UpdateDeliveryPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Order?.Customer != null)
        {
            customerNameLabel.Text = Order.Customer.Name;
            customerAddressLabel.Text = Order.Customer.Address;
            orderDateLabel.Text = Order.OrderDate.ToString("dd-MM-yyyy");
        }
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        if (statusPicker.SelectedItem == null)
        {
            await DisplayAlert("Fout", "Kies een status", "OK");
            return;
        }

        var selectedStatus = Enum.Parse<DeliveryStateEnum>(statusPicker.SelectedItem.ToString());

        var deliveryState = new
        {
            id = 0,
            state = (int)selectedStatus,
            datetime = DateTime.UtcNow,
            orderId = Order.Id,
            order = "string", // tijdelijk dummy, tenzij verplicht
            deliveryServiceId = 1,
            deliveryService = new
            {
                id = 1,
                name = "DHL"
            }
        };

        var json = JsonSerializer.Serialize(deliveryState);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://51.137.100.120:5000");
        client.DefaultRequestHeaders.Add("ApiKey", "c9e6f1b1-ee5d-4538-8f61-50bf57a9f42b");

        var response = await client.PostAsync("/api/DeliveryStates/UpdateDeliveryState", content);

        if (response.IsSuccessStatusCode)
            await DisplayAlert("Gelukt", "Status is bijgewerkt!", "OK");
        else
            await DisplayAlert("Fout", $"Statuscode: {response.StatusCode}", "OK");
    }
}
