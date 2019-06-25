
The first time when you start the application, you are presented with the following Configuration screen.

![image.png](/images/azuresqldwgenconfig.png)

Please follow the guide below to create a new configuration of update an existing configuration:

| Configuration | Value |
|---------------------------------|----------------|
|Connection Type | SQL Server |
| Authentication | Chose either Integrated or SQL, If you specify SQL Authentication, then you need to specify the SQL User Name and Password |
| Server Name | The server name where the Source database is available |
| Database Name | Name of the source database which is to be migrated |
| User Name | SQL User Name if SQL Authentication was selected |
| Password | Password for SQL User if SQL Authentication was selected |
| BCP Output Folder | Folder where Bulk Copy output will be stored. e.g C:\TEMP\Migration\ |
BCP Output Format | The BCP output should be in Unicode format. SQL Server 2014 SP2 onward supports Unicode output. If your SQL Server version is before SQL Server 2014 SP2, please select UTF8 and the application will convert the file output to Unicode format |
| Storage Account | BLOB Storage account Name. The output files from the source database is uploaded to Azure BLOB Storage account |
| *Container Name* | The container name in the BLOB Storage | 
| Storage Key | The Storage Key to access the BLOB Storage account |
| *7-zip Location* | The file path for 7-zip |
| *AZCopy Location* | The file path for AZCopy |
| Configuration Name | The name for Configuration settings |

You can click on the ![image.png](/.attachments/image-f2cd50ec-13a3-4244-8e2b-fbc6ed05f298.png) buttons to load the default values for some Configuration. (Container Name, 7-zip, AZCopy).

Click Save after you have updated the Configuration. And chose Load Configuration to load start to use the information from the configuration.

Please note you can have multiple configuration files if you are working with more than one source databases.
