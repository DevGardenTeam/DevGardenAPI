using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model
{
    public class CommitDetails : IEquatable<CommitDetails>
    {
        private Committer _committer;

        [JsonProperty("committer")]
        public Committer Committer { get; set; }

        public CommitDetails()
        {
            Committer = _committer;
        }

        public bool Equals(CommitDetails? other)
        {
            throw new NotImplementedException();
        }
    }
}
