CREATE TABLE Names (
	Id int not null identity primary key,
	Name nvarchar(50) not null
)

CREATE TABLE Messages (
	Id int not null identity primary key,
	Created datetime2 not null,
	Temperature int not null,
	Humidity int not null
)
GO

INSERT INTO Names VALUES ('Hans'),('Tommy'),('Joakim'),('Haithem'),('Alicja')
INSERT INTO Messages VALUES (GETDATE(), 22, 34)


SELECT * FROM Names
SELECT * FROM Messages