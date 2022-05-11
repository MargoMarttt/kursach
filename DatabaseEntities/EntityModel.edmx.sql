
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/08/2022 22:16:49
-- Generated from EDMX file: C:\Users\Дмитрий\source\repos\Выбор поставщиков автозапчастей(попарное сравнение)\DatabaseEntities\EntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Temp];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ExpertRate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RateSet] DROP CONSTRAINT [FK_ExpertRate];
GO
IF OBJECT_ID(N'[dbo].[FK_RateDetailSupplier_Rate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RateDetailSupplier] DROP CONSTRAINT [FK_RateDetailSupplier_Rate];
GO
IF OBJECT_ID(N'[dbo].[FK_RateDetailSupplier_DetailSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RateDetailSupplier] DROP CONSTRAINT [FK_RateDetailSupplier_DetailSupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_DetailSupplierDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetailSet] DROP CONSTRAINT [FK_DetailSupplierDetail];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[RateSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RateSet];
GO
IF OBJECT_ID(N'[dbo].[AdminSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdminSet];
GO
IF OBJECT_ID(N'[dbo].[ClientSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientSet];
GO
IF OBJECT_ID(N'[dbo].[ExpertSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExpertSet];
GO
IF OBJECT_ID(N'[dbo].[DetailSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetailSet];
GO
IF OBJECT_ID(N'[dbo].[DetailSupplierSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetailSupplierSet];
GO
IF OBJECT_ID(N'[dbo].[RateDetailSupplier]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RateDetailSupplier];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'RateSet'
CREATE TABLE [dbo].[RateSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] float  NOT NULL,
    [TimeOfCommit] datetime  NOT NULL,
    [Expert_Id] int  NOT NULL
);
GO

-- Creating table 'AdminSet'
CREATE TABLE [dbo].[AdminSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [UserStatus] int  NOT NULL,
    [LastOnline] datetime  NOT NULL
);
GO

-- Creating table 'ClientSet'
CREATE TABLE [dbo].[ClientSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [UserStatus] int  NOT NULL,
    [LastOnline] datetime  NOT NULL
);
GO

-- Creating table 'ExpertSet'
CREATE TABLE [dbo].[ExpertSet] (
    [RateWeight] float  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [UserStatus] int  NOT NULL,
    [LastOnline] datetime  NOT NULL
);
GO

-- Creating table 'DetailSet'
CREATE TABLE [dbo].[DetailSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [VendorCode] float  NOT NULL,
    [DetailSupplierId] int  NOT NULL
);
GO

-- Creating table 'DetailSupplierSet'
CREATE TABLE [dbo].[DetailSupplierSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TotalRate] float  NOT NULL
);
GO

-- Creating table 'RateDetailSupplier'
CREATE TABLE [dbo].[RateDetailSupplier] (
    [Rate_Id] int  NOT NULL,
    [DetailSupplier_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'RateSet'
ALTER TABLE [dbo].[RateSet]
ADD CONSTRAINT [PK_RateSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AdminSet'
ALTER TABLE [dbo].[AdminSet]
ADD CONSTRAINT [PK_AdminSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientSet'
ALTER TABLE [dbo].[ClientSet]
ADD CONSTRAINT [PK_ClientSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExpertSet'
ALTER TABLE [dbo].[ExpertSet]
ADD CONSTRAINT [PK_ExpertSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DetailSet'
ALTER TABLE [dbo].[DetailSet]
ADD CONSTRAINT [PK_DetailSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DetailSupplierSet'
ALTER TABLE [dbo].[DetailSupplierSet]
ADD CONSTRAINT [PK_DetailSupplierSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Rate_Id], [DetailSupplier_Id] in table 'RateDetailSupplier'
ALTER TABLE [dbo].[RateDetailSupplier]
ADD CONSTRAINT [PK_RateDetailSupplier]
    PRIMARY KEY CLUSTERED ([Rate_Id], [DetailSupplier_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Expert_Id] in table 'RateSet'
ALTER TABLE [dbo].[RateSet]
ADD CONSTRAINT [FK_ExpertRate]
    FOREIGN KEY ([Expert_Id])
    REFERENCES [dbo].[ExpertSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExpertRate'
CREATE INDEX [IX_FK_ExpertRate]
ON [dbo].[RateSet]
    ([Expert_Id]);
GO

-- Creating foreign key on [Rate_Id] in table 'RateDetailSupplier'
ALTER TABLE [dbo].[RateDetailSupplier]
ADD CONSTRAINT [FK_RateDetailSupplier_Rate]
    FOREIGN KEY ([Rate_Id])
    REFERENCES [dbo].[RateSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DetailSupplier_Id] in table 'RateDetailSupplier'
ALTER TABLE [dbo].[RateDetailSupplier]
ADD CONSTRAINT [FK_RateDetailSupplier_DetailSupplier]
    FOREIGN KEY ([DetailSupplier_Id])
    REFERENCES [dbo].[DetailSupplierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RateDetailSupplier_DetailSupplier'
CREATE INDEX [IX_FK_RateDetailSupplier_DetailSupplier]
ON [dbo].[RateDetailSupplier]
    ([DetailSupplier_Id]);
GO

-- Creating foreign key on [DetailSupplierId] in table 'DetailSet'
ALTER TABLE [dbo].[DetailSet]
ADD CONSTRAINT [FK_DetailSupplierDetail]
    FOREIGN KEY ([DetailSupplierId])
    REFERENCES [dbo].[DetailSupplierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetailSupplierDetail'
CREATE INDEX [IX_FK_DetailSupplierDetail]
ON [dbo].[DetailSet]
    ([DetailSupplierId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------