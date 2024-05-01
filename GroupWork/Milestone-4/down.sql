-- Drop foreign key constraints
ALTER TABLE [FitnessDataEntry] DROP CONSTRAINT [FK_FitnessDataEntry_Climber_ID];
ALTER TABLE [FitnessDataEntry] DROP CONSTRAINT [FK_FitnessDataEntry_Test_ID];
ALTER TABLE [ClimbAttempt] DROP CONSTRAINT [FK_ClimbAttempt_Climber_ID];
ALTER TABLE [GroupList] DROP CONSTRAINT [FK_GroupList_Climber_ID];
ALTER TABLE [GroupList] DROP CONSTRAINT [FK_GroupList_CommunityGroup_ID];
ALTER TABLE [CommunityGroup] DROP CONSTRAINT [FK_CommunityGroup_Climber_OwnerID];

-- Drop tables
DROP TABLE [FitnessDataEntry];
DROP TABLE [FitnessTest];
DROP TABLE [Climber];
DROP TABLE [ClimbAttempt];
DROP TABLE [GroupList];
DROP TABLE [CommunityGroup];

