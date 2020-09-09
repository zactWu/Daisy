CREATE TABLE users(
    account        VARCHAR(20) NOT NULL,
    name           VARCHAR(20) NOT NULL,
    password       VARCHAR(20) NOT NULL,
    nickname       VARCHAR(20) NOT NULL,
    phone_num      VARCHAR(14),
    email_address  VARCHAR(30),
    sex            VARCHAR(6)
        CHECK (sex IN ('male', 'female')),
    school         VARCHAR(50),
    college        VARCHAR(20),
    grade          VARCHAR(5)
        CHECK (grade IN ('one', 'two', 'three', 'four', 'five')),
    student_number VARCHAR(10),
    qq             VARCHAR(15),
    wechat         VARCHAR(20),
    weibo          VARCHAR(20),
    icon_url       VARCHAR(30),
    intro          VARCHAR(100),
    signature      VARCHAR(50),
    status         VARCHAR(6) NOT NULL
        CHECK (status IN ('normal', 'banned')),
    start_time     VARCHAR(20),
    length         NUMERIC(3, 0) CHECK (length > 0),
    PRIMARY KEY  (account)
);

CREATE TABLE administrator(
    administrator_id    VARCHAR(10),
    name                VARCHAR(20) NOT NULL,
    password            VARCHAR(20) NOT NULL,
    phone_num           VARCHAR(14),
    email_address       VARCHAR(30),
    nickname            VARCHAR(20) DEFAULT 'admin',
    PRIMARY KEY  (administrator_id)
);

CREATE TABLE project(
    project_id          VARCHAR(10),
    name                VARCHAR(20) NOT NULL,
    introduction        VARCHAR(100),
    participants_number NUMERIC(2, 0) CHECK (participants_number > 0) DEFAULT 1,
    start_time          VARCHAR(20),
    end_time            VARCHAR(20),
    host                VARCHAR(20),
    PRIMARY KEY (project_id),
);

