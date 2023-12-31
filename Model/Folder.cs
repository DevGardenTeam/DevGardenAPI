using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Folder : File, IEquatable<Folder>
    {
        #region Fields

        private string _name;

        #endregion

        #region Properties

        #endregion

        #region Constructor

        public Folder()
        {
            Name = _name;
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
