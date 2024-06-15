using DevGardenAPI.DTO.Gitlab;
using Model;
using Newtonsoft.Json;

namespace DevGardenAPI.Adapter
{
    public class GitlabAdapter : PlatformAdapter
    {
        public List<Repository> ExtractRepositories(string rawData)
        {
            var repositories = new List<Repository>();

            try
            {
                // Deserialize into the appropriate repository class
                List<RepositoryGitlabDTO> repositoriesDTO = JsonConvert.DeserializeObject<List<RepositoryGitlabDTO>>(rawData);
                if (repositoriesDTO == null) throw new InvalidOperationException("Deserialized data is null.");

                foreach (var repoDTO in repositoriesDTO)
                {
                    var repository = new Repository
                    {
                        // Non-nested attributes
                        Name = repoDTO.Name,
                        Description = repoDTO.Description,
                        IsPrivate = repoDTO.IsPrivate == "private",
                        IsFork = repoDTO.IsFork == "enabled",
                        Url = repoDTO.Url,
                        CreationDate = repoDTO.CreationDate,

                        // Nested attributes
                        Size = repoDTO.NestedSize?.Size ?? 0,
                        Owner = new Member
                        {
                            Name = repoDTO.NestedOwner?.Name,
                            PhotoUrl = repoDTO.NestedOwner?.PhotoUrl
                        }
                    };

                    repositories.Add(repository);
                }
            }
            catch (JsonSerializationException ex)
            {
                // Log the exception or throw a custom exception to indicate JSON parsing failure
                throw new InvalidOperationException("Failed to deserialize GitLab repositories data.", ex);
            }
            catch (Exception ex)
            {
                // Handle other unforeseen errors
                throw new InvalidOperationException("An error occurred while extracting repositories.", ex);
            }

            return repositories;
        }
    }
}
