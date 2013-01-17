<Query Kind="SQL">
  <Connection>
    <ID>382a789a-a9c0-4a08-965e-5934cb346213</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>TopXDeveloperMistakes</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

USE tempdb
BEGIN TRY
	DROP TABLE #Categories
END TRY BEGIN CATCH END CATCH

-- Setup
CREATE TABLE #Categories (
	ID int,
	ParentID int,
	Name varchar(128)
)

-- Sample data
INSERT INTO
	#Categories (ID, ParentID, Name)
VALUES
	(1, null, 'Root'),
	(2, 1, 'Languages'),
	(3, 2, 'C#'),
	(4, 2, 'T-SQL'),
	(5, 1, 'Operating systems'),
	(6, 5, 'Windows'),
	(7, 5, 'OS X');

-- Query
WITH CTE AS
(
	SELECT
		ID,
		ParentID,
		Name,
		4 AS Indent
	FROM
		#Categories
	WHERE
		ParentID IS NULL

	UNION ALL

	SELECT
		C.ID,
		C.ParentID,
		CAST(REPLICATE(' ', CTE.Indent) + C.Name AS varchar(128)) AS Name,
		Indent + 4 AS Indent
	FROM
		#Categories C
	INNER JOIN
		CTE ON C.ParentID = CTE.ID
)
SELECT ID, ParentID, Name FROM CTE

-- Query
WITH CTE AS
(
	SELECT
		ID,
		ParentID,
		Name
	FROM
		#Categories
	WHERE
		ParentID IS NULL

	UNION ALL

	SELECT
		C.ID,
		C.ParentID,
		CAST(CTE.Name + '/' + C.Name AS varchar(128)) AS Name
	FROM
		#Categories C
	INNER JOIN
		CTE ON C.ParentID = CTE.ID
)
SELECT Name FROM CTE