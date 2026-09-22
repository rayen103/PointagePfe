SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
-- Auto-generated Idempotent Seeder for Azure Database
-- Table: Gouvernorat
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-3b87-36b3-003afbb50bcc')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-3b87-36b3-003afbb50bcc', N'61', N'Sfax', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-71f8-1106-0e8a79dc370a')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-71f8-1106-0e8a79dc370a', N'43', N'Sidi Bouzid', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-9533-697b-0fb0a8c2092c')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-9533-697b-0fb0a8c2092c', N'41', N'Kairouan', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-a204-88f6-134826736c13')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-a204-88f6-134826736c13', N'71', N'Gafsa', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-fa87-c753-191c4bd2dc0c')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-fa87-c753-191c4bd2dc0c', N'82', N'Médenine', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-f1b0-15cf-1ee9f5a4e16e')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-f1b0-15cf-1ee9f5a4e16e', N'52', N'Monastir', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-dcfb-563c-1f423864bf88')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-dcfb-563c-1f423864bf88', N'11', N'Tunis', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-7d2a-771a-234fb1ced832')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-7d2a-771a-234fb1ced832', N'22', N'Zaghouan', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-ddd7-415f-25d0d3a02b64')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-ddd7-415f-25d0d3a02b64', N'12', N'Ariana', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-0084-c251-2eb3ec1e4a02')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-0084-c251-2eb3ec1e4a02', N'52', N'Monastir', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-16f0-ebf2-2f6b27c3870b')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-16f0-ebf2-2f6b27c3870b', N'32', N'Jendouba', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-1688-d7c4-3464b0812bf9')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-1688-d7c4-3464b0812bf9', N'51', N'Sousse', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-7d52-4ba2-3c7f260c1cf1')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-7d52-4ba2-3c7f260c1cf1', N'22', N'Zaghouan', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-c9aa-ae58-405dd473b463')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-c9aa-ae58-405dd473b463', N'14', N'Manouba', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-a9b9-1fca-4a12547b5651')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-a9b9-1fca-4a12547b5651', N'21', N'Nabeul', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-3d79-4ef8-4e66297e457e')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-3d79-4ef8-4e66297e457e', N'31', N'Béja', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-26fa-52f9-53546ccd955b')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-26fa-52f9-53546ccd955b', N'81', N'Gabès', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-0831-d237-54336c6f9aed')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-0831-d237-54336c6f9aed', N'23', N'Bizerte', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-a0d4-62d8-5e1df146bbdb')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-a0d4-62d8-5e1df146bbdb', N'31', N'Béja', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-7ff4-9a7b-61eaeda6c6e9')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-7ff4-9a7b-61eaeda6c6e9', N'43', N'Sidi Bouzid', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-9a12-35bd-657f354e8c61')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-9a12-35bd-657f354e8c61', N'73', N'Kebili', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-dd24-f828-65edf521f802')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-dd24-f828-65edf521f802', N'11', N'Tunis', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-dce8-9ff7-71085e865dbb')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-dce8-9ff7-71085e865dbb', N'71', N'Gafsa', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-3779-c619-776d451f5f00')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-3779-c619-776d451f5f00', N'53', N'Mahdia', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-4a2a-4da4-795e9ea03f1d')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-4a2a-4da4-795e9ea03f1d', N'82', N'Médenine', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-15cc-76fa-7f226b444e34')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-15cc-76fa-7f226b444e34', N'41', N'Kairouan', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-8e16-a6e0-7f9583f9a656')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-8e16-a6e0-7f9583f9a656', N'42', N'Kasserine', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-037d-fdaa-86f4f0b2c8df')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-037d-fdaa-86f4f0b2c8df', N'72', N'Tozeur', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-e982-9279-8929336c3000')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-e982-9279-8929336c3000', N'81', N'Gabès', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-c1d8-9d96-9ac36a8cfc42')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-c1d8-9d96-9ac36a8cfc42', N'51', N'Sousse', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-a321-dba1-9cc68236f2ab')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-a321-dba1-9cc68236f2ab', N'21', N'Nabeul', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-5471-397d-a888b3c81b97')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-5471-397d-a888b3c81b97', N'13', N'Ben Arous', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-0cd7-a4e9-aa3a464a88d5')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-0cd7-a4e9-aa3a464a88d5', N'83', N'Tataouine', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-a056-a3d1-ab351410f5eb')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-a056-a3d1-ab351410f5eb', N'14', N'Manouba', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-2146-0ace-b311c9afc436')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-2146-0ace-b311c9afc436', N'32', N'Jendouba', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-2027-7bf3-b56f86f735d5')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-2027-7bf3-b56f86f735d5', N'61', N'Sfax', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-1fe8-ecc1-bbcd5e5bc220')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-1fe8-ecc1-bbcd5e5bc220', N'83', N'Tataouine', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-beba-ccbe-c352dd771190')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-beba-ccbe-c352dd771190', N'13', N'Ben Arous', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-973d-6cbd-cd7d0cf3f3fd')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-973d-6cbd-cd7d0cf3f3fd', N'73', N'Kebili', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-9ab3-2518-d76b5bd3758c')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-9ab3-2518-d76b5bd3758c', N'53', N'Mahdia', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-6fe1-c436-d858e86c3c08')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-6fe1-c436-d858e86c3c08', N'33', N'Le Kef', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-6e35-afa3-decb1fbe8416')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-6e35-afa3-decb1fbe8416', N'34', N'Siliana', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-1600-134d-dfffa1e5d6a1')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-1600-134d-dfffa1e5d6a1', N'42', N'Kasserine', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-e776-65b8-e0ae245dff16')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-e776-65b8-e0ae245dff16', N'23', N'Bizerte', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-8acf-0f32-eaac62a31418')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-8acf-0f32-eaac62a31418', N'34', N'Siliana', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '01a03d89-d630-28ec-5417-ee113f2757c9')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'01a03d89-d630-28ec-5417-ee113f2757c9', N'33', N'Le Kef', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-52ac-1001-f0f10d980a15')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-52ac-1001-f0f10d980a15', N'12', N'Ariana', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Gouvernorat] WHERE [GouvernoratId] = '019e5a15-28f3-d146-c65b-f9cdcb62b126')
BEGIN
    INSERT INTO dbo.[Gouvernorat] ([GouvernoratId], [CodeGouvernorat], [LibelleGouvernorat], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [IsActive]) VALUES (N'019e5a15-28f3-d146-c65b-f9cdcb62b126', N'72', N'Tozeur', N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, CAST(1 AS bit));
