using Model;
using Newtonsoft.Json;

namespace DevGardenAPI.DTO.Gitlab
{
    // All attributes are nullable for handling incomplete
    // data from external API (avoid code breaking exceptions).
    // The null values will be handled on the mapping phase, from
    // DTO to Model.
    public class RepositoryGitlabDTO
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("owner")]
        public MemberGitlabDTO NestedOwner { get; set; }

        [JsonProperty("security_and_compliance_access_level")]
        public string IsPrivate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("forking_access_level")]
        public string IsFork { get; set; } // enabled/disabled

        [JsonProperty("http_url_to_repo")]
        public string Url { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreationDate { get; set; }

        // GITLAB's REPO SCHEMA DOESN'T SEND US LANGUAGE BY DEFAULT

        [JsonProperty("size")]
        public Statistics NestedSize { get; set; }

        public class Statistics
        {
            [JsonProperty("repository_size")]
            public long Size { get; set; }
        }
    }


}
