using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model
{
    public class Member : ModelBase, IEquatable<Member>
    {
        private string _name;
        private string _photoUrl;
        private List<Repository>? _repositories = new();

        [JsonProperty("login")]
        public string Name { get; set; }

        [JsonProperty("avatar_url")]
        public string PhotoUrl { get; set; }

        public ReadOnlyCollection<Repository>? Repositories { get; set; }

        public Member()
        {
            Name = _name;
            PhotoUrl = _photoUrl;
            Repositories = new ReadOnlyCollection<Repository>(_repositories);
        }

        public bool Equals(Member? other)
        {
            throw new NotImplementedException();
        }
    }
}
