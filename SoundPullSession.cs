using SoundPull.SoundCloud;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

namespace SoundPull
{
    /// <summary>
    /// Used to pull tracks, playlists, comments, and more from SoundCloud.
    /// </summary>
    public class SoundPullSession
    {
        private readonly HttpClient jsonClient = new HttpClient();
        private JsonSerializer serializer = new JsonSerializer();

        private const string apiResolveURL = "https://api.soundcloud.com/resolve.json?url=";
        private const string soundCloudURL = "https://soundcloud.com/";

        private string clientID;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        public SoundPullSession(string clientID)
        {
            this.clientID = clientID;
        }
        
        /// <summary>
        /// Gets a SoundCloud track.
        /// </summary>
        /// <param name="userPermalink"></param>
        /// <param name="trackPermalink"></param>
        /// <returns></returns>
        public SoundCloudTrack GetTrack(string userPermalink, string trackPermalink)
        {
            string resolveUrl = apiResolveURL + (soundCloudURL + userPermalink + "/" + trackPermalink).Replace(":", "%3A").Replace("/", "%2F") + "&client_id=" + clientID;

            using (Stream s = jsonClient.GetStreamAsync(resolveUrl).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<SoundCloudTrack>(reader);
            }
        }

        /// <summary>
        /// Gets a SoundCloud user.
        /// </summary>
        /// <param name="userPermalink"></param>
        /// <returns></returns>
        public SoundCloudUser GetUser(string userPermalink)
        {
            string resolveUrl = apiResolveURL + (soundCloudURL + userPermalink).Replace(":", "%3A").Replace("/", "%2F") + "&client_id=" + clientID;

            using (Stream s = jsonClient.GetStreamAsync(resolveUrl).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<SoundCloudUser>(reader);
            }
        }

        /// <summary>
        /// Gets a SoundCloud playlist.
        /// Please note that this may have problems with very large playlists (usually >100)
        /// </summary>
        /// <param name="userPermalink"></param>
        /// <param name="playlistPermalink"></param>
        /// <returns></returns>
        public SoundCloudPlaylist GetPlaylist(string userPermalink, string playlistPermalink)
        {
            string resolveUrl = apiResolveURL + (soundCloudURL + userPermalink + "/sets/" + playlistPermalink).Replace(":", "%3A").Replace("/", "%2F") + "&client_id=" + clientID;

            using (Stream s = jsonClient.GetStreamAsync(resolveUrl).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<SoundCloudPlaylist>(reader);
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
