using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Issue : ModelBase, IEquatable<Issue>
    {
        #region Fields

        private string _name;
        private List<Label>? _labels = new();

        #endregion

        #region Properties

        public string Name { get; set; }

        public ReadOnlyCollection<Label>? Labels { get; set; }

        #endregion

        #region Constructor

        public Issue()
        {
            Name = _name;
            Labels = new ReadOnlyCollection<Label>(_labels);
        }

        #endregion

        #region Methods

        public bool Equals(Issue? other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
