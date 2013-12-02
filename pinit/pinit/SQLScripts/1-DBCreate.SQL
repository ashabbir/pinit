/***********************
** This Is DB Create Script
** Last Part of the Script will Create Full Text Catalog and Index for Contains and FreeText Searches
***********************/


use Master
go

if exists(select * from sys.databases where name = 'Pinit')
    DROP database Pinit
go

Create database Pinit
go


use Pinit

go


Create table UserInfo (
UserName  varchar(100) primary key,
FirstName varchar(100) not null,
LastName varchar(100) not null,
Email varchar(255) not null,
[Password] varchar(100) not null,
DateRegistered DateTime not null default getdate()
)


Create Table Friend (
SourceUser varchar(100) not null,
TargetUser varchar(100) not null,
RequestStatus varchar (9) not null default('requested'),
DateRequested DateTime not null default getdate(),
DateModified DateTime ,
primary key (SourceUser,TargetUser),
Foreign Key (SourceUser) references UserInfo(UserName),
Foreign Key (TargetUser) references UserInfo(UserName)
)


Create Table FollowStream(
StreamId int IDENTITY(1,1) NOT NULL, -- THIS Makes It Auto Increment
StreamName varchar(100) not null,
UserName varchar(100) not null,
DateCreated DateTime not null default getdate(),
CONSTRAINT [PK_FollowStream] PRIMARY KEY CLUSTERED ([StreamId] ASC), -- This makes it a primary key
Foreign Key (UserName) references UserInfo(UserName)
)

Create Table Board (
BoardId int IDENTITY(1,1) NOT NULL, -- THIS Makes It Auto Increment
BoardName varchar(100) not null,
BoardOwner varchar(100) not null,
DateCreated DateTime not null default getdate(),
PrivateComments bit not null default 0
CONSTRAINT [PK_Board] PRIMARY KEY CLUSTERED ([BoardId] ASC), -- This makes it a primary key
Foreign Key (BoardOwner) references UserInfo(UserName)
)




Create Table BoardFollow(
StreamId int not null,
BoardId int not null,
DateCreated DateTime not null default getdate(),
primary key (StreamId, BoardId),
Foreign Key (StreamId) references FollowStream(StreamId),
Foreign Key (BoardId) references Board(BoardId)
)

Create Table Picture (
PictureId int IDENTITY(1,1) NOT NULL, -- THIS Makes It Auto Increment
ImageUrl varchar(255) not null,
DateUploaded DateTime not null default getdate(),
CONSTRAINT [PK_Picture] PRIMARY KEY CLUSTERED ([PictureId] ASC)
)


Create Table Pin(
PinId int IDENTITY(1,1) NOT NULL, -- THIS Makes It Auto Increment
PictureId int not null,
BoardId int not null,
DateCreated DateTime not null default getdate(),
CONSTRAINT [PK_Pin] PRIMARY KEY CLUSTERED ([PinId] ASC),
Foreign Key (PictureId) references Picture(PictureId),
Foreign Key (BoardId) references Board(BoardId),
)


Create Table Tag (
TagId int IDENTITY(1,1) NOT NULL, -- THIS Makes It Auto Increment
TagName varchar(100) not null,
CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ([TagId] ASC),
)

Create Table PinTag (
PinId int not null,
TagId int not null,
DateTaged DateTime not null default getdate(),
Primary Key (PinId,TagId),
Foreign Key (PinId) references Pin(PinId),
Foreign Key (TagId) references Tag(TagId)
)

Create Table Repin(
PinId int not null,
TargetPinId int not null,
Foreign Key (PinId) references Pin(PinId),
Foreign Key (TargetPinId) references Pin(PinId),
Unique (PinId, TargetPinId)
)

Create Table Comment(
CommentId int IDENTITY(1,1) NOT NULL, -- This makes it auto increment 
UserName varchar(100) not null, 
PinId int not null,
CommentText text not null,
DateCommented Datetime not null Default GetDate() ,
CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([CommentId] ASC), --This Gives it the Key
Foreign Key (UserName) references UserInfo(UserName),
Foreign Key (PinId) references Pin(PinId)
)

Create Table UserLike(
UserName varchar(100) not null,
PinId int not null,
DateLiked Datetime not null Default GetDate() ,
Unique (UserName,PinId),
Foreign Key (UserName) references userinfo (UserName),
Foreign Key (PinId) references Pin (PinId),

)

--/*
--Create Full Text Catalog -- File Group
-- this will not work on Express version of SQL it will only work on paid Version
---- Add file to filegroup
---- Add catalog
---- Add Index
--*/
--ALTER DATABASE PINIT
--ADD FILEGROUP FG_FULLINDEX
--GO


--ALTER DATABASE PINIT
--ADD FILE
--(
--NAME = FG_FULLINDEX,
--FILENAME = 'C:\fg_fullindex.mdf',
--SIZE = 100MB,
--MAXSIZE = 200MB,
--FILEGROWTH = 20MB
--) TO FILEGROUP FG_FULLINDEX;
--GO


--CREATE FULLTEXT CATALOG FT_TAGS ON FILEGROUP FG_FULLINDEX AS DEFAULT;

--CREATE FULLTEXT INDEX ON [dbo].[Tag](TagName)
--KEY INDEX [PK_Tag] ON FT_TAGS
--WITH CHANGE_TRACKING AUTO



use master


