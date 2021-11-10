CREATE TABLE dbo.Users
(
	Id uniqueidentifier NOT NULL Primary Key default(newid()),
	DisplayName varchar(100) NOT NULL,
	FirebaseUid uniqueidentifier NOT NULL,
	Email varchar(80) NOT NULL 
)