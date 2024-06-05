USE [RedisDB]
GO

INSERT INTO [dbo].[Truck]
           ([TruckNumber]
           ,[EngineNumber]
           ,[ChasisNumber]
           ,[Wheels])
     VALUES
           ('JH051234'
           ,'ENG123456789'
           ,'CH123456789'
           ,10)
GO