END;

-- Table: Societe
IF NOT EXISTS (SELECT 1 FROM dbo.[Societe] WHERE [SocieteId] = '01a03dc6-7972-6c42-d687-070eb5f07c6d')
BEGIN
    INSERT INTO dbo.[Societe] ([SocieteId], [LogoPath], [Nom], [MatriculeFiscal], [Rne], [Capital], [DateOverture], [Telephone1], [Telephone2], [Fax1], [Fax2], [Email], [Adresse], [CodeSociete], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodePostal], [Initiales], [Pays], [Rc], [Tva], [Ville]) VALUES (N'01a03dc6-7972-6c42-d687-070eb5f07c6d', NULL, N'ww', N'PENDING', NULL, 0.000, '2026-08-26 00:00:00.000', NULL, NULL, NULL, NULL, N'rayen.farhani@esprit.tn', NULL, NULL, N'', '2026-08-26 12:13:33.127', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Societe] WHERE [SocieteId] = '01a03db8-f3e1-4d8b-dbd2-0b2e71c60ada')
BEGIN
    INSERT INTO dbo.[Societe] ([SocieteId], [LogoPath], [Nom], [MatriculeFiscal], [Rne], [Capital], [DateOverture], [Telephone1], [Telephone2], [Fax1], [Fax2], [Email], [Adresse], [CodeSociete], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodePostal], [Initiales], [Pays], [Rc], [Tva], [Ville]) VALUES (N'01a03db8-f3e1-4d8b-dbd2-0b2e71c60ada', NULL, N'mm', N'PENDING', NULL, 0.000, '2026-08-26 00:00:00.000', NULL, NULL, NULL, NULL, N'rayen.farhani@esprit.tn', NULL, NULL, N'', '2026-08-26 11:58:46.766', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Societe] WHERE [SocieteId] = '01a03d96-589e-6d9c-b88a-16a852fefdd2')
BEGIN
    INSERT INTO dbo.[Societe] ([SocieteId], [LogoPath], [Nom], [MatriculeFiscal], [Rne], [Capital], [DateOverture], [Telephone1], [Telephone2], [Fax1], [Fax2], [Email], [Adresse], [CodeSociete], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodePostal], [Initiales], [Pays], [Rc], [Tva], [Ville]) VALUES (N'01a03d96-589e-6d9c-b88a-16a852fefdd2', NULL, N'traveltodo', N'PENDING', NULL, 0.000, '2026-08-26 00:00:00.000', NULL, NULL, NULL, NULL, N'rayen.farhani@esprit.tn', NULL, NULL, N'', '2026-08-26 11:20:59.523', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Societe] WHERE [SocieteId] = '018b1055-d0b7-de38-752f-1b18f580c2e0')
BEGIN
    INSERT INTO dbo.[Societe] ([SocieteId], [LogoPath], [Nom], [MatriculeFiscal], [Rne], [Capital], [DateOverture], [Telephone1], [Telephone2], [Fax1], [Fax2], [Email], [Adresse], [CodeSociete], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodePostal], [Initiales], [Pays], [Rc], [Tva], [Ville]) VALUES (N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, N'CST', N'MF-CST-001', N'RNE-CST-001', 0.000, '2024-01-01 00:00:00.000', NULL, NULL, NULL, NULL, N'admin@cst.tn', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Societe] WHERE [SocieteId] = '01a03db4-a5e9-13d6-7cd3-b7657d40be0c')
