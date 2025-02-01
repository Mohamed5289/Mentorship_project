IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'security') IS NULL EXEC(N'CREATE SCHEMA [security];');
GO

CREATE TABLE [security].[Roles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [security].[Users] (
    [Id] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [ProfilePicture] nvarchar(max) NULL,
    [Bio] nvarchar(max) NULL,
    [DateOfBirth] date NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [security].[RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [security].[Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [security].[RefreshToken] (
    [AppUserId] nvarchar(450) NOT NULL,
    [Id] int NOT NULL IDENTITY,
    [Token] nvarchar(max) NOT NULL,
    [Expires] datetime2 NOT NULL,
    [Created] datetime2 NOT NULL,
    [Revoked] datetime2 NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([AppUserId], [Id]),
    CONSTRAINT [FK_RefreshToken_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [security].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [security].[UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [security].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [security].[UserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [security].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [security].[UserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [security].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [security].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [security].[UserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [security].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_RoleClaims_RoleId] ON [security].[RoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [security].[Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_UserClaims_UserId] ON [security].[UserClaims] ([UserId]);
GO

CREATE INDEX [IX_UserLogins_UserId] ON [security].[UserLogins] ([UserId]);
GO

CREATE INDEX [IX_UserRoles_RoleId] ON [security].[UserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [security].[Users] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [security].[Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250129134932_Initial', N'8.0.12');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[security].[Roles]'))
    SET IDENTITY_INSERT [security].[Roles] ON;
INSERT INTO [security].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
VALUES (N'a7106b01-6c45-4d32-bb20-ea9d034fbf0e', N'Mentee', N'MENTEE', N'5dca5dfc-b149-42a3-ab11-d9ce05f7844e');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[security].[Roles]'))
    SET IDENTITY_INSERT [security].[Roles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[security].[Roles]'))
    SET IDENTITY_INSERT [security].[Roles] ON;
INSERT INTO [security].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
VALUES (N'937b4e99-1ff5-461f-89fa-64aec1448985', N'Admin', N'ADMIN', N'6109100b-d3b2-4a26-a44f-223bc649f444');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[security].[Roles]'))
    SET IDENTITY_INSERT [security].[Roles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[security].[Roles]'))
    SET IDENTITY_INSERT [security].[Roles] ON;
INSERT INTO [security].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
VALUES (N'82aef2ff-9ade-4e64-83ba-1d5f0a3abc49', N'Mentor', N'MENTOR', N'12a85256-cf3d-4bbd-8f3e-6dd1de974ece');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'NormalizedName', N'ConcurrencyStamp') AND [object_id] = OBJECT_ID(N'[security].[Roles]'))
    SET IDENTITY_INSERT [security].[Roles] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250129191343_SeedRoles', N'8.0.12');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[security].[Users]') AND [c].[name] = N'UserName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [security].[Users] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [security].[Users] SET [UserName] = N'' WHERE [UserName] IS NULL;
ALTER TABLE [security].[Users] ALTER COLUMN [UserName] nvarchar(50) NOT NULL;
ALTER TABLE [security].[Users] ADD DEFAULT N'' FOR [UserName];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[security].[Users]') AND [c].[name] = N'PhoneNumber');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [security].[Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [security].[Users] ALTER COLUMN [PhoneNumber] nvarchar(20) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[security].[Users]') AND [c].[name] = N'PasswordHash');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [security].[Users] DROP CONSTRAINT [' + @var2 + '];');
UPDATE [security].[Users] SET [PasswordHash] = N'' WHERE [PasswordHash] IS NULL;
ALTER TABLE [security].[Users] ALTER COLUMN [PasswordHash] nvarchar(max) NOT NULL;
ALTER TABLE [security].[Users] ADD DEFAULT N'' FOR [PasswordHash];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[security].[Users]') AND [c].[name] = N'LastName');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [security].[Users] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [security].[Users] ALTER COLUMN [LastName] nvarchar(50) NOT NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[security].[Users]') AND [c].[name] = N'FirstName');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [security].[Users] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [security].[Users] ALTER COLUMN [FirstName] nvarchar(50) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[security].[Users]') AND [c].[name] = N'Email');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [security].[Users] DROP CONSTRAINT [' + @var5 + '];');
UPDATE [security].[Users] SET [Email] = N'' WHERE [Email] IS NULL;
ALTER TABLE [security].[Users] ALTER COLUMN [Email] nvarchar(50) NOT NULL;
ALTER TABLE [security].[Users] ADD DEFAULT N'' FOR [Email];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[security].[Users]') AND [c].[name] = N'DateOfBirth');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [security].[Users] DROP CONSTRAINT [' + @var6 + '];');
UPDATE [security].[Users] SET [DateOfBirth] = '0001-01-01' WHERE [DateOfBirth] IS NULL;
ALTER TABLE [security].[Users] ALTER COLUMN [DateOfBirth] date NOT NULL;
ALTER TABLE [security].[Users] ADD DEFAULT '0001-01-01' FOR [DateOfBirth];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[security].[Users]') AND [c].[name] = N'Bio');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [security].[Users] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [security].[Users] ALTER COLUMN [Bio] nvarchar(500) NULL;
GO

CREATE TABLE [Admin] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [AppUserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Admin] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Admin_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [security].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Mentee] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [AppUserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Mentee] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Mentee_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [security].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Mentor] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [AppUserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Mentor] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Mentor_Users_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [security].[Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Mentorship] (
    [MentorshipId] int NOT NULL IDENTITY,
    [Discription] nvarchar(max) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [status] nvarchar(max) NOT NULL,
    [Hours] int NOT NULL,
    [StartDate] date NOT NULL,
    [EndDate] date NOT NULL,
    [MentorId] int NOT NULL,
    CONSTRAINT [PK_Mentorship] PRIMARY KEY ([MentorshipId]),
    CONSTRAINT [FK_Mentorship_Mentor_MentorId] FOREIGN KEY ([MentorId]) REFERENCES [Mentor] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Achievement] (
    [Id] int NOT NULL,
    [MentorshipId] int NOT NULL,
    [MenteeId] int NOT NULL,
    [Score] int NOT NULL,
    [AverageRating] int NOT NULL,
    [Feedback] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Achievement] PRIMARY KEY ([MenteeId], [Id], [MentorshipId]),
    CONSTRAINT [FK_Achievement_Mentee_MenteeId] FOREIGN KEY ([MenteeId]) REFERENCES [Mentee] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Achievement_Mentorship_MentorshipId] FOREIGN KEY ([MentorshipId]) REFERENCES [Mentorship] ([MentorshipId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [MentorshipRegistration] (
    [MentorshipRegistrationId] int NOT NULL IDENTITY,
    [RegistrationDate] date NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [MentorshipId] int NOT NULL,
    [MenteeId] int NOT NULL,
    [MentorId] int NOT NULL,
    CONSTRAINT [PK_MentorshipRegistration] PRIMARY KEY ([MentorshipRegistrationId]),
    CONSTRAINT [FK_MentorshipRegistration_Mentee_MenteeId] FOREIGN KEY ([MenteeId]) REFERENCES [Mentee] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_MentorshipRegistration_Mentor_MentorId] FOREIGN KEY ([MentorId]) REFERENCES [Mentor] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_MentorshipRegistration_Mentorship_MentorshipId] FOREIGN KEY ([MentorshipId]) REFERENCES [Mentorship] ([MentorshipId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Task] (
    [TaskId] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Deadline] date NOT NULL,
    [CreationTime] date NOT NULL,
    [MentorshipId] int NOT NULL,
    [MenteeId] int NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY ([TaskId]),
    CONSTRAINT [FK_Task_Mentee_MenteeId] FOREIGN KEY ([MenteeId]) REFERENCES [Mentee] ([Id]),
    CONSTRAINT [FK_Task_Mentorship_MentorshipId] FOREIGN KEY ([MentorshipId]) REFERENCES [Mentorship] ([MentorshipId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [TaskSubmission] (
    [TaskSubmissionId] int NOT NULL IDENTITY,
    [Solution] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Grade] int NOT NULL,
    [DueDate] date NOT NULL,
    [Feedback] nvarchar(max) NOT NULL,
    [TaskId] int NOT NULL,
    [MenteeId] int NOT NULL,
    CONSTRAINT [PK_TaskSubmission] PRIMARY KEY ([TaskSubmissionId]),
    CONSTRAINT [FK_TaskSubmission_Mentee_MenteeId] FOREIGN KEY ([MenteeId]) REFERENCES [Mentee] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TaskSubmission_Task_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Task] ([TaskId]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Achievement_MentorshipId] ON [Achievement] ([MentorshipId]);
GO

CREATE UNIQUE INDEX [IX_Admin_AppUserId] ON [Admin] ([AppUserId]);
GO

CREATE UNIQUE INDEX [IX_Mentee_AppUserId] ON [Mentee] ([AppUserId]);
GO

CREATE UNIQUE INDEX [IX_Mentor_AppUserId] ON [Mentor] ([AppUserId]);
GO

CREATE INDEX [IX_Mentorship_MentorId] ON [Mentorship] ([MentorId]);
GO

CREATE INDEX [IX_MentorshipRegistration_MenteeId] ON [MentorshipRegistration] ([MenteeId]);
GO

CREATE INDEX [IX_MentorshipRegistration_MentorId] ON [MentorshipRegistration] ([MentorId]);
GO

CREATE INDEX [IX_MentorshipRegistration_MentorshipId] ON [MentorshipRegistration] ([MentorshipId]);
GO

CREATE INDEX [IX_Task_MenteeId] ON [Task] ([MenteeId]);
GO

CREATE INDEX [IX_Task_MentorshipId] ON [Task] ([MentorshipId]);
GO

CREATE INDEX [IX_TaskSubmission_MenteeId] ON [TaskSubmission] ([MenteeId]);
GO

CREATE INDEX [IX_TaskSubmission_TaskId] ON [TaskSubmission] ([TaskId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250201112137_CreateTables', N'8.0.12');
GO

COMMIT;
GO

