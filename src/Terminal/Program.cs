using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Domain;
using Terminal;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMyAppTerminal();
    })
    .Build();

var dbContext = host.Services.GetRequiredService<MyAppContext>();
dbContext.Database.EnsureCreated();

while (true)
{
    var command = host.Services.GetRequiredService<CompositeCommand>();
    await command.ExecuteAsync(Menu.Main);

    /*
    Menu.Main

    var commandType = AnsiConsole.Prompt(
        new SelectionPrompt<Type>()
            .Title("Make your selection")
            .PageSize(10)
            .UseConverter(x => ICommand.Name(x))
            .AddChoices(new Type[] {
                typeof(ListUsers), typeof(AddUser), typeof(CreateDatabase)
            }));

    var command = serviceProvider.GetService(commandType) as ICommand;
    await command!.ExecuteAsync(NoCommandArguments.Value);
    */
}
