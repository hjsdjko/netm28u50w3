using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xiezn.Core.Models.ViewModel
{
    public class FaceMatchViewModel
    {
        public string image { get; set; }

        public string image_type { get; set; } = "BASE64";

        public string face_type { get; set; } = "LIVE";

        public string quality_control { get; set; } = "LOW";

        public string liveness_control { get; set; } = "NONE";
    }
}
