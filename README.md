# Redis Prototype
This is a simple redis prototype application demonstrating the actual implementation of redis cache.

### Functionalities
* Add a truck
* Get data from cache if exists else fetch from DB add to cache and return the response
* Get data from DB
* Get data from cache

### Endpoints
**[POST]** https://localhost:44388/api/Trucks/Save

**Request Body:**
```
{
    "truckNumber": "JH05AB1234",
    "engineNumber": "EN1234",
    "chasisNumber": "CH1234",
    "wheels": 10
}
```

**[GET]** https://localhost:44388/api/Trucks/{Id}

here `Id` is trucke Id which is a guid.

**[GET]** https://localhost:44388/api/Trucks/GetFromDB/{Id}

here `Id` is trucke Id which is a guid.

**[GET]** https://localhost:44388/api/Trucks/GetFromCache/{Id}

here `Id` is trucke Id which is a guid.

### How to setup Database
To create database and table run sql scripts located at location 
RedisPrototype -> DB Files -> sql scripts(create DataBase & Table)

### Setup development environment with Docker compose
To setup development environment in docker use the below command

`docker-compose up -d`

This docker compose contain the following:

1. SQL Server 2022
2. Redis