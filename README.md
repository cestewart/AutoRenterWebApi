Auto Renter API

This is the .Net 4.5, C#, WebApi solution for the API for the Auto Renter Reference Application.

You do not have to run the API. It can be found hosted on Azure at http://autorenterapi.azurewebsites.net/api/.

To confirm the API is working call http://autorenterapi.azurewebsites.net/api/HelloWorld/ with the GET verb.

The API uses Code First and a SQL Server 2012 database. To get a local copy of the database update the connection string to use LocalDb.

<add name="AutoRenterDatabaseContext" connectionString="Data Source=(localdb)\v11.0;Initial Catalog=Invoicing;Integrated Security=True;MultipleActiveResultSets=True" providerName="System.Data.SqlClient" />

The API uses ELMAH logging. You can see the log at http://autorenterapi.azurewebsites.net/elmah.axd.