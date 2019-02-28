using Newtonsoft.Json;
using SoundPull.SoundCloud;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SoundPull.Tools
{
    /// <summary>
    /// Allows for searching of tracks, playlists, etc.
    /// </summary>
    public class SoundCloudQuery
    {
        private readonly HttpClient jsonClient = new HttpClient();
        private string clientID;
        private const string apiURL = "https://api.soundcloud.com/";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        public SoundCloudQuery(string clientID)
        {
            this.clientID = clientID;
        }

        /// <summary>
        /// Gets a Tuple, containing queries for tracks, users, and playlists.
        /// Please note that this process can take considerably long.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Tuple<List<SoundCloudTrack>, List<SoundCloudUser>, List<SoundCloudPlaylist>> GetAllQuery (string query)
        {
            List<SoundCloudTrack> tracks = GetTrackQuery(query);
            List<SoundCloudUser> users = GetUserQuery(query);
            List<SoundCloudPlaylist> playlists = GetPlaylistQuery(query);
            return Tuple.Create(tracks, users, playlists);
        }

        /// <summary>
        /// Gets a list of tracks based on the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SoundCloudTrack> GetTrackQuery(string query)
        {
            string trackQueryURL = (apiURL + "tracks.json?q=" + query + "&client_id=" + clientID).Replace(" ", "%20");

            using (Stream s = jsonClient.GetStreamAsync(trackQueryURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<List<SoundCloudTrack>>(reader);
            }
        }

        /// <summary>
        /// Gets a list of users based on the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SoundCloudUser> GetUserQuery(string query)
        {
            string trackQueryURL = (apiURL + "users.json?q=" + query + "&client_id=" + clientID).Replace(" ", "%20");

            using (Stream s = jsonClient.GetStreamAsync(trackQueryURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<List<SoundCloudUser>>(reader);
            }
        }

        /// <summary>
        /// Gets a list of playlists based on the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SoundCloudPlaylist> GetPlaylistQuery(string query)
        {
            string trackQueryURL = (apiURL + "playlists.json?q=" + query + "&client_id=" + clientID).Replace(" ", "%20");

            using (Stream s = jsonClient.GetStreamAsync(trackQueryURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<List<SoundCloudPlaylist>>(reader);
            }
        }

        /// <summary>
        /// Get the client id.
        /// </summary>
        /// <returns></returns>
        public string GetClientID()
        {
            return clientID;
        }
    }
}
