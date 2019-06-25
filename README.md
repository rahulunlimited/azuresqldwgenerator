With Azure SQL Data Warehouse, organisations can leverage the Massively Parallel Processing architecture to process petabytes of data seamlessly. The cloud based Enterprise Data Warehouse has significantly reduced the time to implement a Data Warehouse. 

However, migrating existing data from SQL Server which can easily scale from couple of 100 GBs to TBs can be challenging and time consuming. The application helps migrate existing metadata and set the most optimised configuration for tables on the Azure SQL DW environment. It also generates easy to use scripts for Bulk Copy and leverages the PolyBase technology to import data easily and quickly to Azure SQL DW.

[Pre-requisites](/docs/pre-requisites.md)

[Initial Configuration](docs/configuration.md)


## Common Data Migration issues
The Script generator resolves some of the common **issues** while migrating data:
- UniqueIdentifier not supported in External table – Replace it with Varchar
- Text columns in database with CRLF does not work with BCP. Replace the CRLF character with space. 
- Ensure Column Orders are same in Table, External Table, BCP Output, INSERT
- Varchar(MAX) have a LEFT(Column, <LEN>)
- Geography to Varbinary
- Column separator has to be consistent and unique. Should not have the same value in the text description columns. E.g. |~| as column separator
- Does not support MAX length columns
- Certain data types (e.g XML, sysname) not supported

## SCRIPTS Required
In order to migrate data, the following scripts are required to be generated.
- BCP for All Tables and (Month/Year/Day/Specific Day/ID)
    - BCP Should contain create folder
    - BCP Script should have CRLF handling in SELECT
- PowerShell Script to Convert Unicode Files to UTF8
- PowerShell Script to Compress the File
- PowerShell/Batch File to copy the Files to Azure BLOB Storage
- CREATE TABLE 
   - Main Table with Distribution and Index
   - EXTERNAL TABLE with BLOB Name
- INSERT FROM EXTERNAL TABLE TO TABLE

## Benefits of using the Script Generator 

- Uses PolyBase to load data
- Allows to perform initial load in stages. Very helpful for large tables
- Can migrate one or many tables at a time
- Allows data on large tables to be split as per convenience
- Also allows to move schema from one environment to another
- Maintain metadata for multiple environments
- Follows best practices recommended by SQL Server CAT team. [Blog](https://blogs.msdn.microsoft.com/sqlcat/2016/08/18/migrating-data-to-azure-sql-data-warehouse-in-practice/)
- Has potential to integrate other data sources like Oracle, MySQL


