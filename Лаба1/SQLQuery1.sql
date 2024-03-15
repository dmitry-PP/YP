create database LibraryDB;
go

use LibraryDB;
go

create table Authors(
	ID int primary key identity(1,1),
	SureName varchar(25) not null,
	FirstName varchar(25) not null,
	LastName varchar(25),
	Birthday date not null,
	Nationality varchar(25)
);


create table Books(
	ID int primary key identity(1,1),
	Title varchar(50) not null,
	Pages int not null,
	Price int not null,
	DateRelease date not null,
	Amount bit not null,
	Author_ID int not null,
	foreign key (Author_ID) references Authors(ID) on delete cascade
);

insert into Authors(FirstName,SureName,LastName,Birthday,Nationality)
values ('������','��������','������������','1978-05-15','�������'),
('������','������',null,'1988-04-5','�����'),
('��������','��������',null,'1988-04-5','���������'),
('����','�����','�����','1988-04-5','����������'),
('�������','������','�������������','1988-04-5','�����');
go

insert into Books(Title,Pages,Price,DateRelease,Amount,Author_ID)
values ('���� �����',324,1200,'2024-03-1',34,1),
('�����',400,120,'2023-12-1',100,2),
('����������� �����',60,700,'2022-06-6',455,3),
('������� �������',221,2300,'2024-01-3',10,4),
('������� ���������',34,450,'2020-3-15',34,1);