/*
    AddEmailColumn.sql
    -------------------------------------------------------------------------
    Adds an optional email address to members so the app can notify them when
    their account is approved or rejected. Run once in SSMS.

    Until this column exists (and members have addresses), email notifications
    are simply skipped — the approval workflow still works normally.
    -------------------------------------------------------------------------
*/

USE NeedyNest;
GO

IF COL_LENGTH('dbo.signup', 'email') IS NULL
    ALTER TABLE dbo.signup ADD email NVARCHAR(256) NULL;
GO

PRINT 'signup.email column added.';
