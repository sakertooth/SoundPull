using SoundPull.SoundCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPull.Other
{
    /// <summary>
    /// The user query object.
    /// </summary>
    public class UserQueryObject
    {
        public List<SoundCloudUser> collection { get; set; }
        public string next_href { get; set; }
    }
}
