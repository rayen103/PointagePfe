using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransportReferenceSatellites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.Bus', 'CodeChauffeur') IS NULL
BEGIN
    ALTER TABLE [dbo].[Bus] ADD [CodeChauffeur] nvarchar(50) NULL;
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Chauffeur]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Chauffeur] (
        [ChauffeurId] uniqueidentifier NOT NULL,
        [CodeChauffeur] nvarchar(50) NOT NULL,
        [Nom] nvarchar(100) NOT NULL,
        [Prenom] nvarchar(100) NULL,
        [CIN] nvarchar(50) NULL,
        [RFIDChauffeur] nvarchar(50) NULL,
        [Externe] bit NOT NULL CONSTRAINT [DF_Chauffeur_Externe] DEFAULT CAST(0 AS bit),
        [IsActive] bit NOT NULL CONSTRAINT [DF_Chauffeur_IsActive] DEFAULT CAST(1 AS bit),
        [SocieteId] uniqueidentifier NOT NULL,
        [InsererPar] nvarchar(max) NULL,
        [DateInsertion] datetime2 NULL,
        [ModifierPar] nvarchar(max) NULL,
        [DateModification] datetime2 NULL,
        CONSTRAINT [PK_Chauffeur] PRIMARY KEY ([ChauffeurId]),
        CONSTRAINT [FK_Chauffeur_Societe_SocieteId] FOREIGN KEY ([SocieteId]) REFERENCES [dbo].[Societe] ([SocieteId]) ON DELETE NO ACTION
    );
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Gouvernorat]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Gouvernorat] (
        [GouvernoratId] uniqueidentifier NOT NULL,
        [CodeGouvernorat] nvarchar(50) NOT NULL,
        [LibelleGouvernorat] nvarchar(200) NULL,
        [IsActive] bit NOT NULL CONSTRAINT [DF_Gouvernorat_IsActive] DEFAULT CAST(1 AS bit),
        [SocieteId] uniqueidentifier NOT NULL,
        [InsererPar] nvarchar(max) NULL,
        [DateInsertion] datetime2 NULL,
        [ModifierPar] nvarchar(max) NULL,
        [DateModification] datetime2 NULL,
        CONSTRAINT [PK_Gouvernorat] PRIMARY KEY ([GouvernoratId]),
        CONSTRAINT [FK_Gouvernorat_Societe_SocieteId] FOREIGN KEY ([SocieteId]) REFERENCES [dbo].[Societe] ([SocieteId]) ON DELETE NO ACTION
    );
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Modem]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Modem] (
        [ModemId] uniqueidentifier NOT NULL,
        [IMEI] nvarchar(50) NOT NULL,
        [ModelModem] nvarchar(100) NULL,
        [NumeroSim] nvarchar(50) NULL,
        [IsActive] bit NOT NULL CONSTRAINT [DF_Modem_IsActive] DEFAULT CAST(1 AS bit),
        [SocieteId] uniqueidentifier NOT NULL,
        [InsererPar] nvarchar(max) NULL,
        [DateInsertion] datetime2 NULL,
        [ModifierPar] nvarchar(max) NULL,
        [DateModification] datetime2 NULL,
        CONSTRAINT [PK_Modem] PRIMARY KEY ([ModemId]),
        CONSTRAINT [FK_Modem_Societe_SocieteId] FOREIGN KEY ([SocieteId]) REFERENCES [dbo].[Societe] ([SocieteId]) ON DELETE NO ACTION
    );
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Region]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Region] (
        [RegionId] uniqueidentifier NOT NULL,
        [CodeRegion] nvarchar(50) NOT NULL,
        [LibelleRegion] nvarchar(200) NULL,
        [CodeGouvernorat] nvarchar(50) NULL,
        [IsActive] bit NOT NULL CONSTRAINT [DF_Region_IsActive] DEFAULT CAST(1 AS bit),
        [SocieteId] uniqueidentifier NOT NULL,
        [InsererPar] nvarchar(max) NULL,
        [DateInsertion] datetime2 NULL,
        [ModifierPar] nvarchar(max) NULL,
        [DateModification] datetime2 NULL,
        CONSTRAINT [PK_Region] PRIMARY KEY ([RegionId]),
        CONSTRAINT [FK_Region_Societe_SocieteId] FOREIGN KEY ([SocieteId]) REFERENCES [dbo].[Societe] ([SocieteId]) ON DELETE NO ACTION
    );
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Chauffeur]', N'U') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Chauffeur_SocieteId' AND object_id = OBJECT_ID(N'[dbo].[Chauffeur]'))
BEGIN
    CREATE INDEX [IX_Chauffeur_SocieteId] ON [dbo].[Chauffeur]([SocieteId]);
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Gouvernorat]', N'U') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Gouvernorat_SocieteId' AND object_id = OBJECT_ID(N'[dbo].[Gouvernorat]'))
BEGIN
    CREATE INDEX [IX_Gouvernorat_SocieteId] ON [dbo].[Gouvernorat]([SocieteId]);
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Modem]', N'U') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Modem_SocieteId' AND object_id = OBJECT_ID(N'[dbo].[Modem]'))
BEGIN
    CREATE INDEX [IX_Modem_SocieteId] ON [dbo].[Modem]([SocieteId]);
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Region]', N'U') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Region_SocieteId' AND object_id = OBJECT_ID(N'[dbo].[Region]'))
BEGIN
    CREATE INDEX [IX_Region_SocieteId] ON [dbo].[Region]([SocieteId]);
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Chauffeur]', N'U') IS NOT NULL
BEGIN
    DROP TABLE [dbo].[Chauffeur];
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Gouvernorat]', N'U') IS NOT NULL
BEGIN
    DROP TABLE [dbo].[Gouvernorat];
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Modem]', N'U') IS NOT NULL
BEGIN
    DROP TABLE [dbo].[Modem];
END;
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[Region]', N'U') IS NOT NULL
BEGIN
    DROP TABLE [dbo].[Region];
END;
");

            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.Bus', 'CodeChauffeur') IS NOT NULL
BEGIN
    ALTER TABLE [dbo].[Bus] DROP COLUMN [CodeChauffeur];
END;
");
        }
    }
}
