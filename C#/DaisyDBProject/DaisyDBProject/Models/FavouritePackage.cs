using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class FavouritePackage
    {
        public FavouritePackage()
        {
            MomentStar = new HashSet<MomentStar>();
            PostStar = new HashSet<PostStar>();
        }

        public string Account { get; set; }
        public string Name { get; set; }
        public string CreateTime { get; set; }
        public string Privacy { get; set; }

        public virtual Users AccountNavigation { get; set; }
        public virtual ICollection<MomentStar> MomentStar { get; set; }
        public virtual ICollection<PostStar> PostStar { get; set; }
    }
}
