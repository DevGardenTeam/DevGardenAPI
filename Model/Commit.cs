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
        private Member _author;
        private int _repositoryId;

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("author")]
        public Member Author { get; set; }

        public Commit()
        {
            Message = _message;
            Author = _author;
        }

        public bool Equals(Commit? other)
        {
            throw new NotImplementedException();
        }
    }
}
