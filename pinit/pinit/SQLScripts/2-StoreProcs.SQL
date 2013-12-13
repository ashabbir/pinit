use Pinit
go


-- ================================================
-- Author:		Ahmed
-- Create date: 11/12/2013
-- Description:	Created new user account is called by the web app, 
-- will return a tuple having success (bit) and message (varchar)
-- if success there will be no message and success bit will be 1 else success bit will be 0 and there will be error message
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_SignUp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_SignUp]
GO


Create PROC [dbo].[UP_SignUp] (
	@username varchar(100),
	@password varchar(100),
	@FirstName varchar(100),
	@LastName varchar(100),
	@Email varchar(255) ) 
AS

	IF EXISTS (Select * from UserInfo Where UserName = @UserName )
	BEGIN
		Select CAST(0 as Bit) as Success, 'User Already Exist' as msg
		Return 
	END
	
	BEGIN TRANSACTION
		BEGIN TRY
		Insert into Userinfo (UserName , FirstName , Email , LastName ,[Password] ) 
		values (@username , @FirstName , @email , @LastName , @password)
		COMMIT TRANSACTION
		Select CAST(1 as Bit) as Success, '' as msg
	END TRY
	BEGIN CATCH
	    ROLLBACK TRANSACTION
	    DECLARE @Msg NVARCHAR(MAX)  
        SELECT @Msg='User Could not be created '+ ERROR_MESSAGE() 
	    Select CAST(0 as Bit) as Success, @Msg as msg
	END CATCH

Go



-- ================================================
-- Author:		Ahmed
-- Create date: 11/19/2013
-- Description: this will allow the user to login to the system
-- will return a tuple having success (bit) and message (varchar)
-- if success there will be no message and success bit will be 1 else success bit will be 0 and there will be error message
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_Login]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_Login]
GO


Create PROC [dbo].[UP_Login] (
	@username varchar(100),
	@password varchar(100) ) 
AS
	--COLLATE CASE SENSITIVE We want password case to match but not the username case
	IF EXISTS (Select * from UserInfo Where UserName = @UserName and [password] COLLATE Latin1_General_CS_AS =  @password )
		BEGIN
			Select CAST(1 as Bit) as Success, '' as msg
		Return 
		END
		Select CAST(0 as Bit) as Success, 'incorrect username or password' as msg
Go





-- ================================================
-- Author:		Ahmed
-- Create date: 11/19/2013
-- Description: this will allow the user to update their profile
-- will return a tuple having success (bit) and message (varchar)
-- if success there will be no message and success bit will be 1 else success bit will be 0 and there will be error message
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_UpdateProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_UpdateProfile]
GO


Create PROC [dbo].[UP_UpdateProfile] (
	@username varchar(100),
	@newPassword varchar(100),
	@newFirstName varchar(100), 
	@newLastName varchar(100), 
	@newEmail varchar(255)) 
AS
BEGIN TRANSACTION
	BEGIN TRY
		Update UserInfo
		Set [password] = @newPassword, LastName = @newLastName , FirstName = @newFirstName , email = @newEmail
		where username = @username
		COMMIT TRANSACTION
		Select CAST(1 as Bit) as Success, '' as msg
	END TRY
	BEGIN CATCH
	    ROLLBACK TRANSACTION
	    DECLARE @Msg NVARCHAR(MAX)  
        SELECT @Msg='User profile could not be updated '+ ERROR_MESSAGE() 
	    Select CAST(0 as Bit) as Success, @Msg as msg
	END CATCH

Go



-- ================================================
-- Author:		Ahmed
-- Create date: 11/12/2013
-- Description:Send a Friend Request to an other User
-- will return a tuple having success (bit) and message (varchar)
-- if success there will be no message and success bit will be 1 else success bit will be 0 and there will be error message
-- =============================================

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_SendFriendShipRequest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_SendFriendShipRequest]
GO


Create PROC [dbo].[UP_SendFriendShipRequest] (
@FromUser varchar(100),
@ToUser varchar(100))
AS

Declare @status as varchar(9)

