using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class PostStar
    {
        public string ProjectId { get; set; }
        public string GroupId { get; set; }
        public string PostId { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }

        public virtual FavouritePackage FavouritePackage { get; set; }
        public virtual Post Post { get; set; }
    }
}
