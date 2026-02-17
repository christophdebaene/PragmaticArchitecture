var builder = DistributedApplication.CreateBuilder(args);

//var dbPassword = builder.AddParameter("db-password");

var sqlServer = builder.AddSqlServer("sqlserver")
  //.WithDataVolume()
  .WithLifetime(ContainerLifetime.Persistent);

var todoDatabase = sqlServer.AddDatabase("Todo");

var api = builder.AddProject<Projects.Api>("api")
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Scalar";
        url.Url = "/scalar";
    })
    .WithReference(todoDatabase)
    .WaitFor(todoDatabase);

builder.Build().Run();
