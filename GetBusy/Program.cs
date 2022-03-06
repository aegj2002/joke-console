using GetBusy;
using GetBusy.Core;
using GetBusy.Services.Jokes;
using GetBusy.Utils;
using RestSharp;

ConsoleKeyInfo consoleKey;

var options = new RestClientOptions("https://api.chucknorris.io");
var chuckNorrisClient = new RestClient(options);
var logger = new SimpleLogger();

IJokeService jokeService = new ChuckNorrisJokeService(chuckNorrisClient, logger);


Console.WriteLine("J: Get new joke, P|N: Navigate back|forward in previous jokes.");


do
{
    consoleKey = Console.ReadKey();

    try
    {
        Console.WriteLine();
        await RunChucknorrisAsync(consoleKey.Key);
        Console.WriteLine();
    }
    catch (BadRequestException ex)
    {
        Console.WriteLine();
        CustomConsole.WriteWarning(ex.Message);
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        logger.LogError(ex.Message, ex.StackTrace);
        CustomConsole.WriteError("Unexpected error occured. Please try again later. If the issue still exists, please report this to the development team.");
    }

}
while (consoleKey.Key != ConsoleKey.X);




async Task RunChucknorrisAsync(ConsoleKey key, CancellationToken token = default)
{
    switch (key)
    {
        case ConsoleKey.J:
            Console.WriteLine((await jokeService.GetNewJokeAsync(token)).Value);
            break;
        case ConsoleKey.P:
            Console.WriteLine(jokeService.GetPreviousJoke().Value);
            break;
        case ConsoleKey.N:
            Console.WriteLine(jokeService.GetNextJoke().Value);
            break;
        default:
            throw new BadRequestException($"Key {key} is not supported.");
    }
}
