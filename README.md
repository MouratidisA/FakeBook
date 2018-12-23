# FakeBook
MVC Web application for training purposes

## Application Core Scenario
Create a web application that can post text on a wall similar to Facebook post. The application must have the following features:
*	Register new user (can take one of two roles {Admin, User})
*	Login existing user
*	Change current password
*	Connection with SQL Database
*	Logout
*	Differentiate users by role and see different areas and features

---As a (Role = User)
*	List all your existing posts on page load
*	Post on your wall from the “Post Area”
*	Posts are ordered from Top to Bottom, newest being on Top
*	Every new post is appended to the current DOM on Top positioned first
  *	Mention another user on your post (it is assumed that you can see all other users [Role = user] from the database as current friend-list)
  *Select options to transform the written text to bold or/and add random background color
*	Edit all the properties on your already posted posts but not the text (properties are the options from the “Post Area” and the mentioned friends)
---	As a (Role = Admin)
*	You cannot post new posts (the “Post Area is not visible to an Admin”)
*	See all posts from every user on your wall on page load
*	Edit every post (as the user does) including the text
*	Delete an entire post


### Technologies Required
•	MVC 5, C# (Preferable IDE Visual Studio or Visual Studio Code)
•	HTML 5, JS, CSS
•	Bootstrap (any)
•	JQuery (any)
•	Git (the final project will be taken from your git repository)

## User's Actions:
* User Profile
* Create Edit Delete posts
* tag a friend
* View all his/her posts

## Administrator’s Actions:
* View all users' posts
* Edit Delete posts

## Training Goals: 
* Entity Framework - Database manipulation
* Identity Framework - For Login-Register and other user's Identity controls
* CRUD implementation
* RBAC implementation - Role access control between Administrators and simple User accounts 
* API Controllers - CRUD actions between application and database
