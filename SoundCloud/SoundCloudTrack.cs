using Newtonsoft.Json;
using SoundPull.Other;
using SoundPull.SoundCloud.Subresources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SoundPull.SoundCloud
{
    /// <summary>
    /// Holds the information of a track from SoundCloud.
    /// </summary>
    public class SoundCloudTrack
    {
        private const string apiURL = "https://api.soundcloud.com/";
        private readonly HttpClient jsonClient = new HttpClient();

        public readonly string kind = "track";

        public int id { get; set; }
        public string created_at { get; set; }
        public int user_id { get; set; }
        public int duration { get; set; }
        public bool? commentable { get; set; }
        public string state { get; set; }
        public int original_content_size { get; set; }
        public string last_modified { get; set; }
        public string sharing { get; set; }
        public string tag_list { get; set; }
        public string permalink { get; set; }
        public bool? streamable { get; set; }
        public string embeddable_by { get; set; }
        public string purchase_url { get; set; }
        public string purchase_title { get; set; }
        public int? label_id { get; set; }
        public string genre { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string label_name { get; set; }
        public string release { get; set; }
        public string track_type { get; set; }
        public string key_signature { get; set; }
        public string isrc { get; set; }
        public string video_url { get; set; }
        public string bpm { get; set; }
        public int? release_year { get; set; }
        public int? release_month { get; set; }
        public int? relaese_day { get; set; }
        public string original_format { get; set; }
        public string license { get; set; }
        public string uri { get; set; }
        public SoundCloudUser user { get; set; }
        public string permalink_url { get; set; }
        public string artwork_url { get; set; }
        public string stream_url { get; set; }
        public string download_url { get; set; }
        public int playback_count { get; set; }
        public int download_count { get; set; }
        public int favoritings_count { get; set; }
        public int reposts_count { get; set; }
        public int comment_count { get; set; }
        public bool? downloadable { get; set; }
        public string waveform_url { get; set; }
        public string attachments_url { get; set; }
        public string policy { get; set; }
        public string monetization_model { get; set; }

        /// <summary>
        /// Gets the comment for this track.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<SoundCloudComment> GetCommentResource(string clientID)
        {
            string commentResourceUrl = apiURL + "tracks/" + id + "/comments?client_id=" + clientID;
            using (Stream s = jsonClient.GetStreamAsync(commentResourceUrl).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<List<SoundCloudComment>>(reader);
            }
        }

        /// <summary>
        /// Gets the users who have favorited this track.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public UserQueryObject GetFavoritersResource(string clientID)
        {
            string favoritersResourceUrl = apiURL + "tracks/" + id + "/favoriters?linked_partitioning=1&limit=50&client_id=" + clientID;
            using (Stream s = jsonClient.GetStreamAsync(favoritersResourceUrl).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<UserQueryObject>(reader);
            }
        }
    }
}
