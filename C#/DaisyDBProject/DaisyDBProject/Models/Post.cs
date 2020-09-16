using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace DaisyDBProject.Models
{
    public partial class Post
    {
        public Post()
        {
            PostStar = new HashSet<PostStar>();
        }

        public Post(int projectId, int groupId, string postTime, string content, int maxMemberNum) {
            ProjectId = projectId;
            GroupId = groupId;
            PostTime = postTime;
            Content = content;
            MaxMemberNum = maxMemberNum;
            PostStar = new HashSet<PostStar>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public string PostTime { get; set; }
        public string Content { get; set; }
        public int MaxMemberNum { get; set; }

        [JsonIgnore]
        public virtual Usergroups Usergroups { get; set; }
        public virtual ICollection<PostStar> PostStar { get; set; }
    }
}
