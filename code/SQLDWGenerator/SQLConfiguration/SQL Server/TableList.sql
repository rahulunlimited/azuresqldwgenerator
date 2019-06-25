WITH TableList AS 
(
	SELECT 
		s.Name + '.' + t.name AS TableID
		,s.Name AS SchemaName
		,t.Name AS TableName
		,(select sum(p2.rows)
			from sys.indexes i2 inner join sys.partitions p2 ON i2.object_id = p2.OBJECT_ID AND i2.index_id = p2.index_id
			where i2.object_id = t.object_id and i2.object_id > 255 and (i2.index_id = 0 or i2.index_id = 1)
		) as TotalRows
		,SUM(CASE WHEN (i.index_id=0) OR (i.index_id=1) THEN a.total_pages * 8 ELSE 0 END)/1024 AS DataSizeMB
	FROM sys.tables t
	INNER JOIN sys.schemas s ON s.schema_id = t.schema_id
	INNER JOIN sys.indexes i ON t.OBJECT_ID = i.object_id
	INNER JOIN sys.partitions p ON i.object_id = p.OBJECT_ID AND i.index_id = p.index_id
	INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id
	WHERE t.is_ms_shipped = 0 AND i.OBJECT_ID > 255 
	GROUP BY t.object_id, t.Name, s.Name
)
SELECT 
	TableID
	,SchemaName
	,TableName
	,TotalRows
	,DataSizeMB
	,'ROUND_ROBIN' AS DistributionType
	,CASE WHEN TotalRows >= 10000000 THEN 'CLUSTERED COLUMNSTORE INDEX' ELSE 'HEAP' END AS IndexType
	,'No' AS TableSplit
	,'No' AS ReplaceNewLine
	,'True' AS SelectTable
FROM TableList
ORDER BY DataSizeMB DESC, TotalRows DESC