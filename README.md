pinit
=====

NYU Poly project cs 6038


### Login
* ~~User should be able to create account, login and longoff~~
* ~~User should be able update profile~~

### Boards
* ~~user should be able to create , manage and delete baords~~
* ~~boards should display pins~~
* ~~add cascading delete on boards~~

### Pinning 
* ~~user should be able to take a image url and pin it to his board~~
* ~~user should be able to delete pins~~
* ~~add cascading deletes to pins~~
* user should be able to upload single or multi files 

### Friendship frame work
* ~~user should be able to search other users by email or username~~
* ~~user should be able to send a friend request to other~~
* ~~user should be able to deny or accept a request that was made by some one for friendship~~
* user should be able to retract request that he sent out

### Comment framework
* ~~user can add comments to pictures that he can see only. his pics / his friends pics~~
* ~~cascade delete on baords should delete comments~~
* ~~private baords should not be commentable by non friends~~

### tag framework
* ~~user can add tags to a board~~ 
* ~~user can search for tags returns his boards and other boards that he is following or his friends boards~~
* ~~cascade delete on boards should delete pin tags but not the tag dictionary~~

### Follow Frame work
* ~~user should be able to follow boards~~

### Repin
* ~~user should be able to repin~~
* ~~do a cascade delete on pin delete~~
* ~~deleting repin should not delete the master pin~~
* ~~comments go against the repin~~
* likes go aginst the master pin






