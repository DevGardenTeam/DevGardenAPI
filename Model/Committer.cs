using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Committer : IEquatable<Committer>
    {
        #region Fields

        private string _name;
        private DateTime _date;

        #endregion

        #region Properties

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        #endregion

        #region Constructor

        public Committer()
        {
            Name = _name;
            Date = _date;
        }

        #endregion

        #region Methods

        public bool Equals(Committer? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
