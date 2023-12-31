using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Repository : ModelBase, IEquatable<Repository>
    {
        #region Fields

        private string _name;
        private List<Folder>? _folders = new();
        private List<File> _files = new();
        private List<Branch> _branches = new();
        private List<Issue> _issues = new();

        #endregion

        #region Properties

        public string Name { get; set; }

        public ReadOnlyCollection<Folder>? Folders { get; set; }

        public ReadOnlyCollection<File> Files { get; set; }

        public ReadOnlyCollection<Branch> Branches { get; set; }

        public ReadOnlyCollection<Issue> Issues { get; set; }

        #endregion

        #region Constructor

        public Repository()
        {
            Name = _name;
            Folders = new ReadOnlyCollection<Folder>(_folders);
            Files = new ReadOnlyCollection<File>(_files);
            Branches = new ReadOnlyCollection<Branch>(_branches);
            Issues = new ReadOnlyCollection<Issue>(_issues);
        }

        #endregion

        #region Methods

        public bool Equals(Repository? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
