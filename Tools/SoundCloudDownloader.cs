using SoundPull.SoundCloud;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoundPull.Tools
{
    /// <summary>
    /// Download SoundCloud tracks and playlists.
    /// </summary>
    public class SoundCloudDownloader
    {
        private readonly WebClient client = new WebClient();
        private readonly string clientID;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientID"></param>
        public SoundCloudDownloader(string clientID)
        {
            this.clientID = clientID;
            ServicePointManager.DefaultConnectionLimit = 50;
        }

        /// <summary>
        /// Downloads a track.
        /// </summary>
        /// <param name="track"></param>
        /// <param name="dir"></param>
        /// <param name="format"></param>
        public void DownloadTrack(SoundCloudTrack track, string dir, string format)
        {
            string trackUrl = track.stream_url + "?client_id=" + clientID;
            client.DownloadFileTaskAsync(trackUrl, dir + @"\" + RemoveIllegalCharacters(track.user.username + " - " + track.title) + "." + format);
        }

        /// <summary>
        /// WIP - Downloads a playlist.
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="dir"></param>
        /// <param name="format"></param>
        public void DownloadPlaylist(SoundCloudPlaylist playlist, string dir, string format)
        {
            for (int i = 0; i < playlist.track_count; i++)
            {
                Parallel.ForEach(playlist.tracks, (track) =>
                {
                    try
                    {
                        DownloadTrack(track, dir, format);
                    }
                    catch { }
                });
            }
        }

        /// <summary>
        /// Downloads any resource.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="dir"></param>
        /// <param name="format"></param>
        /// <param name="resourceName"></param>
        public void DownloadResource(string resource, string dir, string resourceName, string format)
        {
            client.DownloadFile(resource, dir + @"\" + RemoveIllegalCharacters(resourceName) + "." + format);
        }

        private string RemoveIllegalCharacters(string stringToRemove)
        {
            return stringToRemove.Replace("<", "").Replace(">", "").Replace(":", "").Replace("\"", "").Replace("/", "").Replace(@"\", "").Replace("|", "").Replace("?", "").Replace("*", "");
        }

    }
}
