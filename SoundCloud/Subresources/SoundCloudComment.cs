using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPull.SoundCloud.Subresources
{
    /// <summary>
    /// Holds the information of a comment on SoundCloud
    /// </summary>
    public class SoundCloudComment
    {
        private readonly string kind = "comment";

        public int id { get; set; }
        public string created_at { get; set; }
        public int user_id { get; set; }
        public int track_id { get; set; }
        public int timestamp { get; set; }
        public string body { get; set; }
        public string uri { get; set; }
        public SoundCloudUser user { get; set; }

        /// <summary>
        /// Detects if this comment is a reply to another comment.
        /// </summary>
        /// <returns></returns>
        public bool IsAReply()
        {
            if (body.StartsWith("@"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
