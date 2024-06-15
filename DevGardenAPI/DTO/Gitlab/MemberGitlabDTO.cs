using Newtonsoft.Json;

namespace DevGardenAPI.DTO.Gitlab
{
    public class MemberGitlabDTO
    {
        [JsonProperty("username")]
        public string Name { get; set; }

        [JsonProperty("avatar_url")]
        public string PhotoUrl { get; set; }

        public MemberGitlabDTO()
        {
            Name = string.Empty;
            PhotoUrl = string.Empty;
        }
    }
}
