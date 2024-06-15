using Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DevGardenAPI.DTO.Gitlab
{
    public class IssueGitlabDTO
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Body { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("author")]
        public MemberGitlabDTO Author { get; set; }

        // Temporarily hold the label names
        [JsonProperty("labels")]
        public List<string> LabelNames { get; set; }

        // The actual Label objects
        public List<Label> Labels { get; set; }

        public IssueGitlabDTO()
        {
            Title = string.Empty;
            Body = string.Empty;
            State = string.Empty;
            Author = new MemberGitlabDTO();
            LabelNames = new List<string>();
            Labels = new List<Label>();
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            // Convert each string label name to a Label object
            Labels = LabelNames.Select(name => new Label { Name = name }).ToList();
        }
    }
}
