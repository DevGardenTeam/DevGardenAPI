using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CommitDetails : IEquatable<CommitDetails>
    {
        #region Fields

        private Committer _committer;

        #endregion

        #region Properties

        [JsonProperty("committer")]
        public Committer Committer  { get; set; }

        #endregion

        #region Constructor

        public CommitDetails()
        {
            Committer = _committer;
        }

        #endregion

        #region Methods

        public bool Equals(CommitDetails? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
