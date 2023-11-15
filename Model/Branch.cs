using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Branch : ModelBase, IEquatable<Branch>
    {
        #region Fields

        private string _name;
        private List<Commit> _commits = new();

        #endregion

        #region Properties

        public string Name { get; set; }

        public ReadOnlyCollection<Commit> Commits { get; set; }

        #endregion

        public Branch()
        {
            Name = _name;
            Commits = new ReadOnlyCollection<Commit>(_commits);
        }

        #region Constructor

        #endregion

        #region Methods

        public bool Equals(Branch? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
