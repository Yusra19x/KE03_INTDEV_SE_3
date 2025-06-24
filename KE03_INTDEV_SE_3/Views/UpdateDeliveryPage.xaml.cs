using KE03_INTDEV_SE_3.Models;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;

namespace KE03_INTDEV_SE_3.Views;

[QueryProperty(nameof(Order), "Order")]
public partial class UpdateDeliveryPage : ContentPage
{
    public Order Order { get; set; }

    // Deze lijst bevat de mogelijke statusopties voor de leveringsstatus
    public List<string> StatusOptions { get; set; } = Enum.GetNames(typeof(DeliveryStateEnum)).ToList();

    public UpdateDeliveryPage()
    {
        InitializeComponent();         
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //Als er een order en customer aanwezig is, vul dan de labels met de gegevens van de order en klant
        if (Order?.Customer != null)
        {
            customerNameLabel.Text = Order.Customer.Name;
            customerAddressLabel.Text = Order.Customer.Address;
            orderDateLabel.Text = Order.OrderDate.ToString("dd-MM-yyyy");
        }
    }

    //private async void OnUpdateClicked(object sender, EventArgs e)Add commentMore actions
    //{
    //    if (statusPicker.SelectedItem == null)
    //    {
    //        await DisplayAlert("Fout", "Kies een status", "OK");
    //        return;
    //    }

    //    var selectedStatus = Enum.Parse<DeliveryStateEnum>(statusPicker.SelectedItem.ToString());

    //    var deliveryState = new
    //    {
    //        Id = 0,
    //        State = (int)selectedStatus,
    //        DateTime = DateTime.UtcNow,
    //        OrderId = Order.Id,
    //        Order = new
    //        {
    //            Id = Order.Id,
    //            OrderDate = Order.OrderDate,
    //            CustomerId = Order.CustomerId,
    //            Customer = new
    //            {
    //                Id = Order.Customer.Id,
    //                Name = Order.Customer.Name,
    //                Address = Order.Customer.Address,
    //                Active = Order.Customer.Active
    //            }
    //        },
    //        DeliveryServiceId = 1,
    //        DeliveryService = new
    //        {
    //            Id = 1,
    //            Name = "DHL",

    //        }

    //    };


    //    var json = JsonSerializer.Serialize(deliveryState);
    //    var content = new StringContent(json, Encoding.UTF8, "application/json");

    //    try
    //    {

    //        using var client = new HttpClient();
    //        client.BaseAddress = new Uri("http://51.137.100.120:5000");
    //        client.DefaultRequestHeaders.Add("ApiKey", "c9e6f1b1-ee5d-4538-8f61-50bf57a9f42b");


    //        await DisplayAlert("Fout", content.ToString(), "OK");

    //        var response = await client.PostAsync("/api/DeliveryStates/UpdateDeliveryState", content);

    //        if (response.IsSuccessStatusCode)
    //        {
    //            await DisplayAlert("Gelukt", "Status is bijgewerkt!", "OK");
    //        }

    //        else
    //        {
    //            var errorContent = await response.Content.ReadAsStringAsync();
    //            await DisplayAlert("Fout", $"Statuscode: {response.StatusCode}\nDetails: {errorContent}", "OK");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.WriteLine($"[UpdateDelivery ERROR] {ex.Message}");
    //        await DisplayAlert("Fout", "Kan geen verbinding maken met de server", "OK");
    //    }

    //}

    // Deze methode wordt aangeroepen wanneer de gebruiker op de knop klikt om de status bij te werken
    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        if (statusPicker.SelectedItem == null)
        {
            await DisplayAlert("Fout", "Kies een status", "OK");
            return;
        }

        bool bevestiging = await DisplayAlert(
            "Bevestigen",
            $"Weet je zeker dat je de status wilt markeren als '{statusPicker.SelectedItem}'?",
            "Ja",
            "Nee"
        );

        if (!bevestiging)
            return;

        await DisplayAlert("Voltooid", "De status is bevestigd.", "OK");
    }

    // Deze methode wordt aangeroepen wanneer de gebruiker op de knop klikt om een foto te maken
    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.CapturePhotoAsync();

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                PhotoResultImage.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fout", $"Kon geen foto maken: {ex.Message}", "OK");
        }
    }

    // Deze methode wordt aangeroepen wanneer de gebruiker op de knop klikt om een foto te kiezen
    private async void OnPickPhotoClicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync();

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                PhotoResultImage.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fout", $"Kon geen foto kiezen: {ex.Message}", "OK");
        }
    }
}


