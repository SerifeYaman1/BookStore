﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.LogModel
{
    public class LogDetails
    {
        public LogDetails()
        {
            CrateAt = DateTime.UtcNow;
        }
        public Object? ModelName {  get; set; }
        public Object? Controller { get; set;}
        public Object? Action { get; set; }
        public Object? Id { get; set; }
        public Object? CrateAt { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
