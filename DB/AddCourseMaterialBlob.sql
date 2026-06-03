/*
    AddCourseMaterialBlob.sql
    -------------------------------------------------------------------------
    Course materials used to be stored as a local FILE PATH (e.g. D:\Downloads\x.pdf),
    which does not exist on any other machine. These columns let the actual file
    be stored IN the database (like Books/Slides already are).

    Existing courses keep working: if a course has no blob yet, the app falls
    back to the old `materials` path.

    Run once in SSMS against the NeedyNest database.
    -------------------------------------------------------------------------
*/

USE NeedyNest;
GO

IF COL_LENGTH('dbo.Course', 'materialData') IS NULL
    ALTER TABLE dbo.Course ADD materialData VARBINARY(MAX) NULL;
GO
IF COL_LENGTH('dbo.Course', 'materialName') IS NULL
    ALTER TABLE dbo.Course ADD materialName NVARCHAR(255) NULL;
GO
IF COL_LENGTH('dbo.Course', 'materialExt') IS NULL
    ALTER TABLE dbo.Course ADD materialExt NVARCHAR(20) NULL;
GO

PRINT 'Course material blob columns added (materialData, materialName, materialExt).';
