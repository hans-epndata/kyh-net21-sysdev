CREATE TABLE YourInformation (
	Email varchar(150) not null primary key,
	FirstName nvarchar(max) null,
	LastName nvarchar(max) null
)

CREATE TABLE DeviceInformation (
	DeviceId nvarchar(450) not null primary key,
	DeviceType nvarchar(max) not null,
	DeviceName nvarchar(max) null,
	Location nvarchar(max) null,
	ConnectionString nvarchar(max) null,
	IsConfigured bit not null
)