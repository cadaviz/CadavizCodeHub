using CadavizCodeHub.WebApi.Setup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

WebHost.CreateDefaultBuilder(args)
       .UseStartup<Startup>()
       .Build()
       .Run();