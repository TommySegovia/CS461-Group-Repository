-- Drop foreign key constraints
ALTER TABLE [FitnessDataEntry] DROP CONSTRAINT [FK_FitnessDataEntry_Climber_ID];
ALTER TABLE [FitnessTest] DROP CONSTRAINT [FK_FitnessDataEntry_Test_ID];

-- Drop tables
DROP TABLE [FitnessDataEntry];
DROP TABLE [FitnessTest];
DROP TABLE [Climber];