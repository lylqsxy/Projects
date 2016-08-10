
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/05/2016 11:56:47
-- Generated from EDMX file: c:\users\administrator\documents\visual studio 2015\Projects\WebApplication6\WebApplication6\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [StudentTest];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [StudentGood_Id] int  NOT NULL,
    [StudentBad_Id] int  NOT NULL
);
GO

-- Creating table 'StudentGoods'
CREATE TABLE [dbo].[StudentGoods] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Good] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StudentBads'
CREATE TABLE [dbo].[StudentBads] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Bad] nvarchar(max)  NOT NULL,
    [Info] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StudentGoods'
ALTER TABLE [dbo].[StudentGoods]
ADD CONSTRAINT [PK_StudentGoods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StudentBads'
ALTER TABLE [dbo].[StudentBads]
ADD CONSTRAINT [PK_StudentBads]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StudentGood_Id] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_StudentStudentGood]
    FOREIGN KEY ([StudentGood_Id])
    REFERENCES [dbo].[StudentGoods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentStudentGood'
CREATE INDEX [IX_FK_StudentStudentGood]
ON [dbo].[Students]
    ([StudentGood_Id]);
GO

-- Creating foreign key on [StudentBad_Id] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_StudentStudentBad]
    FOREIGN KEY ([StudentBad_Id])
    REFERENCES [dbo].[StudentBads]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentStudentBad'
CREATE INDEX [IX_FK_StudentStudentBad]
ON [dbo].[Students]
    ([StudentBad_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------