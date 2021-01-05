using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAutomateForCrmSolution.PowerAutomateTemplates.CRM
{
	/// <summary>
	/// shared_commondataservice
	/// </summary>
	public class CommonCrmActions
	{
		#region List_records
		public static string List_records
		{
			get { return Get_List_records(); }
		}

		static string Get_List_records()
		{
			return "actions\\\":{ \\\"List_records\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataservice\\\",\\\"operationId\\\":\\\"GetItems_V2\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_commondataservice\\\"},\\\"parameters\\\":{\\\"dataset\\\":\\\"default.cds\\\",\\\"table\\\":\\\"accounts\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"},\\\"runAfter\\\":{}}}";

		}
		#endregion

		#region Create_a_new_record

		public static string Create_a_new_record
		{
			get { return Get_Create_a_new_record(); }
		}
		static string Get_Create_a_new_record()
		{
			return "actions\\\":{ \\\"Create_a_new_record\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataservice\\\",\\\"operationId\\\":\\\"PostItem_V2\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_commondataservice\\\"},\\\"parameters\\\":{\\\"dataset\\\":\\\"default.cds\\\",\\\"table\\\":\\\"accounts\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"},\\\"runAfter\\\":{}}}";
			
		}
		#endregion


		#region Delete_a_record
		//DeleteItem
		public static string Delete_a_record
		{
			get { return Get_Delete_a_record(); }
		}
		static string Get_Delete_a_record()
		{
			return "actions\\\":{ \\\"Delete_a_record\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataservice\\\",\\\"operationId\\\":\\\"DeleteItem\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApp/apis/shared_commondataservice\\\"},\\\"parameters\\\":{\\\"dataset\\\":\\\"\\\",\\\"table\\\":\\\"\\\",\\\"id\\\":\\\"\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"},\\\"runAfter\\\":{}}}";
		}

		#endregion

		#region Update_a_record

		public static string Update_a_record
		{
			get { return Get_Update_a_record(); }
		}

		static string Get_Update_a_record()
		{
			return "actions\\\":{ \\\"Update_a_record\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataservice\\\",\\\"operationId\\\":\\\"PatchItem_V2\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_commondataservice\\\"},\\\"parameters\\\":{\\\"dataset\\\":\\\"default.cds\\\",\\\"table\\\":\\\"accounts\\\",\\\"id\\\":\\\"\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"},\\\"runAfter\\\":{}}}";
		}
		#endregion

		#region Get_record

		public static string Get_record
		{
			get { return Get_single_record(); }
		}

		static string Get_single_record()
		{
			return "actions\\\":{ \\\"Get_record\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataservice\\\",\\\"operationId\\\":\\\"GetItem_V2\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_commondataservice\\\"},\\\"parameters\\\":{\\\"dataset\\\":\\\"default.cds\\\",\\\"table\\\":\\\"accounts\\\",\\\"id\\\":\\\"\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"},\\\"runAfter\\\":{}}}";
		}

		#endregion

	}
}