BEGIN
    INSERT INTO dbo.[Societe] ([SocieteId], [LogoPath], [Nom], [MatriculeFiscal], [Rne], [Capital], [DateOverture], [Telephone1], [Telephone2], [Fax1], [Fax2], [Email], [Adresse], [CodeSociete], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodePostal], [Initiales], [Pays], [Rc], [Tva], [Ville]) VALUES (N'01a03db4-a5e9-13d6-7cd3-b7657d40be0c', NULL, N'hh', N'PENDING', NULL, 0.000, '2026-08-26 00:00:00.000', NULL, NULL, NULL, NULL, N'mouhamedaminechouaikhia9@gmail', NULL, NULL, N'', '2026-08-26 11:54:04.857', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Societe] WHERE [SocieteId] = '01a03dc0-30c3-a1a6-5a04-c5e2b3491a9f')
BEGIN
    INSERT INTO dbo.[Societe] ([SocieteId], [LogoPath], [Nom], [MatriculeFiscal], [Rne], [Capital], [DateOverture], [Telephone1], [Telephone2], [Fax1], [Fax2], [Email], [Adresse], [CodeSociete], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodePostal], [Initiales], [Pays], [Rc], [Tva], [Ville]) VALUES (N'01a03dc0-30c3-a1a6-5a04-c5e2b3491a9f', NULL, N'dd', N'PENDING', NULL, 0.000, '2026-08-26 00:00:00.000', NULL, NULL, NULL, NULL, N'rayen.farhani@esprit.tn', NULL, NULL, N'', '2026-08-26 12:06:41.208', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Societe] WHERE [SocieteId] = '019970f9-ba22-f7a8-1e5c-e9d1206bf8b5')
BEGIN
    INSERT INTO dbo.[Societe] ([SocieteId], [LogoPath], [Nom], [MatriculeFiscal], [Rne], [Capital], [DateOverture], [Telephone1], [Telephone2], [Fax1], [Fax2], [Email], [Adresse], [CodeSociete], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodePostal], [Initiales], [Pays], [Rc], [Tva], [Ville]) VALUES (N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'01K5RFKEH2YYM1WQ79T4G6QY5N.jpeg', N'intercolor', N'0020515K/A/M/000', N'0020515K', 1134700000.000, '2025-09-22 00:00:00.000', N'71 433 290 ', N'71 434 133 ', N'71 434 758', NULL, N'intercolor@gnet.tn', N'8 rue de la gare Z I sidi rezig merine', NULL, N'2025-09-22 11:30:38.8222812', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
END;

-- Table: RoleUtilisateur
IF NOT EXISTS (SELECT 1 FROM dbo.[RoleUtilisateur] WHERE [RoleUtilisateurId] = '019ecc11-2b4d-4e84-9fd9-18cb856238ca')
BEGIN
    INSERT INTO dbo.[RoleUtilisateur] ([RoleUtilisateurId], [LibelleRoleUtilisateur], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [SocieteId]) VALUES (N'019ecc11-2b4d-4e84-9fd9-18cb856238ca', N'rayenRole', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-06-15 17:15:38.103', NULL, NULL, N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[RoleUtilisateur] WHERE [RoleUtilisateurId] = '019c2902-d0fc-b9ea-e2b1-41eef795238b')
BEGIN
    INSERT INTO dbo.[RoleUtilisateur] ([RoleUtilisateurId], [LibelleRoleUtilisateur], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [SocieteId]) VALUES (N'019c2902-d0fc-b9ea-e2b1-41eef795238b', N'admin', N'', '2026-02-04 15:16:16.492', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-02-05 14:04:46.121', NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[RoleUtilisateur] WHERE [RoleUtilisateurId] = '019cb3c5-c5c8-dff3-5131-bf39e983311b')
BEGIN
    INSERT INTO dbo.[RoleUtilisateur] ([RoleUtilisateurId], [LibelleRoleUtilisateur], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [SocieteId]) VALUES (N'019cb3c5-c5c8-dff3-5131-bf39e983311b', N'user1', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-03-03 13:56:48.946', NULL, NULL, NULL);
END;

-- Table: Navigation
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'circuits.circuit' AND [RoleUtilisateurId] = '019c2902-d0fc-b9ea-e2b1-41eef795238b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'circuits.circuit', N'019c2902-d0fc-b9ea-e2b1-41eef795238b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'circuits.circuit' AND [RoleUtilisateurId] = '019cb3c5-c5c8-dff3-5131-bf39e983311b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'circuits.circuit', N'019cb3c5-c5c8-dff3-5131-bf39e983311b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'circuits.equipe' AND [RoleUtilisateurId] = '019c2902-d0fc-b9ea-e2b1-41eef795238b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'circuits.equipe', N'019c2902-d0fc-b9ea-e2b1-41eef795238b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'circuits.ordretravail' AND [RoleUtilisateurId] = '019c2902-d0fc-b9ea-e2b1-41eef795238b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'circuits.ordretravail', N'019c2902-d0fc-b9ea-e2b1-41eef795238b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'circuits.pointcollecte' AND [RoleUtilisateurId] = '019c2902-d0fc-b9ea-e2b1-41eef795238b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'circuits.pointcollecte', N'019c2902-d0fc-b9ea-e2b1-41eef795238b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'circuits.pointcollecte' AND [RoleUtilisateurId] = '019cb3c5-c5c8-dff3-5131-bf39e983311b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'circuits.pointcollecte', N'019cb3c5-c5c8-dff3-5131-bf39e983311b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'circuits.rattachement' AND [RoleUtilisateurId] = '019c2902-d0fc-b9ea-e2b1-41eef795238b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'circuits.rattachement', N'019c2902-d0fc-b9ea-e2b1-41eef795238b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.bus' AND [RoleUtilisateurId] = '019ecc11-2b4d-4e84-9fd9-18cb856238ca')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.bus', N'019ecc11-2b4d-4e84-9fd9-18cb856238ca', N'View,Add,Edit,Delete,Preview,Print,Export,Search,Duplicate');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.employe' AND [RoleUtilisateurId] = '019ecc11-2b4d-4e84-9fd9-18cb856238ca')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.employe', N'019ecc11-2b4d-4e84-9fd9-18cb856238ca', N'View,Add,Edit,Delete,Preview,Print,Export,Search,Duplicate');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.employe' AND [RoleUtilisateurId] = '019c2902-d0fc-b9ea-e2b1-41eef795238b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.employe', N'019c2902-d0fc-b9ea-e2b1-41eef795238b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.employe' AND [RoleUtilisateurId] = '019cb3c5-c5c8-dff3-5131-bf39e983311b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.employe', N'019cb3c5-c5c8-dff3-5131-bf39e983311b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.role-utilisateur' AND [RoleUtilisateurId] = '019c2902-d0fc-b9ea-e2b1-41eef795238b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.role-utilisateur', N'019c2902-d0fc-b9ea-e2b1-41eef795238b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.role-utilisateur' AND [RoleUtilisateurId] = '019cb3c5-c5c8-dff3-5131-bf39e983311b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.role-utilisateur', N'019cb3c5-c5c8-dff3-5131-bf39e983311b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.shift' AND [RoleUtilisateurId] = '019ecc11-2b4d-4e84-9fd9-18cb856238ca')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.shift', N'019ecc11-2b4d-4e84-9fd9-18cb856238ca', N'View,Add,Edit,Delete,Preview,Print,Export,Search,Duplicate');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.societe' AND [RoleUtilisateurId] = '019c2902-d0fc-b9ea-e2b1-41eef795238b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.societe', N'019c2902-d0fc-b9ea-e2b1-41eef795238b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.societe' AND [RoleUtilisateurId] = '019cb3c5-c5c8-dff3-5131-bf39e983311b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.societe', N'019cb3c5-c5c8-dff3-5131-bf39e983311b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.utilisateur' AND [RoleUtilisateurId] = '019c2902-d0fc-b9ea-e2b1-41eef795238b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.utilisateur', N'019c2902-d0fc-b9ea-e2b1-41eef795238b', N'Add,Edit,Delete');
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Navigation] WHERE [NavigationId] = 'fichier.utilisateur' AND [RoleUtilisateurId] = '019cb3c5-c5c8-dff3-5131-bf39e983311b')
BEGIN
    INSERT INTO dbo.[Navigation] ([NavigationId], [RoleUtilisateurId], [Actions]) VALUES (N'fichier.utilisateur', N'019cb3c5-c5c8-dff3-5131-bf39e983311b', N'Add,Edit,Delete');
