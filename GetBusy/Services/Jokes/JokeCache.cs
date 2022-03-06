namespace GetBusy.Services.Jokes
{
    public class JokeCache : JokeDto
    {
        public string PreviousJokeId { get; set; }

        public string NextJokeId { get; set; }
    }
}
