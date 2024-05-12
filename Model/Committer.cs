using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model
{
    public class Committer : IEquatable<Committer>
    {
        private string _name;
        private DateTime _date;

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        public Committer()
        {
            Name = _name;
            Date = _date;
        }

        public bool Equals(Committer? other)
        {
            throw new NotImplementedException();
        }
    }
}
