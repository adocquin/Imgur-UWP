using Newtonsoft.Json;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace ImgurUWP
{
    public class ImgurImage
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int datetime { get; set; }
        public string type { get; set; }
        public bool animated { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public int views { get; set; }
        public double bandwidth { get; set; }
        public string deletehash { get; set; }
        public string name { get; set; }
        public string section { get; set; }
        public string link { get; set; }
        public string gifv { get; set; }
        public string mp4 { get; set; }
        public int mp4_size { get; set; }
        public bool looping { get; set; }
        public bool favorite { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool nsfw { get; set; }
        public string vote { get; set; }
        public bool in_gallery { get; set; }

        public ImgurImage() {}
        public ImgurImage(GalleryImage img)
        {
            this.id = img.id;
            this.title = img.title;
            this.description = img.description;
            this.datetime = img.datetime;
            this.type = img.type;
            this.animated = img.animated;
            this.width = img.width;
            this.height = img.height;
            this.size = img.size;
            this.views = img.views;
            this.bandwidth = img.bandwidth;
            this.deletehash = img.deletehash;
            this.section = img.section;
            this.link = img.link;
            this.gifv = img.gifv;
            this.mp4 = img.mp4;
            this.mp4_size = img.mp4_size;
            this.looping = img.looping;
            this.favorite = img.favorite;
            this.nsfw = img.nsfw;
            this.vote = img.vote;
        }
    }

    public class Album
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int datetime { get; set; }
        public string cover { get; set; }
        public int cover_width { get; set; }
        public int cover_height { get; set; }
        public string account_url { get; set; }
        public int account_id { get; set; }
        public string privacy { get; set; }
        public string layout { get; set; }
        public int views { get; set; } 
        public string link { get; set; }
        public bool favorite { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool nsfw { get; set; } 
        public string section { get; set; }
        public int order { get; set; }
        public string deletehash { get; set; }
        public int images_count { get; set; }
        public List<ImgurImage> images { get; set; }
        public bool in_gallery { get; set; }
    }

    public class GalleryImage
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int datetime { get; set; }
        public string type { get; set; }
        public bool animated { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public int views { get; set; }
        public double bandwidth { get; set; }
        public string deletehash { get; set; }
        public string link { get; set; }
        public string gifv { get; set; }
        public string mp4 { get; set; }
        public int mp4_size { get; set; }
        public bool looping { get; set; }
        public string vote { get; set; }
        public bool favorite { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool nsfw { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int comment_count { get; set; }
        public string topic { get; set; }
        public int topic_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string section { get; set; }
        public string account_url { get; set; }
        public int account_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ups { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int downs { get; set; }
        public int points { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int score { get; set; }
        public bool is_album { get; set; }
    }

    public class GalleryAlbum
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int datetime { get; set; }
        public string cover { get; set; }
        public int cover_width { get; set; }
        public int cover_height { get; set; }
        public string account_url { get; set; }
        public int account_id { get; set; }
        public string privacy { get; set; }
        public string layout { get; set; }
        public int views { get; set; }
        public string link { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ups { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int downs { get; set; }
        public int points { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int score { get; set; }
        public bool is_album { get; set; }
        public string vote { get; set; }
        public bool favorite { get; set; }
        public bool nsfw { get; set; }
        public int comment_count { get; set; }
        public string topic { get; set; }
        public int topic_id { get; set; }
        public int images_count { get; set; }
        public List<ImgurImage> images { get; set; }
        public bool in_gallery { get; set; }
    }

    public class GridViewImage
    {
        public BitmapImage Photo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string image_id { get; set; }
    }
}
