/*
    AddProfilePhotoColumn.sql
    -------------------------------------------------------------------------
    Adds an optional profile photo (stored in the database) to members.
    Run once in SSMS. Until it exists, the Edit Profile screen simply shows a
    placeholder and photo changes are skipped.
    -------------------------------------------------------------------------
*/

USE NeedyNest;
GO

IF COL_LENGTH('dbo.signup', 'photo') IS NULL
    ALTER TABLE dbo.signup ADD photo VARBINARY(MAX) NULL;
GO

PRINT 'signup.photo column added.';
