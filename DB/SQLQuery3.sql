CREATE TABLE Downloads (
    DownloadID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL,  -- Username from signup/login table
    BookID INT NOT NULL,  -- Foreign key from Books table
    DownloadDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (BookID) REFERENCES Books(id)
);