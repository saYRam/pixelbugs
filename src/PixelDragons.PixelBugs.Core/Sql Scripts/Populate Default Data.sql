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
delete Card
delete [User]
delete CardPriority
delete CardStatus
delete CardType
delete CardComment

insert into [User] (Id, FirstName, LastName, Email, UserName, Password) values (@adminUserId, 'Test', 'Admin', 'test.admin@domain.com', 'test.admin', 'password')
insert into [User] (Id, FirstName, LastName, Email, UserName, Password) values (@viewOnlyUserId, 'Test', 'ViewOnly', 'test.viewOnly@domain.com', 'test.viewOnly', 'password')

insert into Role (Id, [Name], [Permissions]) values	(@adminRoleId, 'Administrator', 'CreateCards,ViewCards,EditCards')
insert into Role (Id, [Name], [Permissions]) values (@viewOnlyRoleId, 'View Only', 'ViewCards')

insert into UserRole ([User_Id], [Role_Id]) values (@adminUserId, @adminRoleId)
insert into UserRole ([User_Id], [Role_Id]) values (@viewOnlyUserId, @viewOnlyRoleId)
	
insert into CardStatus (Id, [Name], Ordinal) values (newid(), 'Waiting', 0)
insert into CardStatus (Id, [Name], Ordinal) values (newid(), 'In Dev', 1)
insert into CardStatus (Id, [Name], Ordinal) values (newid(), 'In QA', 2)
insert into CardStatus (Id, [Name], Ordinal) values (newid(), 'Complete', 3)

insert into CardPriority (Id, [Name], Colour) values (newid(), 'High', '#FF3A3A')
insert into CardPriority (Id, [Name], Colour) values (newid(), 'Medium', '#FFD739')
insert into CardPriority (Id, [Name], Colour) values (newid(), 'Low', '#88FF39')

insert into CardType (Id, [Name], Colour) values (newid(), 'Story', '#FAFAD2')
insert into CardType (Id, [Name], Colour) values (newid(), 'Defect', '#FAD2D2')
insert into CardType (Id, [Name], Colour) values (newid(), 'QA', '#D2D8FA')
insert into CardType (Id, [Name], Colour) values (newid(), 'Tech Debt', '#D2FADA')
