using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAutomateForCrmSolution.PowerAutomateTemplates.CRM
{
	public class PABaseTemplate
	{

		public static string StartTemaplte
		{
			get { return Get_StartTemaplte(); }
		}
		static string Get_StartTemaplte()
		{
			return "\"{\\\"properties\\\":{\\\"definition\\\":{\\\"$schema\\\":\\\"https:\\/\\/schema.management.azure.com\\/providers\\/Microsoft.Logic\\/schemas\\/2016-06-01\\/workflowdefinition.json#\\\",\\\"actions\\\":{\\\"List_records\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"ListRecords\\\",\\\"apiId\\\":\\\"\\/providers\\/Microsoft.PowerApps\\/apis\\/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"entityName\\\":\\\"subscriptions\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"},\\\"runAfter\\\":{}}},\\\"parameters\\\":{\\\"$connections\\\":{\\\"defaultValue\\\":{},\\\"type\\\":\\\"Object\\\"},\\\"$authentication\\\":{\\\"defaultValue\\\":{},\\\"type\\\":\\\"SecureObject\\\"}},\"";
		}






		public static string EndTemaplte
		{
			get { return Get_EndTemaplte(); }
		}

		static string Get_EndTemaplte()
		{
			return "\"outputs\\\": {}}},\\\"schemaVersion\\\":\\\"1.0.0.0\\\"}\"";
		}
	}
}
