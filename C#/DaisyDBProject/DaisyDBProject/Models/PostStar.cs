using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class PostStar
    {
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public int PostId { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }

        public virtual FavouritePackage FavouritePackage { get; set; }
        public virtual Post Post { get; set; }
    }
}
