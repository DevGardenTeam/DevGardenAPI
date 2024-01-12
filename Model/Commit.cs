using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Commit : ModelBase, IEquatable<Commit>
    {
        #region Fields

        private string? _message;
        private Member _author;
        private Repository _repository;

        #endregion

        #region Properties

        public string? Message { get; set; }

        public Member Author { get; set; }

        public Repository Repository { get; set; }

        #endregion

        #region Constructor

        public Commit()
        {
            Message = _message;
            Author = _author;
            Repository = _repository;
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
