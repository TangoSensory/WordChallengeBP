namespace WordChallenge.Services.Interfaces
{
    using WordChallenge.Model;

    public interface IChallengeSolver
    {
        WordPair Solve(string startWord, string targetWord);
    }
}
