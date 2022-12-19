<h3 align="center">Number After String</h3>

  <p align="center">
    An example CLR function which returns the number in a string after a user defined pattern.
    <br />
  </p>
</div>

## About
SQL Server CLR enables you to extend the capabilities of the SQL Server database engine with the rich programming possibilities of the . NET Framework in terms of maths and string handling.

This project is an example CLR function developed in C# which returns the number in a string after a user defined pattern.  The T-SQL code needed to integrate the function into SQL server is included below.

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Prerequisites

* Microsoft SQL Server 2016 (or later).
* Microsoft SQL Server Management Studio
* Visual Studio
<p align="right">(<a href="#top">back to top</a>)</p>

## Integration

Having compiled the NumberAfterString.dll library, it needs to be integrated into SQL Server and the corresponding function needs to be created in the relevant database.

Place the library on the server or system that hosts your instance of SQL Server and make a note of the path.

Use Management Studio to connect to the database that you want to use the function with and run the following script (substituting the path I have used for the location where you have placed the library file).

```TSQL
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

sp_configure 'show advanced options', 1
RECONFIGURE
GO
sp_configure 'clr enabled', 1
RECONFIGURE
GO
sp_configure 'show advanced options', 0
RECONFIGURE
GO

IF EXISTS(SELECT 1 FROM sys.assemblies WHERE name = 'NumberAfterString')
BEGIN
DROP ASSEMBLY [NumberAfterString]
END
GO
CREATE ASSEMBLY [NumberAfterString]
FROM 'C:\CLR\NumberAfterString.dll'
WITH PERMISSION_SET = SAFE
GO

```

Now that the relevant assembly is in place, running the script below will create the corresponding function that allows it to be used within the database.

```TSQL
IF OBJECT_ID('[NumberAfterString]') IS NOT NULL
DROP FUNCTION [NumberAfterString]
GO
CREATE FUNCTION [NumberAfterString]
(
  @string_in NVARCHAR(MAX),
  @pattern NVARCHAR(255)
)
RETURNS NVARCHAR(255)
WITH EXECUTE AS CALLER
AS 
EXTERNAL NAME [NumberAfterString].[CLRFunction].[NumberAfterString]
GO

```

<p align="right">(<a href="#top">back to top</a>)</p>


## Example Usage

```TSQL
SELECT dbo.NumberAfterString('Year:1984', 'Year:')
-- returns 1984

SELECT dbo.NumberAfterString('Annual Sales 4402', 'Annual Sales')
-- returns 4402

SELECT dbo.NumberAfterString('She earns 4000 per month', 'She earns')
-- returns 4000

```

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. 

* Fork the Project
* Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
* Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
* Push to the Branch (`git push origin feature/AmazingFeature`)
* Open a Pull Request
<p align="right">(<a href="#top">back to top</a>)</p>

## Authors

* **Christopher Lee** (https://github.com/chrislee1018/)
<p align="right">(<a href="#top">back to top</a>)</p>

## License

This project is licensed under the GNU General Public License License 3.0 - see the [LICENSE](LICENSE) file for details.
<p align="right">(<a href="#top">back to top</a>)</p>

