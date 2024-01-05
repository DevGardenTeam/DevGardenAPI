using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class File : ModelBase, IEquatable<File>
    {
        #region Fields

        private string _name;
        private long _size;
        private string _encoding;
        private string _content;

        #endregion

        #region Properties

        public string Name { get; set; }
        
        public long Size { get; set; }

        public string Encoding { get; set; }

        public string Content { get; set; }

        #endregion

        #region Constructor

        public File()
        {
            Name = _name;
            Size = _size;
            Encoding = _encoding;
            Content = _content;
        }

        #endregion

        #region Methods

        public bool Equals(File? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
