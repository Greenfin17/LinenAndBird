create table BirdAccessories (
	Id UniqueIdentifier not Null primary key default(newsequentialid()),
	Name varchar(50) not Null,
	BirdId UniqueIdentifier not Null,
	CONSTRAINT FK_BirdAccessories_Birds FOREIGN KEY (BirdId)
		REFERENCES Birds(Id)
		)
