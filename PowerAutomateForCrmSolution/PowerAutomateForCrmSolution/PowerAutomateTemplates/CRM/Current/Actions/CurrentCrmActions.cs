using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAutomateForCrmSolution.PowerAutomateTemplates.CRM
{
	/// <summary>
	/// shared_commondataserviceforapps
	/// </summary>
	public class CurrentCrmActions
	{
		#region List_records
		public static string List_records
		{
			get { return Get_List_records(); }
		}

		static string Get_List_records()
		{
			return "actions\\\":{\\\"List_records\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"ListRecords\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"entityName\\\":\\\"accounts\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"},\\\"runAfter\\\":{}}}";

		}
		#endregion

		#region Create_a_new_record

		public static string Create_a_new_record
		{
			get { return Get_Create_a_new_record(); }
		}

		static string Get_Create_a_new_record()
		{
			return "actions\\\":{ \\\"Create_a_new_record\\\":{ \\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{ \\\"host\\\":{ \\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"CreateRecord\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{ \\\"entityName\\\":\\\"accounts\\\"},\\\"authentication\\\":{ \\\"type\\\":\\\"Raw\\\",\\\"value\\\":\\\"@json(decodeBase64(triggerOutputs().headers['X-MS-APIM-Tokens']))['$ConnectionKey']\\\"} },\\\"runAfter\\\":{}}}";
		}
		#endregion


		#region Get_record

		public static string Get_record
		{
			get { return Get_single_record(); }
		}

		static string Get_single_record()
		{
			return "actions\\\":{\\\"Get_a_record\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"GetItem\\\",\\\"apiId\\\":\\\"/providers/Microsoft.PowerApps/apis/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"entityName\\\":\\\"\\\",\\\"recordId\\\":\\\"\\\"},\\\"authentication\\\":\\\"@parameters('$authentication')\\\"},\\\"runAfter\\\":{}}}";
		}

		#endregion

		#region Delete_a_record

		public static string Delete_a_record
		{
			get { return Get_Delete_a_record(); }
		}

		static string Get_Delete_a_record()
		{
			return "actions\\\":{ \\\"Delete_a_record\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"DeleteRecord\\\",\\\"apiId\\\":\\\"\\/providers\\/Microsoft.PowerApps\\/apis\\/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"entityName\\\":\\\"accounts\\\",\\\"recordId\\\":\\\"\\\"},\\\"authentication\\\":{\\\"type\\\":\\\"Raw\\\",\\\"value\\\":\\\"@json(decodeBase64(triggerOutputs().headers['X-MS-APIM-Tokens']))['$ConnectionKey']\\\"}},\\\"runAfter\\\":{}}}";
		}
		#endregion


		#region Get_file_or_image_content
		public static string Get_file_or_image_content
		{
			get { return file_or_image_content(); }
		}
		static string file_or_image_content()
		{
			return "actions\\\":{ \\\"Get_file_or_image_content\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"GetEntityFileImageFieldContent\\\",\\\"apiId\\\":\\\"\\/providers\\/Microsoft.PowerApps\\/apis\\/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"entityName\\\":\\\"accounts\\\",\\\"recordId\\\":\\\"\\\",\\\"fileImageFieldName\\\":\\\"\\\"},\\\"authentication\\\":{\\\"type\\\":\\\"Raw\\\",\\\"value\\\":\\\"@json(decodeBase64(triggerOutputs().headers['X-MS-APIM-Tokens']))['$ConnectionKey']\\\"}},\\\"runAfter\\\":{}}}";
		}

		#endregion


		#region Perform_a_bound_action

		public static string Perform_a_bound_action
		{
			get { return Get_Perform_a_bound_action(); }
		}

		static string Get_Perform_a_bound_action()
		{
			return "actions\\\":{ \\\"Perform_a_bound_action\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"PerformBoundAction\\\",\\\"apiId\\\":\\\"\\/providers\\/Microsoft.PowerApps\\/apis\\/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"entityName\\\":\\\"accounts\\\",\\\"actionName\\\":\\\"\\\",\\\"recordId\\\":\\\"\\\"},\\\"authentication\\\":{\\\"type\\\":\\\"Raw\\\",\\\"value\\\":\\\"@json(decodeBase64(triggerOutputs().headers['X-MS-APIM-Tokens']))['$ConnectionKey']\\\"}},\\\"runAfter\\\":{}}}";

		}

		#endregion

		#region Perform_an_unbound_action
		public static string Perform_an_unbound_action
		{
			get { return Get_Perform_an_unbound_action(); }
		}

		static string Get_Perform_an_unbound_action()
		{
			return "actions\\\":{ \\\"Perform_an_unbound_action\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"PerformUnboundAction\\\",\\\"apiId\\\":\\\"\\/providers\\/Microsoft.PowerApps\\/apis\\/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"actionName\\\":\\\"\\\"},\\\"authentication\\\":{\\\"type\\\":\\\"Raw\\\",\\\"value\\\":\\\"@json(decodeBase64(triggerOutputs().headers['X-MS-APIM-Tokens']))['$ConnectionKey']\\\"}},\\\"runAfter\\\":{}}}";
		}

		#endregion


		#region Update_a_record

		public static string Update_a_record
		{
			get { return Get_Update_a_record(); }
		}

		static string Get_Update_a_record()
		{
			return "actions\\\":{ \\\"Update_a_record\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"UpdateRecord\\\",\\\"apiId\\\":\\\"\\/providers\\/Microsoft.PowerApps\\/apis\\/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"entityName\\\":\\\"accounts\\\",\\\"recordId\\\":\\\"\\\"},\\\"authentication\\\":{\\\"type\\\":\\\"Raw\\\",\\\"value\\\":\\\"@json(decodeBase64(triggerOutputs().headers['X-MS-APIM-Tokens']))['$ConnectionKey']\\\"}},\\\"runAfter\\\":{}}}";
		}
		#endregion

		#region Relate_records
		public static string Relate_records
		{
			get { return Get_Relate_records(); }
		}

		static string Get_Relate_records()
		{
			return "actions\\\":{ \\\"Relate_records\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"AssociateEntities\\\",\\\"apiId\\\":\\\"\\/providers\\/Microsoft.PowerApps\\/apis\\/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"entityName\\\":\\\"accounts\\\",\\\"recordId\\\":\\\"\\\",\\\"associationEntityRelationship\\\":\\\"\\\",\\\"item\\/@odata.id\\\":\\\"\\\"},\\\"authentication\\\":{\\\"type\\\":\\\"Raw\\\",\\\"value\\\":\\\"@json(decodeBase64(triggerOutputs().headers['X-MS-APIM-Tokens']))['$ConnectionKey']\\\"}},\\\"runAfter\\\":{}}}";
		}

		#endregion


		#region Unrelate_records

		public static string Unrelate_records
		{
			get { return Get_Unrelate_records(); }
		}

		static string Get_Unrelate_records()
		{
			return "actions\\\":{ \\\"Unrelate_records\\\":{\\\"type\\\":\\\"OpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"DisassociateEntities\\\",\\\"apiId\\\":\\\"\\/providers/Microsoft.PowerApps\\/apis\\/shared_commondataserviceforapps\\\"},\\\"parameters\\\":{\\\"entityName\\\":\\\"accoutns\\\",\\\"recordId\\\":\\\"\\\",\\\"associationEntityRelationship\\\":\\\"\\\",\\\"$id\\\":\\\"\\\"},\\\"authentication\\\":{\\\"type\\\":\\\"Raw\\\",\\\"value\\\":\\\"@json(decodeBase64(triggerOutputs().headers['X-MS-APIM-Tokens']))['$ConnectionKey']\\\"}},\\\"runAfter\\\":{}}}";

		}
		#endregion

		#region Executes_a_changeset_request

		public static string Executes_a_changeset_request
		{
			get { return Get_Executes_a_changeset_request(); }
		}

		static string Get_Executes_a_changeset_request()
		{
			return "actions\\\":{ \\\"Executes_a_changeset_request\\\":{\\\"type\\\":\\\"Changeset\\\",\\\"kind\\\":\\\"ODataOpenApiConnection\\\",\\\"inputs\\\":{\\\"host\\\":{\\\"connectionName\\\":\\\"shared_commondataserviceforapps\\\",\\\"operationId\\\":\\\"ExecuteChangeset\\\",\\\"apiId\\\":\\\"\\/providers\\/Microsoft.PowerApps\\/apis\\/shared_commondataserviceforapps\\\"}},\\\"actions\\\":{},\\\"runAfter\\\":{}}}";

		}


		#endregion


	}
}
