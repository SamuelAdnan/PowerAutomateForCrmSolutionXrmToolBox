using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAutomateForCrmSolution.Models
{
	class FlowSharing
	{
	}
	public class Target
	{
		[JsonProperty("@odata.type")]
		public string OdataType { get; set; }
		public string workflowid { get; set; }
	}

	public class Principal
	{
		[JsonProperty("@odata.type")]
		public string OdataType { get; set; }
		public string ownerid { get; set; }
	}

	public class PrincipalAccess
	{
		public Principal Principal { get; set; }
		public string AccessMask { get; set; }
	}

	public class ShareFlow
	{
		public Target Target { get; set; }
		public PrincipalAccess PrincipalAccess { get; set; }
	}
}
