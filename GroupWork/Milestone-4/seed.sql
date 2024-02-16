
SET IDENTITY_INSERT [Climber] ON;
INSERT INTO [Climber] (ID,ASPNetIdentityId,FirstName,LastName) VALUES (0, '0', 'Jim', 'Bob');
INSERT INTO [Climber] (ID,ASPNetIdentityId,FirstName,LastName) VALUES (1, '0', 'Captain', 'Crunch');
SET IDENTITY_INSERT [Climber] OFF;

SET IDENTITY_INSERT [FitnessTest] ON;
INSERT INTO [FitnessTest] (ID,Name,Description) VALUES (0, 'Hang Test', 'Measuring how long one can hang from hand strength alone');
SET IDENTITY_INSERT [FitnessTest] OFF;

SET IDENTITY_INSERT [FitnessDataEntry] ON;
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,EntryDate) VALUES (0, 0, 0, 'Success: lasted 20 seconds', '2024-02-07 14:30:00');
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,EntryDate) VALUES (1, 1, 0, 'Success: lasted 2 minutes', '1956-07-02 10:30:00');
SET IDENTITY_INSERT [FitnessDataEntry] OFF;