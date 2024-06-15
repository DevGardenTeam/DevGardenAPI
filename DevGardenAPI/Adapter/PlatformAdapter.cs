using Model;

namespace DevGardenAPI.Adapter
{
    /// <summary>
    /// The platform adapter interface. 
    /// (Adapter design pattern)
    /// </summary>
    public interface PlatformAdapter
    {
        /// <summary>
        /// Extracts the required repository data to build a repository model object.
        /// </summary>
        /// <param name="rawData">The raw data from external the external </param>
        /// <returns></returns>
        List<Repository> ExtractRepositories(string rawData);

        /// <summary>
        /// Extracts the required issue data to build a issue model object.
        /// </summary>
        /// <param name="rawData">The raw data from external the external </param>
        /// <returns></returns>
        List<Issue> ExtractIssues(string rawData);
    }
}
