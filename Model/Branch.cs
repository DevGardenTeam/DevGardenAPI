using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Represents a branch
    /// </summary>
    public class Branch : ModelBase
    {
       [JsonProperty("name")]
       public string Name { get; set; } = string.Empty;

       public List<Commit> Commits { get; set; } = new List<Commit>();
    }
}
