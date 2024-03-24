using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Model
{
    public class Member : ModelBase, IEquatable<Member>
    {
        #region Fields

        private string _name;
        private string _photoUrl;
        private List<Repository>? _repositories = new ();

        #endregion

        #region Properties

        [JsonProperty("login")]
        public string Name { get; set; }

        [JsonProperty("avatar_url")]
        public string PhotoUrl { get; set; }

        public ReadOnlyCollection<Repository>? Repositories { get; set; }

        #endregion

        #region Constructor

        public Member()
        {
            Name = _name;
            PhotoUrl = _photoUrl;
            Repositories = new ReadOnlyCollection<Repository>(_repositories);
        }

        #endregion

        #region Methods

        public bool Equals(Member? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
