using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAutomateForCrmSolution
{
	public partial class PowerAppsTemplates
	{





		#region HttpClient


		public struct Triggers
		{
			public static string When_a_HTTP_request_is_received = Get_When_a_HTTP_request_is_received();
			public static string Recurrance = Get_Recurrance();
			public static string HTTP = Get_HTTP();

			#region Private Helpers

			static string Get_When_a_HTTP_request_is_received()
			{
				When_a_HTTP_request_is_received = "\\\"triggers\\\":{\\\"manual\\\":{\\\"type\\\":\\\"Request\\\",\\\"kind\\\":\\\"Http\\\",\\\"inputs\\\":{\\\"schema\\\":{}}}}";
				return When_a_HTTP_request_is_received;
			}

			static string Get_Recurrance()
			{
				Recurrance = "\\\"triggers\\\": {\\\"Recurrence\\\": {\\\"recurrence\\\": {\\\"frequency\\\": \\\"Minute\\\",\\\"interval\\\": 5},\\\"type\\\": \\\"Recurrence\\\"}}";
				return Recurrance;
			}

			static string Get_HTTP()
			{
				HTTP = "\\\"triggers\\\":{ \\\"HTTP\\\":{ \\\"type\\\":\\\"Http\\\",\\\"inputs\\\":{ \\\"method\\\":\\\"GET\\\",\\\"uri\\\":\\\"https:\\/\\/youtube.com\\\"},\\\"recurrence\\\":{ \\\"interval\\\":1,\\\"frequency\\\":\\\"Minute\\\"}}}";
				return HTTP;
			}




			#endregion

		}

		#endregion


		#region Actions
		public struct Actions
		{
			public static string PowerPlatFormListAllEnvirnoments = Get_PowerPlatFormListAllEnvirnoments();
			public static string AllPowerApps = GetAllPowerApps();

			static string Get_PowerPlatFormListAllEnvirnoments()
			{
				PowerPlatFormListAllEnvirnoments = "actions\\\":{\\\"List_Environments_as_Admin\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_powerplatformforadmins\\\",\\\"operationId\\\":\\\"Get-AdminEnvironment\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_powerplatformforadmins\\\"},\\\"parameters\\\":{\\\"api-version\\\":\\\"2018-10-01\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"},\\\"runAfter\\\":{}}}";
				return PowerPlatFormListAllEnvirnoments;
			}

			static string GetAllPowerApps()
			{
				AllPowerApps = "actions\\\":{\\\"Get_Apps_as_Admin\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_powerappsforadmins\\\",\\\"operationId\\\":\\\"Get-AdminApps\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_powerappsforadmins\\\"},\\\"parameters\\\":{\\\"environment\\\":\\\"2016-11-01\\\",\\\"$top\\\":250},\\\"authentication\\\":{\\\"type\\\":\\\"Raw\\\",\\\"value\\\":\\\"@json(decodeBase64(triggerOutputs().headers['X-MS-APIM-Tokens']))['$ConnectionKey']\\\"}},\\\"runAfter\\\":{}}}";
				return AllPowerApps;
			}

		}


		#endregion



	}
}