Select @status = requeststatus from Friend Where  
				(SourceUser = @FromUser and TargetUser = @ToUser ) OR
				(SourceUser = @ToUser and TargetUser = @FromUser )


IF @status is null or @status = ''
BEGIN
	Insert into Friend (SourceUser , TargetUser ) values (@FromUser , @ToUser)	
	Select CAST(1 as Bit) as Success, '' as msg
	return
END

Select CAST(0 as Bit) as Success, 'A friend ship request is already in ' + @status + ' status' as msg
Return 

Go





-- ================================================
-- Author:		Ahmed
-- Create date: 11/12/2013
-- Description: Accpet / Reject a FriendShip Request
-- will return a tuple having success (bit) and message (varchar)
-- if success there will be no message and success bit will be 1 else success bit will be 0 and there will be error message
-- =============================================

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_AcceptOrRejectFriendShipRequest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_AcceptOrRejectFriendShipRequest]
GO


Create PROC [dbo].[UP_AcceptOrRejectFriendShipRequest] (
@FromUser varchar(100),
@ToUser varchar(100), 
@accepted bit = 1 )
AS

Declare @status as varchar(9)

Select @status = requeststatus from Friend Where  
				(SourceUser = @FromUser and TargetUser = @ToUser ) 


IF @status is null or @status = ''
	BEGIN
	Select CAST(0 as Bit) as Success, 'Request does not exist' as msg
	return
	END

IF @status = 'requested'
	    BEGIN
		declare @NewStatus varchar(9)
		
		IF( @accepted  = 1) 
		BEGIN
			SET @NewStatus = 'Accepted' 
		END
		ELSE 
			SET @NewStatus = 'Rejected' 
		
	
		Update Friend  
		set RequestStatus = @NewStatus , DateModified = getdate()
		where SourceUser = @FromUser and TargetUser = @ToUser 
	
		Select CAST(1 as Bit) as Success, '' as msg
		return
		END

Select CAST(0 as Bit) as Success, 'Request can not be modified as its already ' + @status  as msg
Return 

Go





-- ================================================
-- Author:		Ahmed
-- Create date: 11/12/2013
-- Description: Performs a key word search over tage
-- =============================================

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_KeyWordSearchOnTags]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_KeyWordSearchOnTags]
GO


Create PROC [dbo].[UP_KeyWordSearchOnTags] (
@KeyWork varchar(255))
AS
	--select pinid From tag Join PinTag on tag.tagid = PinTag.tagid  where Contains(tag.tagname , @KeyWork)
	--Just a temp to make it work on Express Version When deploying will Move it to Contains
	select pinid From tag Join PinTag on tag.tagid = PinTag.tagid  where tag.tagname like '%' + @KeyWork + '%'
Go






-- ================================================
-- Author:		Jason
-- Create date: 11/19/2013
-- Description:	Create Board
-- 1. Create Board if not already exist
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_New_Board]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_New_Board]
GO


Create PROC [dbo].[UP_New_Board] (
@boardname varchar(100),
@owner varchar(100),
@privatecomments bit = 0)
AS

IF  EXISTS (Select * from Board Where boardname = @boardname and BoardOwner = @owner)
	BEGIN
	select CAST(0 as bit) as Success, 'Board already exists.' as msg
	return 
	END

BEGIN TRANSACTION
		BEGIN TRY
			Insert into Board(boardname , BoardOwner , privatecomments ) 
			values (@boardname , @owner , @privatecomments)
			COMMIT TRANSACTION
			Select CAST(1 as Bit) as Success, '' as msg
		END TRY
			BEGIN CATCH
			ROLLBACK TRANSACTION
			DECLARE @Msg NVARCHAR(MAX)  
			SELECT @Msg='board could not be created '+ ERROR_MESSAGE() 
			Select CAST(0 as Bit) as Success, @Msg as msg
		END CATCH
Go









-- ================================================
-- Author:		Jason
-- Create date: 11/19/2013
-- Description:	Pinning
-- 1. Insert Picture if not already exist
-- 2. Insert Pin into board if not already exist (if same boardid and same pictureid then it exists)
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_Pin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_Pin]
GO


