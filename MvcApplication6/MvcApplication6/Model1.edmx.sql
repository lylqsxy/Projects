
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/03/2016 15:26:29
-- Generated from EDMX file: C:\Users\Administrator\Documents\Visual Studio 2012\Projects\MvcApplication6\MvcApplication6\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Test2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_OrderCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_OrderProduct];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [OrderId] int  NOT NULL,
    [OrderDetails] nvarchar(max)  NOT NULL,
    [ProductId] int  NOT NULL,
    [CustomerId] int  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductId] int  NOT NULL,
    [ProductName] nvarchar(max)  NOT NULL,
    [ProductPrice] decimal(18,0)  NOT NULL,
    [ProductDes] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerId] int  NOT NULL,
    [CustomerName] nvarchar(max)  NOT NULL,
    [CustomerAddr] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [OrderId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([OrderId] ASC);
GO

-- Creating primary key on [ProductId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [CustomerId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_OrderProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderProduct'
CREATE INDEX [IX_FK_OrderProduct]
ON [dbo].[Orders]
    ([ProductId]);
GO

-- Creating foreign key on [CustomerId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_OrderCustomer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderCustomer'
CREATE INDEX [IX_FK_OrderCustomer]
ON [dbo].[Orders]
    ([CustomerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------