END;

-- Table: Utilisateur
IF NOT EXISTS (SELECT 1 FROM dbo.[Utilisateur] WHERE [UtilisateurId] = '019c2903-0d54-105d-fa74-08f82a436369')
BEGIN
    INSERT INTO dbo.[Utilisateur] ([UtilisateurId], [NomUtilisateur], [Nom], [Prenom], [Email], [Password], [RoleUtilisateurId], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [ApprovalToken], [VerificationCode]) VALUES (N'019c2903-0d54-105d-fa74-08f82a436369', N'root', N'root', N'root', N'root@root.com', N'31C8884E9D5BC293771F5C233414C3BE3CC4EAAE53A84B1B125E106ADD9F219DD6FF0AA5E34A8BD9446933CCB0211363AE9544B550A4BC5FB2CF4545FF2A2A76', NULL, CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'', '2026-02-04 15:16:32.104', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-02-17 10:50:23.548', NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Utilisateur] WHERE [UtilisateurId] = '018b1055-d0b7-de38-752f-1b18f580c2e0')
BEGIN
    INSERT INTO dbo.[Utilisateur] ([UtilisateurId], [NomUtilisateur], [Nom], [Prenom], [Email], [Password], [RoleUtilisateurId], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [ApprovalToken], [VerificationCode]) VALUES (N'018b1055-d0b7-de38-752f-1b18f580c2e0', N'admin', N'Admin', N'CST', N'admin@cst.tn', N'E2CF9A6F4CFCA46F74FC0E4CF7A5B278D3C20D9178E0DB936DBB3CF8E614C89E4D1C33229F39A457014D2D581CAA3DCE7F49C53803A176A4F891A9EB1D5A34BA', NULL, CAST(1 AS bit), N'018b1055-d0b7-de38-752f-1b18f580c2e0', NULL, NULL, NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Utilisateur] WHERE [UtilisateurId] = '019ecc22-a4e6-267f-50a1-3a04b83adedc')
