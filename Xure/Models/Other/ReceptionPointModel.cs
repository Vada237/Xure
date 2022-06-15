using Xure.Data;
using System.Collections.Generic;

namespace Xure.App.Models
{
    public class ReceptionPointModel
    {
        public IEnumerable<ReceptionPoint> receptionPoints { get; set; }

        public ReceptionPoint ReceptionPoint { get; set; }
    }
}
