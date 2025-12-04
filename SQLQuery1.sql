SELECT * FROM Customers

INSERT INTO Customers (FistName, LastName, Email, PhoneNumber) 
                                  VALUES ('bruno', 'nasdcimento', 'bruno@gmail.com', '1194494499')

ALTER TABLE Customers ADD [Status] NVARCHAR(10) 

DELETE FROM Customers

DROP TABLE Customers

CREATE TABLE [dbo].[Customers](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL UNIQUE,
	[PhoneNumber] [nvarchar](15) NULL
	)