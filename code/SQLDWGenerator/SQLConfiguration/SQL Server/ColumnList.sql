SELECT 
    c.name ColumnName,
    t.Name DataType,
    c.max_length ColumnLength,
    c.precision ,
    c.scale ,
    c.is_nullable,
	c.column_id AS ColumnOrder,
	CASE WHEN ic.object_id IS NOT NULL THEN 1 ELSE 0 END AS PKColumn
FROM sys.columns c
INNER JOIN sys.types t ON c.system_type_id = t.user_type_id
INNER JOIN sys.tables tbl on c.object_id = tbl.object_id
INNER JOIN sys.schemas s on tbl.schema_id = s.schema_id
LEFT JOIN sys.indexes i on i.object_id = tbl.object_id and i.is_primary_key = 1
LEFT JOIN sys.index_columns ic on i.index_id = ic.index_id and i.object_id = ic.object_id and ic.column_id = c.column_id
WHERE
    s.name = @SchemaName
	and tbl.name = @TableName
ORDER BY c.column_id