Create PROC [dbo].[UP_Pin] (
@imgurl varchar(255),
@boardid int)
AS

IF NOT EXISTS (Select * from Picture Where imageUrl = @imgurl)
BEGIN
	Insert into Picture(imageUrl) 
	values (@imgurl)
END

Declare @pictureid as int

Select @pictureid = pictureid from Picture Where imageUrl = @imgurl

IF NOT EXISTS (Select * from Pin Where pictureid = @pictureid AND boardid = @boardid)
BEGIN
	Insert into Pin(pictureid, boardid) 
	values (@pictureid, @boardid)
	Select CAST(1 as Bit) as Success, '' as msg
	return
END
Select CAST(0 as Bit) as Success, 'User already has the image pined to this board'   as msg
return
Go




-- ================================================
-- Author:		Jason
-- Create date: 11/19/2013
-- Description:	Delete Pin
-- 1. Delete Pin if exists
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_Delete_Pin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_Delete_Pin]
GO


Create PROC [dbo].[UP_Delete_Pin] (
@pinid int)
AS

IF NOT EXISTS (Select * from Pin Where pinid = @pinid)
BEGIN
	Select CAST(0 as Bit) as Success, 'Request cannot be deleted as it doesn''t exist.'  as msg
	return
END

Delete from Pin where pinid = @pinid

Select CAST(1 as Bit) as Success, '' as msg
return
Go


-- ================================================
-- Author:		Jason
-- Create date: 11/19/2013
-- Description:	Repinning
-- 1. Insert Pin into board if not already exist (if same boardid and same pictureid then it exists)
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_Repin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_Repin]
GO


Create PROC [dbo].[UP_Repin] (
@pinid int,
@boardid int)
AS

Declare @pictureid as int
Select @pictureid = pictureid from Pin Where pinid = @pinid

IF NOT EXISTS (Select * from Pin Where pictureid = @pictureid AND boardid = @boardid)
BEGIN
	Insert into Pin(pictureid, boardid) 
	values (@pictureid, @boardid)
	Declare @newpinid as int
	Select @newpinid = pinid from Pin Where pictureid = @pictureid AND boardid = @boardid
	Insert into Repin(pinid, targetpinid) 
	values (@newpinid, @pinid)
	Select CAST(1 as Bit) as Success, '' as msg
	return
END

Select CAST(0 as Bit) as Success, 'Request can not be modified as its already '   as msg
return
Go


-- ================================================
-- Author:		Jason
-- Create date: 11/19/2013
-- Description:	Create Follow Stream
-- 1. Create Stream if not already exist
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_New_Stream]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_New_Stream]
GO


Create PROC [dbo].[UP_New_Stream] (
@streamname varchar(100) , @username varchar(100))
AS

IF NOT EXISTS (Select * from FollowStream Where streamname = @streamname and username = @username)
BEGIN
	Insert into FollowStream(streamname, username) 
	values (@streamname, @username)
		
	Select CAST(1 as Bit) as Success, '' as msg
	return
END

Select CAST(0 as Bit) as Success, 'Stream already exists.'  as msg
return
Go


-- ================================================
-- Author:		Jason
-- Create date: 11/19/2013
-- Description:	Add Board to Follow Stream
-- 1. Create Stream if not already exist
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_Follow]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_Follow]
GO


Create PROC [dbo].[UP_Follow] (
@boardid int,
@streamid int)
AS

IF NOT EXISTS (Select * from BoardFollow Where boardid = @boardid AND streamid = @streamid)
BEGIN
	Insert into BoardFollow(boardid, streamid) 
	values (@boardid, @streamid)
		
	Select CAST(1 as Bit) as Success, '' as msg
	return
END

Select CAST(0 as Bit) as Success, 'Already following that board.'  as msg
return
Go


-- ================================================
-- Author:		Jason
-- Create date: 11/19/2013
-- Description:	Display Stream
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_Display_Stream]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_Display_Stream]
GO


