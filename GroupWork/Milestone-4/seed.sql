
SET IDENTITY_INSERT [Climber] ON;
INSERT INTO [Climber] (ID,ASPNetIdentityId,FirstName,LastName, UserName) VALUES (1, '0', 'Tommy', 'Segovia', 'TomSeg1');
INSERT INTO [Climber] (ID,ASPNetIdentityId,FirstName,LastName, UserName) VALUES (2, '0', 'Jaron', 'Solomon', 'JarSol2');
INSERT INTO [Climber] (ID,ASPNetIdentityId,FirstName,LastName, UserName) VALUES (3, '0', 'Arthur', 'Kock', 'ArtKoc3');
INSERT INTO [Climber] (ID,ASPNetIdentityId,FirstName,LastName, UserName) VALUES (4, '0', 'Benjamin', 'Zuk', 'BenZuk4');
INSERT INTO [Climber] (ID,ASPNetIdentityId,FirstName,LastName, UserName) VALUES (5, '0', 'Bastian', 'Cruzel', 'BasCru5');

SET IDENTITY_INSERT [Climber] OFF;

SET IDENTITY_INSERT [FitnessTest] ON;
INSERT INTO [FitnessTest] (ID,Name,Description) VALUES (0, 'Hang Test', 'Measuring how much weight can be added/subtracted from a 20mm edge and held for 7 seconds in half crimp');
INSERT INTO [FitnessTest] (ID,Name,Description) VALUES (1, 'Pull Test', 'Measuring how much weight can be added/subtracted from a single pullup');
INSERT INTO [FitnessTest] (ID,Name,Description) VALUES (2, 'Hammer Curl Test', 'Measuring how much weight can be lifted by each individual arm on the hammer curl');
INSERT INTO [FitnessTest] (ID,Name,Description) VALUES (3, 'Hip Flexibility Test', 'Measuring the maximum distance in inches between the heels in the middle splits stretch');
INSERT INTO [FitnessTest] (ID,Name,Description) VALUES (4, 'Hamstring Flexibility Test', 'Measuring the distance between fingers and toes in the seated hamstring sit and reach test');
INSERT INTO [FitnessTest] (ID,Name,Description) VALUES (5, 'Repeater Test', 'Measuring the maximum time possible in a 7 second on, 3 second off, two arm repeater on a 20mm edge at 60% of a 7 second max hang on 20mm');
INSERT INTO [FitnessTest] (ID,Name,Description) VALUES (6, 'Smallest Edge Test', 'Measuring the smallest mm edge that can be hung for 7 seconds with both arms');
INSERT INTO [FitnessTest] (ID,Name,Description) VALUES (7, 'Campus Board Test', 'Measuring the maximum pull set distance that can be pulled on the moon regulation spacing with medium rung size');


SET IDENTITY_INSERT [FitnessTest] OFF;

SET IDENTITY_INSERT [FitnessDataEntry] ON;
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (0, 1, 0, 94, 165, '2023-02-06 14:30:00');
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (1, 1, 0, 112, 175, '2023-06-10 14:30:00');
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (2, 1, 0, 117, 175, '2023-10-04 14:30:00');
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (3, 2, 0, 115, 135, '2024-02-14 10:30:00');
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (4, 3, 0, 109, 160, '2024-05-14 10:30:00');
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (5, 4, 0, 70, 170, '2024-06-14 10:30:00');
INSERT INTO [FitnessDataEntry] (ID,ClimberID,TestID,Result,BodyWeight,EntryDate) VALUES (6, 5, 0, 95, 148, '2024-06-14 10:30:00');
SET IDENTITY_INSERT [FitnessDataEntry] OFF;