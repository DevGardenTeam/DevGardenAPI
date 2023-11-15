using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ModelBase
    {
        #region Properties

        public long Id { get; set; }
        
        public DateTime CreationDate { get; set; } = DateTime.Now;

        #endregion
    }
}
