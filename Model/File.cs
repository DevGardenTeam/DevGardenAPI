using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class File : IEquatable<File>
    {
        #region Fields

        private string _name;

        #endregion

        #region Properties

        public string Name { get; set; }

        #endregion

        #region Constructor

        public File()
        {
            Name = _name;
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
