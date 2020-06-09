using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VolunteeringWebsite
{
    public class Const
    {
        public class Place
        {
            public const string home = "home";
            public const string romania = "romania";
            public const string abroad = "abroad";
            public const string favourites = "favourites";
            public const string applied = "applied";
            public const string finished = "finished";
        }

        public class ProjectStatus
        {
            public const int favourites = 1;
            public const int applied = 2;
            public const int finished = 3;
        }
    }
}
