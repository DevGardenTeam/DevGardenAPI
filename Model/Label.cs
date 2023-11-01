using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Label : IEquatable<Label>
    {
        #region Fields

        private string _name;

        #endregion

        #region Properties

        public string Name { get; set; }

        #endregion

        #region Constructor

        public Label()
        {
            Name = _name;
        }

        #endregion

        #region Methods

        public bool Equals(Label? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
