using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Represents a branch.
    /// </summary>
    public class Branch : ModelBase, IEquatable<Branch>
    {
        private string _name;
        private List<Commit> _commits = new();

        public string Name { get; set; }

        public ReadOnlyCollection<Commit> Commits { get; set; }

        public Branch()
        {
            Name = _name;
            Commits = new ReadOnlyCollection<Commit>(_commits);
        }

        public bool Equals(Branch? other)
        {
            throw new NotImplementedException();
        }
    }
}
