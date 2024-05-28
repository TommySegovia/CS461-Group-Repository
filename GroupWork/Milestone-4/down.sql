-- Drop foreign key constraints
ALTER TABLE [CommunityMessage] DROP CONSTRAINT [FK_CommunityMessage_Climber_ID];
ALTER TABLE [CommunityMessage] DROP CONSTRAINT [FK_CommunityMessage_CommunityGroup_ID];
ALTER TABLE [ClimbTagEntry] DROP CONSTRAINT [FK_ClimbTagEntry_ClimbAttemptID];
ALTER TABLE [ClimbTagEntry] DROP CONSTRAINT [FK_ClimbTagEntry_TagID];
ALTER TABLE [FitnessDataEntry] DROP CONSTRAINT [FK_FitnessDataEntry_Climber_ID];
ALTER TABLE [FitnessDataEntry] DROP CONSTRAINT [FK_FitnessDataEntry_Test_ID];
ALTER TABLE [ClimbAttempt] DROP CONSTRAINT [FK_ClimbAttempt_Climber_ID];
ALTER TABLE [GroupList] DROP CONSTRAINT [FK_GroupList_Climber_ID];
ALTER TABLE [GroupList] DROP CONSTRAINT [FK_GroupList_CommunityGroup_ID];
ALTER TABLE [CommunityGroup] DROP CONSTRAINT [FK_CommunityGroup_Climber_OwnerID];

-- Drop tables
DROP TABLE [ClimbTagEntry];
DROP TABLE [Tag];
DROP TABLE [FitnessDataEntry];
DROP TABLE [FitnessTest];
DROP TABLE [ClimbAttempt];
DROP TABLE [GroupList];
DROP TABLE [CommunityGroup];
DROP TABLE [Climber];
DROP TABLE [CommunityMessage];
