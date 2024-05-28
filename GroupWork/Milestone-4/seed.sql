-- Seed file to populate Climber, FitnessTest, and FitnessDataEntry tables

-- Populate Climber table
SET IDENTITY_INSERT [Climber] ON;


INSERT INTO [Climber] (ID, ASPNetIdentityId, FirstName, LastName, UserName, DisplayName, Bio, ImageLink)
VALUES
    (1, '0', 'Tommy', 'Segovia', 'TomSeg1', 'Tommy', 'Bio for Tommy', 'image_link_for_tommy'),
    (2, '0', 'Jaron', 'Solomon', 'JarSol2', 'Jaron', 'Bio for Jaron', 'image_link_for_jaron'),
    (3, '0', 'Arthur', 'Kock', 'ArtKoc3', 'Arthur', 'Bio for Arthur', 'image_link_for_arthur'),
    (4, '0', 'Benjamin', 'Zuk', 'BenZuk4', 'Benjamin', 'Bio for Benjamin', 'image_link_for_benjamin'),
    (5, '0', 'Bastian', 'Cruzel', 'BasCru5', 'Bastian', 'Bio for Bastian', 'image_link_for_bastian');

SET IDENTITY_INSERT [Climber] OFF;

-- Populate FitnessTest table
SET IDENTITY_INSERT [FitnessTest] ON;

INSERT INTO [FitnessTest] (ID, Name, Description)
VALUES
    (0, 'Hang Test', 'Measuring how much weight can be added/subtracted from a 20mm edge and held for 7 seconds in half crimp'),
    (1, 'Pull Test', 'Measuring how much weight can be added/subtracted from a single pullup'),
    (2, 'Hammer Curl Test', 'Measuring how much weight can be lifted by each individual arm on the hammer curl'),
    (3, 'Hip Flexibility Test', 'Measuring the maximum distance in inches between the heels in the middle splits stretch'),
    (4, 'Hamstring Flexibility Test', 'Measuring the distance between fingers and toes in the seated hamstring sit and reach test'),
    (5, 'Repeater Test', 'Measuring the maximum time possible in a 7 second on, 3 second off, two arm repeater on a 20mm edge at 60% of a 7 second max hang on 20mm'),
    (6, 'Smallest Edge Test', 'Measuring the smallest mm edge that can be hung for 7 seconds with both arms'),
    (7, 'Campus Board Test', 'Measuring the maximum pull set distance that can be pulled on the moon regulation spacing with medium rung size');

SET IDENTITY_INSERT [FitnessTest] OFF;

-- Populate FitnessDataEntry table

-- Seed data for Hang Test (TestID: 0)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 0, 94, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 0, 112, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 0, 117, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 0, 115, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 0, 109, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 0, 70, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 0, 95, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00');

-- Seed data for Pull Test (TestID: 1)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 1, 80, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 1, 92, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 1, 95, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 1, 88, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 1, 78, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 1, 65, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 1, 85, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00');

-- Seed data for Hammer Curl Test (TestID: 2)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 2, 50, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 2, 58, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 2, 62, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 2, 55, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 2, 48, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 2, 40, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 2, 52, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00');

-- Seed data for Hip Flexibility Test (TestID: 3)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 3, 10, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 3, 11, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 3, 12, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 3, 9, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 3, 8, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 3, 7, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 3, 9, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00');

-- Seed data for Hamstring Flexibility Test (TestID: 4)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 4, 15, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 4, 17, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 4, 20, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 4, 12, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 4, 10, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 4, 8, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 4, 9, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00');

-- Seed data for Repeater Test (TestID: 5)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 5, 20, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 5, 22, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 5, 24, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 5, 18, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 5, 16, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 5, 14, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 5, 17, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00');

-- Seed data for Smallest Edge Test (TestID: 6)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 6, 5, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 6, 7, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 6, 8, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 6, 4, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 6, 3, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 6, 2, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 6, 6, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00');

-- Seed data for Campus Board Test (TestID: 7)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 7, 147, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 7, 135, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 7, 135, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 7, 159, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 7, 135, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 7, 135, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00');

--Seed data for CommunityGroup
SET IDENTITY_INSERT [CommunityGroup] ON;

INSERT INTO [CommunityGroup] (ID, OwnerID, Name, Description)
VALUES
    (1, 1, 'The RockBoxx', 'Rock climbing gym located in Salem, Oregon.');

SET IDENTITY_INSERT [CommunityGroup] OFF;

--Seed data for GroupList
SET IDENTITY_INSERT [GroupList] ON;

INSERT INTO [GroupList] (ID, ClimberID, CommunityGroupID)
VALUES
    (1, 1, 1)

SET IDENTITY_INSERT [GroupList] OFF;

--Add climber with ID = 1 to CommunityGroup with ID = 1
UPDATE [Climber]
SET [GroupListID] = 1
WHERE [ID] = 1;

--Seed tag data
SET IDENTITY_INSERT [Tag] ON;

INSERT INTO [Tag] (ID, TagName)
VALUES
    (1, 'Crimpy'),
    (2, 'Slopers'),
    (3, 'Pockets'),
    (4, 'Juggy'),
    (5, 'Pinches'),
    (6, 'Technical'),
    (7, 'Powerful'),
    (8, 'Compression'),
    (9, 'Highball'),
    (10, 'Slab'),
    (11, 'Tension'),
    (12, 'Pumpy'),
    (13, 'Dyno'),
    (14, 'Classic'),
    (15, 'Unique')

SET IDENTITY_INSERT [Tag] OFF;

SET IDENTITY_INSERT [CommunityMessage] ON;

INSERT INTO [CommunityMessage] (ID, ClimberID, CommunityGroupID, DisplayName, Message)
VALUES
    (1, 2, 1, 'Janet', 'Hello, this is a test comment!')

SET IDENTITY_INSERT [CommunityMessage] OFF;
--Seed climb attempt data
SET IDENTITY_INSERT [ClimbAttempt] ON;

INSERT INTO [ClimbAttempt] (ID, ClimberID, ClimbName, Attempts, Rating, ClimbId, SuggestedGrade, EntryDate)
VALUES
    (1, 1, 'Evergreen', 3, 5, '4af5ee1d-280a-5e8e-bb03-2528dc508792', 5, '2024-05-26T22:58:21.657'),
    (2, 1, 'The Mandala', 3, 5, 'c2bc20b8-eaca-54d6-a1ea-503a0031f0b6', 12, '2024-05-26T22:58:21.657'),
    (3, 1, 'Necromancer', 3, 5, '7c78d6fe-2e74-5d3a-b5e3-87e07ec100e7', 5.9, '2024-05-26T22:58:21.657'),
    (4, 1, 'Let''s Lay Back', 3, 5, '6dbaeb8f-b65f-5808-a71e-14d7d623396b', 4, '2024-05-26T22:58:21.657');

SET IDENTITY_INSERT [ClimbAttempt] OFF;

--Seed climb tag entry data
SET IDENTITY_INSERT [ClimbTagEntry] ON;

INSERT INTO [ClimbTagEntry] (ID, ClimbAttemptID, TagID)
VALUES
    (0, 1, 1),
    (1, 1, 3),
    (2, 1, 6),

    (3, 2, 1),
    (4, 2, 9),
    (5, 2, 7),
    (6, 2, 6),

    (7, 3, 3),
    (8, 3, 5),
    (9, 3, 8),

    (10, 4, 1),
    (11, 4, 6)
    
SET IDENTITY_INSERT [ClimbTagEntry] OFF;
