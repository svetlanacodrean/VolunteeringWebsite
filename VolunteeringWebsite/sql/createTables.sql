create table Project (
	Id int primary key identity,
	[Name] varchar(255),
	StartDate date not null,
	EndDate date not null,
	[Description] varchar(1000),
	Activities varchar(1000),
	CoinsGiven int not null
)

CREATE TABLE Country (
  Id  int PRIMARY KEY IDENTITY,
  Iso  varchar(2) NOT NULL,
  [Name]  varchar(80) NOT NULL,
) 

CREATE TABLE City (
  Id  int PRIMARY KEY IDENTITY,
  [Name]  varchar(80) NOT NULL,
  CountryId int NOT NULL,
  CONSTRAINT fk_city_country FOREIGN KEY (CountryId) REFERENCES Country(Id)
) 

create table [Location] (
Id int primary key identity,
StreetName varchar(80),
StreetNumber int,
CityId int,
constraint fk_location_city foreign key (CityId) references City(Id)
)

alter table Project
add LocationId int 

alter table Project
add constraint fk_project_location foreign key (LocationId) references [Location](Id)

create table Topic (
	Id int primary key identity,
	[Name] varchar(80)
)

alter table Project
	add TopicId int

alter table Project
	add constraint fk_project_topic foreign key (TopicId) references Topic(Id)