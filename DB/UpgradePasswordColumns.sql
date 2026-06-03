/*
    UpgradePasswordColumns.sql
    -------------------------------------------------------------------------
    Password hashing produces a string of ~80-90 characters, e.g.:
        PBKDF2$100000$<24 base64 chars>$<44 base64 chars>

    The original `password` columns were sized for short plaintext passwords,
    so they must be widened before hashes can be stored. Run this ONCE in SSMS
    against the NeedyNest database.

    Existing plaintext passwords keep working: the app verifies them directly
    and upgrades each account to a hash automatically on its next login.
    -------------------------------------------------------------------------
*/

USE NeedyNest;
GO

ALTER TABLE dbo.signup ALTER COLUMN password NVARCHAR(200) NOT NULL;
GO

-- The login table is an audit log; widen it too (it stores the hash, never plaintext).
IF COL_LENGTH('dbo.login', 'password') IS NOT NULL
BEGIN
    ALTER TABLE dbo.login ALTER COLUMN password NVARCHAR(200) NULL;
END
GO

PRINT 'Password columns widened to NVARCHAR(200). Hashing is now safe to use.';
