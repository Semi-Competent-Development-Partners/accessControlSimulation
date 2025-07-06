/* Add optional, but DISTINCT ISIC ID Field to dbo.Employees1 table */
ALTER TABLE dbo.Employees1
ADD ISIC_ID INT NULL;

CREATE UNIQUE INDEX IX_Employees1_ISIC_ID ON dbo.Employees1(ISIC_ID)
WHERE ISIC_ID IS NOT NULL;