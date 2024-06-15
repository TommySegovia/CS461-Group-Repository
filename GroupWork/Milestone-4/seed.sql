-- Seed file to populate Climber, FitnessTest, and FitnessDataEntry tables

-- Populate Climber table
SET IDENTITY_INSERT [Climber] ON;


INSERT INTO [Climber] (ID, ASPNetIdentityId, FirstName, LastName, UserName, DisplayName, Bio, ImageLink)
VALUES
    (1, '0', 'Tommy', 'Segovia', 'TheRealJasonMomoa', 'Tommy', 'Bio for Tommy', 'image_link_for_tommy'),
    (2, '0', 'Jaron', 'Solomon', 'Dynosaur', 'Jaron', 'Bio for Jaron', 'image_link_for_jaron'),
    (3, '0', 'Arthur', 'Kock', 'TheGoat22', 'Arthur', 'Bio for Arthur', 'image_link_for_arthur'),
    (4, '0', 'Benjamin', 'Zuk', 'TheLegend27', 'Benjamin', 'Bio for Benjamin', 'image_link_for_benjamin'),
    (5, '0', 'Bastian', 'Cruzel', 'MikeJorne', 'Bastian', 'Bio for Bastian', 'image_link_for_bastian'),
    (6, '0', 'Cameron', 'Diaz', 'CrimpGod', 'Cameron', 'Bio for Cameron', 'image_link_for_cameron'),
    (7, '0', 'Dylan', 'Efron', 'DylEfr7', 'Dylan', 'Bio for Dylan', 'image_link_for_dylan'),
    (8, '0', 'Ethan', 'Fisher', 'EthFis8', 'Ethan', 'Bio for Ethan', 'image_link_for_ethan'),
    (9, '0', 'Finn', 'Garcia', 'FinGar9', 'Finn', 'Bio for Finn', 'image_link_for_finn'),
    (10, '0', 'Gavin', 'Harris', 'GavHar10', 'Gavin', 'Bio for Gavin', 'image_link_for_gavin'),
    (11, '0', 'Hudson', 'Ivanov', 'HudIva11', 'Hudson', 'Bio for Hudson', 'image_link_for_hudson'),
    (12, '0', 'Isaac', 'Johnson', 'IsaJoh12', 'Isaac', 'Bio for Isaac', 'image_link_for_isaac'),
    (13, '0', 'Jasper', 'Khan', 'JasKha13', 'Jasper', 'Bio for Jasper', 'image_link_for_jasper'),
    (14, '0', 'Kai', 'Lopez', 'KaiLop14', 'Kai', 'Bio for Kai', 'image_link_for_kai'),
    (15, '0', 'Liam', 'Morgan', 'LiaMor15', 'Liam', 'Bio for Liam', 'image_link_for_liam'),
    (16, '0', 'Mason', 'Nelson', 'MasNel16', 'Mason', 'Bio for Mason', 'image_link_for_mason'),
    (17, '0', 'Noah', 'Olsen', 'NoaOls17', 'Noah', 'Bio for Noah', 'image_link_for_noah'),
    (18, '0', 'Oliver', 'Perez', 'OliPer18', 'Oliver', 'Bio for Oliver', 'image_link_for_oliver');

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
    (5, 0, 95, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00'),
    (6, 0, 75, 170, 28, 'Male', 'Intermediate', 8, '2023-02-06 14:30:00'),
    (7, 0, 65, 170, 26, 'Male', 'Intermediate', 7, '2023-06-10 14:30:00'),
    (8, 0, 60, 170, 24, 'Male', 'Intermediate', 7, '2023-10-04 14:30:00'),
    (9, 0, 55, 170, 22, 'Male', 'Intermediate', 6, '2024-02-14 10:30:00'),
    (10, 0, 45, 170, 20, 'Male', 'Intermediate', 6, '2024-05-14 10:30:00');

-- Seed data for Pull Test (TestID: 1)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 1, 80, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 1, 92, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 1, 95, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 1, 88, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 1, 78, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 1, 65, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 1, 85, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00'),
    (6, 1, 85, 170, 28, 'Male', 'Intermediate', 8, '2023-02-06 14:30:00'),
    (7, 1, 70, 170, 26, 'Male', 'Intermediate', 7, '2023-06-10 14:30:00'),
    (8, 1, 60, 170, 24, 'Male', 'Intermediate', 7, '2023-10-04 14:30:00'),
    (9, 1, 40, 170, 22, 'Male', 'Intermediate', 6, '2024-02-14 10:30:00'),
    (10, 1, 45, 170, 20, 'Male', 'Intermediate', 6, '2024-05-14 10:30:00');

-- Seed data for Hammer Curl Test (TestID: 2)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 2, 50, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 2, 58, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 2, 62, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 2, 55, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 2, 48, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 2, 40, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 2, 52, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00'),
    (6, 2, 45, 170, 28, 'Male', 'Intermediate', 8, '2023-10-04 14:30:00'),
    (7, 2, 40, 170, 26, 'Male', 'Intermediate', 7, '2024-02-14 10:30:00'),
    (8, 2, 35, 170, 24, 'Male', 'Intermediate', 7, '2024-05-14 10:30:00'),
    (9, 2, 30, 170, 22, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00'),
    (10, 2, 30, 170, 20, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00');

-- Seed data for Hip Flexibility Test (TestID: 3)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 3, 70, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 3, 65, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 3, 72, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 3, 71, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 3, 65, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 3, 63, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 3, 74, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00'),
    (6, 3, 60, 170, 28, 'Male', 'Intermediate', 8, '2023-10-04 14:30:00'),
    (7, 3, 55, 170, 26, 'Male', 'Intermediate', 7, '2024-02-14 10:30:00'),
    (8, 3, 55, 170, 24, 'Male', 'Intermediate', 7, '2024-05-14 10:30:00'),
    (9, 3, 50, 170, 22, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00'),
    (10, 3, 50, 170, 20, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00');

-- Seed data for Hamstring Flexibility Test (TestID: 4)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 4, 2, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 4, 4, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 4, 5, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 4, 5, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 4, 1, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 4, 2, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 4, 3, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00'),
    (6, 4, 2, 170, 28, 'Male', 'Intermediate', 8, '2023-10-04 14:30:00'),
    (7, 4, 1, 170, 26, 'Male', 'Intermediate', 7, '2024-02-14 10:30:00'),
    (8, 4, 1, 170, 24, 'Male', 'Intermediate', 7, '2024-05-14 10:30:00'),
    (9, 4, 0, 170, 22, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00'),
    (10, 4, 0, 170, 20, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00');

-- Seed data for Repeater Test (TestID: 5)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 5, 140, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 5, 150, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 5, 155, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 5, 170, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 5, 130, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 5, 130, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 5, 180, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00'),
    (6, 5, 120, 170, 28, 'Male', 'Intermediate', 8, '2023-10-04 14:30:00'),
    (7, 5, 110, 170, 26, 'Male', 'Intermediate', 7, '2024-02-14 10:30:00'),
    (8, 5, 100, 170, 24, 'Male', 'Intermediate', 7, '2024-05-14 10:30:00'),
    (9, 5, 90, 170, 22, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00'),
    (10, 5, 80, 170, 20, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00');

-- Seed data for Smallest Edge Test (TestID: 6)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 6, 8, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 6, 8, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 6, 8, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 6, 6, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 6, 10, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 6, 10, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 6, 6, 148, 23, 'Male', 'Advanced', 12, '2024-06-14 10:30:00'),
    (6, 6, 10, 170, 28, 'Male', 'Intermediate', 8, '2023-10-04 14:30:00'),
    (7, 6, 12, 170, 26, 'Male', 'Intermediate', 7, '2024-02-14 10:30:00'),
    (8, 6, 14, 170, 24, 'Male', 'Intermediate', 7, '2024-05-14 10:30:00'),
    (9, 6, 16, 170, 22, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00'),
    (10, 6, 20, 170, 20, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00');

-- Seed data for Campus Board Test (TestID: 7)
INSERT INTO [FitnessDataEntry] (ClimberID, TestID, Result, BodyWeight, Age, Gender, ClimbingExperience, ClimbingGrade, EntryDate)
VALUES
    (1, 7, 147, 165, 22, 'Male', 'Advanced', 10, '2023-02-06 14:30:00'),
    (1, 7, 147, 175, 22, 'Male', 'Advanced', 10, '2023-06-10 14:30:00'),
    (1, 7, 135, 175, 22, 'Male', 'Advanced', 10, '2023-10-04 14:30:00'),
    (2, 7, 147, 135, 24, 'Male', 'Advanced', 11, '2024-02-14 10:30:00'),
    (3, 7, 135, 160, 27, 'Male', 'Advanced', 9, '2024-05-14 10:30:00'),
    (4, 7, 135, 170, 21, 'Male', 'Advanced', 9, '2024-06-14 10:30:00'),
    (5, 7, 159, 175, 22, 'Male', 'Advanced', 12, '2023-10-04 14:30:00'),
    (6, 7, 135, 170, 28, 'Male', 'Intermediate', 8, '2024-02-14 10:30:00'),
    (7, 7, 135, 170, 26, 'Male', 'Intermediate', 7, '2024-05-14 10:30:00'),
    (8, 7, 135, 170, 24, 'Male', 'Intermediate', 7, '2024-06-14 10:30:00'),
    (9, 7, 123, 170, 22, 'Male', 'Intermediate', 6, '2024-05-14 10:30:00'),
    (10, 7, 123, 170, 20, 'Male', 'Intermediate', 6, '2024-06-14 10:30:00');

--Seed data for CommunityGroup
SET IDENTITY_INSERT [CommunityGroup] ON;

INSERT INTO [CommunityGroup] (ID, OwnerID, Name, Description)
VALUES
    (1, 1, 'The RockBoxx', 'Rock climbing gym located in Salem, Oregon.'),
    (2, 2, 'The Bishop Co.', 'Rock climbing coalition located in Bishop, California.');

SET IDENTITY_INSERT [CommunityGroup] OFF;

--Seed data for GroupList
SET IDENTITY_INSERT [GroupList] ON;

INSERT INTO [GroupList] (ID, ClimberID, CommunityGroupID)
VALUES
    (1, 18, 1),
    (2, 1, 2),
    (3, 2, 2),
    (4, 3, 2),
    (5, 4, 2),
    (6, 5, 2),
    (7, 6, 2);

SET IDENTITY_INSERT [GroupList] OFF;

--Add climber with ID = 1 to CommunityGroup with ID = 1
UPDATE [Climber]
SET [GroupListID] = 1
WHERE [ID] = 18;

UPDATE [Climber]
SET [GroupListID] = 2
WHERE [ID] between 2 and 7;

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
    (1, 2, 1, 'Janet', 'Wow this gym is so cool!'),
    (2, 3, 2, 'John', 'Dont crush the brush! <3'),
    (3, 4, 2, 'Jill', 'Why is the rubber tester so sandbagged??? V0???'),
    (4, 5, 2, 'John', 'I just sent my first V5!'),
    (5, 6, 2, 'Mike', 'Jill, I can help give you some tips if you''re out this weekend.'),
    (6, 7, 2, 'Ben', 'I''m looking for some new friends to climb with, mind if I tag along?'),
    (7, 8, 2, 'Jill', 'I''m down!'),
    (8, 9, 2, 'Jason', 'Guys wtf happened to the bathroom? Like did you have a blindfold on???'),
    (9, 10, 2, 'Mike', 'Fundraiser at the carwash this weekend, try to invite people if you can!');

SET IDENTITY_INSERT [CommunityMessage] OFF;
--Seed climb attempt data
SET IDENTITY_INSERT [ClimbAttempt] ON;

INSERT INTO [ClimbAttempt] (ID, ClimberID, ClimberName, ClimbName, Attempts, Rating, ClimbId, SuggestedGrade, EntryDate)
VALUES
--funny ones
    (1, 2,'John', 'Stinky Buddy', 6, 5, '73cc011b-61e7-535b-aadb-7f4373cc6119', '5.9+', '2024-05-26T22:58:21.657'),
    (2, 2,'Mike', 'Riding the Underbelly of a Shark', 3, 4, '95f2b835-ae43-597c-ab43-4c62c0598422', 'V6', '2024-05-26T22:58:21.657'),
    (3, 2,'Jason', 'Potato Peel', 7, 5, 'dcb2e943-e8df-5cf2-bed7-278c45ec3021', '5.10a', '2024-05-26T22:58:21.657'),
    (4, 2,'Mike', 'slice of cheese', 3, 5, 'f6d51d45-f88f-5f41-b772-0020504b5bac', 'V2', '2024-05-26T22:58:21.657'),
    (5, 2,'Jill', 'Her Dreads Are Lumpy', 1, 1, 'ba6e33cf-3da4-5971-b17c-01cdb44badc4', 'V1', '2024-05-26T22:58:21.657'),

    (6, 18,'Bill', 'Poopship Destroyer', 1, 5, '88ef716f-0cb1-5972-b09f-9b17ee139b65', 'V7', '2024-05-26T22:58:21.657'),
    (7, 18,'Keenan', 'The Mandala', 1, 5, 'c2bc20b8-eaca-54d6-a1ea-503a0031f0b6', 'V12', '2024-05-26T22:58:21.657'),
    (8, 18,'Aaron', 'Necromancer', 1, 5, '7c78d6fe-2e74-5d3a-b5e3-87e07ec100e7', '5.9', '2024-05-26T22:58:21.657'),
    (9, 18,'Josiah', 'Let''s Lay Back', 1, 5, '6dbaeb8f-b65f-5808-a71e-14d7d623396b', 'V4', '2024-05-26T22:58:21.657'),
    (10, 18,'Joe', 'Evergreen', 3, 3, '4af5ee1d-280a-5e8e-bb03-2528dc508792', 'V5', '2024-05-26T22:58:21.657'),
    (11, 18, 'Ayden', 'Naughty Karate', 20, 4, 'dbb4d947-bd27-5df7-95a5-6a212a06e530', 'V10', '2024-05-26T22:58:21.657');

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
    (11, 4, 6),
    (12, 4, 7),

    (13, 5, 2),
    (14, 5, 4),
    (15, 5, 6),

    (16, 6, 1),
    (17, 6, 3),
    (18, 6, 12),

    (19, 7, 1),
    (20, 7, 6),
    (21, 7, 13),

    (22, 8, 3),
    (23, 8, 7),
    (24, 8, 14),

    (25, 9, 4),
    (26, 9, 9),
    (27, 9, 15),

    (28, 10, 1),
    (29, 10, 2),
    (30, 10, 15),

    (31, 11, 1),
    (32, 11, 6),
    (33, 11, 7);
    
SET IDENTITY_INSERT [ClimbTagEntry] OFF;
