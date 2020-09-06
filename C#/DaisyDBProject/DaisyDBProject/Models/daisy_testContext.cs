using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DaisyDBProject.Models
{
    public partial class daisy_testContext : DbContext
    {
        public daisy_testContext()
        {
        }

        public daisy_testContext(DbContextOptions<daisy_testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Discussion> Discussion { get; set; }
        public virtual DbSet<FavouritePackage> FavouritePackage { get; set; }
        public virtual DbSet<Follow> Follow { get; set; }
        public virtual DbSet<LeaveMessage> LeaveMessage { get; set; }
        public virtual DbSet<LiftBan> LiftBan { get; set; }
        public virtual DbSet<LikeDisc> LikeDisc { get; set; }
        public virtual DbSet<LikeMoment> LikeMoment { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Moment> Moment { get; set; }
        public virtual DbSet<MomentStar> MomentStar { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostStar> PostStar { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Reply> Reply { get; set; }
        public virtual DbSet<ReplyReply> ReplyReply { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<ReportCom> ReportCom { get; set; }
        public virtual DbSet<ReportDisc> ReportDisc { get; set; }
        public virtual DbSet<ReportMom> ReportMom { get; set; }
        public virtual DbSet<ReportReply> ReportReply { get; set; }
        public virtual DbSet<Subscribe> Subscribe { get; set; }
        public virtual DbSet<UserNotice> UserNotice { get; set; }
        public virtual DbSet<UserNotifi> UserNotifi { get; set; }
        public virtual DbSet<Usergroups> Usergroups { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("administrator");

                entity.Property(e => e.AdministratorId)
                    .HasColumnName("administrator_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("email_address")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Nickname)
                    .HasColumnName("nickname")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'admin'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PhoneNum)
                    .HasColumnName("phone_num")
                    .HasColumnType("varchar(14)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.GroupId, e.Account })
                    .HasName("PRIMARY");

                entity.ToTable("application");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.HasIndex(e => new { e.GroupId, e.ProjectId })
                    .HasName("group_id");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("application_ibfk_2");

                entity.HasOne(d => d.Usergroups)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => new { d.GroupId, d.ProjectId })
                    .HasConstraintName("application_ibfk_1");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.HasIndex(e => e.MomentId)
                    .HasName("moment_id");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.MomentId)
                    .HasColumnName("moment_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_ibfk_1");

                entity.HasOne(d => d.Moment)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.MomentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("comment_ibfk_2");
            });

            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.HasKey(e => new { e.DiscussionId, e.ProjectId })
                    .HasName("PRIMARY");

                entity.ToTable("discussion");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("project_id");

                entity.Property(e => e.DiscussionId)
                    .HasColumnName("discussion_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PictureUrl)
                    .HasColumnName("picture_url")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Discussion)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("discussion_ibfk_1");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Discussion)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("discussion_ibfk_2");
            });

            modelBuilder.Entity<FavouritePackage>(entity =>
            {
                entity.HasKey(e => new { e.Account, e.Name })
                    .HasName("PRIMARY");

                entity.ToTable("favourite_package");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Privacy)
                    .HasColumnName("privacy")
                    .HasColumnType("varchar(7)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.FavouritePackage)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favourite_package_ibfk_1");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasKey(e => new { e.FollowedAccount, e.Account })
                    .HasName("PRIMARY");

                entity.ToTable("follow");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.Property(e => e.FollowedAccount)
                    .HasColumnName("followed_account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.FollowAccountNavigation)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("follow_ibfk_2");

                entity.HasOne(d => d.FollowedAccountNavigation)
                    .WithMany(p => p.FollowFollowedAccountNavigation)
                    .HasForeignKey(d => d.FollowedAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("follow_ibfk_1");
            });

            modelBuilder.Entity<LeaveMessage>(entity =>
            {
                entity.ToTable("leave_message");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.Property(e => e.LeaveMessageId)
                    .HasColumnName("leave_message_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ReadTag)
                    .HasColumnName("read_tag")
                    .HasColumnType("decimal(1,0)");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.LeaveMessage)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("leave_message_ibfk_1");
            });

            modelBuilder.Entity<LiftBan>(entity =>
            {
                entity.ToTable("lift_ban");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.Property(e => e.LiftBanId)
                    .HasColumnName("lift_ban_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DealStatus)
                    .HasColumnName("deal_status")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.LiftBan)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lift_ban_ibfk_1");
            });

            modelBuilder.Entity<LikeDisc>(entity =>
            {
                entity.HasKey(e => new { e.DiscussionId, e.Account, e.ProjectId })
                    .HasName("PRIMARY");

                entity.ToTable("like_disc");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.HasIndex(e => new { e.DiscussionId, e.ProjectId })
                    .HasName("discussion_id");

                entity.Property(e => e.DiscussionId)
                    .HasColumnName("discussion_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.LikeDisc)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("like_disc_ibfk_2");

                entity.HasOne(d => d.Discussion)
                    .WithMany(p => p.LikeDisc)
                    .HasForeignKey(d => new { d.DiscussionId, d.ProjectId })
                    .HasConstraintName("like_disc_ibfk_1");
            });

            modelBuilder.Entity<LikeMoment>(entity =>
            {
                entity.HasKey(e => new { e.MomentId, e.Account })
                    .HasName("PRIMARY");

                entity.ToTable("like_moment");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.Property(e => e.MomentId)
                    .HasColumnName("moment_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.LikeMoment)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("like_moment_ibfk_2");

                entity.HasOne(d => d.Moment)
                    .WithMany(p => p.LikeMoment)
                    .HasForeignKey(d => d.MomentId)
                    .HasConstraintName("like_moment_ibfk_1");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.GroupId, e.Account })
                    .HasName("PRIMARY");

                entity.ToTable("member");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.HasIndex(e => new { e.GroupId, e.ProjectId })
                    .HasName("group_id");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Member)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("member_ibfk_2");

                entity.HasOne(d => d.Usergroups)
                    .WithMany(p => p.Member)
                    .HasForeignKey(d => new { d.GroupId, d.ProjectId })
                    .HasConstraintName("member_ibfk_1");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.HasIndex(e => e.ReceiveAccount)
                    .HasName("receive_account");

                entity.HasIndex(e => e.SendAccount)
                    .HasName("send_account");

                entity.Property(e => e.MessageId)
                    .HasColumnName("message_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ReadTag)
                    .HasColumnName("read_tag")
                    .HasColumnType("decimal(1,0)");

                entity.Property(e => e.ReceiveAccount)
                    .IsRequired()
                    .HasColumnName("receive_account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.SendAccount)
                    .IsRequired()
                    .HasColumnName("send_account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.ReceiveAccountNavigation)
                    .WithMany(p => p.MessageReceiveAccountNavigation)
                    .HasForeignKey(d => d.ReceiveAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("message_ibfk_2");

                entity.HasOne(d => d.SendAccountNavigation)
                    .WithMany(p => p.MessageSendAccountNavigation)
                    .HasForeignKey(d => d.SendAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("message_ibfk_1");
            });

            modelBuilder.Entity<Moment>(entity =>
            {
                entity.ToTable("moment");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.Property(e => e.MomentId)
                    .HasColumnName("moment_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ContentUrl)
                    .IsRequired()
                    .HasColumnName("content_url")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Moment)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("moment_ibfk_1");
            });

            modelBuilder.Entity<MomentStar>(entity =>
            {
                entity.HasKey(e => new { e.MomentId, e.Account, e.Name })
                    .HasName("PRIMARY");

                entity.ToTable("moment_star");

                entity.HasIndex(e => new { e.Account, e.Name })
                    .HasName("account");

                entity.Property(e => e.MomentId)
                    .HasColumnName("moment_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Moment)
                    .WithMany(p => p.MomentStar)
                    .HasForeignKey(d => d.MomentId)
                    .HasConstraintName("moment_star_ibfk_1");

                entity.HasOne(d => d.FavouritePackage)
                    .WithMany(p => p.MomentStar)
                    .HasForeignKey(d => new { d.Account, d.Name })
                    .HasConstraintName("moment_star_ibfk_2");
            });

            modelBuilder.Entity<Notice>(entity =>
            {
                entity.ToTable("notice");

                entity.Property(e => e.NoticeId)
                    .HasColumnName("notice_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => new { e.NotifiId, e.ProjectId })
                    .HasName("PRIMARY");

                entity.ToTable("notification");

                entity.HasIndex(e => e.AdministratorId)
                    .HasName("administrator_id");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("project_id");

                entity.Property(e => e.NotifiId)
                    .HasColumnName("notifi_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.AdministratorId)
                    .HasColumnName("administrator_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.AdministratorId)
                    .HasConstraintName("notification_ibfk_2");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("notification_ibfk_1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.ProjectId, e.GroupId })
                    .HasName("PRIMARY");

                entity.ToTable("post");

                entity.HasIndex(e => new { e.GroupId, e.ProjectId })
                    .HasName("group_id");

                entity.Property(e => e.PostId)
                    .HasColumnName("post_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CurMemberNum)
                    .HasColumnName("cur_member_num")
                    .HasColumnType("decimal(2,0)");

                entity.Property(e => e.MaxMemberNum)
                    .HasColumnName("max_member_num")
                    .HasColumnType("decimal(2,0)");

                entity.Property(e => e.PostTime)
                    .HasColumnName("post_time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Usergroups)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => new { d.GroupId, d.ProjectId })
                    .HasConstraintName("post_ibfk_1");
            });

            modelBuilder.Entity<PostStar>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.GroupId, e.Account, e.PostId, e.Name })
                    .HasName("PRIMARY");

                entity.ToTable("post_star");

                entity.HasIndex(e => new { e.Account, e.Name })
                    .HasName("account");

                entity.HasIndex(e => new { e.PostId, e.ProjectId, e.GroupId })
                    .HasName("post_id");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PostId)
                    .HasColumnName("post_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.FavouritePackage)
                    .WithMany(p => p.PostStar)
                    .HasForeignKey(d => new { d.Account, d.Name })
                    .HasConstraintName("post_star_ibfk_2");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostStar)
                    .HasForeignKey(d => new { d.PostId, d.ProjectId, d.GroupId })
                    .HasConstraintName("post_star_ibfk_1");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("project");

                entity.HasIndex(e => e.AdministratorId)
                    .HasName("administrator_id");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.AdministratorId)
                    .IsRequired()
                    .HasColumnName("administrator_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Host)
                    .HasColumnName("host")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Introduction)
                    .HasColumnName("introduction")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ParticipantsNumber)
                    .HasColumnName("participants_number")
                    .HasColumnType("decimal(2,0)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("project_ibfk_1");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.ToTable("reply");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.HasIndex(e => e.CommentId)
                    .HasName("comment_id");

                entity.Property(e => e.ReplyId)
                    .HasColumnName("reply_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Reply)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reply_ibfk_1");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Reply)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("reply_ibfk_2");
            });

            modelBuilder.Entity<ReplyReply>(entity =>
            {
                entity.HasKey(e => new { e.IdToReply, e.IdReplied })
                    .HasName("PRIMARY");

                entity.ToTable("reply_reply");

                entity.HasIndex(e => e.IdReplied)
                    .HasName("id_replied");

                entity.Property(e => e.IdToReply)
                    .HasColumnName("id_to_reply")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdReplied)
                    .HasColumnName("id_replied")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.IdRepliedNavigation)
                    .WithMany(p => p.ReplyReplyIdRepliedNavigation)
                    .HasForeignKey(d => d.IdReplied)
                    .HasConstraintName("reply_reply_ibfk_2");

                entity.HasOne(d => d.IdToReplyNavigation)
                    .WithMany(p => p.ReplyReplyIdToReplyNavigation)
                    .HasForeignKey(d => d.IdToReply)
                    .HasConstraintName("reply_reply_ibfk_1");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("report");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.Property(e => e.ReportId)
                    .HasColumnName("report_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DealStatus)
                    .HasColumnName("deal_status")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ReportType)
                    .IsRequired()
                    .HasColumnName("report_type")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("report_ibfk_1");
            });

            modelBuilder.Entity<ReportCom>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.CommentId })
                    .HasName("PRIMARY");

                entity.ToTable("report_com");

                entity.HasIndex(e => e.CommentId)
                    .HasName("comment_id");

                entity.Property(e => e.ReportId)
                    .HasColumnName("report_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.ReportCom)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("report_com_ibfk_2");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportCom)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("report_com_ibfk_1");
            });

            modelBuilder.Entity<ReportDisc>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.ProjectId, e.DiscussionId })
                    .HasName("PRIMARY");

                entity.ToTable("report_disc");

                entity.HasIndex(e => new { e.DiscussionId, e.ProjectId })
                    .HasName("discussion_id");

                entity.Property(e => e.ReportId)
                    .HasColumnName("report_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DiscussionId)
                    .HasColumnName("discussion_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportDisc)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("report_disc_ibfk_1");

                entity.HasOne(d => d.Discussion)
                    .WithMany(p => p.ReportDisc)
                    .HasForeignKey(d => new { d.DiscussionId, d.ProjectId })
                    .HasConstraintName("report_disc_ibfk_2");
            });

            modelBuilder.Entity<ReportMom>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.MomentId })
                    .HasName("PRIMARY");

                entity.ToTable("report_mom");

                entity.HasIndex(e => e.MomentId)
                    .HasName("moment_id");

                entity.Property(e => e.ReportId)
                    .HasColumnName("report_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.MomentId)
                    .HasColumnName("moment_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Moment)
                    .WithMany(p => p.ReportMom)
                    .HasForeignKey(d => d.MomentId)
                    .HasConstraintName("report_mom_ibfk_2");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportMom)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("report_mom_ibfk_1");
            });

            modelBuilder.Entity<ReportReply>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.ReplyId })
                    .HasName("PRIMARY");

                entity.ToTable("report_reply");

                entity.HasIndex(e => e.ReplyId)
                    .HasName("reply_id");

                entity.Property(e => e.ReportId)
                    .HasColumnName("report_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ReplyId)
                    .HasColumnName("reply_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.Reply)
                    .WithMany(p => p.ReportReply)
                    .HasForeignKey(d => d.ReplyId)
                    .HasConstraintName("report_reply_ibfk_2");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportReply)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("report_reply_ibfk_1");
            });

            modelBuilder.Entity<Subscribe>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.Account })
                    .HasName("PRIMARY");

                entity.ToTable("subscribe");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Subscribe)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subscribe_ibfk_2");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Subscribe)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("subscribe_ibfk_1");
            });

            modelBuilder.Entity<UserNotice>(entity =>
            {
                entity.HasKey(e => new { e.NoticeId, e.Account })
                    .HasName("PRIMARY");

                entity.ToTable("user_notice");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.Property(e => e.NoticeId)
                    .HasColumnName("notice_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ReadTag)
                    .HasColumnName("read_tag")
                    .HasColumnType("decimal(1,0)");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.UserNotice)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_notice_ibfk_2");

                entity.HasOne(d => d.Notice)
                    .WithMany(p => p.UserNotice)
                    .HasForeignKey(d => d.NoticeId)
                    .HasConstraintName("user_notice_ibfk_1");
            });

            modelBuilder.Entity<UserNotifi>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.NotifiId, e.Account })
                    .HasName("PRIMARY");

                entity.ToTable("user_notifi");

                entity.HasIndex(e => e.Account)
                    .HasName("account");

                entity.HasIndex(e => new { e.NotifiId, e.ProjectId })
                    .HasName("notifi_id");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.NotifiId)
                    .HasColumnName("notifi_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ReadTag)
                    .HasColumnName("read_tag")
                    .HasColumnType("decimal(1,0)");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.UserNotifi)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_notifi_ibfk_2");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.UserNotifi)
                    .HasForeignKey(d => new { d.NotifiId, d.ProjectId })
                    .HasConstraintName("user_notifi_ibfk_1");
            });

            modelBuilder.Entity<Usergroups>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.ProjectId })
                    .HasName("PRIMARY");

                entity.ToTable("usergroups");

                entity.HasIndex(e => e.LeaderAccount)
                    .HasName("leader_account");

                entity.HasIndex(e => e.ProjectId)
                    .HasName("project_id");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ProjectId)
                    .HasColumnName("project_id")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Introduction)
                    .HasColumnName("introduction")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.LeaderAccount)
                    .IsRequired()
                    .HasColumnName("leader_account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.LeaderAccountNavigation)
                    .WithMany(p => p.Usergroups)
                    .HasForeignKey(d => d.LeaderAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usergroups_ibfk_2");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Usergroups)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("usergroups_ibfk_1");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Account)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.College)
                    .HasColumnName("college")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("email_address")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Grade)
                    .HasColumnName("grade")
                    .HasColumnType("varchar(5)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.IconUrl)
                    .HasColumnName("icon_url")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Intro)
                    .HasColumnName("intro")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasColumnType("decimal(3,0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PhoneNum)
                    .HasColumnName("phone_num")
                    .HasColumnType("varchar(14)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Qq)
                    .HasColumnName("qq")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.School)
                    .HasColumnName("school")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Signature)
                    .HasColumnName("signature")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.StudentNumber)
                    .HasColumnName("student_number")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Wechat)
                    .HasColumnName("wechat")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Weibo)
                    .HasColumnName("weibo")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
