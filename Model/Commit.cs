using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model
{
    public class Commit : ModelBase, IEquatable<Commit>
    {
        private string? _message;
        private CommitDetails _commitDetails;
        private Member _author;
        private Repository _repository;

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("commit")]
        public CommitDetails CommitDetails { get; set; }

        [JsonProperty("author")]
        public Member Author { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        public Commit()
        {
            Message = _message;
            CommitDetails = _commitDetails;
            Author = _author;
            Repository = _repository;
        }

        public bool Equals(Commit? other)
        {
            throw new NotImplementedException();
        }
    }
}
