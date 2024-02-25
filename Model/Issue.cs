using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Issue : ModelBase, IEquatable<Issue>
    {
        #region Fields

        private string _title;
        private string _body;
        private string _state;
        private DateTime _creationDate;
        private Member _author;
        private Milestone _milestone;
        private Repository _repository;
        private List<Label>? _labels = new();

        #endregion

        #region Properties

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

        [JsonProperty("milestone")]
        public Milestone Milestone { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("labels")]
        public ReadOnlyCollection<Label>? Labels { get; set; }

        #endregion

        #region Constructor

        public Issue()
        {
            Title = _title;
            Body = _body;
            State = _state;
            CreationDate = _creationDate;
            Author = _author;
            Milestone = _milestone;
            Repository = _repository;
            Labels = new ReadOnlyCollection<Label>(_labels);
        }

        #endregion

        #region Methods

        public bool Equals(Issue? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
