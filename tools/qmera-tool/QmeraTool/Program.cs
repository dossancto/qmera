using Cocona;

using QmeraTool.App.Commands;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand("app.server", AppCommands.AppServer)
.WithDescription("Run Qmera Application");

app.AddCommand("gen.scheme", GenerateCommands.GenerateScheme)
.WithDescription("Gen Application Scheme only");

app.AddCommand("gen.api", GenerateCommands.GenerateApi)
.WithDescription("Gen Application Api");

app.AddCommand("gen.web", GenerateCommands.GenerateWeb)
.WithDescription("Gen Application web pages using Blazor");

app.Run();