using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Member : IEquatable<Member>
    {
        #region Fields

        private List<Repository>? _repositories = new ();

        #endregion

        #region Properties

        public ReadOnlyCollection<Repository>? Repositories { get; set; }

        #endregion

        #region Constructor

        public Member()
        {
            Repositories = new ReadOnlyCollection<Repository>(_repositories);
        }

        #endregion

        #region Methods

        public bool Equals(Member? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
