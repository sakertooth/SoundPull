using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPull.SoundCloud
{
    /// <summary>
    /// Holds the information of a playlist on SoundCloud.
    /// </summary>
    public class SoundCloudPlaylist
    {
        public string kind = "playlist";

        public int id { get; set; }
        public int duration { get; set; }
        public string permalink_url { get; set; }
        public int reposts_count { get; set; }
        public string genre { get; set; }
        public string permalink { get; set; }
        public string purchase_url { get; set; }
        public int? release_month { get; set; }
        public string description { get; set; }
        public string uri { get; set; }
        public string label_name { get; set; }
        public string tag_list { get; set; }
        public int? release_year { get; set; }
        public int track_count { get; set; }
        public int user_id { get; set; }
        public string last_modified { get; set; }
        public string license { get; set; }
        public List<SoundCloudTrack> tracks { get; set; }
        public string playlist_type { get; set; }
        public bool? downloadable { get; set; }
        public string sharing { get; set; }
        public string created_at { get; set; }
        public int? release { get; set; }
        public int likes_count { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string purchase_title { get; set; }
        public string artwork_url { get; set; }
        public string ean { get; set; }
        public bool? streamable { get; set; }
        public SoundCloudUser user { get; set; }
        public string embeddable_by { get; set; }
        public int? label_id { get; set; }
    }
}