BEGIN
    INSERT INTO dbo.[Utilisateur] ([UtilisateurId], [NomUtilisateur], [Nom], [Prenom], [Email], [Password], [RoleUtilisateurId], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [ApprovalToken], [VerificationCode]) VALUES (N'019ecc22-a4e6-267f-50a1-3a04b83adedc', N'rayen103', N'farhani', N'rayen', N'rayenfarhani9@gmail.com', N'949C4F0F907EEFFB46F5655E0938F17D334A6C7BA1C72CE2F55A2F0667501B45E65F50EA6B34B52A116F93D9915968BD474C68A4244528B83AE1A106503FBF3F', N'019ecc11-2b4d-4e84-9fd9-18cb856238ca', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-06-15 17:34:43.698', NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Utilisateur] WHERE [UtilisateurId] = '01a03dc6-7a0b-e08e-1f3c-3f8360e7cb65')
BEGIN
    INSERT INTO dbo.[Utilisateur] ([UtilisateurId], [NomUtilisateur], [Nom], [Prenom], [Email], [Password], [RoleUtilisateurId], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [ApprovalToken], [VerificationCode]) VALUES (N'01a03dc6-7a0b-e08e-1f3c-3f8360e7cb65', N'rayen.farhani', N'ww', N'ww', N'rayen.farhani@esprit.tn', N'DB68E85635EEAC0C7B1DFACB738B2D62014671630EBCFA55BA146071749D67A10F2834D62F07E61B8C9E0A529AFD94D22C786F62CFE855F61E114B8914662B45', NULL, CAST(1 AS bit), N'01a03dc6-7972-6c42-d687-070eb5f07c6d', N'', '2026-08-26 12:13:33.127', N'', '2026-08-26 12:13:57.162', NULL, NULL);
END;

-- Table: Region
IF NOT EXISTS (SELECT 1 FROM dbo.[Region] WHERE [RegionId] = '019e7e8b-7c50-0e24-9f74-2da7fc34cb98')
BEGIN
    INSERT INTO dbo.[Region] ([RegionId], [CodeRegion], [LibelleRegion], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodeGouvernorat]) VALUES (N'019e7e8b-7c50-0e24-9f74-2da7fc34cb98', N'070', N'ss', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-05-31 15:58:51.199', NULL, NULL, N'11');
END;

-- Table: Site
IF NOT EXISTS (SELECT 1 FROM dbo.[Site] WHERE [SiteId] = '018b1055-d0b7-de38-752f-1b18f580c2e0')
BEGIN
    INSERT INTO dbo.[Site] ([SiteId], [Code], [LibelleSite], [Siege], [Longitude], [Latitude], [Rayon], [TimeMinute], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification]) VALUES (N'018b1055-d0b7-de38-752f-1b18f580c2e0', N'21', N'test', CAST(1 AS bit), 12321.0000000000, 1234.0000000000, 123.000, 123, CAST(1 AS bit), N'018b1055-d0b7-de38-752f-1b18f580c2e0', N'1', '2026-02-11 00:00:00.000', N'1', NULL);
END;

