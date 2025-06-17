using KE03_INTDEV_SE_3.Models;
using Microsoft.Maui.Controls;
using System.Text.Json;

namespace KE03_INTDEV_SE_3.Views;

public partial class OverviewOrder : ContentPage
{
    public OverviewOrder()
    {
        InitializeComponent();

        try
        {
            LoadOrders();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ERROR] {ex.Message}");
        }
    }

    private async void LoadOrders()
    {
        try
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://51.137.100.120:5000");
            client.DefaultRequestHeaders.Add("ApiKey", "c9e6f1b1-ee5d-4538-8f61-50bf57a9f42b");

            var response = await client.GetAsync("/api/Order");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<List<Order>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                foreach (var order in orders)
                {
                    System.Diagnostics.Debug.WriteLine($"Order: {order.Id} heeft {order.DeliveryStates?.Count ?? 0} status(sen).");
                }

                OrdersView.ItemsSource = orders;
            }
            else
            {
                await DisplayAlert("Fout", $"Statuscode: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[LoadOrders ERROR] {ex.Message}");
            await DisplayAlert("Fout", "Kan geen verbinding maken met de server", "OK");
        }
        
    }

    private async void OnOrderSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedOrder = e.CurrentSelection.FirstOrDefault() as Order;
        if (selectedOrder == null)
            return;

        System.Diagnostics.Debug.WriteLine($"[TAP] Je klikte op order {selectedOrder.Id}");

        await Shell.Current.GoToAsync(nameof(UpdateDeliveryPage), true, new Dictionary<string, object>
{
    { "Order", selectedOrder }
        });

        ((CollectionView)sender).SelectedItem = null;
    }
}
   