using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaisyDBProject.Models
{
    public partial class Post
    {
        public Post()
        {
            PostStar = new HashSet<PostStar>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public string PostTime { get; set; }
        public string Content { get; set; }
        public int MaxMemberNum { get; set; }
        public int CurMemberNum { get; set; }

        public virtual Usergroups Usergroups { get; set; }
        public virtual ICollection<PostStar> PostStar { get; set; }
    }
}