## For Reference
### Original project requirements
* Polytechnic Institute of NYU November 3, 2013
* Computer Science and Engineering
* CS 6083, Fall 2013
* Project #1 (due November 20 at 5:00pm)
* You are hired by a startup company to help build the database backend for a new web-based service, similar to Pinterest,
that allows people to maintain online “pinboards” with pictures that they find and like and want to share with others. Users can sign up for the service, and can then create one or more pinboards. Later, users can “pin” pictures that they find on the web or upload themselves, and these pictures then become visible on one of their pinboards. Users can also “repin” pictures that they find on other user’s pinboards, which adds them to their own boards. Users can also follow other people’s pinboards, and can invite users to be their friends. Finally, users can “like” pictures they find on other boards, and can add
short comments to other’s pictures.
As an example, consider two users, Erica and Timmy. Erica likes to travel, and also loves antique furniture. She signs up
and creates two pinboards, “Furniture” and “Dream Vacations”. Whenever she sees a picture on the web that she likes and
wants to show to her friends, say a picture of a nice sofa on an website, or a picture of a beautiful beach, she pins it to one
of her boards. Erica also has friends who often look at her images and sometimes like the pictures or leave comments such
as “Cute” or “love it!”. Timmy is seven years old, likes dinosaurs and monsters, and when he grows up he wants to become
a pirate. He creates boards named “Super Dinosaurs” and “Pirates” and whenever he sees a picture of dinosaurs or pirates
(or even better, dinosaurs and pirates) he pins it to his boards. He also follows several pinboards by others that have a lot
of pictures of monsters and dinosaurs – to do so he defines a ‘follow stream” called “Monsters and Dinosaurs” containing
pictures from four other boards that he follows. He also sometimes repins some of these pictures so they appear on his own
board (but the follow stream is only visible to him and is not public).
So this describes the basic idea behind the system. (You may also explore services such as Pinterest to get the idea.) In this
first part of the course project, you will have to design the relational database schema that stores all the information about
users, boards, pictures, friendships, follow streams, repins, likes, and comments. In the second part of the project, you have
to design a web-accessible interface that makes this system usable for real users.
You should use your own database system on your laptop or an internet-accessible server. Use a system that supports text
operators such as like and contains. Both parts of the projectmay be done individually or in teams of two students. However,
you have to decide on a partner and email the TAs with your names by Monday, November 11. If we do not hear from you
by that date, we will assume that you are doing an individual project. The second part of the project will be due a few days
before the final exam. Note that the second project builds on top of this one, so you cannot skip this project.
Before starting your work, you should think about what kind of operations need to be performed, and what kind of data
needs to be stored. For example, there should be a login page, a page where a user can sign up for the first time (by supplying
an email address and choosing a user name), and a page where users can create or update their profiles. Users should be able
to create pinboards. ask other users to become friends, and should be able to answer friend requests. They should be able
to pin pictures they find on the web, or which they upload themselves. They should be able to repin and like pictures. For
simplicity, we assume that all boards, pictures, pins, and likes are visible to everyone, and that all pictures could be repinned
and liked by any other user. However, people may decide to only allow their friends to comment on pictures on their board
(this is a setting they can choose for each board). When a user likes a picture, this is counted as a like of the original
picture, not of a particular repinning of the picture. However, comments about a repinned picture are only associated with
the repinned and not the original picture. Users may also use keyword queries to search for pictures, by matching against
their tags, and the system would then return pictures matching the keywords sorted by either time, relevance, or number of
likes.
Some words about pinning and repinning, which will mainly be important for the second part of the project. When a user
pins a picture on the web, she should supply the URL of the image, the URL of the page in which the image was found, and
a few tags (e.g., “couch, brown, modern’, ikea). The system should also download and store the picture itself in the database
as a blob (in case the image changes later or is removed from the original site). If the user uploads an image, the system
would store the image on its site, assign a URL to the image, and then pin that URL. When a user repins (or re-repins etc.)
an image, this does not result in a copy of the picture, but is just a pointer to the picture as it was first pinned, with the same
URL and tags, and if the first pinner removes it, it should become inaccessible everywhere it was repinned. Of course, the
same picture might be originally pinned by several users, possibly under different URLs, and you do not have to remove
such duplicate pictures. Also, ideally images would be pinned using a button on your browser that is provided as a browser
plugin, but you do not have to do this as part of this project, so your system will probably require users to paste URLs into
a dialog box.
Extra credit: There are many opportunities for extra credit in this assignment, but they will only be graded with the second
part of the project. Still, you may want to plan ahead. For extra credit, you could allow keyword queries to be used when
creating follow streams. For example, Timmy could create a follow stream “Monsters” that contains the pictures from four
other pinboards plus any results from the query “slimy monsters” (with duplicates removed). Or you could assume that the
system adds for every user a special follow stream called “Our Recommendations” with pictures that the system thinks the
user might like, say pictures that share a lot of tags, or that are liked by the same people, as the pictures liked or pinned by
the user. You could even try to figure out how to create the above mentioned browser button with suitable tools.
Two more remarks: First, it is recommended to always store time stamps for any action such as pinning, liking, commenting,
as real services use such log information for later data mining. Second, you should of course not use database permissions
or views to implement user identification. There will not be a separate DBMS account for each user, but the web interface
and application itself will log into the database. So, the system you implement can see all the content, but has to make sure
at the application level that each logged-in user is identified through the use of cookies in the second part of the project.
Project Steps: Note that the following list of suggested steps is intended to help you attack the problem. You do not need to
follow them in this order, as long as you come up with a good overall design that achieves the requested functionality. Note
that in this first problem, you will only deal with the database side of this project - a suitable web interface will be designed
in the second project. However, you should already envision, plan, and describe the interface that you plan to implement.
(a) Describe some basic assumptions that you will make in your design. Describe any extra features you plan to add to the
description, and any things you are planning to not support because they seem too complicated or useless (or you ran out of
time). Discuss why you made these decisions.
(b) Design, justify, and create an appropriate database schema for the above situation. Make sure your schema is space
efficient and suitably normalized. Show an ER diagram of your design, and a translation into relational format. Identify
keys and foreign key constraints. Note that you may have to revisit your design if it turns out later that the design is not
suitable. Discuss in particular how you model users, boards, pictures, pins, comments, likes, repins etc.
(c) Use a relational database system to create the schema, together with key, foreign key, and other constraints.
(d) Write SQL queries (or sequences of SQL queries) for the following tasks.
(1) Signing Up, Creating Boards, and Pinning: Write queries that users need to sign up, to login, to create or edit their
profile, to create pinboards, to pin a picture, and to delete a pinned picture.
(2) Friends: Write queries for asking another user to be friends, and for answering a friend request.
(3) Repinning and Following: Write queries for repinning a picture and for creating a follow stream. Also, write a
query that given a follow stream, displays all pictures belonging to that follow stream in reverse chronological order.
(4) Liking and Commmenting: Write queries to like a picture, and to add a comment to a picture (while making sure
the user is allowed to comment on this picture).
(5) Keyword Search: Write a query to perform a keyword search for pictures whose tags match the keywords. Use the
contain operator to do so.
(e) Populate your database with some sample data, and test the queries you have written in part (e). Make sure to input
interesting and meaningful data and to test a number of cases. Limit yourself to a few users and a few pictures and boards,
but make sure there is enough data to generate interesting test cases. It is suggested that you design your test data very
carefully. Draw and submit a page with the content of all the tables in easily understandable form (not a long list of insert
statements) and discuss the structure of the data.
(f) Document and log your design and testing appropriately. Submit a properly documented description and justification
of your entire design, including ER diagrams, tables, constraints, queries, procedures, and tests on sample data, and a few
pages of description. This should be a paper of say 10-15 pages with introduction, explanations, ER and other diagrams,
etc., that you will then revise and expand in the second part.

