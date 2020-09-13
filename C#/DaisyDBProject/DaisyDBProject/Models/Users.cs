using System;
using System.Collections.Generic;

namespace DaisyDBProject.Models
{
    public partial class Users
    {
        public Users()
        {
            Application = new HashSet<Application>();
            Comment = new HashSet<Comment>();
            Discussion = new HashSet<Discussion>();
            FavouritePackage = new HashSet<FavouritePackage>();
            FollowAccountNavigation = new HashSet<Follow>();
            FollowFollowedAccountNavigation = new HashSet<Follow>();
            LeaveMessage = new HashSet<LeaveMessage>();
            LiftBan = new HashSet<LiftBan>();
            LikeDisc = new HashSet<LikeDisc>();
            LikeMoment = new HashSet<LikeMoment>();
            Member = new HashSet<Member>();
            MessageReceiveAccountNavigation = new HashSet<Message>();
            MessageSendAccountNavigation = new HashSet<Message>();
            Moment = new HashSet<Moment>();
            Reply = new HashSet<Reply>();
            Report = new HashSet<Report>();
            Subscribe = new HashSet<Subscribe>();
            UserGroupMessage = new HashSet<UserGroupMessage>();
            UserNotice = new HashSet<UserNotice>();
            UserNotifi = new HashSet<UserNotifi>();
            Usergroups = new HashSet<Usergroups>();
        }

        public string Account { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string PhoneNum { get; set; }
        public string EmailAddress { get; set; }
        public string Sex { get; set; }
        public string School { get; set; }
        public string College { get; set; }
        public int? Grade { get; set; }
        public string StudentNumber { get; set; }
        public string IconUrl { get; set; }
        public string Intro { get; set; }

        public virtual ICollection<Application> Application { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Discussion> Discussion { get; set; }
        public virtual ICollection<FavouritePackage> FavouritePackage { get; set; }
        public virtual ICollection<Follow> FollowAccountNavigation { get; set; }
        public virtual ICollection<Follow> FollowFollowedAccountNavigation { get; set; }
        public virtual ICollection<LeaveMessage> LeaveMessage { get; set; }
        public virtual ICollection<LiftBan> LiftBan { get; set; }
        public virtual ICollection<LikeDisc> LikeDisc { get; set; }
        public virtual ICollection<LikeMoment> LikeMoment { get; set; }
        public virtual ICollection<Member> Member { get; set; }
        public virtual ICollection<Message> MessageReceiveAccountNavigation { get; set; }
        public virtual ICollection<Message> MessageSendAccountNavigation { get; set; }
        public virtual ICollection<Moment> Moment { get; set; }
        public virtual ICollection<Reply> Reply { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual ICollection<Subscribe> Subscribe { get; set; }
        public virtual ICollection<UserGroupMessage> UserGroupMessage { get; set; }
        public virtual ICollection<UserNotice> UserNotice { get; set; }
        public virtual ICollection<UserNotifi> UserNotifi { get; set; }
        public virtual ICollection<Usergroups> Usergroups { get; set; }
    }
}
