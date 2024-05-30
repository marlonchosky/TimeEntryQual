using ConsoleApp.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTimeEntryRecordLogic(builder);

using var host = builder.Build();
await host.RunAsync();
