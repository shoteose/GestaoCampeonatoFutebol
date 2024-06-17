
create database GestaoCampeonatoFutebol

-------------
USE GestaoCampeonatoFutebol
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------
CREATE TABLE [dbo].[__EFMigrationsHistory] (
    [MigrationId]    NVARCHAR (150) NOT NULL,
    [ProductVersion] NVARCHAR (32)  NOT NULL
);

CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (450)     NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[AspNetUsers]([NormalizedEmail] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);


GO
ALTER TABLE [dbo].[AspNetUsers]
    ADD CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC);

-----------------------------------------------------------------------------------
CREATE TABLE [dbo].[AspNetUserTokens] (
    [UserId]        NVARCHAR (450) NOT NULL,
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [Name]          NVARCHAR (128) NOT NULL,
    [Value]         NVARCHAR (MAX) NULL
);


---------------------------------------------------------------------------------
CREATE TABLE [dbo].[AspNetRoles] (
    [Id]               NVARCHAR (450) NOT NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([NormalizedName] ASC) WHERE ([NormalizedName] IS NOT NULL);


GO
ALTER TABLE [dbo].[AspNetRoles]
    ADD CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC);






	--------------------------------------------------------------------------------
		--QUERY CRIAR User Claims

CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (450) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);


GO
ALTER TABLE [dbo].[AspNetUserClaims]
    ADD CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[AspNetUserClaims]
    ADD CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;

		--------------------------------------------------------------------------------
		--Query create Role Claims
CREATE TABLE [dbo].[AspNetRoleClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [RoleId]     NVARCHAR (450) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId]
    ON [dbo].[AspNetRoleClaims]([RoleId] ASC);
GO
ALTER TABLE [dbo].[AspNetRoleClaims]
    ADD CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[AspNetRoleClaims]
    ADD CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE;
--------------------------------------------------------------------------------
	--QUERY CRIAR User logins

CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider]       NVARCHAR (128) NOT NULL,
    [ProviderKey]         NVARCHAR (128) NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX) NULL,
    [UserId]              NVARCHAR (450) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);


GO
ALTER TABLE [dbo].[AspNetUserLogins]
    ADD CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC);


GO
ALTER TABLE [dbo].[AspNetUserLogins]
    ADD CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;

	--------------------------------------------------------------------------------
		--QUERY CRIAR UserRoles

CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (450) NOT NULL,
    [RoleId] NVARCHAR (450) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);


GO
ALTER TABLE [dbo].[AspNetUserRoles]
    ADD CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC);


GO
ALTER TABLE [dbo].[AspNetUserRoles]
    ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE;


GO
ALTER TABLE [dbo].[AspNetUserRoles]
    ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


	--------------------------------------------------------------------
	--QUERY CRIAR Arbitro

CREATE TABLE [dbo].[Arbitros] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Nome]  NVARCHAR (MAX) NOT NULL,
    [Idade] INT            NOT NULL
);


----------------------------------------------------------------------------------------------------------------------
	--QUERY CRIAR Estadios


CREATE TABLE [dbo].[Estadios] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Nome]        NVARCHAR (MAX) NOT NULL,
    [Localizacao] NVARCHAR (MAX) NOT NULL
);
---------------------------------------------------------------------------------------------------------
	--QUERY CRIAR Clubes

CREATE TABLE [dbo].[Clubes] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Nome]       NVARCHAR (MAX) NOT NULL,
    [Presidente] NVARCHAR (MAX) NOT NULL,
    [EstadioId]  INT            NOT NULL
);


GO


CREATE NONCLUSTERED INDEX [IX_Clubes_EstadioId]
    ON [dbo].[Clubes]([EstadioId] ASC);


GO
ALTER TABLE [dbo].[Clubes]
    ADD CONSTRAINT [PK_Clubes] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Clubes]
    ADD CONSTRAINT [FK_Clubes_Estadios_EstadioId] FOREIGN KEY ([EstadioId]) REFERENCES [dbo].[Estadios] ([Id]) ON DELETE CASCADE;


-------------------------------------------------------------------------------------------------------------------------------------
	--QUERY CRIAR Equipas

CREATE TABLE [dbo].[Equipas] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Nome]    NVARCHAR (MAX) NOT NULL,
    [ClubeId] INT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Equipas_ClubeId]
    ON [dbo].[Equipas]([ClubeId] ASC);


GO
ALTER TABLE [dbo].[Equipas]
    ADD CONSTRAINT [PK_Equipas] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Equipas]
    ADD CONSTRAINT [FK_Equipas_Clubes_ClubeId] FOREIGN KEY ([ClubeId]) REFERENCES [dbo].[Clubes] ([Id]) ON DELETE CASCADE;

	------------------------------------------------------------------------------------------------------------
	--QUERY CRIAR Jogadores
CREATE TABLE [dbo].[Jogadores] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Nome]     NVARCHAR (MAX) NOT NULL,
    [Idade]    INT            NOT NULL,
    [EquipaId] INT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Jogadores_EquipaId]
    ON [dbo].[Jogadores]([EquipaId] ASC);


GO
ALTER TABLE [dbo].[Jogadores]
    ADD CONSTRAINT [PK_Jogadores] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Jogadores]
    ADD CONSTRAINT [FK_Jogadores_Equipas_EquipaId] FOREIGN KEY ([EquipaId]) REFERENCES [dbo].[Equipas] ([Id]) ON DELETE CASCADE;

	------------------------------------------------------------------------------------------------------------
	--QUERY CRIAR Perfil
CREATE TABLE [dbo].[Perfis] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [PrimeiroNome] NVARCHAR (MAX) NOT NULL,
    [UltimoNome]   NVARCHAR (MAX) NOT NULL,
    [UserId]       NVARCHAR (450) NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Perfis_UserId]
    ON [dbo].[Perfis]([UserId] ASC);


GO
ALTER TABLE [dbo].[Perfis]
    ADD CONSTRAINT [PK_Perfis] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Perfis]
    ADD CONSTRAINT [FK_Perfis_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]);

------------------------------------------------------------------------------------------------------------------------------------
--QUERY CRIAR JOGOS

CREATE TABLE [dbo].[Jogos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataHora] [datetime2](7) NOT NULL,
	[EquipaOneId] [int] NOT NULL,
	[EquipaTwoId] [int] NOT NULL,
 CONSTRAINT [PK_Jogos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Jogos]  WITH CHECK ADD  CONSTRAINT [FK_Jogos_Equipas_EquipaOneId] FOREIGN KEY([EquipaOneId])
REFERENCES [dbo].[Equipas] ([Id])
GO

ALTER TABLE [dbo].[Jogos] CHECK CONSTRAINT [FK_Jogos_Equipas_EquipaOneId]
GO

ALTER TABLE [dbo].[Jogos]  WITH CHECK ADD  CONSTRAINT [FK_Jogos_Equipas_EquipaTwoId] FOREIGN KEY([EquipaTwoId])
REFERENCES [dbo].[Equipas] ([Id])
GO

ALTER TABLE [dbo].[Jogos] CHECK CONSTRAINT [FK_Jogos_Equipas_EquipaTwoId]
GO



