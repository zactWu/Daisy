using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class MomentStar
    {
        public string MomentId { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }

        public virtual FavouritePackage FavouritePackage { get; set; }
        public virtual Moment Moment { get; set; }
    }
}
