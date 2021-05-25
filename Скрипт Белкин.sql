USE [master]
GO

CREATE DATABASE [КР]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'КР', FILENAME = N'c:\users\public\КР.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'КР_log', FILENAME = N'c:\users\public\КР_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

USE [КР]

GO
CREATE TABLE [dbo].[Booking](
	[IdBooking] [nvarchar](20) NOT NULL,
	[DateBooking] [date] NULL,
	[LoginPassenger] [nvarchar](50) NULL,
	[IdFly] [nvarchar](10) NULL,
	[DateOut] [date] NOT NULL,
 CONSTRAINT [PK_Билеты] PRIMARY KEY CLUSTERED 
(
	[IdBooking] ASC,
	[DateOut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[Aircrafts](
	[IdAircraft] [nvarchar](3) NOT NULL,
	[NameAircraft] [nvarchar](50) NULL,
	[PlacesCount] [int] NULL,
 CONSTRAINT [PK_Самолеты] PRIMARY KEY CLUSTERED 
(
	[IdAircraft] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[Airports](
	[IdAirport] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[NameAirport] [nvarchar](50) NULL,
 CONSTRAINT [PK_Аэропорты] PRIMARY KEY CLUSTERED 
(
	[IdAirport] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Flights](
	[IdFly] [nvarchar](10) NOT NULL,
	[IdAircraft] [nvarchar](3) NULL,
	[IdAirportOut] [nvarchar](50) NULL,
	[IdAirportIn] [nvarchar](50) NULL,
	[DateOut] [date] NOT NULL,
	[TimeOut] [time](7) NULL,
	[DateIn] [date] NULL,
	[TimeIn] [time](7) NULL,
 CONSTRAINT [PK_Рейсы] PRIMARY KEY CLUSTERED 
(
	[IdFly] ASC,
	[DateOut] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE VIEW [dbo].[zapros]
AS
SELECT dbo.Booking.IdFly, dbo.Booking.DateOut, COUNT(dbo.Booking.IdBooking) AS count_booking, dbo.Aircrafts.PlacesCount, Airports_1.Country, Airports_1.City, dbo.Airports.Country AS Expr1, dbo.Airports.City AS Expr2
FROM     dbo.Booking INNER JOIN
                  dbo.Flights ON dbo.Booking.IdFly = dbo.Flights.IdFly AND dbo.Booking.DateOut = dbo.Flights.DateOut INNER JOIN
                  dbo.Aircrafts ON dbo.Flights.IdAircraft = dbo.Aircrafts.IdAircraft INNER JOIN
                  dbo.Airports ON dbo.Flights.IdAirportIn = dbo.Airports.IdAirport INNER JOIN
                  dbo.Airports AS Airports_1 ON dbo.Flights.IdAirportOut = Airports_1.IdAirport
GROUP BY dbo.Booking.IdFly, dbo.Booking.DateOut, dbo.Aircrafts.PlacesCount, Airports_1.Country, Airports_1.City, dbo.Airports.Country, dbo.Airports.City
HAVING (COUNT(dbo.Booking.IdBooking) < dbo.Aircrafts.PlacesCount)
GO

CREATE TABLE [dbo].[People](
	[Login] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[Phone] [char](16) NULL,
	[Email] [nvarchar](50) NULL,
	[Dob] [date] NULL,
	[Passport] [nvarchar](15) NULL,
 CONSTRAINT [PK_Пассажиры] PRIMARY KEY CLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Roles](
	[IdRole] [int] NOT NULL,
	[NameRole] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Users](
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[IdRole] [int] NULL,
 CONSTRAINT [PK_Пользователи] PRIMARY KEY CLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Aircrafts] ([IdAircraft], [NameAircraft], [PlacesCount]) VALUES (N'1', N'Boing-737', 529)
INSERT [dbo].[Aircrafts] ([IdAircraft], [NameAircraft], [PlacesCount]) VALUES (N'2', N'Airbus-320', 290)
INSERT [dbo].[Aircrafts] ([IdAircraft], [NameAircraft], [PlacesCount]) VALUES (N'3', N'Airbus-310', 350)
GO
INSERT [dbo].[Airports] ([IdAirport], [Country], [City], [NameAirport]) VALUES (N'1', N'Россия', N'Москва', N'Внуково')
INSERT [dbo].[Airports] ([IdAirport], [Country], [City], [NameAirport]) VALUES (N'2', N'Россия', N'Москва', N'Домодедово')
INSERT [dbo].[Airports] ([IdAirport], [Country], [City], [NameAirport]) VALUES (N'3', N'Россия', N'Самара', N'Курумоч')
INSERT [dbo].[Airports] ([IdAirport], [Country], [City], [NameAirport]) VALUES (N'4', N'Россия', N'Ростов-на-дону', N'Платов')
INSERT [dbo].[Airports] ([IdAirport], [Country], [City], [NameAirport]) VALUES (N'5', N'Узбекистан', N'Ташкент', N'Аэровокзал')
GO
INSERT [dbo].[Booking] ([IdBooking], [DateBooking], [LoginPassenger], [IdFly], [DateOut]) VALUES (N'2021032607081058', CAST(N'2021-03-26' AS Date), N'log999', N'1058', CAST(N'2021-06-12' AS Date))
INSERT [dbo].[Booking] ([IdBooking], [DateBooking], [LoginPassenger], [IdFly], [DateOut]) VALUES (N'2021032708241058', CAST(N'2021-03-27' AS Date), N'log999', N'1058', CAST(N'2021-06-12' AS Date))
INSERT [dbo].[Booking] ([IdBooking], [DateBooking], [LoginPassenger], [IdFly], [DateOut]) VALUES (N'2021032812151058', CAST(N'2021-03-28' AS Date), N'log999', N'1058', CAST(N'2021-06-12' AS Date))
INSERT [dbo].[Booking] ([IdBooking], [DateBooking], [LoginPassenger], [IdFly], [DateOut]) VALUES (N'20210493', CAST(N'2021-03-18' AS Date), N'log999', N'1058', CAST(N'2021-06-12' AS Date))
INSERT [dbo].[Booking] ([IdBooking], [DateBooking], [LoginPassenger], [IdFly], [DateOut]) VALUES (N'20210494', CAST(N'2021-03-19' AS Date), N'332ewr', N'1056', CAST(N'2021-04-18' AS Date))
GO
INSERT [dbo].[Flights] ([IdFly], [IdAircraft], [IdAirportOut], [IdAirportIn], [DateOut], [TimeOut], [DateIn], [TimeIn]) VALUES (N'1056', N'1', N'1', N'4', CAST(N'2021-04-18' AS Date), CAST(N'21:30:00' AS Time), CAST(N'2021-04-18' AS Date), CAST(N'22:57:00' AS Time))
INSERT [dbo].[Flights] ([IdFly], [IdAircraft], [IdAirportOut], [IdAirportIn], [DateOut], [TimeOut], [DateIn], [TimeIn]) VALUES (N'1056', N'2', N'2', N'4', CAST(N'2021-04-19' AS Date), CAST(N'21:30:00' AS Time), CAST(N'2021-04-19' AS Date), CAST(N'22:57:00' AS Time))
INSERT [dbo].[Flights] ([IdFly], [IdAircraft], [IdAirportOut], [IdAirportIn], [DateOut], [TimeOut], [DateIn], [TimeIn]) VALUES (N'1057', N'1', N'4', N'1', CAST(N'2021-04-20' AS Date), CAST(N'13:40:00' AS Time), CAST(N'2021-04-18' AS Date), CAST(N'14:50:00' AS Time))
INSERT [dbo].[Flights] ([IdFly], [IdAircraft], [IdAirportOut], [IdAirportIn], [DateOut], [TimeOut], [DateIn], [TimeIn]) VALUES (N'1058', N'2', N'3', N'4', CAST(N'2021-06-12' AS Date), CAST(N'06:20:00' AS Time), CAST(N'2021-06-12' AS Date), CAST(N'07:37:00' AS Time))
INSERT [dbo].[Flights] ([IdFly], [IdAircraft], [IdAirportOut], [IdAirportIn], [DateOut], [TimeOut], [DateIn], [TimeIn]) VALUES (N'4324', N'2', N'3', N'4', CAST(N'2021-03-28' AS Date), CAST(N'23:00:00' AS Time), CAST(N'2021-03-29' AS Date), CAST(N'00:00:00' AS Time))
INSERT [dbo].[Flights] ([IdFly], [IdAircraft], [IdAirportOut], [IdAirportIn], [DateOut], [TimeOut], [DateIn], [TimeIn]) VALUES (N'7777', N'1', N'1', N'4', CAST(N'2021-03-28' AS Date), CAST(N'23:55:00' AS Time), CAST(N'2021-03-28' AS Date), CAST(N'23:55:00' AS Time))
INSERT [dbo].[Flights] ([IdFly], [IdAircraft], [IdAirportOut], [IdAirportIn], [DateOut], [TimeOut], [DateIn], [TimeIn]) VALUES (N'7843', N'2', N'2', N'5', CAST(N'2021-03-28' AS Date), CAST(N'23:07:00' AS Time), CAST(N'2021-03-29' AS Date), CAST(N'23:55:00' AS Time))
GO
INSERT [dbo].[People] ([Login], [LastName], [FirstName], [MiddleName], [Phone], [Email], [Dob], [Passport]) VALUES (N'21st899', N'Баринов', N'Василий', N'Николаевич', N'+7(999)00089898 ', N'ami@mail.ru', CAST(N'2000-02-21' AS Date), N'5555667788')
INSERT [dbo].[People] ([Login], [LastName], [FirstName], [MiddleName], [Phone], [Email], [Dob], [Passport]) VALUES (N'332ewr', N'Веренов', N'Николай', N'Александрович', N'+7(999)8995656  ', N'irys@rambler.ru', CAST(N'2000-04-02' AS Date), N'4444556677')
INSERT [dbo].[People] ([Login], [LastName], [FirstName], [MiddleName], [Phone], [Email], [Dob], [Passport]) VALUES (N'log999', N'Грибанов', N'Михаил', N'Олегович', N'+7(995)9004545  ', N'turus@ya.ru', CAST(N'1984-02-21' AS Date), N'5555774433')
INSERT [dbo].[People] ([Login], [LastName], [FirstName], [MiddleName], [Phone], [Email], [Dob], [Passport]) VALUES (N'sda', N'adsa', N'asda', N'asda', N'asdad           ', N'asdasd', CAST(N'2000-02-05' AS Date), N'0202002')
GO
INSERT [dbo].[Roles] ([IdRole], [NameRole]) VALUES (1, N'Администратор')
INSERT [dbo].[Roles] ([IdRole], [NameRole]) VALUES (2, N'Пассажир')
GO
INSERT [dbo].[Users] ([Login], [Password], [IdRole]) VALUES (N'21st899', N'1234', 1)
INSERT [dbo].[Users] ([Login], [Password], [IdRole]) VALUES (N'332ewr', N'2345', 2)
INSERT [dbo].[Users] ([Login], [Password], [IdRole]) VALUES (N'log999', N'1234', 2)
INSERT [dbo].[Users] ([Login], [Password], [IdRole]) VALUES (N'sda', N'1234', 2)
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Flights] FOREIGN KEY([IdFly], [DateOut])
REFERENCES [dbo].[Flights] ([IdFly], [DateOut])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Flights]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_People] FOREIGN KEY([LoginPassenger])
REFERENCES [dbo].[People] ([Login])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_People]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_Рейсы_Аэропорты] FOREIGN KEY([IdAirportOut])
REFERENCES [dbo].[Airports] ([IdAirport])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_Рейсы_Аэропорты]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_Рейсы_Аэропорты1] FOREIGN KEY([IdAirportIn])
REFERENCES [dbo].[Airports] ([IdAirport])
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_Рейсы_Аэропорты1]
GO
ALTER TABLE [dbo].[Flights]  WITH CHECK ADD  CONSTRAINT [FK_Рейсы_Самолеты] FOREIGN KEY([IdAircraft])
REFERENCES [dbo].[Aircrafts] ([IdAircraft])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Flights] CHECK CONSTRAINT [FK_Рейсы_Самолеты]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_People] FOREIGN KEY([Login])
REFERENCES [dbo].[People] ([Login])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_People]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Roles] ([IdRole])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
