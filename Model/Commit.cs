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
        private string _sha;
        private string? _message;
        private DateTime? _date;
        private Member _author;
        private int _repositoryId;

        [JsonProperty("sha")]
        public string? Sha { get; set; }

        [JsonProperty("commit.message")]
        public string? Message { get; set; }

        [JsonProperty("commit.committer.date")]
        public DateTime? Date { get; set; }

        [JsonProperty("author")]
        public Member Author { get; set; }

        public Commit()
        {
            Sha = _sha;
            Message = _message;
            Date = _date;
            Author = _author;
        }

        public bool Equals(Commit? other)
        {
            throw new NotImplementedException();
        }
    }
}
