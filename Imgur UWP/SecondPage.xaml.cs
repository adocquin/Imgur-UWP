using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace ImgurUWP
{
    public sealed partial class SecondPage : Page
    {
        private ImgurUWP_Imgur Imgur;
        private string selected_imageId = "";

        enum actual_shown {HOME,FAVORITES,USER};
        private int actual_flux;

        public SecondPage()
        {
            this.InitializeComponent();
            buttonFav.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            buttonDelete.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            buttonSearch.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            searchBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Imgur = e.Parameter as ImgurUWP_Imgur;
            textBlock.Text = "Connected as " + Imgur.account_username;
        }

        private void GridViewAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                GridViewImage image = view.SelectedItem as GridViewImage;
                this.selected_imageId = image.image_id;
            }
            catch
            {

            }
        }

        private async void buttonUpload_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker browser = new FileOpenPicker();
            browser.FileTypeFilter.Add(".png");
            browser.FileTypeFilter.Add(".jpg");
            browser.FileTypeFilter.Add(".jpeg");
            StorageFile file = await browser.PickSingleFileAsync();
            if (file != null)
            {
                Stream stream = await file.OpenStreamForReadAsync();
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StreamContent(stream);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Imgur.access_token);
                    MultipartFormDataContent data = new MultipartFormDataContent();
                    data.Add(content, "image", "image");
                    data.Add(content, "name", file.Name);
                    data.Add(content, "description", "Another ImgurUWP upload =)");
                    HttpResponseMessage response = await client.PostAsync(new Uri("https://api.imgur.com/3/upload"), data);
                    string ret = await response.Content.ReadAsStringAsync();
                    if (this.actual_flux == (int)actual_shown.USER)
                    {
                        List<ImgurImage> images = await Imgur.getAllUserImages();
                        LoadImage(images);
                    }
                }
            }
        }

        private void LoadImage(List<ImgurImage> images)
        {
            string tmp;
            string gal_url ="http://im";

            List<GridViewImage> list = new List<GridViewImage>();
            for (int i = 0; i < 50 && i < images.Count; i++)
            {
                ImgurImage img = images[i];
                if (img.description != null)
                    tmp = img.description;
                else
                    tmp = "No description";
                if (!img.link.StartsWith(gal_url))
                    list.Add(new GridViewImage() { Name = img.title, Description = tmp, Photo = new BitmapImage(new Uri(img.link, UriKind.Absolute)), image_id = img.id });
                else
                    list.Add(new GridViewImage() { Name = img.title, Description = tmp, Photo = new BitmapImage(new Uri("ms-appx:///Assets/Apps-Gallery-icon.png", UriKind.Absolute)), image_id = img.id });
            }
            GridViewAlbum.ItemsSource = list;
        }

        private async void buttonGalHot_Click(object sender, RoutedEventArgs e)
        {
            this.actual_flux = (int)actual_shown.HOME;
            buttonFav.Visibility = Windows.UI.Xaml.Visibility.Visible;
            buttonSearch.Visibility = Windows.UI.Xaml.Visibility.Visible;
            buttonDelete.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            searchBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
            List<ImgurImage> images = await Imgur.getGallery();
            LoadImage(images);
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            await Imgur.RemoveImage(selected_imageId);
            List<ImgurImage> images = await Imgur.getAllUserImages();
            LoadImage(images);
        }

        private async void buttonGalFav_Click(object sender, RoutedEventArgs e)
        {
            this.actual_flux = (int)actual_shown.FAVORITES;
            buttonFav.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            buttonSearch.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            buttonDelete.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            searchBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            List<ImgurImage> images = await Imgur.getFavorites();
            LoadImage(images);
        }

        private async void buttonGalUser_Click(object sender, RoutedEventArgs e)
        {
            this.actual_flux = (int)actual_shown.USER;
            buttonFav.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            buttonSearch.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            buttonDelete.Visibility = Windows.UI.Xaml.Visibility.Visible;
            searchBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            List<ImgurImage> images = await Imgur.getAllUserImages();
            LoadImage(images);
        }

        private async void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            List<ImgurImage> images = await Imgur.SearchGallery(searchBox.Text);
            LoadImage(images);
        }

        private async void buttonFav_Click(object sender, RoutedEventArgs e)
        {
            await Imgur.AddFavorite(selected_imageId);
        }
    }
}

                                                                            