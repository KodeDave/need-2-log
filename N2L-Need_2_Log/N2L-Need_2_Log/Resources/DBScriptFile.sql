BEGIN TRANSACTION;
CREATE TABLE "username" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`value`	TEXT,
	`entry_id`	INTEGER NOT NULL UNIQUE,
	FOREIGN KEY(`entry_id`) REFERENCES entry(id)
);
CREATE TABLE "url" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`value`	TEXT,
	`entry_id`	INTEGER UNIQUE,
	FOREIGN KEY(`entry_id`) REFERENCES entry(id)
);
CREATE TABLE "type" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`name`	TEXT NOT NULL UNIQUE,
	`default_icon`	TEXT NOT NULL DEFAULT 'default.png' UNIQUE,
	`url`	TEXT
	);
	INSERT INTO `type` VALUES (1,'Generic','generic.png',NULL);
	INSERT INTO `type` VALUES (2,'Email','email.png',NULL);
	INSERT INTO `type` VALUES (3,'Gmail','gmail.png','mail.google.com/');
	INSERT INTO `type` VALUES (4,'Outlook','outlook.png','www.outlook.com');
	INSERT INTO `type` VALUES (5,'Facebook','facebook.png','www.facebook.com');
	INSERT INTO `type` VALUES (6,'Instagram','instagram.png','www.instagram.com');
	INSERT INTO `type` VALUES (7,'Twitter','twitter.png','twitter.com');
	INSERT INTO `type` VALUES (8,'Bank Account','baccount.png',NULL);
	INSERT INTO `type` VALUES (9,'Forum','forum.png',NULL);
	INSERT INTO `type` VALUES (10,'E Bay','ebay.png','www.ebay.com');
	INSERT INTO `type` VALUES (11,'Amazon','amazon.png','www.amazon.com');
	INSERT INTO `type` VALUES (12,'LinkedIn','linkedin.png','www.linkedin.com');
	INSERT INTO `type` VALUES (13,'Wordpress','wordpress.png','www.wordpress.com');
	INSERT INTO `type` VALUES (14,'Esse3 Uniurb','uniurb.png','www.uniurb.esse3.cineca.it/auth/Logon.do');
	INSERT INTO `type` VALUES (15,'GitHub','github.png','github.com');
CREATE TABLE "password" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`value`	TEXT,
	`entry_id`	INTEGER NOT NULL UNIQUE,
	FOREIGN KEY(`entry_id`) REFERENCES entry(id)
);
CREATE TABLE "note" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`value`	TEXT,
	`entry_id`	INTEGER NOT NULL UNIQUE,
	FOREIGN KEY(`entry_id`) REFERENCES entry(id)
);
CREATE TABLE "icon" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`entry_id`	INTEGER NOT NULL UNIQUE,
	`name`	TEXT NOT NULL DEFAULT 'default.png',
	FOREIGN KEY(`entry_id`) REFERENCES `entry`(`id`)
);
CREATE TABLE "entry" (
	`id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`name`	TEXT NOT NULL UNIQUE,
	`type_id`	INTEGER NOT NULL DEFAULT 1,
	FOREIGN KEY(`type_id`) REFERENCES `type`(`id`)
);
CREATE VIEW 'Main' AS
	SELECT
		entry.id AS ID, entry.name AS NAME, icon.name AS IMAGE, url.value AS URL
		FROM
			entry, icon, url
		WHERE
			icon.entry_id = entry.id AND url.entry_id = entry.id;
CREATE VIEW DetailedInfo AS
	SELECT
		entry.id AS ID,
		entry.name AS NAME,
		icon.name AS IMAGE,
		note.value as NOTE,
		password.value AS PASSWORD,
		url.value AS URL,
		username.value AS USERNAME,
		entry.type_id AS TYPE_ID
		FROM
			entry, icon, note, password, url, username
		WHERE
			icon.entry_id = entry.id AND
			note.entry_id = entry.id AND
			password.entry_id = entry.id AND
			url.entry_id = entry.id AND
			username.entry_id = entry.id;
COMMIT;