* Polytechnic Institute of NYU November 24, 2013
* Computer Science and Engineering
* CS 6083, Fall 2013
* Project #2 (due December 13)
* In the second project, you have to create a web-based user interface for the online pinboard service database designed
in the first project. In particular, users should be able to register, create a profile, log in, create boards, add pictures to
their boards via pinning, create streams to follow other boards, repin pictures, like pictures, send and answer friend
requests, etc., as described.
Note that you have more freedom in this second project to design your own system. You still have to follow the basic
guidelines, but you can choose the actual look and feel of the site, and offer other features that you find useful. In
general, design an overall nice and functional system. There will be some extra points available for a nice and smooth
design. If you are doing the project in a group, note that both students have to attend the demo and know ALL details
of the design. So work together with your partner, not separately. Also, slightly more will be expected if you are
working in a team. Start by revising your design from the first project as needed. In general, part of the credit for this
project will be given for revising and improving the design you did in the first project.
A note about the interface you are expected to build for this project. It is of course central that users be able to see a
board with pictures, basically a web page containing many pictures arranged from top to bottom, maybe with their
names, tags and numbers of likes, and maybe as thumbnails with several pictures in each row. (The pictures in a
follow stream could be displayed in the same way.) To do this, you may want to look for libraries that allow you to
create thumbnails from pictures. Also, repinning, liking, and commenting could be done via appropriate buttons for
each picture, but this is up to you.
Users should be able to perform all operations via a standard web browser. This should be implemented by writing a
program that is called by a web server, connects to your database, then calls appropriate stored procedures that you
have defined in the database (or maybe send queries), and finally returns the results as a web page. You can
implement the interface in several different ways. You may use frameworks such as PHP, Java, Ruby on Rails, or VB
to connect to your backend database. Contact the TAs for technical questions. There also should be ways for users to
search certain streams, or all streams on the site, by typing in keywords that are matched against tags or names.
Every group is expected to demo their project to one of the TAs at the end of the semester. If you use your own
installation, make sure you can access this during the demo. One popular choice is to use a local web server, database,
and browser on your laptop, which means you need to bring your own laptop to the demo. (In this case, your project
does not have to be available on the public Internet; it is enough to have it run locally on your laptop). Also, one thing
to consider is how to keep state for a user session and how to assign URLs to content – it might be desirable if users
could bookmark a picture, a board, a user profile, or the results of a search. Grading will be done on the entire project
based on what features are supported, how attractive and convenient the system is for users, your project description
and documentation (important), and the appropriateness of your design in terms of overall architecture and use of the
database system. Make sure to input some interesting data so you can give a good demo.
Describe and document your design. Log some sessions with your system. Bring your description (documentation)
and the logs in hardcopy to the demo. You should also be able to show your source code during the demo. The
documentation should consist of 15‐20 pages of carefully written text describing and justifying your design and the
decisions you made during the implementation, and describing how a user should use your system. Note that your
documentation and other materials should cover both Projects 1 and 2, so you should modify and extend your
materials from the first project appropriately.
