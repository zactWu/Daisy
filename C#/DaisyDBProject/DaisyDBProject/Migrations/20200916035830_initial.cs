using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace DaisyDBProject.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "administrator",
                columns: table => new
                {
                    administrator_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(20)", nullable: false),
                    password = table.Column<string>(type: "varchar(20)", nullable: false),
                    phone_num = table.Column<string>(type: "varchar(14)", nullable: true),
                    email_address = table.Column<string>(type: "varchar(30)", nullable: true),
                    nickname = table.Column<string>(type: "varchar(20)", nullable: true, defaultValueSql: "'admin'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrator", x => x.administrator_id);
                });

            migrationBuilder.CreateTable(
                name: "notice",
                columns: table => new
                {
                    notice_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(20)", nullable: false),
                    time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notice", x => x.notice_id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    project_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(20)", nullable: false),
                    introduction = table.Column<string>(type: "varchar(100)", nullable: true),
                    participants_number = table.Column<int>(nullable: true, defaultValueSql: "'1'"),
                    start_time = table.Column<string>(type: "varchar(20)", nullable: true),
                    end_time = table.Column<string>(type: "varchar(20)", nullable: true),
                    host = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    name = table.Column<string>(type: "varchar(20)", nullable: false),
                    password = table.Column<string>(type: "varchar(20)", nullable: false),
                    nickname = table.Column<string>(type: "varchar(20)", nullable: false),
                    phone_num = table.Column<string>(type: "varchar(14)", nullable: true),
                    email_address = table.Column<string>(type: "varchar(30)", nullable: true),
                    sex = table.Column<string>(type: "varchar(6)", nullable: true),
                    school = table.Column<string>(type: "varchar(50)", nullable: true),
                    college = table.Column<string>(type: "varchar(20)", nullable: true),
                    grade = table.Column<int>(nullable: true),
                    student_number = table.Column<string>(type: "varchar(10)", nullable: true),
                    icon_url = table.Column<string>(type: "varchar(10000)", nullable: true),
                    intro = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.account);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    notifi_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    project_id = table.Column<int>(nullable: false),
                    time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: true),
                    title = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.notifi_id, x.project_id });
                    table.ForeignKey(
                        name: "notification_ibfk_1",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "discussion",
                columns: table => new
                {
                    discussion_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    project_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: false),
                    picture_url = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.discussion_id, x.project_id });
                    table.ForeignKey(
                        name: "discussion_ibfk_1",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "discussion_ibfk_2",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favourite_package",
                columns: table => new
                {
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    name = table.Column<string>(type: "varchar(20)", nullable: false),
                    create_time = table.Column<string>(type: "varchar(20)", nullable: true),
                    privacy = table.Column<string>(type: "varchar(7)", nullable: true),
                    type = table.Column<string>(type: "varchar(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.account, x.name });
                    table.ForeignKey(
                        name: "favourite_package_ibfk_1",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "follow",
                columns: table => new
                {
                    followed_account = table.Column<string>(type: "varchar(20)", nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.followed_account, x.account });
                    table.ForeignKey(
                        name: "follow_ibfk_2",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "follow_ibfk_1",
                        column: x => x.followed_account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "leave_message",
                columns: table => new
                {
                    leave_message_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: true),
                    read_tag = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_message", x => x.leave_message_id);
                    table.ForeignKey(
                        name: "leave_message_ibfk_1",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lift_ban",
                columns: table => new
                {
                    lift_ban_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: true),
                    deal_status = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lift_ban", x => x.lift_ban_id);
                    table.ForeignKey(
                        name: "lift_ban_ibfk_1",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    message_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    send_account = table.Column<string>(type: "varchar(20)", nullable: false),
                    receive_account = table.Column<string>(type: "varchar(20)", nullable: false),
                    read_tag = table.Column<int>(nullable: true),
                    time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message", x => x.message_id);
                    table.ForeignKey(
                        name: "message_ibfk_2",
                        column: x => x.receive_account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "message_ibfk_1",
                        column: x => x.send_account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "moment",
                columns: table => new
                {
                    moment_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    title = table.Column<string>(type: "varchar(20)", nullable: false),
                    time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moment", x => x.moment_id);
                    table.ForeignKey(
                        name: "moment_ibfk_1",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "report",
                columns: table => new
                {
                    report_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    report_type = table.Column<string>(type: "varchar(10)", nullable: false),
                    time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: true),
                    deal_status = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report", x => x.report_id);
                    table.ForeignKey(
                        name: "report_ibfk_1",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "subscribe",
                columns: table => new
                {
                    project_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.project_id, x.account });
                    table.ForeignKey(
                        name: "subscribe_ibfk_2",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "subscribe_ibfk_1",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_notice",
                columns: table => new
                {
                    notice_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    read_tag = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.notice_id, x.account });
                    table.ForeignKey(
                        name: "user_notice_ibfk_2",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_notice_ibfk_1",
                        column: x => x.notice_id,
                        principalTable: "notice",
                        principalColumn: "notice_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usergroups",
                columns: table => new
                {
                    group_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    project_id = table.Column<int>(nullable: false),
                    leader_account = table.Column<string>(type: "varchar(20)", nullable: false),
                    name = table.Column<string>(type: "varchar(20)", nullable: true),
                    introduction = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.group_id, x.project_id });
                    table.ForeignKey(
                        name: "usergroups_ibfk_2",
                        column: x => x.leader_account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "usergroups_ibfk_1",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_notifi",
                columns: table => new
                {
                    project_id = table.Column<int>(nullable: false),
                    notifi_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    read_tag = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.project_id, x.notifi_id, x.account });
                    table.ForeignKey(
                        name: "user_notifi_ibfk_2",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_notifi_ibfk_1",
                        columns: x => new { x.notifi_id, x.project_id },
                        principalTable: "notification",
                        principalColumns: new[] { "notifi_id", "project_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "like_disc",
                columns: table => new
                {
                    discussion_id = table.Column<int>(nullable: false),
                    project_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.discussion_id, x.account, x.project_id });
                    table.ForeignKey(
                        name: "like_disc_ibfk_2",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "like_disc_ibfk_1",
                        columns: x => new { x.discussion_id, x.project_id },
                        principalTable: "discussion",
                        principalColumns: new[] { "discussion_id", "project_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    comment_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    moment_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.comment_id);
                    table.ForeignKey(
                        name: "comment_ibfk_1",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "comment_ibfk_2",
                        column: x => x.moment_id,
                        principalTable: "moment",
                        principalColumn: "moment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "like_moment",
                columns: table => new
                {
                    moment_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.moment_id, x.account });
                    table.ForeignKey(
                        name: "like_moment_ibfk_2",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "like_moment_ibfk_1",
                        column: x => x.moment_id,
                        principalTable: "moment",
                        principalColumn: "moment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "moment_star",
                columns: table => new
                {
                    moment_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    name = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.moment_id, x.account, x.name });
                    table.ForeignKey(
                        name: "moment_star_ibfk_1",
                        column: x => x.moment_id,
                        principalTable: "moment",
                        principalColumn: "moment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "moment_star_ibfk_2",
                        columns: x => new { x.account, x.name },
                        principalTable: "favourite_package",
                        principalColumns: new[] { "account", "name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_disc",
                columns: table => new
                {
                    report_id = table.Column<int>(nullable: false),
                    project_id = table.Column<int>(nullable: false),
                    discussion_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.report_id, x.project_id, x.discussion_id });
                    table.ForeignKey(
                        name: "report_disc_ibfk_1",
                        column: x => x.report_id,
                        principalTable: "report",
                        principalColumn: "report_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "report_disc_ibfk_2",
                        columns: x => new { x.discussion_id, x.project_id },
                        principalTable: "discussion",
                        principalColumns: new[] { "discussion_id", "project_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_mom",
                columns: table => new
                {
                    report_id = table.Column<int>(nullable: false),
                    moment_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.report_id, x.moment_id });
                    table.ForeignKey(
                        name: "report_mom_ibfk_2",
                        column: x => x.moment_id,
                        principalTable: "moment",
                        principalColumn: "moment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "report_mom_ibfk_1",
                        column: x => x.report_id,
                        principalTable: "report",
                        principalColumn: "report_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application",
                columns: table => new
                {
                    project_id = table.Column<int>(nullable: false),
                    group_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.project_id, x.group_id, x.account });
                    table.ForeignKey(
                        name: "application_ibfk_2",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "application_ibfk_1",
                        columns: x => new { x.group_id, x.project_id },
                        principalTable: "usergroups",
                        principalColumns: new[] { "group_id", "project_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_message",
                columns: table => new
                {
                    group_message_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    group_id = table.Column<int>(nullable: false),
                    project_id = table.Column<int>(nullable: false),
                    content = table.Column<string>(type: "varchar(100)", nullable: false),
                    time = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_message", x => x.group_message_id);
                    table.ForeignKey(
                        name: "group_message_ibfk_1",
                        columns: x => new { x.group_id, x.project_id },
                        principalTable: "usergroups",
                        principalColumns: new[] { "group_id", "project_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "member",
                columns: table => new
                {
                    project_id = table.Column<int>(nullable: false),
                    group_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.project_id, x.group_id, x.account });
                    table.ForeignKey(
                        name: "member_ibfk_2",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "member_ibfk_1",
                        columns: x => new { x.group_id, x.project_id },
                        principalTable: "usergroups",
                        principalColumns: new[] { "group_id", "project_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    post_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    project_id = table.Column<int>(nullable: false),
                    group_id = table.Column<int>(nullable: false),
                    post_time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: false),
                    max_member_num = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.post_id, x.project_id, x.group_id });
                    table.ForeignKey(
                        name: "post_ibfk_1",
                        columns: x => new { x.group_id, x.project_id },
                        principalTable: "usergroups",
                        principalColumns: new[] { "group_id", "project_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reply",
                columns: table => new
                {
                    reply_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    comment_id = table.Column<int>(nullable: true),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    time = table.Column<string>(type: "varchar(20)", nullable: true),
                    content = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reply", x => x.reply_id);
                    table.ForeignKey(
                        name: "reply_ibfk_1",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "reply_ibfk_2",
                        column: x => x.comment_id,
                        principalTable: "comment",
                        principalColumn: "comment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_com",
                columns: table => new
                {
                    report_id = table.Column<int>(nullable: false),
                    comment_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.report_id, x.comment_id });
                    table.ForeignKey(
                        name: "report_com_ibfk_2",
                        column: x => x.comment_id,
                        principalTable: "comment",
                        principalColumn: "comment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "report_com_ibfk_1",
                        column: x => x.report_id,
                        principalTable: "report",
                        principalColumn: "report_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_group_message",
                columns: table => new
                {
                    group_message_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    account = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.group_message_id, x.account });
                    table.ForeignKey(
                        name: "user_group_message_ibfk_2",
                        column: x => x.account,
                        principalTable: "users",
                        principalColumn: "account",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "user_group_message_ibfk_1",
                        column: x => x.group_message_id,
                        principalTable: "group_message",
                        principalColumn: "group_message_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_star",
                columns: table => new
                {
                    project_id = table.Column<int>(nullable: false),
                    group_id = table.Column<int>(nullable: false),
                    post_id = table.Column<int>(nullable: false),
                    account = table.Column<string>(type: "varchar(20)", nullable: false),
                    name = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.project_id, x.group_id, x.account, x.post_id, x.name });
                    table.ForeignKey(
                        name: "post_star_ibfk_2",
                        columns: x => new { x.account, x.name },
                        principalTable: "favourite_package",
                        principalColumns: new[] { "account", "name" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "post_star_ibfk_1",
                        columns: x => new { x.post_id, x.project_id, x.group_id },
                        principalTable: "post",
                        principalColumns: new[] { "post_id", "project_id", "group_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reply_reply",
                columns: table => new
                {
                    id_to_reply = table.Column<int>(nullable: false),
                    id_replied = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id_to_reply, x.id_replied });
                    table.ForeignKey(
                        name: "reply_reply_ibfk_2",
                        column: x => x.id_replied,
                        principalTable: "reply",
                        principalColumn: "reply_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "reply_reply_ibfk_1",
                        column: x => x.id_to_reply,
                        principalTable: "reply",
                        principalColumn: "reply_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_reply",
                columns: table => new
                {
                    report_id = table.Column<int>(nullable: false),
                    reply_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.report_id, x.reply_id });
                    table.ForeignKey(
                        name: "report_reply_ibfk_2",
                        column: x => x.reply_id,
                        principalTable: "reply",
                        principalColumn: "reply_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "report_reply_ibfk_1",
                        column: x => x.report_id,
                        principalTable: "report",
                        principalColumn: "report_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "account",
                table: "application",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "group_id",
                table: "application",
                columns: new[] { "group_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "account",
                table: "comment",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "moment_id",
                table: "comment",
                column: "moment_id");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "discussion",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "project_id",
                table: "discussion",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "follow",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "group_id",
                table: "group_message",
                columns: new[] { "group_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "account",
                table: "leave_message",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "lift_ban",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "like_disc",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "discussion_id",
                table: "like_disc",
                columns: new[] { "discussion_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "account",
                table: "like_moment",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "member",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "group_id",
                table: "member",
                columns: new[] { "group_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "receive_account",
                table: "message",
                column: "receive_account");

            migrationBuilder.CreateIndex(
                name: "send_account",
                table: "message",
                column: "send_account");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "moment",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "moment_star",
                columns: new[] { "account", "name" });

            migrationBuilder.CreateIndex(
                name: "project_id",
                table: "notification",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "group_id",
                table: "post",
                columns: new[] { "group_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "account",
                table: "post_star",
                columns: new[] { "account", "name" });

            migrationBuilder.CreateIndex(
                name: "post_id",
                table: "post_star",
                columns: new[] { "post_id", "project_id", "group_id" });

            migrationBuilder.CreateIndex(
                name: "account",
                table: "reply",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "comment_id",
                table: "reply",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "id_replied",
                table: "reply_reply",
                column: "id_replied");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "report",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "comment_id",
                table: "report_com",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "discussion_id",
                table: "report_disc",
                columns: new[] { "discussion_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "moment_id",
                table: "report_mom",
                column: "moment_id");

            migrationBuilder.CreateIndex(
                name: "reply_id",
                table: "report_reply",
                column: "reply_id");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "subscribe",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "user_group_message",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "user_notice",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "account",
                table: "user_notifi",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "notifi_id",
                table: "user_notifi",
                columns: new[] { "notifi_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "leader_account",
                table: "usergroups",
                column: "leader_account");

            migrationBuilder.CreateIndex(
                name: "project_id",
                table: "usergroups",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrator");

            migrationBuilder.DropTable(
                name: "application");

            migrationBuilder.DropTable(
                name: "follow");

            migrationBuilder.DropTable(
                name: "leave_message");

            migrationBuilder.DropTable(
                name: "lift_ban");

            migrationBuilder.DropTable(
                name: "like_disc");

            migrationBuilder.DropTable(
                name: "like_moment");

            migrationBuilder.DropTable(
                name: "member");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "moment_star");

            migrationBuilder.DropTable(
                name: "post_star");

            migrationBuilder.DropTable(
                name: "reply_reply");

            migrationBuilder.DropTable(
                name: "report_com");

            migrationBuilder.DropTable(
                name: "report_disc");

            migrationBuilder.DropTable(
                name: "report_mom");

            migrationBuilder.DropTable(
                name: "report_reply");

            migrationBuilder.DropTable(
                name: "subscribe");

            migrationBuilder.DropTable(
                name: "user_group_message");

            migrationBuilder.DropTable(
                name: "user_notice");

            migrationBuilder.DropTable(
                name: "user_notifi");

            migrationBuilder.DropTable(
                name: "favourite_package");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "discussion");

            migrationBuilder.DropTable(
                name: "reply");

            migrationBuilder.DropTable(
                name: "report");

            migrationBuilder.DropTable(
                name: "group_message");

            migrationBuilder.DropTable(
                name: "notice");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "usergroups");

            migrationBuilder.DropTable(
                name: "moment");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
