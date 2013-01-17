<Query Kind="SQL">
  <Connection>
    <ID>6f1893f0-280f-4d88-a5b4-df41106dddae</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>master</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

-- Reset
USE tempdb
IF(EXISTS(SELECT * FROM sys.databases WHERE name = 'TopXDeveloperMistakes')) BEGIN
	ALTER DATABASE TopXDeveloperMistakes SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE TopXDeveloperMistakes	
END
CREATE DATABASE TopXDeveloperMistakes
GO
USE TopXDeveloperMistakes


-- Setup data
CREATE TABLE Pages (
	Name varchar(128),
	Hits int
)

INSERT INTO
	Pages
VALUES
	('A', 1),
	('B', 2)

	
-- Test
BEGIN TRAN
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

SELECT * FROM Pages WHERE Name = 'A'
--SELECT * FROM Pages WITH (UPDLOCK) WHERE Name = 'A'

UPDATE Pages SET Hits = Hits + 1 WHERE Name = 'A'

COMMIT