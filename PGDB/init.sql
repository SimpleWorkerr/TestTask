CREATE TABLE Arrays (
    Id SERIAL PRIMARY KEY
);

CREATE TABLE Items (
    Id SERIAL PRIMARY KEY,
    Value INTEGER NOT NULL,
    ArrayDbId INTEGER,
    CONSTRAINT FK_Items_Arrays_ArrayDbId FOREIGN KEY (ArrayDbId) REFERENCES Arrays(Id)
);