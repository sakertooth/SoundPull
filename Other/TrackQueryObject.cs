using SoundPull.SoundCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPull.Other
{
    /// <summary>
    /// The track query object.
    /// </summary>
    public class TrackQueryObject
    {
        public List<SoundCloudTrack> collection { get; set; }
        public string next_href { get; set; }
    }
}
