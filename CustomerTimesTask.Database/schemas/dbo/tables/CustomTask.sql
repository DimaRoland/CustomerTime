CREATE TABLE dbo.CustomTasks
(
    Id INT IDENTITY(1, 1) not null CONSTRAINT PK_Tasks PRIMARY KEY,
    TaskName VARCHAR(255) not null,
    IsTaskStart BIT not null,
    IsTaskEnd BIT not null
);