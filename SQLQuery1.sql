-- SELECT * FROM [Entries1] GROUP BY EmployeeId;


--	CONVERT(DATE, Timestamp) can also work	 Date (without quotes also works)
SELECT 
	FullName, 
	CAST(Timestamp AS date) AS 'Date', 
	FORMAT(Timestamp, 'HH:mm:ss') AS 'Time',	-- HH -> 24 hour fromat
	EventType 
FROM [Entries1] en
JOIN Employees1 em ON en.EmployeeId = em.id;