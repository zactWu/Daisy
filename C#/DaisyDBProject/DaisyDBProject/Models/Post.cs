using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Post
    {
        public Post()
        {
            PostStar = new HashSet<PostStar>();
        }

        public string PostId { get; set; }
        public string ProjectId { get; set; }
        public string GroupId { get; set; }
        public string PostTime { get; set; }
        public string Content { get; set; }
        public int? MaxMemberNum { get; set; }
        public int? CurMemberNum { get; set; }

        public virtual Usergroups Usergroups { get; set; }
        public virtual ICollection<PostStar> PostStar { get; set; }
    }
}
