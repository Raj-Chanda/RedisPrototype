USE [RedisDB]
GO

UPDATE [dbo].[Truck]
   SET [TruckNumber] = 'JH054569'
      ,[EngineNumber] = 'ENG987654321'
      ,[ChasisNumber] = 'CH987654321'
      ,[Wheels] = 20
 WHERE [Id] = 'C73D31B9-E0DD-45B8-911E-6A1C13F4345D'
GO