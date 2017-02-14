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
INSERT INTO `type` VALUES (3,'Gmail','gmail.png','mail.google.com');
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
COMMIT;