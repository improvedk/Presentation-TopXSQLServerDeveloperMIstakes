BEGIN TRY
	DROP TABLE Pages
END TRY BEGIN CATCH END CATCH

CREATE TABLE Pages (
	Name varchar(128),
	Hits int
)

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRAN

SELECT * FROM Pages WITH (UPDLOCK) WHERE Name = 'Test'

INSERT INTO Pages (Name, Hits) VALUES ('Test', 1)

UPDATE Pages SET Hits = Hits + 1 WHERE Name = 'Test'

ROLLBACK

MERGE
	Pages AS Target
USING
	(SELECT 'Test', 1) AS Source (Name, Hits)
ON
	Target.Name = Source.Name
WHEN MATCHED THEN
	UPDATE SET Target.Hits = Target.Hits + Source.Hits
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Name, Hits) VALUES (Source.Name, Source.Hits);