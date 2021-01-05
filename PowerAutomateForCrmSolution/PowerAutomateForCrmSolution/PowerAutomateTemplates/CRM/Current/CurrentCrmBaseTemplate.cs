﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAutomateForCrmSolution.PowerAutomateTemplates.CRM
{
	/// <summary>
	/// shared_commondataserviceforapps  (current)
	/// </summary>
	public class CurrentCrmBaseTemplate
	{

		#region Public members

		public static string StartTemaplte
		{
			get { return Get_StartTemaplte(); }
		}

		public static string EndTemaplte
		{
			get { return Get_EndTemaplte(); }
		}

		#endregion


		#region Private Helpers
		static string Get_StartTemaplte()
		{
			string connectionName = $"shared_commondataserviceforapps-{Guid.NewGuid().ToString()}";
			return "\"{\\\"properties\\\":{\\\"connectionReferences\\\":{\\\"shared_commondataserviceforapps\\\":{\\\"connectionName\\\":\\\"" + connectionName + "\\\",\\\"source\\\":\\\"Invoker\\\",\\\"id\\\":\\\"\\/providers\\/Microsoft.Power Apps\\/apis\\/shared_commondataserviceforapps\\\"}},\\\"definition\\\":{\\\"$schema\\\": \\\"https:\\/\\/schema.management.azure.com\\/providers\\/Microsoft.Logic\\/schemas\\/2016-06-01\\/workflowdefinition.json#\\\",\\\"contentVersion\\\": \\\"1.0.0.0\\\",\\\"parameters\\\": {\\\"$connections\\\": {\\\"defaultValue\\\": {},\\\"type\\\": \\\"Object\\\"},\\\"$authentication\\\": {\\\"defaultValue\\\": {},\\\"type\\\": \\\"SecureObject\\\"}},\\\"";
		}
		static string Get_EndTemaplte()
		{
			return "\\\"outputs\\\": {}}},\\\"schemaVersion\\\":\\\"1.0.0.0\\\"}\"";
		}

		#endregion
	}
}
