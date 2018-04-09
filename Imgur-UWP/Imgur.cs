using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ImgurUWP
{
    public class Imgur_GetAlbum
    {
        public Album data;
        public bool success;
        public int status;
    }
    public class Imgur_GetGallery
    {
        public List<GalleryImage> data;
        public bool success;
        public int status;
    }
    public class Imgur_AllImages
    {
        public List<ImgurImage> data;
        public bool success;
        public int status;
    }
    public class Imgur_GetUserFavorites
    {
        public List<GalleryImage> data;
        public bool success;
        public int status;
    }
    public class Imgur_Error
    {
        public string error { get; set; }
        public string request { get; set; }
        public string method { get; set; }
    }
    public class Imgur_Request
    {
        public Imgur_Error data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
    }

    public class ImgurUWP_Imgur
    {
		// Change secret key and app id here
        public string app_id = "";
        public string secret = "";
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public object scope { get; set; }
        public string refresh_token { get; set; }
        public int account_id { get; set; }
        public string account_username { get; set; }

        public async Task<string> HttpGet(string url)
        {
            string ret;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.access_token);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        ret = await content.ReadAsStringAsync();
                    }
                }
            }

            return ret;
        }

        public async Task<string> HttpDelete(string url)
        {
            string ret;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.access_token);
                using (HttpResponseMessage response = await client.DeleteAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        ret = await content.ReadAsStringAsync();
                    }
                }
            }
            return ret;
        }

        public async Task<Album> getAlbum(string album_id)
        {
            string ret = await this.HttpGet("https://api.imgur.com/3/album/" + album_id);
            return JsonConvert.DeserializeObject<Imgur_GetAlbum>(ret).data;
        }

        public async Task<List<ImgurImage>> getAllUserImages(int page = 0)
        {
            string ret = await this.HttpGet("https://api.imgur.com/3/account/" + this.account_username + "/images/" + page.ToString());
            return JsonConvert.DeserializeObject<Imgur_AllImages>(ret).data;
        }

        public async Task<List<ImgurImage>> getFavorites()
        {
            string ret = await this.HttpGet("https://api.imgur.com/3/account/" + this.account_username + "/favorites/");
            return Convert(JsonConvert.DeserializeObject<Imgur_GetUserFavorites>(ret).data);
        }

        public async Task RemoveFavorite(string picture_id)
        {
            await this.HttpDelete("https://api.imgur.com/3/image/" + picture_id + "/favorite");
        }

        public async Task RemoveImage(string picture_id)
        {
            await this.HttpDelete("https://api.imgur.com/3/image/" + picture_id);
        }

        public async Task<List<ImgurImage>> SearchGallery(string search)
        {
            string ret = await this.HttpGet("https://api.imgur.com/3/gallery/search?q=" + search);
            return Convert(JsonConvert.DeserializeObject<Imgur_GetUserFavorites>(ret).data);
        }

        public async Task AddFavorite(string picture_id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.access_token);
                HttpResponseMessage response = await client.PostAsync(new Uri("https://api.imgur.com/3/image/" + picture_id + "/favorite"), null);
                string ret = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<List<ImgurImage>> getGallery(string section = null, string sort = null, int page = 3, string window = null, bool showViral = true)
        {
            string url = "https://api.imgur.com/3/gallery/";

            // hot | top | user_id - Defaults : hot
            if (section != null) { url = url + section + "/"; } else { url = url + "hot/"; }
            // viral | top | time (only with user) - Defaults : viral
            if (sort != null) { url = url + sort + "/"; } else { url = url + "viral/"; }
            // number
            url = url + page.ToString() + "/";
            // day | week | month | year | all
            if (window != null) { url = url + window + "/"; } else { url = url + "day/"; }
            // true | false
            url = url + showViral.ToString();
            string ret = await this.HttpGet(url);
            return Convert(JsonConvert.DeserializeObject<Imgur_GetGallery>(ret).data);
        }

        public List<ImgurImage> Convert(List<GalleryImage> list)
        {
            List<ImgurImage> ret = new List<ImgurImage>();
            foreach (GalleryImage img in list)
            {
                ret.Add(new ImgurImage(img));
            }
            return ret;
        }
    }
}
