using Model;
using Newtonsoft.Json;
using static Model.Commit;

namespace DevGardenAPI.DTO.Gitlab
{
    public class CommitGitlabDTO
    {
        [JsonProperty("id")]
        public string Sha { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("authored_date")]
        public DateTime Date { get; set; }

        public CommitGitlabDTO()
        {
            AuthorName = string.Empty;
            Sha = string.Empty;
            Message = string.Empty;
            Date = DateTime.MinValue;
        }
    }
}
