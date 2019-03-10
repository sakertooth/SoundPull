using HtmlAgilityPack;
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
    /// Holds the information of a user on SoundCloud.
    /// </summary>
    public class SoundCloudUser
    {
        private const string apiURL = "https://api.soundcloud.com/";

        private readonly HttpClient jsonClient = new HttpClient();
        private readonly HtmlWeb webClient = new HtmlWeb();
        private readonly JsonSerializer serializer = new JsonSerializer();

        public readonly string kind = "user";
        
        public int id { get; set; }
        public string permalink { get; set; }
        public string username { get; set; }
        public string last_modified { get; set; }
        public string uri { get; set; }
        public string permalink_url { get; set; }
        public string avatar_url { get; set; }
        public string country { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string full_name { get; set; }
        public string description { get; set; }
        public string city { get; set; }
        public string discogs_name { get; set; }
        public string myspace_name { get; set; }
        public string website { get; set; }
        public string website_title { get; set; }
        public int? track_count { get; set; }
        public int? playlist_count { get; set; }
        public bool online { get; set; }
        public string plan { get; set; }
        public int? public_favorites_count { get; set; }
        public int? followers_count { get; set; }
        public int? followings_count { get; set; }
        public int? likes_count { get; set; }
        public int? reposts_count { get; set; }
        public int? comments_count { get; set; }

        /// <summary>
        /// Gets a list of tracks of the user.
        /// </summary>
        /// <returns></returns>
        public TrackQueryObject GetTracksResource(string clientID)
        {
            string trackResourceURL = apiURL + "users/" + id + "/tracks?linked_partitioning=1&limit=50&client_id=" + clientID;

            using (Stream s = jsonClient.GetStreamAsync(trackResourceURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<TrackQueryObject>(reader);
            }
        }

        /// <summary>
        /// Gets a list of tracks favorited by the user.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public TrackQueryObject GetFavoritesResource(string clientID)
        {
            string favoritesResourceURL = apiURL + "users/" + id + "/favorites?linked_partitioning=1&limit=50&client_id=" + clientID;

            using (Stream s = jsonClient.GetStreamAsync(favoritesResourceURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<TrackQueryObject>(reader);
            }
        }
        
        /// <summary>
        /// Gets a list of playlist (sets) of the user.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public PlaylistQueryObject GetPlaylistResource(string clientID)
        {
            string playlistResourceURL = apiURL + "users/" + id + "/playlists?linked_partitioning=1&limit=50&client_id=" + clientID;

            using (Stream s = jsonClient.GetStreamAsync(playlistResourceURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<PlaylistQueryObject>(reader);
            }
        }

        /// <summary>
        /// Gets a list of users who are followed by the user.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public UserQueryObject GetFollowingsResource(string clientID)
        {
            string followingsResourceURL = apiURL + "users/" + id + "/followings?linked_partitioning=1&limit=50&client_id=" + clientID;

            using (Stream s = jsonClient.GetStreamAsync(followingsResourceURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<UserQueryObject>(reader);
            }
        }

        /// <summary>
        /// Gets a list of users who are following the user.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public UserQueryObject GetFollowersResource(string clientID)
        {
            string followersResourceURL = apiURL + "users/" + id + "/followers?linked_partitioning=1&limit=50&client_id=" + clientID;

            using (Stream s = jsonClient.GetStreamAsync(followersResourceURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<UserQueryObject>(reader);
            }
        }

        /// <summary>
        /// Gets a list of comments from this user.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<SoundCloudComment> GetCommentResource(string clientID)
        {
            string commentResourceUrl = apiURL + "users/" + id + "/comments?client_id=" + clientID;
            using (Stream s = jsonClient.GetStreamAsync(commentResourceUrl).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<List<SoundCloudComment>>(reader);
            }
        }

        /// <summary>
        /// Gets the reposts of the user.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public List<SoundCloudTrack> GetTrackReposts(string clientID)
        {
            List<SoundCloudTrack> repostTracks = new List<SoundCloudTrack>();

            string html = permalink_url + "/reposts";

            HtmlDocument htmlDocument = webClient.Load(html);
            HtmlNodeCollection htmlNode = htmlDocument.DocumentNode.SelectNodes("//div[@id='app']//h2//a[starts-with(@href, '/')]");
                                          
            int i = 0;
            foreach (HtmlNode node in htmlNode)
            {
                if (i % 2 == 0)
                {
                    string attrib = node.GetAttributeValue("href", "null");

                    string trackUrl = "http://api.soundcloud.com/resolve?url=http://soundcloud.com" + attrib + "&client_id=" + clientID;

                    using (Stream s = jsonClient.GetStreamAsync(trackUrl).Result)
                    using (StreamReader sr = new StreamReader(s))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        repostTracks.Add(serializer.Deserialize<SoundCloudTrack>(reader));
                    }
                }
                i++;
            }

            return repostTracks;
        }
    }
}
