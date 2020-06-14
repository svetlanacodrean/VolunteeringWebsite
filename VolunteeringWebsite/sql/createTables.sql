
create table Project (
	Id int primary key identity,
	[Name] varchar(255),
	StartDate date not null,
	EndDate date not null,
	[Description] varchar(1000),
	Activities varchar(1000),
	CoinsGiven int not null
)

alter table Project
	add LocationId int 

alter table Project
	add constraint fk_project_location foreign key (LocationId) references [Location](Id)

alter table Project
	add TopicId int

alter table Project
	add constraint fk_project_topic foreign key (TopicId) references Topic(Id)


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


create table Topic (
	Id int primary key identity,
	[Name] varchar(80)
)


create table [Language]
(
	Id int primary key identity,
	[Name] varchar(30)
)

create table Project_Language
(
	ProjectId int,
	LanguageId int,
	constraint project_fk foreign key(ProjectId) references Project(Id),
	constraint language_fk foreign key(LanguageId) references [Language](Id)
)

create table [Level]
(
	Id int primary key,
	[Name] varchar(20)
)

create table Skill
(
	Id int primary key identity,
	[Name] varchar(50),
	LevelId int,
	constraint fk_level foreign key (LevelId) references [Level](Id)
)

create table Project_Skill
(
	ProjectId int,
	SkillId int,
	constraint projectt_fk foreign key(ProjectId) references Project(Id),
	constraint skill_fk foreign key(SkillId) references Skill(Id)
)

alter table project_language
	add Id int primary key identity

alter table project_skill
	add Id int primary key identity

create table ProjectStatus
(
	Id int primary key identity,
	[Name] varchar(15)
)

create table User_Project
(
	Id int primary key identity,
	UserId nvarchar(450) constraint user_fk references AspNetUsers(Id),
	ProjectId int constraint user_project_fk references Project(Id),
	StatusId int constraint project_status_fk references ProjectStatus(Id)
)

create table Vacancy
(
	Id int primary key identity,
	[Name] varchar(50),
	StartDate date,
	EndDate date,
	[Description] varchar(1000),
	Price int,
	LocationId int,
	constraint fk_vacancy_location foreign key (LocationId) references [Location](Id)
)

create table User_Vacancy
(
	Id int primary key identity,
	UserId nvarchar(450) constraint vacancy_user_fk references AspNetUsers(Id),
	VacancyId int constraint user_vacancy_fk references Vacancy(Id)
)