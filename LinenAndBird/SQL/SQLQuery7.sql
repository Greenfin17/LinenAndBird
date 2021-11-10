insert into BirdAccessories (Name, BirdId)
Values ( 'Flower', 'c0175419-5fee-41c1-bb43-02a27425011d');

select * from BirdAccessories;

select * from birds b
	left join BirdAccessories ba
	on b.Id = ba.BirdId;

select * from birds;
select * from BirdAccessories;