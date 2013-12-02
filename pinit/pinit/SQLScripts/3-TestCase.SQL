Use PinIt




insert into tag values ('animal')
/*
TESTCASE 1 Create 3 users 
ashabbir Ahmed Shabbir ahmed@gmail.com someword
jlee Jason Lee jason@gmail.com hiddenword
asmith Agent Smith smith@gmail.com theword
*/

exec UP_SignUp 'ashabbir', 'someword', 'ahmed', 'shabbir', 'shabbir@gmail.com'	
exec UP_SignUp 'jlee', 'theword', 'Jason', 'Lee', 'JLee@gmail.com'	
exec UP_SignUp 'asmith', 'someword', 'Agent', 'Smith', 'asmith@gmail.com'	

exec UP_SignUp 'asmith', 'someword', 'Agent', 'Smith', 'asmith@gmail.com'	



/*
TEST CASE 2 SEND FRIEND REQUEST
Ahmed sends Jason friend ship request
Ahmed Sends Smith Friend ship request
*/
Exec [dbo].[UP_SendFriendShipRequest] 'ashabbir' , 'jlee'
Exec [dbo].[UP_SendFriendShipRequest] 'ashabbir' , 'asmith'

--just a test case to see if duprequest are sent out
Exec [dbo].[UP_SendFriendShipRequest] 'ashabbir' , 'asmith'


/* 
TESTCASE 3 ACCEPT AND REJECT FRIENDS
Ahmed accpet smith ship request
Ahmed Sends Smith Friend ship request
*/

Exec [dbo].[UP_AcceptOrRejectFriendShipRequest] 'ashabbir' , 'asmith' 
Exec [dbo].[UP_AcceptOrRejectFriendShipRequest] 'asmith' , 'ashabbir' 
Exec [dbo].[UP_AcceptOrRejectFriendShipRequest] 'ashabbir' , 'asmith' , 1
Exec [dbo].[UP_AcceptOrRejectFriendShipRequest] 'ashabbir' , 'jlee' , 0

/*
TESTCASE 4 DO A SEARCH FOR SOME THING
*/
Exec [UP_KeyWordSearchOnTags] 'animal'



/*
TESTCASE 5 LOGIN USING ASHABBIR ACCOUNT
*/

EXEC [DBO].[UP_LOGIN] 'ASHABBIR' , 'someword'
EXEC [DBO].[UP_LOGIN] 'ASHABBIR' , 'someworD'



/*
TESTCASE 6 update ahmed last name
*/
exec [UP_UpdateProfile] 'ashabbir', 'someword', 'ahmed', 'thegreat', 'shabbir@gmail.com'	
select * from  userinfo

/*
TESTCASE 7 
Create new board
*/

exec [dbo].[UP_New_Board] 'myfirstboard' , 'ashabbir'
select * from Board



/*
TESTCASE 8
pin some thing
*/
exec [dbo].[UP_Pin] 'www.hotmail.com' , '1'
select * from pin 

/*
TESTCASE 9 
create stream for ahmed call it my boards
*/
exec dbo.UP_New_Stream 'myboards' , 'ashabbir'
exec dbo.UP_New_Stream 'myboards' , 'asmith'


select * from followstream

/*
TESTCASE 10 
add board in ahmed's stream
*/
exec UP_follow 1,1

select * from BoardFollow



/*
TESTCASE 11
display all pics from myboards stream
*/

exec UP_Display_Stream 1




/*
TESTCASE 12
JLEE Like ahmed pin
*/
exec [UP_Like] 1 ,  'jlee'

select * from UserLike


/*
TESTCASE 13
Comment Ahmed pin
*/
exec [UP_Comment] 1,'jlee' , 'this is good '

select * from comment
Use Master