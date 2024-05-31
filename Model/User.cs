using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User : IEquatable<User>
    {
        private string _name;

        public string Name { get; set; }

        public User()
        {
            Name = _name;
        }

        public bool Equals(User? other)
        {
            throw new NotImplementedException();
        }
    }
}
