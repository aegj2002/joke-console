namespace GetBusy.Services.Jokes
{
    public interface IJokeService
    {
        Task<JokeDto> GetNewJokeAsync(CancellationToken token = default);

        JokeDto GetPreviousJoke();

        JokeDto GetNextJoke();
    }
}
