CREATE TABLE [Climber] (
  [ID]        int           PRIMARY KEY IDENTITY(1, 1),
  [Email]     nvarchar(255) NOT NULL,
  [Password]  nvarchar(255) NOT NULL
);

CREATE TABLE [FitnessTest] (
  [ID]          int           PRIMARY KEY IDENTITY(1, 1),
  [Name]        nvarchar(255) NOT NULL,
  [Description] nvarchar(255)
);

CREATE TABLE [FitnessDataEntry] (
  [ID] int    PRIMARY KEY IDENTITY(1, 1),
  [ClimberID] int,
  [TestID]    int,
  [Result]    nvarchar(255),
  [EntryDate] datetime
);

ALTER TABLE [FitnessDataEntry] ADD CONSTRAINT [FK_FitnessDataEntry_Climber_ID] 
  FOREIGN KEY ([ClimberID]) REFERENCES [Climber] ([ID]);

ALTER TABLE [FitnessTest] ADD CONSTRAINT [FK_FitnessDataEntry_Test_ID]
  FOREIGN KEY ([ID]) REFERENCES [FitnessDataEntry] ([TestID]);