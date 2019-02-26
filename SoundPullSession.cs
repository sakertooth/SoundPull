using SoundPull.SoundCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

namespace SoundPull
{
    /// <summary>
    /// Used to pull tracks, playlists, comments, and more from SoundCloud.
    /// </summary>
    /// <param name="clientID"></param>
    public class SoundPullSession
    {
        private JsonSerializer serializer = new JsonSerializer();
        private HttpClient jsonClient = new HttpClient();

        private const string apiResolveURL = "https://api.soundcloud.com/resolve.json?url=";
        private const string soundCloudURL = "https://soundcloud.com/";

        public string clientID;

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
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<SoundCloudTrack>(reader);
            }
        }

        /// <summary>
        /// Gets a SoundCloud user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public SoundCloudUser GetUser(string userPermalink)
        {
            string resolveUrl = apiResolveURL + (soundCloudURL + userPermalink).Replace(":", "%3A").Replace("/", "%2F") + "&client_id=" + clientID;

            using (Stream s = jsonClient.GetStreamAsync(resolveUrl).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<SoundCloudUser>(reader);
            }
        }
    }
}
