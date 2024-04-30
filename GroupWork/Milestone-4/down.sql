-- Drop foreign key constraints
ALTER TABLE [FitnessDataEntry] DROP CONSTRAINT [FK_FitnessDataEntry_Climber_ID];
ALTER TABLE [FitnessDataEntry] DROP CONSTRAINT [FK_FitnessDataEntry_Test_ID];
ALTER TABLE [ClimbAttempt] DROP CONSTRAINT [FK_ClimbAttempt_Climber_ID];

-- Drop tables
DROP TABLE [FitnessDataEntry];
DROP TABLE [FitnessTest];
DROP TABLE [Climber];
DROP TABLE [ClimbAttempt];