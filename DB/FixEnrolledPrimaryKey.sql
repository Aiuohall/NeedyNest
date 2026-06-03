/*
    FixEnrolledPrimaryKey.sql
    -------------------------------------------------------------------------
    Problem:
        The [Enrolled] table's PRIMARY KEY is defined on [course id] alone.
        That means a course can hold only ONE enrollment row, so the second
        person who tries to enrol in the same course gets:

            Violation of PRIMARY KEY constraint 'PK_Enrolled'.
            Cannot insert duplicate key ... The duplicate key value is (5).

    Fix:
        Replace the single-column key with a COMPOSITE key on
        (username, [course id]) so each member may enrol in each course once,
        and many members can share a course.

    How to run:
        Open this file in SQL Server Management Studio against the NeedyNest
        database and execute it once. (Adjust the PK constraint name if yours
        differs - check with: EXEC sp_pkeys 'Enrolled';)
    -------------------------------------------------------------------------
*/

USE NeedyNest;
GO

-- 1) Drop the existing single-column primary key.
IF EXISTS (SELECT 1 FROM sys.key_constraints
           WHERE name = 'PK_Enrolled' AND parent_object_id = OBJECT_ID('dbo.Enrolled'))
BEGIN
    ALTER TABLE dbo.Enrolled DROP CONSTRAINT PK_Enrolled;
END
GO

-- 2) Make sure the key columns cannot be NULL (required for a primary key).
ALTER TABLE dbo.Enrolled ALTER COLUMN username   VARCHAR(50)  NOT NULL;
ALTER TABLE dbo.Enrolled ALTER COLUMN [course id] INT         NOT NULL;
GO

-- 3) Add the composite primary key.
ALTER TABLE dbo.Enrolled
    ADD CONSTRAINT PK_Enrolled PRIMARY KEY (username, [course id]);
GO

PRINT 'Enrolled primary key is now (username, [course id]). Multiple members can enrol per course.';
