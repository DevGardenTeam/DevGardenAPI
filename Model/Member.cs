using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model
{
    public class Member : ModelBase, IEquatable<Member>
    {
        private List<Repository> _repositories = new();

        public string Name { get; set; } = string.Empty;

        public string PhotoUrl { get; set; } = string.Empty;

        public ReadOnlyCollection<Repository> Repositories => _repositories.AsReadOnly();

        public Member() { }

        public bool Equals(Member? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && PhotoUrl == other.PhotoUrl && RepositoriesEquals(Repositories, other.Repositories);
        }

        private static bool RepositoriesEquals(ReadOnlyCollection<Repository> repos1, ReadOnlyCollection<Repository> repos2)
        {
            if (repos1 == null && repos2 == null) return true;
            if (repos1 == null || repos2 == null) return false;
            if (repos1.Count != repos2.Count) return false;

            for (int i = 0; i < repos1.Count; i++)
            {
                if (!repos1[i].Equals(repos2[i])) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, PhotoUrl, Repositories);
        }
    }
}
