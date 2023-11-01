using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Commit : IEquatable<Commit>
    {
        #region Fields

        private string _id;
        private string? _message;

        #endregion

        #region Properties

        public string Id { get; set; }

        public string? Message { get; set; }

        #endregion

        #region Constructor

        public Commit()
        {
            Id = _id;
            Message = _message;
        }

        #endregion

        #region Methods

        public bool Equals(Commit? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