Create PROC [dbo].[UP_Display_Stream] (
@streamid int)
AS

IF EXISTS (Select * from BoardFollow Where streamid = @streamid)
BEGIN	-- this will return multiple tuples with 1,url the calling program will see the bit 1 and then get the image from URL
	Select CAST(1 as Bit) as Success, ImageUrl from Picture as pic 
	join Pin as p on pic.pictureid = p.pictureid
	join Board as b on p.BoardId = b.boardid 
	join BoardFollow as bf on b.boardid = bf.boardid where bf.streamid = @streamid
	return
END

Select CAST(0 as Bit) as Success, 'No pins for this streamid.'  as msg
return
Go



-- ================================================
-- Author:		Jason
-- Create date: 11/19/2013
-- Description:	Liking
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_Like]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_Like]
GO


Create PROC [dbo].[UP_Like] (
@pinid int,
@username varchar(100))
AS

Declare @originalpinid as int
Select @originalpinid = targetpinid from REPIN Where pinid = @pinid
if @originalpinid is null 
BEGIN
	set @originalpinid = @pinid 
END

IF NOT EXISTS (Select * from UserLike Where pinid = @originalpinid and username = @username)
BEGIN
	Insert into UserLike(username, pinid) 
	values (@username, @originalpinid)
	Select CAST(1 as Bit) as Success, '' as msg
	return
END

Select CAST(0 as Bit) as Success, 'User already likes this pin.'  as msg
return
Go



-- ================================================
-- Author:		Jason
-- Create date: 11/19/2013
-- Description:	Commenting
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_Comment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_Comment]
GO


Create PROC [dbo].[UP_Comment] (
@pinid int,
@username varchar(100),
@commenttext TEXT)
AS

Declare @boardowner as varchar(100)
Select @boardowner = boardowner from Board B join Pin P on P.BoardId = B.BoardId Where p.PinId = @pinid

IF EXISTS (Select * from Board Where boardowner = @boardowner and privatecomments = 1)
BEGIN
	IF EXISTS (Select * from Friend Where (SourceUser = @boardowner and TargetUser = @username and RequestStatus = 1) 
	or  (SourceUser = @username and TargetUser = @boardowner and RequestStatus = 1))
	BEGIN
		INSERT INTO Comment(username, pinid, commenttext) 
		values (@username, @pinid, @commenttext)
		Select CAST(1 as Bit) as Success, '' as msg
		return
	END
	Select CAST(0 as Bit) as Success, 'Only friends may comment on this board.'  as msg
	return
END

INSERT INTO Comment(username, pinid, commenttext) 
values (@username, @pinid, @commenttext)
Select CAST(1 as Bit) as Success, '' as msg
return
Go

USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_UpdateProfile]    Script Date: 11/28/2013 11:16:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[UP_UpdateProfile] (
	@username varchar(100),
	@newFirstName varchar(100), 
	@newLastName varchar(100), 
	@newEmail varchar(255)) 
AS
BEGIN TRANSACTION
	BEGIN TRY
		Update UserInfo
		Set LastName = @newLastName , FirstName = @newFirstName , email = @newEmail
		where username = @username
		COMMIT TRANSACTION
		Select CAST(1 as Bit) as Success, '' as msg
	END TRY
	BEGIN CATCH
	    ROLLBACK TRANSACTION
	    DECLARE @Msg NVARCHAR(MAX)  
        SELECT @Msg='User profile could not be updated '+ ERROR_MESSAGE() 
	    Select CAST(0 as Bit) as Success, @Msg as msg
	END CATCH


GO


USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_SignUp]    Script Date: 11/28/2013 11:27:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[UP_SignUp] (
	@username varchar(100),
	@password varchar(100),
	@FirstName varchar(100),
	@LastName varchar(100),
	@Email varchar(255) ) 
