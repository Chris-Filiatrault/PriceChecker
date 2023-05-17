CREATE TABLE Entries (
    Id uniqueidentifier NOT NULL,
    DateRecorded datetime NOT NULL,
    Price decimal(18) NOT NULL,
    Product text NOT NULL,
    Website text NOT NULL
    PRIMARY KEY (Id)
);