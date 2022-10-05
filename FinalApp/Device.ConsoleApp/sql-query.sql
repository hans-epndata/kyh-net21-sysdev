CREATE TABLE Settings (
	DeviceId varchar(450) not null primary key,
	ConnectionString varchar(max) null,
	DeviceName nvarchar(50) null,
	DeviceType nvarchar(50) null,
	Location nvarchar(50) null
)