AS

	IF EXISTS (Select * from UserInfo Where UserName = @UserName )
	BEGIN
		Select CAST(0 as Bit) as Success, 'User Already Exist' as msg
		Return 
	END
	
	BEGIN TRANSACTION
		BEGIN TRY
		Insert into Userinfo (UserName , FirstName , Email , LastName ,[Password] ) 
		values (@username , @FirstName , @email , @LastName , @password)
		COMMIT TRANSACTION
		Select CAST(1 as Bit) as Success, '' as msg
	END TRY
	BEGIN CATCH
	    ROLLBACK TRANSACTION
	    DECLARE @Msg NVARCHAR(MAX)  
        SELECT @Msg='User Could not be created '+ ERROR_MESSAGE() 
	    Select CAST(0 as Bit) as Success, @Msg as msg
	END CATCH


GO


-- ================================================
-- Author:		Ahmed
-- Create date: 11/12/2013
-- Description:	this will find a list of all people that can be my friend
-- =============================================
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_PossibleFriends]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_PossibleFriends]
GO


Create PROC [dbo].[UP_PossibleFriends] (
	@username varchar(100)
	) 
AS


select username , email , firstname , lastname 
 from userinfo where username <> @username and username not in (
select sourceuser as username from friend where TargetUser = @username
union
select targetuser as username from friend where SourceUser = @username
)

GO



USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_UpdateProfile]    Script Date: 12/7/2013 4:23:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_UpdateProfile]    Script Date: 12/12/2013 10:03:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROC [dbo].[UP_UpdateProfile] (
	@username varchar(100),
	@newFirstName varchar(100), 
	@newLastName varchar(100), 
	@newEmail varchar(255)) 
AS
BEGIN TRANSACTION
	BEGIN TRY
		Update UserInfo
		Set LastName = @newLastName , FirstName = @newFirstName , email = @newEmail
		where username = @username
		COMMIT TRANSACTION
		Select CAST(1 as Bit) as Success, '' as msg
	END TRY
	BEGIN CATCH
	    ROLLBACK TRANSACTION
	    DECLARE @Msg NVARCHAR(MAX)  
        SELECT @Msg='User profile could not be updated '+ ERROR_MESSAGE() 
	    Select CAST(0 as Bit) as Success, @Msg as msg
	END CATCH



GO



GO

USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_Pin]    Script Date: 12/9/2013 11:22:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROC [dbo].[UP_Pin] (
@imgurl varchar(255),
@boardid int)
AS



IF NOT EXISTS (Select * from Picture Where imageUrl = @imgurl)
BEGIN
	Insert into Picture(imageUrl) 
	values (@imgurl)
END

Declare @pictureid as int

Select @pictureid = pictureid from Picture Where imageUrl = @imgurl

IF NOT EXISTS (Select * from Pin Where pictureid = @pictureid AND boardid = @boardid)
BEGIN
	Insert into Pin(pictureid, boardid) 
	values (@pictureid, @boardid)
	
	Select CAST(1 as Bit) as Success, '' as msg , SCOPE_IDENTITY() as PinId
	return
END
Select CAST(0 as Bit) as Success, 'User already has the image pined to this board'   as msg , 0 as PinId
return


GO


USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_New_Stream]    Script Date: 12/11/2013 11:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[UP_New_Stream] (
@streamname varchar(100) , @username varchar(100))
AS

IF NOT EXISTS (Select * from FollowStream Where streamname = @streamname and username = @username)
BEGIN
	Insert into FollowStream(streamname, username) 
	values (@streamname, @username)
		
	Select CAST(1 as Bit) as Success, '' as msg , SCOPE_IDENTITY() as StreamId
	return
END

Select CAST(0 as Bit) as Success, 'Stream already exists.'  as msg , 0 as StreamId
return

GO


USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_Display_Stream]    Script Date: 12/11/2013 11:24:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[UP_Display_Stream] (
@streamid int)
AS

IF EXISTS (Select * from BoardFollow Where streamid = @streamid)
BEGIN	-- this will return multiple tuples with 1,url the calling program will see the bit 1 and then get the image from URL
	Select CAST(1 as Bit) as Success, ImageUrl from Picture as pic 
	join Pin as p on pic.pictureid = p.pictureid
	join Board as b on p.BoardId = b.boardid 
	join BoardFollow as bf on b.boardid = bf.boardid where bf.streamid = @streamid
	return
