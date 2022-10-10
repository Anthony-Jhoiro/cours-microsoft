using System;
using System.Collections.Generic;

namespace Leonardo
{
    public partial class TFibonacci
    {
        public Guid FibId { get; set; }
        public long FibInput { get; set; }
        public long FibOutput { get; set; }
        public DateTime FibCreatedTimestamp { get; set; }
        public DateTime FibLastExecutionTimestamp { get; set; }
    }
}