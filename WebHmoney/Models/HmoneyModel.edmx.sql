
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/13/2022 15:49:23
-- Generated from EDMX file: C:\Users\Guillermo Peralta\source\repos\WebHmoney\WebHmoney\Models\HmoneyModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WebHmoney];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_InformacionUsuarioCuentas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cuentas] DROP CONSTRAINT [FK_InformacionUsuarioCuentas];
GO
IF OBJECT_ID(N'[dbo].[FK_CuentasMovimiento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Movimientos] DROP CONSTRAINT [FK_CuentasMovimiento];
GO
IF OBJECT_ID(N'[dbo].[FK_TipoGastoMovimiento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Movimientos] DROP CONSTRAINT [FK_TipoGastoMovimiento];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Cuentas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cuentas];
GO
IF OBJECT_ID(N'[dbo].[InformacionUsuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InformacionUsuarios];
GO
IF OBJECT_ID(N'[dbo].[Movimientos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Movimientos];
GO
IF OBJECT_ID(N'[dbo].[TipoGastos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoGastos];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Cuentas'
CREATE TABLE [dbo].[Cuentas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NombreCuenta] nvarchar(max)  NOT NULL,
    [TipoCuenta] nvarchar(max)  NOT NULL,
    [Moneda] nvarchar(max)  NOT NULL,
    [BalanceInicial] decimal(18,0)  NOT NULL,
    [InformacionUsuarioId] int  NOT NULL,
    [FechaRegistro] datetime  NOT NULL
);
GO

-- Creating table 'InformacionUsuarios'
CREATE TABLE [dbo].[InformacionUsuarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NombreCompleto] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Telefono] nvarchar(max)  NOT NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'Movimientos'
CREATE TABLE [dbo].[Movimientos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [Monto] decimal(18,0)  NOT NULL,
    [CuentasId] int  NOT NULL,
    [TipoGastoId] int  NOT NULL
);
GO

-- Creating table 'TipoGastos'
CREATE TABLE [dbo].[TipoGastos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Cuentas'
ALTER TABLE [dbo].[Cuentas]
ADD CONSTRAINT [PK_Cuentas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InformacionUsuarios'
ALTER TABLE [dbo].[InformacionUsuarios]
ADD CONSTRAINT [PK_InformacionUsuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Movimientos'
ALTER TABLE [dbo].[Movimientos]
ADD CONSTRAINT [PK_Movimientos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TipoGastos'
ALTER TABLE [dbo].[TipoGastos]
ADD CONSTRAINT [PK_TipoGastos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [InformacionUsuarioId] in table 'Cuentas'
ALTER TABLE [dbo].[Cuentas]
ADD CONSTRAINT [FK_InformacionUsuarioCuentas]
    FOREIGN KEY ([InformacionUsuarioId])
    REFERENCES [dbo].[InformacionUsuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InformacionUsuarioCuentas'
CREATE INDEX [IX_FK_InformacionUsuarioCuentas]
ON [dbo].[Cuentas]
    ([InformacionUsuarioId]);
GO

-- Creating foreign key on [CuentasId] in table 'Movimientos'
ALTER TABLE [dbo].[Movimientos]
ADD CONSTRAINT [FK_CuentasMovimiento]
    FOREIGN KEY ([CuentasId])
    REFERENCES [dbo].[Cuentas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentasMovimiento'
CREATE INDEX [IX_FK_CuentasMovimiento]
ON [dbo].[Movimientos]
    ([CuentasId]);
GO

-- Creating foreign key on [TipoGastoId] in table 'Movimientos'
ALTER TABLE [dbo].[Movimientos]
ADD CONSTRAINT [FK_TipoGastoMovimiento]
    FOREIGN KEY ([TipoGastoId])
    REFERENCES [dbo].[TipoGastos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TipoGastoMovimiento'
CREATE INDEX [IX_FK_TipoGastoMovimiento]
ON [dbo].[Movimientos]
    ([TipoGastoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------