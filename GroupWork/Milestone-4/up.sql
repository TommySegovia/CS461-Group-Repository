

CREATE TABLE [Climber] (
  [ID]        int           PRIMARY KEY IDENTITY(1, 1),
  [ASPNetIdentityId] nvarchar(450) NOT NULL,
  [FirstName]     nvarchar(255) NULL,
  [LastName]  nvarchar(255) NULL,
  [UserName] nvarchar(255) NOT NULL,
  [DisplayName] nvarchar(25) NULL, 
  [Bio] nvarchar(1600) NULL,
  [ImageLink] nvarchar(255) NULL,
  [CustomLink] nvarchar(255) NULL,
  [LinkText] nvarchar(255) NULL,
  [City] nvarchar(255) NULL,
  [State] nvarchar(255) NULL,
  [Age] int NULL,
  [GroupListID] int NULL
);

CREATE TABLE [GroupList](
  [ID]  int PRIMARY KEY IDENTITY(1,1),
  [ClimberID] int NOT NULL,
  [CommunityGroupID] int NOT NULL
)

CREATE TABLE [CommunityGroup](
  [ID] int PRIMARY KEY IDENTITY(1,1),
  [OwnerID] int,
  [Name] NVARCHAR(255) NOT NULL,
  [Description] NVARCHAR(1600) NULL
)

CREATE TABLE [CommunityMessage](
  [ID] int PRIMARY KEY IDENTITY(1,1),
  [ClimberID] int NOT NULL,
  [CommunityGroupID] int NOT NULL,
  [DisplayName] NVARCHAR(25),
  [Message] NVARCHAR(512) NOT NULL
)

CREATE TABLE [FitnessTest] (
  [ID]          int           PRIMARY KEY IDENTITY(1, 1),
  [Name]        nvarchar(255) NOT NULL,
  [Description] nvarchar(255)
);

CREATE TABLE [FitnessDataEntry] (
  [ID] int    PRIMARY KEY IDENTITY(1, 1),
  [ClimberID] int,
  [TestID]    int,
  [Result]    int,
  [BodyWeight]  int,
  [Age]       int,
  [Gender]    nvarchar(255),
  [ClimbingExperience]  NVARCHAR(255),
  [ClimbingGrade]     int,
  [EntryDate] datetime
);

CREATE TABLE [ClimbAttempt] (
  [ID]    int     PRIMARY KEY IDENTITY(1, 1),
  [ClimberID]     int,
  [ClimberName]   nvarchar(50),
  [ClimbName] nvarchar(200) NULL,
  [Attempts]   int, 
  [Rating]  int,
  [ClimbId]     nvarchar(50) NOT NULL, 
  [SuggestedGrade] nvarchar(8) NULL,
  [EntryDate] datetime
)

CREATE TABLE [Tag](
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [TagName] NVARCHAR(200)
)

CREATE TABLE [ClimbTagEntry] (
  [ID] int PRIMARY KEY IDENTITY(1, 1),
  [ClimbAttemptID] int,
  [TagID] int
);


ALTER TABLE [FitnessDataEntry] ADD CONSTRAINT [FK_FitnessDataEntry_Climber_ID] 
  FOREIGN KEY ([ClimberID]) REFERENCES [Climber] ([ID]);

ALTER TABLE [FitnessDataEntry] ADD CONSTRAINT [FK_FitnessDataEntry_Test_ID]
  FOREIGN KEY ([TestID]) REFERENCES [FitnessTest] ([ID]);

ALTER TABLE [ClimbAttempt] ADD CONSTRAINT [FK_ClimbAttempt_Climber_ID]
  FOREIGN KEY ([ClimberID]) REFERENCES [Climber] ([ID]);

ALTER TABLE [GroupList] ADD CONSTRAINT [FK_GroupList_Climber_ID] 
  FOREIGN KEY ([ClimberID]) REFERENCES [Climber] ([ID]);

ALTER TABLE [GroupList] ADD CONSTRAINT [FK_GroupList_CommunityGroup_ID] 
  FOREIGN KEY ([CommunityGroupID]) REFERENCES [CommunityGroup] ([ID]);

ALTER TABLE [CommunityGroup] ADD CONSTRAINT [FK_CommunityGroup_Climber_OwnerID] 
  FOREIGN KEY ([OwnerID]) REFERENCES [Climber] ([ID]);

ALTER TABLE [CommunityMessage] ADD CONSTRAINT [FK_CommunityMessage_Climber_ID]
  FOREIGN KEY ([ClimberID]) REFERENCES [Climber] ([ID]);

ALTER TABLE [CommunityMessage] ADD CONSTRAINT [FK_CommunityMessage_CommunityGroup_ID]
  FOREIGN KEY ([CommunityGroupID]) REFERENCES [CommunityGroup] ([ID]);

ALTER TABLE [ClimbTagEntry] ADD CONSTRAINT [FK_ClimbTagEntry_ClimbAttemptID]
FOREIGN KEY ([ClimbAttemptID]) REFERENCES [ClimbAttempt]([ID]);

ALTER TABLE [ClimbTagEntry] ADD CONSTRAINT [FK_ClimbTagEntry_TagID] 
FOREIGN KEY ([TagID]) REFERENCES [Tag]([ID]);

