using GetBusy.Core;
using GetBusy.Utils;
using RestSharp;
using System.Runtime.Caching;
using System.Text.Json;

namespace GetBusy.Services.Jokes
{
    public class ChuckNorrisJokeService : IJokeService
    {
        private readonly RestClient _restHttpClient;
        private readonly IApplicationLogger _logger;

        private MemoryCache JokeCache = new MemoryCache("ChuckNorrisJokes");
        private CacheItemPolicy CachePolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(600.0)
        };

        private JokeCache LastNewJoke = new JokeCache();
        private JokeCache LastNavigatedJoke = new JokeCache();

        internal ChuckNorrisJokeService(RestClient restHttpClient, IApplicationLogger logger)
        {
            _restHttpClient = restHttpClient;
            _logger = logger;
        }

        public async Task<JokeDto> GetNewJokeAsync(CancellationToken token = default)
        {
            JokeDto joke;
            do
            {
                joke = await FetchNewJokeAsync(token);
            } while (JokeCache.Get(joke.Id) != null);

            var cachedJoke = new JokeCache
            {
                Id = joke.Id,
                CreatedAt = joke.CreatedAt,
                IconUrl = joke.IconUrl,
                UpdateAt = joke.UpdateAt,
                URL = joke.URL,
                Value = joke.Value,
                PreviousJokeId = LastNewJoke?.Id
            };

            JokeCache.Add(joke.Id, cachedJoke, CachePolicy);
            LastNewJoke = LastNavigatedJoke = cachedJoke;

            return joke;
        }

        public JokeDto GetNextJoke()
        {
            if (LastNavigatedJoke?.NextJokeId == null)
            {
                throw new BadRequestException("No newer joke available.");
            }

            var nextJoke = (JokeCache)JokeCache.Get(LastNavigatedJoke.NextJokeId);
            nextJoke.PreviousJokeId = LastNavigatedJoke.Id;

            LastNavigatedJoke = nextJoke;

            return nextJoke;
        }

        public JokeDto GetPreviousJoke()
        {
            if (LastNavigatedJoke?.PreviousJokeId == null)
            {
                throw new BadRequestException("No older joke available.");

            }

            var previousJoke = (JokeCache)JokeCache.Get(LastNavigatedJoke.PreviousJokeId);
            previousJoke.NextJokeId = LastNavigatedJoke.Id;

            LastNavigatedJoke = previousJoke;

            return previousJoke;

        }

        private async Task<JokeDto> FetchNewJokeAsync(CancellationToken token = default)
        {
            try
            {
                var randomJokePath = "/jokes/random";
                var response = await _restHttpClient.GetAsync(new RestRequest(randomJokePath), token);

                var jsonOptions = new JsonSerializerOptions();
                jsonOptions.Converters.Add(new DateTimeJsonConverter());

                var joke = JsonSerializer.Deserialize<JokeDto>(response.Content, jsonOptions);

                _logger.LogInformation($"Retrieved a new joke with ID: {joke.Id} ");

                return joke;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                throw;
            }

        }

    }
}
