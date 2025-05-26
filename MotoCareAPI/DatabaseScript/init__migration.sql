IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MotoCare')
BEGIN
    CREATE DATABASE MotoCare;
END
GO

USE MotoCare;
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'people')
    EXEC('CREATE SCHEMA people');
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'services')
    EXEC('CREATE SCHEMA services');
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'schedule')
    EXEC('CREATE SCHEMA schedule');
GO

BEGIN TRANSACTION;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'people' AND TABLE_NAME = 'Customer')
BEGIN
    CREATE TABLE people.Customer (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FirstName VARCHAR(50),
        LastName VARCHAR(50),
        PhoneNumber VARCHAR(15),
        Email VARCHAR(255),
        Note VARCHAR(MAX)
    );
    CREATE INDEX idx_email ON people.Customer(Email);
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'people' AND TABLE_NAME = 'Car')
BEGIN
    CREATE TABLE people.Car (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Brand VARCHAR(50),
        Model VARCHAR(50),
        LicensePlate VARCHAR(10) UNIQUE,
        Year INT,
        CustomerId INT NOT NULL,
        FOREIGN KEY (CustomerId) REFERENCES people.Customer(Id)
    );
    CREATE UNIQUE INDEX idx_license_plate ON people.Car(LicensePlate);
    CREATE INDEX idx_car_customer ON people.Car(CustomerId);
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'services' AND TABLE_NAME = 'Service')
BEGIN
    CREATE TABLE services.Service (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(30),
        Description VARCHAR(255),
        Price DECIMAL(9,2),
        LastPriceUpdate DATETIME
    );
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'services' AND TABLE_NAME = 'ServiceDetails')
BEGIN
    CREATE TABLE services.ServiceDetails (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Note VARCHAR(255),
        PriorityLevel VARCHAR(20),
        DiscountAvailability VARCHAR(50),
        ServiceId INT NOT NULL,
        FOREIGN KEY (ServiceId) REFERENCES services.Service(Id)
    );
    CREATE INDEX idx_service_Service ON services.ServiceDetails(ServiceId);
END
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'services' AND TABLE_NAME = 'RequiredPart')
BEGIN
    CREATE TABLE services.RequiredPart (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ServiceId INT NOT NULL,
        PartName VARCHAR(50),
        FOREIGN KEY (ServiceId) REFERENCES services.Service(Id)
    );
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'schedule' AND TABLE_NAME = 'Appointment')
BEGIN
    CREATE TABLE schedule.Appointment (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title VARCHAR(50),
        Description VARCHAR(MAX),
        CreatedDate DATETIME,
        CustomerId INT NOT NULL,
        CarId INT NOT NULL,
        Status VARCHAR(20),
        FOREIGN KEY (CustomerId) REFERENCES people.Customer(Id),
        FOREIGN KEY (CarId) REFERENCES people.Car(Id)
    );
    CREATE INDEX idx_appointment_customer ON schedule.Appointment(CustomerId);
    CREATE INDEX idx_appointment_car ON schedule.Appointment(CarId);
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'schedule' AND TABLE_NAME = 'AppointmentService')
BEGIN
    CREATE TABLE schedule.AppointmentService (
        AppointmentId INT NOT NULL,
        ServiceId INT NOT NULL,
        PRIMARY KEY (AppointmentId, ServiceId),
        FOREIGN KEY (AppointmentId) REFERENCES schedule.Appointment(Id),
        FOREIGN KEY (ServiceId) REFERENCES services.Service(Id)
    );
    CREATE INDEX idx_appointmentservice_appointment ON schedule.AppointmentService(AppointmentId);
    CREATE INDEX idx_appointmentservice_service ON schedule.AppointmentService(ServiceId);
END
GO

COMMIT;
