using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Label : ModelBase, IEquatable<Label>
    {
        private string _name;

        public string Name { get; set; }

        public Label()
        {
            Name = _name;
        }

        public bool Equals(Label? other)
        {
            throw new NotImplementedException();
        }
    }
}
