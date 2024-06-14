using Microsoft.CSharp.RuntimeBinder;
using Model;
using Newtonsoft.Json;

namespace DevGardenAPI.Adapter
{
    public class GitlabAdapter : PlatformAdapter
    {
        public Repository ExtractRepositories(string rawData)
        {
            var repository = new Repository();
            dynamic json = JsonConvert.DeserializeObject<dynamic>(rawData);

            try
            {
                repository.Name = json.name;
            }
            catch (RuntimeBinderException e)
            {
                // Handle errors related to accessing non-existing properties
                Console.WriteLine($"Error extracting property: {e.Message}");
                throw new Exception(message: e.Message);
            }
            catch (Exception e)
            {
                // Handle other types of errors
                Console.WriteLine($"Error extracting repositories: {e.Message}");
                throw new Exception(message: e.Message);
            }

            return repository;
        }
    }
}
