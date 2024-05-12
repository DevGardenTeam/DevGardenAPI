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
        private List<File> _files;
        private List<Folder> _folders;

        public ReadOnlyCollection<File> Files { get; set; }

        public ReadOnlyCollection<Folder>? Folders { get; set; }

        public Folder()
        {
            Files = new ReadOnlyCollection<File>(_files);
            Folders = new ReadOnlyCollection<Folder>(_folders);
        }

        public bool Equals(Folder? other)
        {
            throw new NotImplementedException();
        }
    }
}
