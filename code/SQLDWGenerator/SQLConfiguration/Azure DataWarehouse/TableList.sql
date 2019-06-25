WITH base
AS
(
SELECT 
(s.name)+'.'+ (t.name)                              AS  [two_part_name]
,s.name                                                               AS  [schema_name]
, t.name                                                               AS  [table_name]
, nps.[row_count]                                                      AS  [row_count]
, (nps.[reserved_page_count] * 8.0)/1000                            AS [reserved_space_MB]
, tp.[distribution_policy_desc]                                        AS  [distribution_policy_name]
, c.[name]                                                             AS  [distribution_column]
, CASE WHEN i.[type_desc] = 'CLUSTERED' THEN 'CLUSTERED INDEX'
WHEN  i.[type_desc] = 'CLUSTERED COLUMNSTORE' THEN 'CLUSTERED COLUMNSTORE INDEX'
ELSE i.[type_desc] END
AS  [index_type_desc]
from 
    sys.schemas s
INNER JOIN sys.tables t ON s.[schema_id] = t.[schema_id]
INNER JOIN sys.indexes i ON  t.[object_id] = i.[object_id] AND i.[index_id] <= 1
INNER JOIN sys.pdw_table_distribution_properties tp ON t.[object_id] = tp.[object_id]
INNER JOIN sys.pdw_table_mappings tm ON t.[object_id] = tm.[object_id]
INNER JOIN sys.pdw_nodes_tables nt ON tm.[physical_name] = nt.[name]
INNER JOIN sys.dm_pdw_nodes pn ON  nt.[pdw_node_id] = pn.[pdw_node_id]
INNER JOIN sys.pdw_distributions di ON  nt.[distribution_id] = di.[distribution_id]
INNER JOIN sys.dm_pdw_nodes_db_partition_stats nps ON nt.[object_id] = nps.[object_id]
    AND nt.[pdw_node_id] = nps.[pdw_node_id]
    AND nt.[distribution_id] = nps.[distribution_id]
LEFT OUTER JOIN (select * from sys.pdw_column_distribution_properties where distribution_ordinal = 1) cdp ON t.[object_id] = cdp.[object_id]
LEFT OUTER JOIN sys.columns c ON cdp.[object_id] = c.[object_id]AND cdp.[column_id] = c.[column_id]
)
SELECT
[two_part_name] AS TableID
,  [schema_name] AS SchemaName
,  [table_name] AS TableName 
,  sum([row_count]) AS TotalRows
,SUM([reserved_space_MB]) aS DataSizeMB
,  [distribution_policy_name] AS DistributionType
,  [distribution_column] AS DistributionColumn
,  [index_type_desc] AS IndexType
	,'No' AS BCPSplit
	,'No' AS ReplaceCRLF
	,'True' AS SelectTable
FROM base
GROUP BY
[two_part_name] 
,  [schema_name]
,  [table_name]
,  [distribution_policy_name] 
,  [distribution_column] 
,  [index_type_desc] 
