using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using PowerAutomateForCrmSolution.Models;
using PowerAutomateForCrmSolution.PowerAutomateTemplates.CRM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace PowerAutomateForCrmSolution
{
	public partial class MyPluginControl : PluginControlBase
	{
		private Settings mySettings;

		public MyPluginControl()
		{
			InitializeComponent();
		}

		private void MyPluginControl_Load(object sender, EventArgs e)
		{
			ShowInfoNotification("Please select crm solution or create a new solution to continue.", new Uri("https://github.com/MscrmTools/XrmToolBox"));

			// Loads or creates the settings for the plugin
			if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
			{
				mySettings = new Settings();

				LogWarning("Settings not found => a new settings file has been created!");
			}
			else
			{
				LogInfo("Settings found and loaded");
			}

			ExecuteMethod(GetSolutions);
			ExecuteMethod(loadFlows);
			ShowInfoNotification("Please select crm solution or create a new solution to continue.", new Uri("https://github.com/MscrmTools/XrmToolBox"));

		}

		private void tsbClose_Click(object sender, EventArgs e)
		{
			CloseTool();
		}

		private void tsbSample_Click(object sender, EventArgs e)
		{
			// The ExecuteMethod method handles connecting to an
			// organization if XrmToolBox is not yet connected
			//ExecuteMethod(GetAccounts);
		}



		/// <summary>
		/// This event occurs when the plugin is closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
		{
			// Before leaving, save the settings
			SettingsManager.Instance.Save(GetType(), mySettings);
		}

		/// <summary>
		/// This event occurs when the connection has been updated in XrmToolBox
		/// </summary>
		public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
		{
			base.UpdateConnection(newService, detail, actionName, parameter);

			if (mySettings != null && detail != null)
			{
				mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
				LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
			}
			ExecuteMethod(GetSolutions);
			ExecuteMethod(loadFlows);
			
		}

		#region private helpers

		private void GetSolutions()
		{
			WorkAsync(new WorkAsyncInfo
			{
				Message = "loading solutions ....",
				Work = (worker, args) =>
				{

					args.Result = ConnectionDetail.ServiceClient.ExecuteCrmWebRequest(HttpMethod.Get,
						"solutions?$select=uniquename,friendlyname,solutionid&$orderby=friendlyname", null, null, "application/json");
				},
				PostWorkCallBack = (args) =>
				{
					CustomComboboxItem comboboxItem = null;
					if (args.Error != null)
					{
						MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					var result = args.Result as HttpResponseMessage;
					if (result != null && result.IsSuccessStatusCode)
					{
						HideNotification();
						var status = result.StatusCode;
						var json = result.Content.ReadAsStringAsync().Result;
						JavaScriptSerializer ser = new JavaScriptSerializer();

						Soltuion soltuions = ser.Deserialize<Soltuion>(json);
						if (soltuions != null && soltuions.value != null && soltuions.value.Count > 0)
						{
							foreach (var sol in soltuions.value)
							{
								comboboxItem = new CustomComboboxItem();
								comboboxItem.Text = sol.friendlyname;
								comboboxItem.Value = sol.uniquename;
								cmbSolutions.Items.Add(comboboxItem);
							}
							ShowInfoNotification("Please select crm solution or create a new solution to continue.", new Uri("https://github.com/MscrmTools/XrmToolBox"));
						}
						LoadTriggers();
						LoadActions();
					}
					else
					{
						VerfiyOAuthConnection();
					}
				}
			});
		}
		private void VerfiyOAuthConnection()
		{
			try
			{
				if (ConnectionDetail != null && ConnectionDetail.ServiceClient != null) //make sure for connection
				{
					if (ConnectionDetail.ServiceClient.ActiveAuthenticationType == Microsoft.Xrm.Tooling.Connector.AuthenticationType.OAuth ||
						   ConnectionDetail.ServiceClient.ActiveAuthenticationType == Microsoft.Xrm.Tooling.Connector.AuthenticationType.Certificate)
					{
						HideNotification();
					}
					else //notfiy and link to which connection will work
					{
						try
						{
							string alert = "This plugin is for Dynamic365 WebAPI so it works only with OAuth or Certificate types connections. Please connect using OAuth connection. Click learn more link. ";
							ShowErrorNotification(alert, new Uri("https://github.com/yesadahmed/xrmtoolboxAddins/blob/main/README.md"));
							alert = "This plugin is for Dynamic365 WebAPI so it works only with OAuth or Certificate types connections. Please connect using OAuth connection. \n\t Follow the link. https://github.com/yesadahmed/xrmtoolboxAddins/blob/main/README.md";
							MessageBox.Show(alert, "OAuth connection required", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						}
						catch (Exception)
						{

						}
					}
				}
				else
				{

					ShowErrorNotification("Something wrong with your connection, Try again or make a new OAuth connection.", new Uri("https://github.com/yesadahmed/xrmtoolboxAddins"));
				}
			}
			catch (Exception)
			{


			}
		}

		private void LoadTriggers()
		{
			listViewTriggers.Items.Clear();
			listViewTriggers.InsertionMark.Color = Color.Green;
			listViewTriggers.View = View.Tile;
			listViewTriggers.AllowDrop = false;
			// Initialize the tile size.
			listViewTriggers.TileSize = new Size(400, 45);

			// Initialize the item icons.
			ImageList myImageList = new ImageList();
			myImageList.Images.Add(
			LoadImage("https://connectoricons-prod.azureedge.net/releases/v1.0.1419/1.0.1419.2241/commondataservice/icon.png"));

			myImageList.Images.Add(
		LoadImage("https://psuxemea.azureedge.net/Content/Images/DesignerOperations/http.png"));//http

			myImageList.Images.Add(
		LoadImage("https://github.com/yesadahmed/xrmtoolboxAddins/blob/main/JsonToCSharp/images/schedule.PNG?raw=true"));//schedule

			myImageList.Images.Add(
		LoadImage("https://psuxemea.azureedge.net/Content/Images/DesignerOperations/excelonlinebusiness.png"));//excel online




			myImageList.ImageSize = new Size(32, 32);
			listViewTriggers.LargeImageList = myImageList;

			// Add column headers so the subitems will appear.
			listViewTriggers.Columns.AddRange(new ColumnHeader[]
				{new ColumnHeader()});

			// Create items and add them to listView2.
			var groupTrg = new ListViewGroup("Triggers (Common Data Service) ");
			listViewTriggers.Groups.Add(groupTrg);


			var groupTrgCurr = new ListViewGroup("Triggers (Current envirnoment) ");
			listViewTriggers.Groups.Add(groupTrgCurr);

			var groupGeneral = new ListViewGroup("Triggers (General) ");
			listViewTriggers.Groups.Add(groupGeneral);

			// Create items and add them to myListView.
			ListViewItem item0 = new ListViewItem(new string[]
				{"When_a_record_is_created"}, 0, groupTrg);
			item0.Tag = CommonCrmTriggers.When_a_record_is_created;

			ListViewItem item1 = new ListViewItem(new string[]
				{"When_a_record_is_selected"}, 0, groupTrg);
			item1.Tag = CommonCrmTriggers.When_a_record_is_deleted;

			ListViewItem item2 = new ListViewItem(new string[]
				{"Update_a_record"}, 0, groupTrg);
			item2.Tag = CommonCrmTriggers.Update_a_record;

			ListViewItem item3 = new ListViewItem(new string[]
				{"When_a_record_is_deleted"}, 0, groupTrg);
			item3.Tag = CommonCrmTriggers.When_a_record_is_deleted;


			ListViewItem item4 = new ListViewItem(new string[]
				{"When_a_record_is_created_updated_or_deleted"}, 0, groupTrgCurr);
			item4.Tag = CurrentCrmTriggers.When_a_record_is_created_updated_or_deleted;

			ListViewItem item6 = new ListViewItem(new string[]
			{"Recurrance"}, 2, groupGeneral);
			item6.Tag = PowerAppsTemplates.Triggers.Recurrance;

			ListViewItem item5 = new ListViewItem(new string[]
				{"When a HTTP request is received"}, 1, groupGeneral);
			item5.Tag = PowerAppsTemplates.Triggers.When_a_HTTP_request_is_received;

			ListViewItem item7 = new ListViewItem(new string[]
					{"HTTP"}, 1, groupGeneral);
			item7.Tag = PowerAppsTemplates.Triggers.HTTP;



			listViewTriggers.Items.AddRange(
			new ListViewItem[] { item0, item1, item2, item3, item4, item6, item5, item7 });


		}

		private void LoadActions()
		{
			listViewActions.Items.Clear();
			listViewActions.InsertionMark.Color = Color.Green;
			listViewActions.View = View.Tile;
			listViewActions.AllowDrop = false;
			// Initialize the tile size.
			listViewActions.TileSize = new Size(400, 45);

			// Initialize the item icons.
			ImageList myImageList = new ImageList();
			myImageList.Images.Add(
			LoadImage("https://connectoricons-prod.azureedge.net/releases/v1.0.1419/1.0.1419.2241/commondataservice/icon.png"));


			myImageList.ImageSize = new Size(32, 32);
			listViewActions.LargeImageList = myImageList;

			// Add column headers so the subitems will appear.
			listViewActions.Columns.AddRange(new ColumnHeader[]
				{new ColumnHeader()});

			// Create items and add them to listView2.
			var groupTrg = new ListViewGroup("Actions (Common Data Service) ");
			listViewActions.Groups.Add(groupTrg);
			var groupTrgCurr = new ListViewGroup("Actions (Current envirnoment) ");
			listViewActions.Groups.Add(groupTrgCurr);

			var groupGeneral = new ListViewGroup("Actions (General) ");
			listViewActions.Groups.Add(groupGeneral);

			// Create items and add them to myListView.
			ListViewItem item0 = new ListViewItem(new string[]
				{"List_records"}, 0, groupTrg);
			item0.Tag = CommonCrmActions.List_records;

			ListViewItem item1 = new ListViewItem(new string[]
				{"Create_a_new_record"}, 0, groupTrg);
			item1.Tag = CommonCrmActions.Create_a_new_record;

			ListViewItem item2 = new ListViewItem(new string[]
				{"Update_a_record"}, 0, groupTrg);
			item2.Tag = CommonCrmActions.Update_a_record;

			ListViewItem item3 = new ListViewItem(new string[]
				{"Get_record"}, 0, groupTrg);
			item3.Tag = CommonCrmActions.Get_record;

			ListViewItem item4 = new ListViewItem(new string[]
				{"Delete_a_record"}, 0, groupTrg);
			item4.Tag = CommonCrmActions.Delete_a_record;

			//current

			ListViewItem item5 = new ListViewItem(new string[]
				{"List_records"}, 0, groupTrgCurr);
			item4.Tag = CurrentCrmActions.List_records;

			ListViewItem item6 = new ListViewItem(new string[]
			{"Get_record"}, 0, groupTrgCurr);
			item6.Tag = CurrentCrmActions.Get_record;

			ListViewItem item7 = new ListViewItem(new string[]
			{"Create_a_new_record"}, 0, groupTrgCurr);
			item7.Tag = CurrentCrmActions.Create_a_new_record;

			ListViewItem item8 = new ListViewItem(new string[]
			{"Update_a_record"}, 0, groupTrgCurr);
			item8.Tag = CurrentCrmActions.Update_a_record;

			ListViewItem item9 = new ListViewItem(new string[]
			{"Relate_records"}, 0, groupTrgCurr);
			item9.Tag = CurrentCrmActions.Relate_records;

			ListViewItem item10 = new ListViewItem(new string[]
			{"Unrelate_records"}, 0, groupTrgCurr);
			item10.Tag = CurrentCrmActions.Unrelate_records;

			//ListViewItem item11 = new ListViewItem(new string[]
			//{"List_records"}, 0, groupTrgCurr);
			//item11.Tag = CurrentCrmActions.List_records;

			ListViewItem item12 = new ListViewItem(new string[]
			{"Executes_a_changeset_request"}, 0, groupTrgCurr);
			item12.Tag = CurrentCrmActions.Executes_a_changeset_request;

			ListViewItem item13 = new ListViewItem(new string[]
			{"Perform_a_bound_action"}, 0, groupTrgCurr);
			item13.Tag = CurrentCrmActions.Perform_a_bound_action;

			ListViewItem item14 = new ListViewItem(new string[]
			{"Perform_an_unbound_action"}, 0, groupTrgCurr);
			item14.Tag = CurrentCrmActions.Perform_an_unbound_action;

			ListViewItem item15 = new ListViewItem(new string[]
			{"Delete_a_record"}, 0, groupTrgCurr);
			item15.Tag = CurrentCrmActions.Delete_a_record;



			listViewActions.Items.AddRange(
			new ListViewItem[] { item0, item1, item2, item3,
				item4,item5,item6,item7,item8,item9,item10
			,item12, item13,item14,item15});

		}


		void loadFlows()
		{
			//https://yt3.ggpht.com/ytc/AAUvwnhFJfURr8yQoGO1YMAOhLWIrh5cHd4OVjMKZvTTWA=s68-c-k-c0x00ffffff-no-rj

			WorkAsync(new WorkAsyncInfo
			{
				Work = (worker, args) =>
				{
					Dictionary<string, List<string>> ODataHeaders = new Dictionary<string, List<string>>() {
					{ "Accept", new List<string>() { "application/json" } },
					{"OData-MaxVersion", new List<string>(){"4.0"}},
					{"OData-Version", new List<string>(){"4.0"}}
				  };

					args.Result = ConnectionDetail.ServiceClient.ExecuteCrmWebRequest(HttpMethod.Get,
						"workflows?$select=statecode,workflowid,name&$filter=category eq 5&$orderby=modifiedon desc", null, ODataHeaders, "application/json");
				},
				PostWorkCallBack = (args) =>
				{
					RootFlow rootFlow = null;
					var result = args.Result as HttpResponseMessage;
					if (args.Error != null)
					{
						MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					if (result != null && result.IsSuccessStatusCode)
					{
						HideNotification();
						var status = result.StatusCode;
						var json = result.Content.ReadAsStringAsync().Result;
						rootFlow = JsonConvert.DeserializeObject<RootFlow>(json);
						if (rootFlow != null && rootFlow.value != null && rootFlow.value.Count > 0)
						{
							listViewFlows.Items.Clear();
							var groupTrg = new ListViewGroup("Power Automate (existing flows) ");
							listViewFlows.Groups.Add(groupTrg);
							ImageList myImageList = new ImageList();
							myImageList.Images.Add(
							LoadImage("https://yt3.ggpht.com/ytc/AAUvwnhFJfURr8yQoGO1YMAOhLWIrh5cHd4OVjMKZvTTWA=s68-c-k-c0x00ffffff-no-rj"));
							myImageList.ImageSize = new Size(32, 32);
							listViewFlows.LargeImageList = myImageList;
							listViewFlows.TileSize = new Size(550, 45);

							// Add column headers so the subitems will appear.
							listViewFlows.Columns.AddRange(new ColumnHeader[]
								{new ColumnHeader(), new ColumnHeader(), new ColumnHeader()});
							ListViewItem lvitem = null;

							foreach (var item in rootFlow.value)
							{
								lvitem = new ListViewItem(new string[] { item.name, item.statecode == 0 ? "Active" : "Inactive", item.workflowid }, 0, groupTrg);
								lvitem.Tag = Convert.ToString(item.workflowid);
								listViewFlows.Items.Add(lvitem);
							}
						}

					}
				}
			});
		}

		private Image LoadImage(string url)
		{
			System.Net.WebRequest request =
				System.Net.WebRequest.Create(url);

			System.Net.WebResponse response = request.GetResponse();
			System.IO.Stream responseStream =
				response.GetResponseStream();

			Bitmap bmp = new Bitmap(responseStream);

			responseStream.Dispose();

			return bmp;
		}

		#endregion

		private void cmbSolutions_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				HideNotification();
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnNewSolution_Click(object sender, EventArgs e)
		{

		}
	}
}