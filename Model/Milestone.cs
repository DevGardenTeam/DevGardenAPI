using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Milestone : ModelBase, IEquatable<Milestone>
    {
        private string _name;

        public string Name { get; set; }

        public Milestone()
        {
            Name = _name;
        }

        public bool Equals(Milestone? other)
        {
            throw new NotImplementedException();
        }
    }
}
