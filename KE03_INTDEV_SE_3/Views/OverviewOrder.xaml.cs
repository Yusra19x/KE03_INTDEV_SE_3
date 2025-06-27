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
        //Toon in output dat de orders worden geladen
        try
        {
            //Maak een nieuwe HttpClient aan en stel de basis-URL en API-sleutel in
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://51.137.100.120:5000");
            client.DefaultRequestHeaders.Add("ApiKey", "c9e6f1b1-ee5d-4538-8f61-50bf57a9f42b");

            //Het pad naar de API endpoint voor het ophalen van orders
            var response = await client.GetAsync("/api/Order");

            //Als de response succesvol is, deserialiseer de JSON naar een lijst van Orders 
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<List<Order>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //Toon in output hoeveel orders er zijn en hoeveel status(sen) elke order heeft
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

        //Toon foutmelding als er geen verbinding kan worden gemaakt met de server
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[LoadOrders ERROR] {ex.Message}");
            await DisplayAlert("Fout", "Kan geen verbinding maken met de server", "OK");
        }
        
    }

    //Toon in output dat er op een order is geklikt
    private async void OnOrderSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedOrder = e.CurrentSelection.FirstOrDefault() as Order;
        if (selectedOrder == null)
            return;

        System.Diagnostics.Debug.WriteLine($"[TAP] Je klikte op order {selectedOrder.Id}");


        //Roept in appshell aan om naar de UpdateDeliveryPage te navigeren
        await Shell.Current.GoToAsync(nameof(UpdateDeliveryPage), true, new Dictionary<string, object>
{
    { "Order", selectedOrder }
        });

        ((CollectionView)sender).SelectedItem = null;
    }
    //private void ApplyFilters()
    //{
    //    var name = NameFilterEntry?.Text?.Trim();
    //    var selectedDate = DateFilter.Date;

    //    bool nameFilterActive = !string.IsNullOrWhiteSpace(name);
    //    bool dateFilterActive = userHeeftDatumGekozen;

    //    if (!nameFilterActive && !dateFilterActive)
    //    {
    //        OrdersView.ItemsSource = allOrders;
    //        return;
    //    }

    //    var filtered = allOrders.AsEnumerable();

    //    if (nameFilterActive && name != null)
    //    {
    //        filtered = filtered.Where(o =>
    //            o.Customer != null &&
    //            !string.IsNullOrWhiteSpace(o.Customer.Name) &&
    //            o.Customer.Name.ToLower().Contains(name.ToLower()));
    //    }

    //    if (dateFilterActive)
    //    {
    //        filtered = filtered.Where(o => o.OrderDate.Date == selectedDate.Date);
    //    }

    //    OrdersView.ItemsSource = filtered.ToList();
    //}

}
