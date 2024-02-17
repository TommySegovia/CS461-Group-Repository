
SET IDENTITY_INSERT [Climber] ON;
INSERT INTO [Climber] (ID,ASPNetIdentityId,FirstName,LastName) VALUES (0, '0', 'Tommy', 'Segovia');
INSERT INTO [Climber] (ID,ASPNetIdentityId,FirstName,LastName) VALUES (1, '0', 'Jaron', 'Solomon');
SET IDENTITY_INSERT [Climber] OFF;

SET IDENTITY_INSERT [FitnessTest] ON;
INSERT INTO [FitnessTest] (ID,Name,Description) VALUES (0, 'Hang Test', 'Measuring how much weight can be added/subtracted from a 20mm edge and held for 7 seconds in half crimp');
SET IDENTITY_INSERT [FitnessTest] OFF;

SET IDENTITY_INSERT [FitnessDataEntry] ON;
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (0, 0, 0, 94, 165, '2023-02-06 14:30:00');
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (1, 0, 0, 112, 175, '2023-06-10 14:30:00');
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (2, 0, 0, 117, 175, '2023-10-04 14:30:00');
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (3, 1, 0, 115, 135, '2024-02-14 10:30:00');
SET IDENTITY_INSERT [FitnessDataEntry] OFF;