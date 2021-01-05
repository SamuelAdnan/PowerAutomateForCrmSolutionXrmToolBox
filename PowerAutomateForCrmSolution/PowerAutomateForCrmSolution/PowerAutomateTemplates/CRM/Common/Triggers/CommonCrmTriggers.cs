using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAutomateForCrmSolution.PowerAutomateTemplates.CRM
{
	public class CommonCrmTriggers
	{

		#region When_a_record_is_created

		public static string When_a_record_is_created
		{
			get { return Get_When_a_record_is_created(); }
		}

		static string Get_When_a_record_is_created()
		{
			return "\\\"triggers\\\":{ \\\"When_a_record_is_created\\\":{\\\"type\\\":\\\"OpenApiConnectionWebhook\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataservice\\\",\\\"operationId\\\":\\\"SubscribeOnNewItems\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_commondataservice\\\"},\\\"parameters\\\":{\\\"dataset\\\":\\\"default.cds\\\",\\\"table\\\":\\\"accounts\\\",\\\"scope\\\":\\\"\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"}}}";
		}

		#endregion


		#region When_a_record_is_deleted
		public static string When_a_record_is_deleted
		{
			get { return Get_When_a_record_is_deleted(); }
		}

		static string Get_When_a_record_is_deleted()
		{
			return "\\\"triggers\\\":{ \\\"When_a_record_is_deleted\\\":{\\\"type\\\":\\\"OpenApiConnectionWebhook\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataservice\\\",\\\"operationId\\\":\\\"SubscribeOnDeletedItems\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_commondataservice\\\"},\\\"parameters\\\":{\\\"dataset\\\":\\\"default.cds\\\",\\\"table\\\":\\\"accounts\\\",\\\"scope\\\":\\\"\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"}}}";
		}


		#endregion


		#region When_a_record_is_selected
		public static string When_a_record_is_selected
		{
			get { return Get_When_a_record_is_selected(); }
		}

		static string Get_When_a_record_is_selected()
		{
			return "\\\"triggers\\\":{ \\\"manual\\\":{\\\"type\\\":\\\"Request\\\",\\\"kind\\\":\\\"apiconnection\\\",\\\"splitOn\\\":\\\"@triggerBody()['rows']\\\",\\\"inputs\\\":{\\\"schema\\\":{\\\"type\\\":\\\"object\\\",\\\"properties\\\":{},\\\"required\\\":[]},\\\"host\\\":{\\\"connection\\\":{\\\"name\\\":\\\"@parameters('$connections')['shared_commondataservice']['connectionId']\\\"}},\\\"operationId\\\":\\\"GetOnNewItems_V2\\\",\\\"parameters\\\":{\\\"dataset\\\":\\\"default.cds\\\",\\\"table\\\":\\\"accounts\\\"}}}}";
		}

		#endregion

		#region Update_a_record

		public static string Update_a_record
		{
			get { return Get_Update_a_record(); }
		}

		static string Get_Update_a_record()
		{
			return "\\\"triggers\\\":{ \\\"When_a_record_is_updated\\\":{\\\"type\\\":\\\"OpenApiConnectionWebhook\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataservice\\\",\\\"operationId\\\":\\\"SubscribeOnUpdatedItems\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_commondataservice\\\"},\\\"parameters\\\":{\\\"dataset\\\":\\\"default.cds\\\",\\\"table\\\":\\\"accounts\\\",\\\"scope\\\":\\\"\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"}}}";
		}
		#endregion


	}
}
