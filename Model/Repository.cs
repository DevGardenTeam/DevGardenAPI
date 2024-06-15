using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model
{
    public class Repository : ModelBase, IEquatable<Repository>
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("owner")]
        public Member Owner { get; set; }

        [JsonProperty("private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fork")]
        public bool IsFork { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        public ReadOnlyCollection<Branch> Branches { get; private set; }

        public ReadOnlyCollection<Commit> Commits { get; private set; }

        public ReadOnlyCollection<Member> Contributors { get; private set; }

        public ReadOnlyCollection<Issue> Issues { get; private set; }

        public ReadOnlyCollection<Folder>? Folders { get; private set; }

        public ReadOnlyCollection<File> Files { get; private set; }

        public Repository()
        {
            Branches = new ReadOnlyCollection<Branch>(new List<Branch>());
            Commits = new ReadOnlyCollection<Commit>(new List<Commit>());
            Contributors = new ReadOnlyCollection<Member>(new List<Member>());
            Issues = new ReadOnlyCollection<Issue>(new List<Issue>());
            Folders = new ReadOnlyCollection<Folder>(new List<Folder>());
            Files = new ReadOnlyCollection<File>(new List<File>());
        }

        public bool Equals(Repository? other)
        {
            if (other == null) return false;
            return Id == Id;
        }

        // Consider overriding Object's Equals and GetHashCode methods.
    }
}