END

Select CAST(0 as Bit) as Success, 'No pins for this streamid.'  as ImageUrl
return

GO


USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_New_Stream]    Script Date: 12/11/2013 11:21:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[UP_New_Stream] (
@streamname varchar(100) , @username varchar(100))
AS

IF NOT EXISTS (Select * from FollowStream Where streamname = @streamname and username = @username)
BEGIN
	Insert into FollowStream(streamname, username) 
	values (@streamname, @username)
		
	Select CAST(1 as Bit) as Success, '' as msg , SCOPE_IDENTITY() as StreamId
	return
END

Select CAST(0 as Bit) as Success, 'Stream already exists.'  as msg , CAST(0 as decimal) as StreamId
return

GO


USE [Pinit]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UP_FollowTop5]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UP_FollowTop5]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



Create PROC [dbo].[UP_FollowTop5] (
@username varchar(100))
AS


BEGIN
	select top 5 Pin.PinId , picture.ImageUrl , picture.DateUploaded 
	from 
	 BoardFollow join FollowStream on FollowStream.StreamID = BoardFollow.StreamID join pin on pin.BoardId = BoardFollow.BoardId
	join picture on picture.PictureId = pin.PictureId
	where FollowStream.UserName = @username
	order by picture.DateUploaded desc
END


return

GO


USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_Repin]    Script Date: 12/12/2013 12:24:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[UP_Repin] (
@pinid int, -- this is the target or the pin i wana repin
@boardid int) -- this is the board where i wana repin
AS

Declare @pictureid as int
Select @pictureid = pictureid from Pin Where pinid = @pinid

IF NOT EXISTS (Select * from Pin Where pictureid = @pictureid AND boardid = @boardid)
BEGIN
	Insert into Pin(pictureid, boardid) 
	values (@pictureid, @boardid)
	Declare @newpinid as int
	set @newpinid = SCOPE_IDENTITY()
	Insert into Repin(pinid, targetpinid) 
	values (@newpinid, @pinid)
	Select CAST(1 as Bit) as Success, '' as msg
	return
END

Select CAST(0 as Bit) as Success, 'Request can not be modified as its already '   as msg
return

GO


USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_Repin]    Script Date: 12/12/2013 12:24:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[UP_Repin] (
@pinid int, -- this is the target or the pin i wana repin
@boardid int) -- this is the board where i wana repin
AS

Declare @pictureid as int
Select @pictureid = pictureid from Pin Where pinid = @pinid

IF NOT EXISTS (Select * from Pin Where pictureid = @pictureid AND boardid = @boardid)
BEGIN
	Insert into Pin(pictureid, boardid) 
	values (@pictureid, @boardid)
	Declare @newpinid as int
	set @newpinid = SCOPE_IDENTITY()
	Insert into Repin(pinid, targetpinid) 
	values (@newpinid, @pinid)
	Select CAST(1 as Bit) as Success, '' as msg
	return
END

Select CAST(0 as Bit) as Success, 'Request can not be modified as its already '   as msg
return

GO

USE [Pinit]
GO

/****** Object:  StoredProcedure [dbo].[UP_KeyWordSearchOnTags]    Script Date: 12/12/2013 11:26:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROC [dbo].[UP_KeyWordSearchOnTags] (
@KeyWork varchar(255))
AS
	--select pinid From tag Join PinTag on tag.tagid = PinTag.tagid  where Contains(tag.tagname , @KeyWork)
	--Just a temp to make it work on Express Version When deploying will Move it to Contains
	select p.pinid , pic.DateUploaded ,pic.ImageUrl , pic.PictureId  
	From tag Join PinTag on tag.tagid = PinTag.tagid 
	join pin p on p.PinId = pintag.PinId 
	join Picture pic on pic.PictureId = p.PictureId
	 where tag.tagname like '%' + @KeyWork + '%'

GO









use master