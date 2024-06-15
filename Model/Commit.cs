using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model
{
    public class Commit : ModelBase, IEquatable<Commit>
    {
        [JsonProperty("sha")]
        public string Sha { get; set; }

        [JsonProperty("author")]
        public Member Author { get; set; }

        // gets message and date
        [JsonProperty("commit")]
        public NestedCommit Commiter { get; set; }

        // holders
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public Commit()
        {
            Author = new Member();
            Sha = string.Empty;
            Message = string.Empty;
            Date = DateTime.MinValue;
        }

        public bool Equals(Commit? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Sha == other.Sha;
        }

        public override int GetHashCode()
        {
            return (Sha != null ? Sha.GetHashCode() : 0);
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.Message = this.Commiter.Message;
            this.Date = this.Commiter.NestedCommitter.Date;
        }

        public class NestedCommit
        {
            [JsonProperty("message")]
            public string Message { get; set; } = string.Empty;

            [JsonProperty("committer")]
            public NestedCommitter NestedCommitter { get; set; } = new();
        }

        public class NestedCommitter
        {
            [JsonProperty("date")]
            public DateTime Date { get; set; }
        }
    }
}
