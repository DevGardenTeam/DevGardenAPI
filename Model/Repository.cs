using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Repository : ModelBase, IEquatable<Repository>
    {
        #region Fields

        private string _name;
        private Member _owner;
        private bool _isPrivate;
        private string _description;
        private bool _isFork;
        private string _url;
        private DateTime _creationDate;
        private string _language;
        private long _size;
        private List<Branch> _branches = new();
        private List<Commit> _commits = new();
        private List<Member> _contributors = new();
        private List<Issue> _issues = new();
        private List<Folder>? _folders = new();
        private List<File> _files = new();

        #endregion

        #region Properties

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

        public ReadOnlyCollection<Branch> Branches { get; set; }
        
        public ReadOnlyCollection<Commit> Commits { get; set; }

        public ReadOnlyCollection<Member> Contributors { get; set; }

        public ReadOnlyCollection<Issue> Issues { get; set; }

        public ReadOnlyCollection<Folder>? Folders { get; set; }

        public ReadOnlyCollection<File> Files { get; set; }

        #endregion

        #region Constructor

        public Repository()
        {
            Name = _name;
            Owner = _owner;
            IsPrivate = _isPrivate;
            Description = _description;
            IsFork = _isFork;
            Url = _url;
            CreationDate = _creationDate;
            Language = _language;
            Size = _size;
            Branches = new ReadOnlyCollection<Branch>(_branches);
            Commits = new ReadOnlyCollection<Commit>(_commits);
            Contributors = new ReadOnlyCollection<Member>(_contributors);
            Issues = new ReadOnlyCollection<Issue>(_issues);
            Folders = new ReadOnlyCollection<Folder>(_folders);
            Files = new ReadOnlyCollection<File>(_files);
        }

        #endregion

        #region Methods

        public bool Equals(Repository? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
