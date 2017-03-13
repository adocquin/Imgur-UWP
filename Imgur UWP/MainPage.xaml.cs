using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ImgurUWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void tokenButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://api.imgur.com/oauth2/authorize?client_id=8e0148d95c9866b&response_type=pin"));
        }

        private async void buttonConnection_Click(object sender, RoutedEventArgs e)
        {
            String pin = pinBox.Text;
            if (pin.Length > 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        { "client_id", "8e0148d95c9866b" },
                        { "client_secret", "46eba94c9f31b5b053a9f2aa2cb289845a454b7e" },
                        { "grant_type", "pin" },
                        { "pin", pinBox.Text }
                    };
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("https://api.imgur.com/oauth2/token", content);
                    string ret = await response.Content.ReadAsStringAsync();
                    ImgurUWP_Imgur Imgur = JsonConvert.DeserializeObject<ImgurUWP_Imgur>(ret);
                    if (Imgur.account_id != 0) {
                        this.Frame.Navigate(typeof(SecondPage), Imgur);
                    } else {
                        Imgur_Request request = JsonConvert.DeserializeObject<Imgur_Request>(ret);
                        infoBlock.Text = "Error : " + request.data.error;
                    }
                }
            } else {
                infoBlock.Text = "Error : Empty pin";
            }
        }

        private void pinBox_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (pinBox.Text.CompareTo("PIN code") == 0)
            {
                pinBox.Text = "";
            }
        }

        private void pinBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
