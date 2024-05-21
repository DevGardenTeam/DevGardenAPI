using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model
{
    public class Issue : ModelBase, IEquatable<Issue>
    {
        private string _title;
        private string _body;
        private string _state;
        private DateTime _creationDate;
        private Member _author;
        private List<Label>? _labels = new();

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("user")]
        public Member Author { get; set; }

        [JsonProperty("labels")]
        public ReadOnlyCollection<Label>? Labels { get; set; }

        public Issue()
        {
            Title = _title;
            Body = _body;
            State = _state;
            CreationDate = _creationDate;
            Author = _author;
            Labels = new ReadOnlyCollection<Label>(_labels);
        }

        public bool Equals(Issue? other)
        {
            throw new NotImplementedException();
        }
    }
}
