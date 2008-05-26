Declare @adminUserId uniqueidentifier
Declare @adminRoleId uniqueidentifier
Declare @viewOnlyUserId uniqueidentifier
Declare @viewOnlyRoleId uniqueidentifier
set @adminUserId = newid()
set @adminRoleId = newid()
set @viewOnlyUserId = newid()
set @viewOnlyRoleId = newid()

delete UserRole
delete [Role]
delete Issue
delete [User]

insert into [User] 
	(Id, FirstName, LastName, Email, UserName, Password)
values
	(@adminUserId, 'Test', 'Admin', 'test.admin@domain.com', 'test.admin', 'password')

insert into [User] 
	(Id, FirstName, LastName, Email, UserName, Password)
values
	(@viewOnlyUserId, 'Test', 'ViewOnly', 'test.viewOnly@domain.com', 'test.viewOnly', 'password')

insert into Role
	(Id, [Name], [Permissions])
values
	(@adminRoleId, 'Administrator', 'CreateIssues,ViewIssues')

insert into Role
	(Id, [Name], [Permissions])
values
	(@viewOnlyRoleId, 'View Only', 'ViewIssues')

insert into UserRole
	([User_Id], [Role_Id])
values
	(@adminUserId, @adminRoleId)
	
insert into UserRole
	([User_Id], [Role_Id])
values
	(@viewOnlyUserId, @viewOnlyRoleId)
	
	