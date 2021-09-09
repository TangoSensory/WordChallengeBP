namespace WordChallenge.Services.Interfaces
{
    public interface IDataWriterService
    {
        bool CreateOutptTarget(string path);
        bool Write(string outputText);
    }
}
