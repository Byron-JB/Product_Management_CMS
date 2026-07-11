var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CMS>("cms");

builder.Build().Run();
