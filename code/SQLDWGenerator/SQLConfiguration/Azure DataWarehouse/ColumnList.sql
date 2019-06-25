SELECT 
    c.name ColumnName,
    t.Name DataType,
    c.max_length ColumnLength,
    c.precision ,
    c.scale ,
    c.is_nullable,
	c.column_id AS ColumnOrder
FROM sys.columns c
INNER JOIN sys.types t ON c.user_type_id = t.user_type_id
INNER JOIN sys.tables tbl on c.object_id = tbl.object_id
INNER JOIN sys.schemas s on tbl.schema_id = s.schema_id
WHERE
    s.name = @SchemaName
	and tbl.name = @TableName
ORDER BY c.column_id