-- Table: Chantier
IF NOT EXISTS (SELECT 1 FROM dbo.[Chantier] WHERE [ChantierId] = '019d2c68-4a1f-aa3d-de30-a5538280e458')
BEGIN
    INSERT INTO dbo.[Chantier] ([ChantierId], [NumeroChantier], [LibelleChantier], [CodeClient], [Adresse], [MontantHT], [MontantTTC], [Nature], [Responsable], [DateDebut], [DateFin], [Status], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification]) VALUES (N'019d2c68-4a1f-aa3d-de30-a5538280e458', N'123', N'', N'', N'', NULL, NULL, N'', N'', NULL, NULL, N'', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-03-27 00:08:45.638', NULL, NULL);
END;

-- Table: Modem
IF NOT EXISTS (SELECT 1 FROM dbo.[Modem] WHERE [ModemId] = '019e7e95-ceca-e034-c254-91d11908a393')
BEGIN
    INSERT INTO dbo.[Modem] ([ModemId], [IMEI], [NumSIM], [Modele], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [ModelModem], [NumeroSim]) VALUES (N'019e7e95-ceca-e034-c254-91d11908a393', N'998', NULL, NULL, CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-05-31 16:10:07.672', NULL, NULL, N'fdqx', N'28178182');
END;

-- Table: Chauffeur
IF NOT EXISTS (SELECT 1 FROM dbo.[Chauffeur] WHERE [ChauffeurId] = '019e7e50-7a21-0e7e-3288-3aba1e439976')
BEGIN
    INSERT INTO dbo.[Chauffeur] ([ChauffeurId], [CodeChauffeur], [Nom], [Prenom], [CIN], [RFIDChauffeur], [Externe], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [BusId]) VALUES (N'019e7e50-7a21-0e7e-3288-3aba1e439976', N'123', N'safwen', N'haboubi', N'1234457', N'432123', CAST(0 AS bit), CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-05-31 14:54:23.955', NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Chauffeur] WHERE [ChauffeurId] = '019ed1b5-4bc0-5a53-255f-89ad29d990a4')
BEGIN
    INSERT INTO dbo.[Chauffeur] ([ChauffeurId], [CodeChauffeur], [Nom], [Prenom], [CIN], [RFIDChauffeur], [Externe], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [BusId]) VALUES (N'019ed1b5-4bc0-5a53-255f-89ad29d990a4', N'27', N'farhani', N'rayen', N'12345678', N'28178182', CAST(0 AS bit), CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-06-16 19:33:00.179', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-08-26 10:12:27.345', N'01a03d57-9cd5-363f-e482-7bbc7c166575');
END;

-- Table: Shift
IF NOT EXISTS (SELECT 1 FROM dbo.[Shift] WHERE [ShiftId] = '019d1ddc-9983-0ff9-b03c-b32cd8116e79')
BEGIN
    INSERT INTO dbo.[Shift] ([ShiftId], [CodeShift], [LibelleShift], [JourSemaine], [HeureDebut], [HeureFin], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification]) VALUES (N'019d1ddc-9983-0ff9-b03c-b32cd8116e79', N'164', N'', N'Lundi', N'15:00:00', N'16:30:00', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-03-24 04:21:29.975', NULL, NULL);
END;

-- Table: Equipe
IF NOT EXISTS (SELECT 1 FROM dbo.[Equipe] WHERE [EquipeId] = '019c57f8-39a6-c9e0-8b9b-895fa3143803')
BEGIN
    INSERT INTO dbo.[Equipe] ([EquipeId], [CodeEquipe], [LibelleEquipe], [CodeClient], [CodeEntrepot], [CodeTarif], [CodeFournisseur], [Responsable], [IsInternal], [CodeVehicule], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification]) VALUES (N'019c57f8-39a6-c9e0-8b9b-895fa3143803', N'0000', N'123', N'123', N'123', N'123', N'123', N'123', CAST(1 AS bit), N'123', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-02-13 18:06:51.525', NULL, NULL);
END;

-- Table: PointCollecte
IF NOT EXISTS (SELECT 1 FROM dbo.[PointCollecte] WHERE [PointCollecteId] = '019c57f6-8bb2-c701-36ce-b725ec917a6d')
BEGIN
    INSERT INTO dbo.[PointCollecte] ([PointCollecteId], [CodePointCollecte], [LibellePointCollecte], [Latitude], [Longitude], [CodeGouvernorat], [CodeRegion], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CircuitId]) VALUES (N'019c57f6-8bb2-c701-36ce-b725ec917a6d', N'123', N'123', 3.0000000000, 3.0000000000, N'13', N'070', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-02-13 18:05:01.524', NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[PointCollecte] WHERE [PointCollecteId] = '019ee041-0c08-9aeb-092b-ea10d4a3e9d3')
BEGIN
    INSERT INTO dbo.[PointCollecte] ([PointCollecteId], [CodePointCollecte], [LibellePointCollecte], [Latitude], [Longitude], [CodeGouvernorat], [CodeRegion], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CircuitId]) VALUES (N'019ee041-0c08-9aeb-092b-ea10d4a3e9d3', N'28178182', N'test', 36.7876330000, 10.1925650000, N'11', N'070', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-06-19 15:20:19.928', NULL, NULL, NULL);
END;

-- Table: Circuit
IF NOT EXISTS (SELECT 1 FROM dbo.[Circuit] WHERE [CircuitId] = '019f3351-c0e4-e603-1e79-929b3b7ed600')
BEGIN
    INSERT INTO dbo.[Circuit] ([CircuitId], [CodeCircuit], [LibelleCircuit], [Description], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [Latitude], [Longitude], [CodePCArrivee], [CodePCDepart], [Couleur], [DistanceKm], [DureeMinutes]) VALUES (N'019f3351-c0e4-e603-1e79-929b3b7ed600', N'28178182', N'BenArous', N'from BenArous to Morneg', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-07-05 18:27:03.869', NULL, NULL, NULL, NULL, N'123', N'123', N'#2196F3', 7.00, 15);
END;

-- Table: Bus
IF NOT EXISTS (SELECT 1 FROM dbo.[Bus] WHERE [BusId] = '019e0ceb-0809-fdbb-df10-59eb7abad66c')
BEGIN
    INSERT INTO dbo.[Bus] ([BusId], [NumeroIMM], [ModelBus], [IMEI], [Capacite], [CodeCircuit], [AppSagem], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [Latitude], [Longitude], [CodeChauffeur], [CurrentOccupancy], [LastOccupancyUpdateAt], [LastPositionAt], [BatteryPercentage], [BatteryVoltage], [DeviceRecordedAtUtc]) VALUES (N'019e0ceb-0809-fdbb-df10-59eb7abad66c', N'2817812', N'man', N'28178182', 50, N'01', CAST(0 AS bit), CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-05-09 14:26:30.349', NULL, NULL, NULL, NULL, NULL, 0, '2026-05-22 17:07:10.675', NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Bus] WHERE [BusId] = '019d1dae-ac0c-d8f7-55e1-61004d5fee50')
BEGIN
    INSERT INTO dbo.[Bus] ([BusId], [NumeroIMM], [ModelBus], [IMEI], [Capacite], [CodeCircuit], [AppSagem], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [Latitude], [Longitude], [CodeChauffeur], [CurrentOccupancy], [LastOccupancyUpdateAt], [LastPositionAt], [BatteryPercentage], [BatteryVoltage], [DeviceRecordedAtUtc]) VALUES (N'019d1dae-ac0c-d8f7-55e1-61004d5fee50', N'190', N'vovlo', N'19122', 55, N'123', CAST(1 AS bit), CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-03-24 03:31:19.934', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Bus] WHERE [BusId] = '01a03d57-9cd5-363f-e482-7bbc7c166575')
BEGIN
    INSERT INTO dbo.[Bus] ([BusId], [NumeroIMM], [ModelBus], [IMEI], [Capacite], [CodeCircuit], [AppSagem], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [Latitude], [Longitude], [CodeChauffeur], [CurrentOccupancy], [LastOccupancyUpdateAt], [LastPositionAt], [BatteryPercentage], [BatteryVoltage], [DeviceRecordedAtUtc]) VALUES (N'01a03d57-9cd5-363f-e482-7bbc7c166575', N'002', N'gf200', N'998', 89, N'28178182', CAST(0 AS bit), CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-08-26 10:12:27.345', NULL, NULL, NULL, NULL, N'27', 0, NULL, NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Bus] WHERE [BusId] = '019ea7b9-49f7-04ac-5364-ae2148ade778')
BEGIN
    INSERT INTO dbo.[Bus] ([BusId], [NumeroIMM], [ModelBus], [IMEI], [Capacite], [CodeCircuit], [AppSagem], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [Latitude], [Longitude], [CodeChauffeur], [CurrentOccupancy], [LastOccupancyUpdateAt], [LastPositionAt], [BatteryPercentage], [BatteryVoltage], [DeviceRecordedAtUtc]) VALUES (N'019ea7b9-49f7-04ac-5364-ae2148ade778', N'007', N'isuzu', N'998', 30, N'01', CAST(0 AS bit), CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-06-08 15:53:18.866', NULL, NULL, 36.7636416963765, 10.2262115478516, N'123', 0, '2026-08-16 13:30:20.707', NULL, NULL, NULL, NULL);
END;

-- Table: Employe
IF NOT EXISTS (SELECT 1 FROM dbo.[Employe] WHERE [EmployeId] = '019ed1b6-d37e-3f49-d4cb-17c588ed0b15')
BEGIN
    INSERT INTO dbo.[Employe] ([EmployeId], [Matricule], [RFID], [Nom], [Prenom], [CodeCircuit], [CodePointCollecte], [CodeShift], [Adresse], [CodeGouvernorat], [CodeRegion], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodeBus], [Latitude], [Longitude]) VALUES (N'019ed1b6-d37e-3f49-d4cb-17c588ed0b15', N'49', N'2338443é', N'chakroun', N'yousef', N'26', N'123', N'164', N'Yassminet', N'13', N'070', N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-06-16 19:34:40.447', NULL, NULL, NULL, NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Employe] WHERE [EmployeId] = '019e3b62-0b15-3599-b48d-ccf5b74fe81c')
BEGIN
    INSERT INTO dbo.[Employe] ([EmployeId], [Matricule], [RFID], [Nom], [Prenom], [CodeCircuit], [CodePointCollecte], [CodeShift], [Adresse], [CodeGouvernorat], [CodeRegion], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodeBus], [Latitude], [Longitude]) VALUES (N'019e3b62-0b15-3599-b48d-ccf5b74fe81c', N'102', N'12312', N'rayen', N'farhani', N'01', N'aez', N'day', NULL, NULL, NULL, N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-05-18 14:59:01.796', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-05-21 14:10:38.555', N'2817812', NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[Employe] WHERE [EmployeId] = '019ed159-f4be-6c00-7747-e2e85e49e49a')
BEGIN
    INSERT INTO dbo.[Employe] ([EmployeId], [Matricule], [RFID], [Nom], [Prenom], [CodeCircuit], [CodePointCollecte], [CodeShift], [Adresse], [CodeGouvernorat], [CodeRegion], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification], [CodeBus], [Latitude], [Longitude]) VALUES (N'019ed159-f4be-6c00-7747-e2e85e49e49a', N'12345678', N'28178182', N'haboubi', N'safwen', N'28178182', N'123', N'164', N'Yassminet', N'13', N'070', N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-06-16 17:53:14.202', NULL, NULL, N'007', NULL, NULL);
END;

-- Table: OrdreTravail
IF NOT EXISTS (SELECT 1 FROM dbo.[OrdreTravail] WHERE [OrdreTravailId] = '019c57fa-60a2-230f-6cb9-4d6e3d6800eb')
BEGIN
    INSERT INTO dbo.[OrdreTravail] ([OrdreTravailId], [NumeroOrdreTravail], [NumeroChantier], [CodeClient], [NumeroBonCommande], [CodeEquipe], [EtatOT], [Montant], [DateCreation], [NumeroConvention], [CodeVehicule], [Libelle], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification]) VALUES (N'019c57fa-60a2-230f-6cb9-4d6e3d6800eb', N'2', N'1', N'12', N'123', N'123', N'123', 123.000, '2026-02-14 00:00:00.000', N'123', N'123', N'1', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-02-13 18:09:12.527', NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[OrdreTravail] WHERE [OrdreTravailId] = '019e3013-094e-430d-952d-c5cf6ba1a984')
BEGIN
    INSERT INTO dbo.[OrdreTravail] ([OrdreTravailId], [NumeroOrdreTravail], [NumeroChantier], [CodeClient], [NumeroBonCommande], [CodeEquipe], [EtatOT], [Montant], [DateCreation], [NumeroConvention], [CodeVehicule], [Libelle], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification]) VALUES (N'019e3013-094e-430d-952d-c5cf6ba1a984', N'103', N'01', N'123', N'103', N'', N'ok', 105.000, '2026-05-30 00:00:00.000', N'102', N'007', N'chantier', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-05-16 10:16:54.482', NULL, NULL);
END;

-- Table: Rattachement
IF NOT EXISTS (SELECT 1 FROM dbo.[Rattachement] WHERE [RattachementId] = '019c57fc-ec38-1739-4e24-2138ab9fb252')
BEGIN
    INSERT INTO dbo.[Rattachement] ([RattachementId], [NumeroRattachement], [Exercice], [DateRattachement], [NumeroChantier], [CodeClient], [IsInternal], [Cout], [Type], [Nature], [Responsable], [HeureDebut], [HeureFin], [Emplacement], [Reference], [Status], [DateCloture], [Remarque], [IsActive], [SocieteId], [InsererPar], [DateInsertion], [ModifierPar], [DateModification]) VALUES (N'019c57fc-ec38-1739-4e24-2138ab9fb252', N'000', 123, '2026-02-14 00:00:00.000', N'123', N'123', CAST(1 AS bit), 123.000, N'123', N'123', N'123', N'12:03:00', N'12:04:00', N'123', N'123', N'123', '2026-02-15 00:00:00.000', N'1123é&"', CAST(1 AS bit), N'019970f9-ba22-f7a8-1e5c-e9d1206bf8b5', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-02-13 18:11:59.289', NULL, NULL);
END;

-- Table: BusRuntimeEvent
IF NOT EXISTS (SELECT 1 FROM dbo.[BusRuntimeEvent] WHERE [BusRuntimeEventId] = '019e50a7-bd7f-1469-650d-2f4818942c34')
BEGIN
    INSERT INTO dbo.[BusRuntimeEvent] ([BusRuntimeEventId], [BusId], [EventType], [Description], [IMEI], [Latitude], [Longitude], [Occupancy], [OccurredAtUtc], [InsererPar], [DateInsertion], [ModifierPar], [DateModification]) VALUES (N'019e50a7-bd7f-1469-650d-2f4818942c34', N'019e0ceb-0809-fdbb-df10-59eb7abad66c', N'BusEmptied', N'Vider le Bus action executed.', N'28178182', NULL, NULL, 0, '2026-05-22 17:07:10.675', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-05-22 18:07:10.958', NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[BusRuntimeEvent] WHERE [BusRuntimeEventId] = '01a00ac4-21d3-03a8-d754-3d33d63893b8')
BEGIN
    INSERT INTO dbo.[BusRuntimeEvent] ([BusRuntimeEventId], [BusId], [EventType], [Description], [IMEI], [Latitude], [Longitude], [Occupancy], [OccurredAtUtc], [InsererPar], [DateInsertion], [ModifierPar], [DateModification]) VALUES (N'01a00ac4-21d3-03a8-d754-3d33d63893b8', N'019ea7b9-49f7-04ac-5364-ae2148ade778', N'BusEmptied', N'Vider le Bus action executed.', N'998', 36.7636416963765, 10.2262115478516, 0, '2026-08-16 13:30:20.707', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-08-16 14:30:21.165', NULL, NULL);
END;
IF NOT EXISTS (SELECT 1 FROM dbo.[BusRuntimeEvent] WHERE [BusRuntimeEventId] = '019ee038-95f2-9e1f-f6b0-4bc151e01e4a')
BEGIN
    INSERT INTO dbo.[BusRuntimeEvent] ([BusRuntimeEventId], [BusId], [EventType], [Description], [IMEI], [Latitude], [Longitude], [Occupancy], [OccurredAtUtc], [InsererPar], [DateInsertion], [ModifierPar], [DateModification]) VALUES (N'019ee038-95f2-9e1f-f6b0-4bc151e01e4a', N'019ea7b9-49f7-04ac-5364-ae2148ade778', N'BusEmptied', N'Vider le Bus action executed.', N'998', 36.7636416963765, 10.2262115478516, 0, '2026-06-19 14:11:05.139', N'019c2903-0d54-105d-fa74-08f82a436369', '2026-06-19 15:11:05.362', NULL, NULL);
END;