CREATE TABLE usergroups(
    group_id            VARCHAR(10),
    project_id          VARCHAR(10) NOT NULL,  
    leader_account      VARCHAR(20) NOT NULL,     
    name                VARCHAR(20),
    introduction        VARCHAR(100),
    PRIMARY KEY (group_id, project_id),
    FOREIGN KEY (project_id) REFERENCES project(project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (leader_account) REFERENCES users(account)
);

CREATE TABLE notice(
    notice_id           VARCHAR(10),
    title               VARCHAR(20) NOT NULL,
    time                VARCHAR(20),
    content             VARCHAR(200),
    PRIMARY KEY (notice_id)
);

CREATE TABLE favourite_package(
    account             VARCHAR(20) NOT NULL,
    name                VARCHAR(20) NOT NULL,
    create_time         VARCHAR(20),
    privacy             VARCHAR(7) CHECK (privacy IN ('public', 'private')),
    PRIMARY KEY (account, name),
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE moment(
    moment_id           VARCHAR(10),
    account             VARCHAR(20) NOT NULL,
    title               VARCHAR(20) NOT NULL,
    time                VARCHAR(20),
    content_url         VARCHAR(30) NOT NULL,            
    PRIMARY KEY (moment_id), 
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE comment(
    comment_id           VARCHAR(10),
    moment_id            VARCHAR(10),
    account              VARCHAR(20) NOT NULL,
    time                 VARCHAR(20),
    content              VARCHAR(200),
    PRIMARY KEY (comment_id), 
    FOREIGN KEY (account) REFERENCES users(account),
    FOREIGN KEY (moment_id) REFERENCES moment(moment_id)
          ON DELETE CASCADE
);

CREATE TABLE reply(
    reply_id               VARCHAR(10),
    comment_id             VARCHAR(10),
    account                VARCHAR(20) NOT NULL,
    time                   VARCHAR(20),
    content                VARCHAR(200),
    PRIMARY KEY (reply_id), 
    FOREIGN KEY (account) REFERENCES users(account),
    FOREIGN KEY (comment_id) REFERENCES comment(comment_id)
          ON DELETE CASCADE
);

CREATE TABLE message(
    message_id              VARCHAR(10),
    send_account            VARCHAR(20) NOT NULL,
    receive_account         VARCHAR(20) NOT NULL,
    read_tag                NUMERIC(1, 0) CHECK (read_tag IN(0, 1)),
    time                    VARCHAR(20),
    content                 VARCHAR(200),
    PRIMARY KEY (message_id), 
    FOREIGN KEY (send_account) REFERENCES users(account),
    FOREIGN KEY (receive_account) REFERENCES users(account)
);

CREATE TABLE report(
    report_id               VARCHAR(10),
    account                 VARCHAR(20) NOT NULL,
    report_type             VARCHAR(10) NOT NULL,
    time                    VARCHAR(20),
    content                 VARCHAR(200),
    deal_status             VARCHAR(20) CHECK (deal_status in ('successful', 'failed', 'Unprocessed')),
    PRIMARY KEY (report_id), 
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE leave_message(
    leave_message_id        VARCHAR(10),
    account                 VARCHAR(20) NOT NULL,
    time                    VARCHAR(20),
    content                 VARCHAR(200),
    read_tag                NUMERIC(1, 0) CHECK (read_tag IN(0, 1)),
    PRIMARY KEY (leave_message_id), 
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE notification(
    notifi_id               VARCHAR(10),
    project_id              VARCHAR(10) NOT NULL,
    time                    VARCHAR(20),
    content                 VARCHAR(200),
    title                   VARCHAR(20) NOT NULL,
    PRIMARY KEY (notifi_id, project_id), 
    FOREIGN KEY (project_id) REFERENCES project(project_id)
          ON DELETE CASCADE,
);

CREATE TABLE lift_ban(
    lift_ban_id              VARCHAR(10),
    account                  VARCHAR(20) NOT NULL,
    time                     VARCHAR(20),
    content                  VARCHAR(200),
    deal_status              VARCHAR(20) CHECK (deal_status in ('successful', 'failed', 'Unprocessed')),
    PRIMARY KEY (lift_ban_id), 
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE discussion(
    discussion_id            VARCHAR(10),
    project_id               VARCHAR(10) NOT NULL,
    account                  VARCHAR(20) NOT NULL,
    time                     VARCHAR(20),
    content                  VARCHAR(200) NOT NULL,
    picture_url              VARCHAR(30),
    PRIMARY KEY (discussion_id, project_id), 
    FOREIGN KEY (account)    REFERENCES users(account),
    FOREIGN KEY (project_id) REFERENCES project(project_id)
        ON DELETE CASCADE
);

CREATE TABLE post(
    post_id                   VARCHAR(10),
    project_id                VARCHAR(10) NOT NULL,
    group_id                  VARCHAR(10) NOT NULL,
    post_time                 VARCHAR(20),
    content                   VARCHAR(200) NOT NULL,
    max_member_num            NUMERIC(2, 0) CHECK(max_member_num > 0),
    cur_member_num            NUMERIC(2, 0) CHECK(cur_member_num > 0),
    PRIMARY KEY (post_id, project_id, group_id),
    FOREIGN KEY (group_id, project_id) REFERENCES usergroups(group_id, project_id)
        ON DELETE CASCADE
);

CREATE TABLE user_notice(
    notice_id                  VARCHAR(10),
    account                    VARCHAR(20) NOT NULL,
    read_tag                   NUMERIC(1, 0) CHECK (read_tag IN (0, 1)),
    PRIMARY KEY (notice_id, account),
    FOREIGN KEY (notice_id) REFERENCES notice(notice_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE follow(
    followed_account           VARCHAR(20) NOT NULL,
    account                    VARCHAR(20) NOT NULL,
    PRIMARY KEY (followed_account, account),
    FOREIGN KEY (followed_account) REFERENCES users(account),
    FOREIGN KEY (account) REFERENCES users(account)
);


CREATE TABLE user_notifi(
    project_id                 VARCHAR(10) NOT NULL,
    notifi_id                  VARCHAR(10) NOT NULL,
    account                    VARCHAR(20) NOT NULL,
    read_tag                   NUMERIC(1, 0) CHECK (read_tag IN(0, 1)),
    PRIMARY KEY (project_id, notifi_id, account),
    FOREIGN KEY (notifi_id, project_id) REFERENCES notification(notifi_id, project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE like_disc(
    discussion_id              VARCHAR(10) NOT NULL,
    project_id                 VARCHAR(10) NOT NULL,
    account                    VARCHAR(20) NOT NULL,
    PRIMARY KEY (discussion_id, account, project_id),
    FOREIGN KEY (discussion_id, project_id) REFERENCES 
        discussion(discussion_id, project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE like_moment(
    moment_id                 VARCHAR(10) NOT NULL,
    account                   VARCHAR(20) NOT NULL,
    PRIMARY KEY (moment_id, account),
    FOREIGN KEY (moment_id) REFERENCES moment(moment_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE subscribe(
    project_id                VARCHAR(10) NOT NULL,
    account                   VARCHAR(20) NOT NULL,
    PRIMARY KEY (project_id, account),
    FOREIGN KEY (project_id) REFERENCES project(project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE application(
    project_id                VARCHAR(10) NOT NULL,
    group_id                  VARCHAR(10) NOT NULL,
    account                   VARCHAR(20) NOT NULL,
    status                    VARCHAR(20) CHECK (status in ('successful', 'failed', 'Unprocessed')),
    content                   VARCHAR(200),
    PRIMARY KEY (project_id, group_id, account),
    FOREIGN KEY (group_id, project_id) REFERENCES usergroups(group_id, project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE member(
    project_id                VARCHAR(10) NOT NULL,
    group_id                  VARCHAR(10) NOT NULL,
    account                   VARCHAR(20) NOT NULL,
    PRIMARY KEY (project_id, group_id, account),
    FOREIGN KEY (group_id, project_id) REFERENCES usergroups(group_id, project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE post_star(
    project_id                VARCHAR(10) NOT NULL,
    group_id                  VARCHAR(10) NOT NULL,
    post_id                   VARCHAR(10) NOT NULL,
    account                   VARCHAR(20) NOT NULL,
    name                      VARCHAR(20) NOT NULL,
    PRIMARY KEY (project_id, group_id, account, post_id, name),
    FOREIGN KEY (post_id, project_id, group_id) REFERENCES post(post_id, project_id, group_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account, name) REFERENCES favourite_package(account, name)
        ON DELETE CASCADE
);

CREATE TABLE moment_star(
    moment_id                 VARCHAR(10) NOT NULL,   
    account                   VARCHAR(20) NOT NULL,
    name                      VARCHAR(20) NOT NULL,
    PRIMARY KEY (moment_id, account, name),
    FOREIGN KEY (moment_id) REFERENCES moment(moment_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account, name) REFERENCES favourite_package(account, name)
        ON DELETE CASCADE
);

CREATE TABLE reply_reply(
    id_to_reply                VARCHAR(10) NOT NULL,   
    id_replied                 VARCHAR(10) NOT NULL,
    PRIMARY KEY (id_to_reply, id_replied),
    FOREIGN KEY (id_to_reply)   REFERENCES reply(reply_id)
        ON DELETE CASCADE,
    FOREIGN KEY (id_replied)    REFERENCES reply(reply_id)
        ON DELETE CASCADE
);

CREATE TABLE report_reply(
    report_id                  VARCHAR(10) NOT NULL,   
    reply_id                   VARCHAR(10) NOT NULL,
    PRIMARY KEY (report_id, reply_id),
    FOREIGN KEY (report_id)     REFERENCES report(report_id)
        ON DELETE CASCADE,
    FOREIGN KEY (reply_id)      REFERENCES reply(reply_id)
        ON DELETE CASCADE
);

CREATE TABLE report_com(
    report_id                  VARCHAR(10) NOT NULL,   
    comment_id                 VARCHAR(10) NOT NULL,
    PRIMARY KEY (report_id, comment_id),
    FOREIGN KEY (report_id)     REFERENCES report(report_id)
        ON DELETE CASCADE,
    FOREIGN KEY (comment_id)    REFERENCES comment(comment_id)
        ON DELETE CASCADE
);

CREATE TABLE report_mom(
    report_id                   VARCHAR(10) NOT NULL,   
    moment_id                   VARCHAR(10) NOT NULL,
    PRIMARY KEY (report_id, moment_id),
    FOREIGN KEY (report_id)     REFERENCES report(report_id)
        ON DELETE CASCADE,
    FOREIGN KEY (moment_id)     REFERENCES moment(moment_Id)
        ON DELETE CASCADE
);

CREATE TABLE report_disc(
    report_id                   VARCHAR(10) NOT NULL,   
    project_id                  VARCHAR(10) NOT NULL,
    discussion_id               VARCHAR(10) NOT NULL,
    PRIMARY KEY (report_id,project_id, discussion_id),
    FOREIGN KEY (report_id)     REFERENCES report(report_id)
          ON DELETE CASCADE,
    FOREIGN KEY (discussion_id, project_id) 
        REFERENCES discussion(discussion_id, project_id)
        ON DELETE CASCADE
);

CREATE TABLE group_message(
    group_message_id            VARCHAR(10) NOT NULL,   
    group_id                    VARCHAR(10),
    project_id                  VARCHAR(10) NOT NULL,
    content                     VARCHAR(100) NOT NULL,
    time                        VARCHAR(20),
    PRIMARY KEY (group_message_id),
    FOREIGN KEY (group_id, project_id)     REFERENCES usergroups(group_id, project_id)
        ON DELETE CASCADE
);

CREATE TABLE user_group_message(
    group_message_id            VARCHAR(10) NOT NULL,
    account                     VARCHAR(20) NOT NULL, 
    PRIMARY KEY (group_message_id, account),
    FOREIGN KEY (group_message_id)     
        REFERENCES group_message(group_message_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) 
        REFERENCES users(account)
        ON DELETE CASCADE
);