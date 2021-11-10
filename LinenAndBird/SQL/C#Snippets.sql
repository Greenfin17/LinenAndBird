select Id, Type, Color, Size, Name from Birds;

insert into birds(Type,Color,Size,Name)
output inserted.Id
values(0, 'yellow', 'small', 'Frank');

delete from birds
where Name='Darc';


Select *
From Birds,
Where id = '';drop table birds --'

// update
update birds
	set Color = 'blueberry', 
	Size = 'medium'
	where color = 'yellow';

select  * from birds
where color = 'yellow';

update birds
	set Color = 'purple', 
	Size = 'medium',
	-- Name = Name + ' - ' + cast(o.price as varchar(10))
output inserted.*, deleted.*
from birds
--	join Orders o
--	on o.BirdId = b.id
where Color = 'yellow';


	