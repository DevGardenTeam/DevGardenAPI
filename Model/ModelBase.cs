﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model
{
    public class ModelBase
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
