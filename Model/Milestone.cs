using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Milestone : ModelBase, IEquatable<Milestone>
    {
        #region Fields

        private string _name;

        #endregion

        #region Properties

        public string Name { get; set; }

        #endregion

        #region Constructor

        public Milestone()
        {
            Name = _name;
        }

        #endregion

        #region Methods

        public bool Equals(Milestone? other)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
