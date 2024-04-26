BEGIN TRANSACTION;
GO

-- Delete custom user data columns from AspNetUsers table
ALTER TABLE [AspNetUsers] DROP COLUMN [Age];
ALTER TABLE [AspNetUsers] DROP COLUMN [ClimbingExperience];
ALTER TABLE [AspNetUsers] DROP COLUMN [Gender];
ALTER TABLE [AspNetUsers] DROP COLUMN [Height];
ALTER TABLE [AspNetUsers] DROP COLUMN [MaxClimbGrade];
ALTER TABLE [AspNetUsers] DROP COLUMN [Weight];
GO

-- Delete custom user data migration record from __EFMigrationsHistory table
DELETE FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240304121925_CustomUserData';
GO

-- Delete the initial schema creation records from __EFMigrationsHistory table
DELETE FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema';
GO

-- Drop foreign key constraints before dropping tables
ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId];
ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId];
ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId];
ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId];
ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId];
ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId];
GO

-- Drop tables in reverse order of creation
DROP TABLE [AspNetUserTokens];
DROP TABLE [AspNetUserRoles];
DROP TABLE [AspNetUserLogins];
DROP TABLE [AspNetUserClaims];
DROP TABLE [AspNetRoleClaims];
DROP TABLE [AspNetRoles];
DROP TABLE [AspNetUsers];
DROP TABLE [__EFMigrationsHistory];
GO

-- Finalize transaction
COMMIT;
GO
