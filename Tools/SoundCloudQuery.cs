using Newtonsoft.Json;
using SoundPull.Other;
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
        private readonly string clientID;
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
        /// <param name="limit"></param>
        /// <returns></returns>
        public Tuple<TrackQueryObject, UserQueryObject, PlaylistQueryObject> GetAllQuery (string query, int limit)
        {
            TrackQueryObject tracks = GetTrackQuery(query, limit);
            UserQueryObject users = GetUserQuery(query, limit);
            PlaylistQueryObject playlists = GetPlaylistQuery(query, limit);
            return Tuple.Create(tracks, users, playlists);
        }

        /// <summary>
        /// Gets a list of tracks based on the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public TrackQueryObject GetTrackQuery(string query, int limit)
        {
            if (limit > 200 || limit < 1)
            {
                limit = 50;
            }

            string trackQueryURL = (apiURL + "tracks.json?q=" + query + "&linked_partitioning=1&limit=" + limit + "&client_id=" + clientID).Replace(" ", "%20");

            Console.WriteLine(trackQueryURL);
            using (Stream s = jsonClient.GetStreamAsync(trackQueryURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<TrackQueryObject>(reader);
            }
        }

        /// <summary>
        /// Gets a list of users based on the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public UserQueryObject GetUserQuery(string query, int limit)
        {
            if (limit > 200 || limit < 1)
            {
                limit = 50;
            }

            string userQueryURL = (apiURL + "users.json?q=" + query + "&linked_partitioning=1&limit=" + limit + "&client_id=" + clientID).Replace(" ", "%20");
            using (Stream s = jsonClient.GetStreamAsync(userQueryURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<UserQueryObject>(reader);
            }
        }

        /// <summary>
        /// Gets a list of playlists based on the specified query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PlaylistQueryObject GetPlaylistQuery(string query, int limit)
        {
            if (limit > 200 || limit < 1)
            {
                limit = 50;
            }

            string playlistQueryURL = (apiURL + "playlists.json?q=" + query + "&linked_partitioning=1&limit=" + limit + "&client_id=" + clientID).Replace(" ", "%20");
            using (Stream s = jsonClient.GetStreamAsync(playlistQueryURL).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<PlaylistQueryObject>(reader);
            }
        }

        /// <summary>
        /// Get the next page in a track query.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public TrackQueryObject GetNextTrackInQuery(string link)
        {
            using (Stream s = jsonClient.GetStreamAsync(link).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                TrackQueryObject newTrackObject = serializer.Deserialize<TrackQueryObject>(reader);
                link = newTrackObject.next_href;

                return newTrackObject;
            }
        }

        /// <summary>
        /// Get the next page in a user query.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public UserQueryObject GetNextUserInQuery(string link)
        {
            using (Stream s = jsonClient.GetStreamAsync(link).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                UserQueryObject newUserObject = serializer.Deserialize<UserQueryObject>(reader);
                link = newUserObject.next_href;

                return newUserObject;
            }
        }

        /// <summary>
        /// Get the next page in a playlist query.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public PlaylistQueryObject GetNextPlaylistInQuery(string link)
        {
            using (Stream s = jsonClient.GetStreamAsync(link).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                PlaylistQueryObject newPlaylistObject = serializer.Deserialize<PlaylistQueryObject>(reader);
                link = newPlaylistObject.next_href;

                return newPlaylistObject;
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
