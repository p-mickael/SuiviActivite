Create database SuiviActivite
Go

use SuiviActivite
Go

create table Users (
	Id	int	primary key identity(1,1),
	FirstName	varchar(50)	not null,
	LastName	varchar(50)	not null,
	IsLocked	bit	not null default(0),
	IsActive	bit	not null default(1)
)

create table Schedules (
	Id	int	primary key identity(1,1),
	UserId	int	references Users(Id) not null,
	DateLogIn	smalldatetime	not null,
	DateLogOut	smalldatetime	null
)