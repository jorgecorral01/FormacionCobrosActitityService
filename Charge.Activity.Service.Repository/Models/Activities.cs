using System;
using System.Collections.Generic;

namespace Charge.Activity.Service.Repository.Entity.Models {
    public partial class Activities {
        public int IdActivity { get; set; }
        public string Identifier { get; set; }
        public DateTime DateReception { get; set; }
        public DateTime? DateSend { get; set; }
        public bool? AddResult { get; set; }
    }
}
