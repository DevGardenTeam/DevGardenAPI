using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class File : ModelBase, IEquatable<File>
    {
        private string _name;
        private long _size;
        private string _encoding;
        private string _content;

        public string Name { get; set; }

        public long Size { get; set; }

        public string Encoding { get; set; }

        public string Content { get; set; }

        public File()
        {
            Name = _name;
            Size = _size;
            Encoding = _encoding;
            Content = _content;
        }

        public bool Equals(File? other)
        {
            throw new NotImplementedException();
        }
    }
}
