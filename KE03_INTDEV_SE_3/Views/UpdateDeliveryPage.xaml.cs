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


