using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAutomateForCrmSolution.Models
{
	public class ConnectorModel
	{
		public List<Connectors> value { get; set; }
	}

	public class Connectors
	{
		public string ActualName { get; set; }
		public string DisplayName { get; set; }
		public string iconUri { get; set; }

		public string publisher { get; set; }
		public string tier { get; set; }
		public string helpUrl { get; set; }
	}
}
