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
    grade          INTEGER,
    student_number VARCHAR(10),
    icon_url       VARCHAR(30),
    intro          VARCHAR(100),
    PRIMARY KEY  (account)
);

CREATE TABLE administrator(
    administrator_id    INTEGER,
    name                VARCHAR(20) NOT NULL,
    password            VARCHAR(20) NOT NULL,
    phone_num           VARCHAR(14),
    email_address       VARCHAR(30),
    nickname            VARCHAR(20) DEFAULT 'admin',
    PRIMARY KEY  (administrator_id)
);

CREATE TABLE project(
    project_id          INTEGER,
    name                VARCHAR(20) NOT NULL,
    introduction        VARCHAR(100),
    participants_number INTEGER CHECK (participants_number > 0) DEFAULT 1,
    start_time          VARCHAR(20),
    end_time            VARCHAR(20),
    host                VARCHAR(20),
    PRIMARY KEY (project_id)
);

CREATE TABLE usergroups(
    group_id            INTEGER,
    project_id          INTEGER NOT NULL,  
    leader_account      VARCHAR(20) NOT NULL,     
    name                VARCHAR(20),
    introduction        VARCHAR(100),
    PRIMARY KEY (group_id, project_id),
    FOREIGN KEY (project_id) REFERENCES project(project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (leader_account) REFERENCES users(account)
);

CREATE TABLE notice(
    notice_id           INTEGER,
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
    type                VARCHAR(6) CHECK (type IN ('post', 'moment')),
    PRIMARY KEY (account, name),
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE moment(
    moment_id           INTEGER,
    account             VARCHAR(20) NOT NULL,
    title               VARCHAR(20) NOT NULL,
    time                VARCHAR(20),
    content             VARCHAR(30) NOT NULL,            
    PRIMARY KEY (moment_id), 
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE comment(
    comment_id           INTEGER,
    moment_id            INTEGER,
    account              VARCHAR(20) NOT NULL,
    time                 VARCHAR(20),
    content              VARCHAR(200),
    PRIMARY KEY (comment_id), 
    FOREIGN KEY (account) REFERENCES users(account),
    FOREIGN KEY (moment_id) REFERENCES moment(moment_id)
          ON DELETE CASCADE
);

CREATE TABLE reply(
    reply_id               INTEGER,
    comment_id             INTEGER,
    account                VARCHAR(20) NOT NULL,
    time                   VARCHAR(20),
    content                VARCHAR(200),
    PRIMARY KEY (reply_id), 
    FOREIGN KEY (account) REFERENCES users(account),
    FOREIGN KEY (comment_id) REFERENCES comment(comment_id)
          ON DELETE CASCADE
);

CREATE TABLE message(
    message_id              INTEGER,
    send_account            VARCHAR(20) NOT NULL,
    receive_account         VARCHAR(20) NOT NULL,
    read_tag                INTEGER CHECK (read_tag IN(0, 1)),
    time                    VARCHAR(20),
    content                 VARCHAR(200),
    PRIMARY KEY (message_id), 
    FOREIGN KEY (send_account) REFERENCES users(account),
    FOREIGN KEY (receive_account) REFERENCES users(account)
);

CREATE TABLE report(
    report_id               INTEGER,
    account                 VARCHAR(20) NOT NULL,
    report_type             VARCHAR(10) NOT NULL,
    time                    VARCHAR(20),
    content                 VARCHAR(200),
    deal_status             VARCHAR(20) CHECK (deal_status in ('successful', 'failed', 'Unprocessed')),
    PRIMARY KEY (report_id), 
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE leave_message(
    leave_message_id        INTEGER,
    account                 VARCHAR(20) NOT NULL,
    time                    VARCHAR(20),
    content                 VARCHAR(200),
    read_tag                INTEGER CHECK (read_tag IN(0, 1)),
    PRIMARY KEY (leave_message_id), 
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE notification(
    notifi_id               INTEGER,
    project_id              INTEGER NOT NULL,
    time                    VARCHAR(20),
    content                 VARCHAR(200),
    title                   VARCHAR(20) NOT NULL,
    PRIMARY KEY (notifi_id, project_id), 
    FOREIGN KEY (project_id) REFERENCES project(project_id)
          ON DELETE CASCADE
);

CREATE TABLE lift_ban(
    lift_ban_id              INTEGER,
    account                  VARCHAR(20) NOT NULL,
    time                     VARCHAR(20),
    content                  VARCHAR(200),
    deal_status              VARCHAR(20) CHECK (deal_status in ('successful', 'failed', 'Unprocessed')),
    PRIMARY KEY (lift_ban_id), 
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE discussion(
    discussion_id            INTEGER,
    project_id               INTEGER NOT NULL,
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
    post_id                   INTEGER,
    project_id                INTEGER NOT NULL,
    group_id                  INTEGER NOT NULL,
    post_time                 VARCHAR(20),
    content                   VARCHAR(200) NOT NULL,
    max_member_num            INTEGER CHECK(max_member_num > 0),
    cur_member_num            INTEGER CHECK(cur_member_num > 0),
    PRIMARY KEY (post_id, project_id, group_id),
    FOREIGN KEY (group_id, project_id) REFERENCES usergroups(group_id, project_id)
        ON DELETE CASCADE
);

CREATE TABLE user_notice(
    notice_id                  INTEGER,
    account                    VARCHAR(20) NOT NULL,
    read_tag                   INTEGER CHECK (read_tag IN (0, 1)),
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
    project_id                 INTEGER NOT NULL,
    notifi_id                  INTEGER NOT NULL,
    account                    VARCHAR(20) NOT NULL,
    read_tag                   INTEGER CHECK (read_tag IN(0, 1)),
    PRIMARY KEY (project_id, notifi_id, account),
    FOREIGN KEY (notifi_id, project_id) REFERENCES notification(notifi_id, project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE like_disc(
    discussion_id              INTEGER NOT NULL,
    project_id                 INTEGER NOT NULL,
    account                    VARCHAR(20) NOT NULL,
    PRIMARY KEY (discussion_id, account, project_id),
    FOREIGN KEY (discussion_id, project_id) REFERENCES 
        discussion(discussion_id, project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE like_moment(
    moment_id                 INTEGER NOT NULL,
    account                   VARCHAR(20) NOT NULL,
    PRIMARY KEY (moment_id, account),
    FOREIGN KEY (moment_id) REFERENCES moment(moment_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE subscribe(
    project_id                INTEGER NOT NULL,
    account                   VARCHAR(20) NOT NULL,
    PRIMARY KEY (project_id, account),
    FOREIGN KEY (project_id) REFERENCES project(project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE application(
    project_id                INTEGER NOT NULL,
    group_id                  INTEGER NOT NULL,
    account                   VARCHAR(20) NOT NULL,
    status                    VARCHAR(20) CHECK (status in ('successful', 'failed', 'Unprocessed')),
    content                   VARCHAR(200),
    PRIMARY KEY (project_id, group_id, account),
    FOREIGN KEY (group_id, project_id) REFERENCES usergroups(group_id, project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE member(
    project_id                INTEGER NOT NULL,
    group_id                  INTEGER NOT NULL,
    account                   VARCHAR(20) NOT NULL,
    PRIMARY KEY (project_id, group_id, account),
    FOREIGN KEY (group_id, project_id) REFERENCES usergroups(group_id, project_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) REFERENCES users(account)
);

CREATE TABLE post_star(
    project_id                INTEGER NOT NULL,
    group_id                  INTEGER NOT NULL,
    post_id                   INTEGER NOT NULL,
    account                   VARCHAR(20) NOT NULL,
    name                      VARCHAR(20) NOT NULL,
    PRIMARY KEY (project_id, group_id, account, post_id, name),
    FOREIGN KEY (post_id, project_id, group_id) REFERENCES post(post_id, project_id, group_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account, name) REFERENCES favourite_package(account, name)
        ON DELETE CASCADE
);

CREATE TABLE moment_star(
    moment_id                 INTEGER NOT NULL,   
    account                   VARCHAR(20) NOT NULL,
    name                      VARCHAR(20) NOT NULL,
    PRIMARY KEY (moment_id, account, name),
    FOREIGN KEY (moment_id) REFERENCES moment(moment_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account, name) REFERENCES favourite_package(account, name)
        ON DELETE CASCADE
);

CREATE TABLE reply_reply(
    id_to_reply                INTEGER NOT NULL,   
    id_replied                 INTEGER NOT NULL,
    PRIMARY KEY (id_to_reply, id_replied),
    FOREIGN KEY (id_to_reply)   REFERENCES reply(reply_id)
        ON DELETE CASCADE,
    FOREIGN KEY (id_replied)    REFERENCES reply(reply_id)
        ON DELETE CASCADE
);

CREATE TABLE report_reply(
    report_id                  INTEGER NOT NULL,   
    reply_id                   INTEGER NOT NULL,
    PRIMARY KEY (report_id, reply_id),
    FOREIGN KEY (report_id)     REFERENCES report(report_id)
        ON DELETE CASCADE,
    FOREIGN KEY (reply_id)      REFERENCES reply(reply_id)
        ON DELETE CASCADE
);

CREATE TABLE report_com(
    report_id                  INTEGER NOT NULL,   
    comment_id                 INTEGER NOT NULL,
    PRIMARY KEY (report_id, comment_id),
    FOREIGN KEY (report_id)     REFERENCES report(report_id)
        ON DELETE CASCADE,
    FOREIGN KEY (comment_id)    REFERENCES comment(comment_id)
        ON DELETE CASCADE
);

CREATE TABLE report_mom(
    report_id                   INTEGER NOT NULL,   
    moment_id                   INTEGER NOT NULL,
    PRIMARY KEY (report_id, moment_id),
    FOREIGN KEY (report_id)     REFERENCES report(report_id)
        ON DELETE CASCADE,
    FOREIGN KEY (moment_id)     REFERENCES moment(moment_Id)
        ON DELETE CASCADE
);

CREATE TABLE report_disc(
    report_id                   INTEGER NOT NULL,   
    project_id                  INTEGER NOT NULL,
    discussion_id               INTEGER NOT NULL,
    PRIMARY KEY (report_id,project_id, discussion_id),
    FOREIGN KEY (report_id)     REFERENCES report(report_id)
          ON DELETE CASCADE,
    FOREIGN KEY (discussion_id, project_id) 
        REFERENCES discussion(discussion_id, project_id)
        ON DELETE CASCADE
);

CREATE TABLE group_message(
    group_message_id            INTEGER NOT NULL,   
    group_id                    INTEGER,
    project_id                  INTEGER NOT NULL,
    content                     VARCHAR(100) NOT NULL,
    time                        VARCHAR(20),
    PRIMARY KEY (group_message_id),
    FOREIGN KEY (group_id, project_id)     REFERENCES usergroups(group_id, project_id)
        ON DELETE CASCADE
);

CREATE TABLE user_group_message(
    group_message_id            INTEGER NOT NULL,
    account                     VARCHAR(20) NOT NULL, 
    PRIMARY KEY (group_message_id, account),
    FOREIGN KEY (group_message_id)     
        REFERENCES group_message(group_message_id)
        ON DELETE CASCADE,
    FOREIGN KEY (account) 
        REFERENCES users(account)
        ON DELETE CASCADE
);