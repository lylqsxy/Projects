
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/02/2016 00:23:39
-- Generated from EDMX file: c:\users\administrator\documents\visual studio 2012\Projects\MvcApplication3\MvcApplication3\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Test];
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

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(100)  NULL,
    [State] nvarchar(2)  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Rate] decimal(18,0)  NOT NULL,
    [Quantity] decimal(18,0)  NOT NULL,
    [ContactId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ContactId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_ContactOrder]
    FOREIGN KEY ([ContactId])
    REFERENCES [dbo].[Contacts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactOrder'
CREATE INDEX [IX_FK_ContactOrder]
ON [dbo].[Orders]
    ([ContactId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------