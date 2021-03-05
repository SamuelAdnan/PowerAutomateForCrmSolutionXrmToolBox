using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAutomateForCrmSolution.Models
{
    public class FlowActions
    {
        public int Total { get; set; }
        public List<string> Actions { get; set; }
    }

    public class FlowTriggers
    {
        public int Total { get; set; }
        public List<string> Triggers { get; set; }
    }

    public class FlowTables
    {
        public int Total { get; set; }
        public List<string> Tables { get; set; }
    }

    public class FlowConnectors
    {
        public int Total { get; set; }
        public List<string> Connectors { get; set; }
    }
    
    public class FlowData
    {
        public string name { get; set; }
        public FlowConnectors Connectors { get; set; }
        public FlowTables Tables { get; set; }
        public FlowTriggers Triggers { get; set; }
        public FlowActions Actions { get; set; }
    }

    public class FlowModel
    {
        public List<FlowData> flows { get; set; }
    }
}
