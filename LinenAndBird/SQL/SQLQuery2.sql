update birds
	set Color = 'purple', 
	Size = 'medium'
	-- Name = Name + ' - ' + cast(o.price as varchar(10))
-- show changes
output inserted.*, deleted.*
from birds
--	join Orders o
--	on o.BirdId = b.id
where Color = 'blueberry';


update Birds
Set Color = @color,
	Name = @name,
	Type = @type,
	Size = @size
Where id= @@IDENTITY