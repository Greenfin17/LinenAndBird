USE [LinenAndbird]
GO

INSERT INTO [dbo].[Orders]
     
           ,[BirdId]
           ,[HatId]
           ,[Price])
     VALUES
           ,@Birdid
           ,@Hatid
		   ,@Price)
GO


select * from hats;

