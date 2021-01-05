using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAutomateForCrmSolution.PowerAutomateTemplates.CRM
{
	public class CurrentCrmTriggers
	{

		#region When_a_record_is_created_updated_or_deleted

		public static string When_a_record_is_created_updated_or_deleted
		{
			get { return Get_When_a_record_is_created_updated_or_deleted(); }
		}

		static string Get_When_a_record_is_created_updated_or_deleted()
		{
			return "\\\"triggers\\\":{ \\\"When_a_record_is_created,_updated_or_deleted\\\":{\\\"type\\\":\\\"OpenApiConnectionWebhook\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"SubscribeWebhookTrigger\\\",\\\"apiId\\\":\\\"\\/providers\\/Microsoft.PowerApps\\/apis\\/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"subscriptionRequest\\/message\\\":\\\"\\\",\\\"subscriptionRequest\\/entityname\\\":\\\"\\\",\\\"subscriptionRequest\\/scope\\\":\\\"\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"}}}";
				
		}

		#endregion
	}
}
