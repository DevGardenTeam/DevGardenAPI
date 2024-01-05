using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Folder : File, IEquatable<Folder>
    {
        #region Fields

        private List<File> _files;
        private List<Folder> _folders;

        #endregion

        #region Properties

        public ReadOnlyCollection<File> Files { get; set; }

        public ReadOnlyCollection<Folder>? Folders { get; set; }

        #endregion

        #region Constructor

        public Folder()
        {
            Files = new ReadOnlyCollection<File>(_files);
            Folders = new ReadOnlyCollection<Folder>(_folders);
        }

        #endregion

        #region Methods

        public bool Equals(Folder? other)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
