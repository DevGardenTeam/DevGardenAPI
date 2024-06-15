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
        private List<Label> _labels = new();

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("body")]
        public string Body { get; set; } = string.Empty;

        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;

        [JsonProperty("created_at")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("user")]
        public Member Author { get; set; } = new Member();

        [JsonProperty("labels")]
        public List<Label> Labels
        {
            get => _labels;
            set => _labels = value ?? new List<Label>();
        }

        public ReadOnlyCollection<Label> ReadOnlyLabels => _labels.AsReadOnly();

        public Issue() { }

        public bool Equals(Issue? other)
        {
            if (other == null) return false;
            return Title == other.Title && Body == other.Body && State == other.State &&
                   CreationDate == other.CreationDate && Equals(Author, other.Author) &&
                   LabelsEqual(Labels, other.Labels);
        }

        private static bool LabelsEqual(List<Label> labels1, List<Label> labels2)
        {
            if (labels1.Count != labels2.Count) return false;
            for (int i = 0; i < labels1.Count; i++)
            {
                if (!labels1[i].Equals(labels2[i])) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Body, State, CreationDate, Author, Labels);
        }
    }
}
