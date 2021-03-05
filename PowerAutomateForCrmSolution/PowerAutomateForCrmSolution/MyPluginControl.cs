using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PowerAutomateForCrmSolution.Models;
using PowerAutomateForCrmSolution.PowerAutomateTemplates.CRM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;

namespace PowerAutomateForCrmSolution
{
    public partial class MyPluginControl : PluginControlBase
    {
        private Settings mySettings;
        private List<CustomComboboxItem> solutionNames;
        private List<CustomComboboxItem> userNames;
        private ImageList treeViewImgList;
        ConnectorModel connectorModel = null;


        public MyPluginControl()
        {
            InitializeComponent();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
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
            //ExecuteMethod(loadFlows);
            ShowInfoNotification("Please select crm solution or create a new solution in order to create a new flow or add existing flow to selected solution.", new Uri("https://github.com/yesadahmed/xrmtoolboxdocumentation"));

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

            if (cmbSolutions.SelectedItem == null)
            {
                ShowInfoNotification("Please select crm solution or create a new solution in order to create a new flow or add existing flow to selected solution.", new Uri("https://github.com/yesadahmed/xrmtoolboxdocumentation"));

            }
            if (cmbusers.SelectedItem == null)
            {
                ShowInfoNotification("Please select user to share flow(as owner).", new Uri("https://github.com/yesadahmed/xrmtoolboxdocumentation"));
            }



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
            //ExecuteMethod(loadFlows);

        }

        #region private helpers

        void GetSolutionFlows()
        {
            listViewsolflows.Items.Clear();
            string solutionid = string.Empty;
            ListViewItem item = null;
            AliasedValue aliasedname = null;
            AliasedValue aliasedstatecode = null;
            OptionSetValue optionSetValue = null;
            AliasedValue clientData = null;
            string wfstate = "";

            if (cmbSolutions.SelectedItem == null)
            {
                return;
            }
            else
            {

                var cbitem = cmbSolutions.SelectedItem as CustomComboboxItem;
                if (cbitem != null && (!string.IsNullOrWhiteSpace(cbitem.extra)))
                {
                    solutionid = cbitem.extra;
                    var groupTrg = new ListViewGroup($"Selected Solution: {cbitem.Text}");
                    listViewsolflows.Groups.Add(groupTrg);
                    ImageList myImageList = new ImageList();
                    myImageList.ColorDepth = ColorDepth.Depth32Bit;
                    myImageList.Images.Add(
                    LoadImage("https://yt3.ggpht.com/ytc/AAUvwnhFJfURr8yQoGO1YMAOhLWIrh5cHd4OVjMKZvTTWA=s68-c-k-c0x00ffffff-no-rj"));
                    myImageList.ImageSize = new Size(32, 32);
                    listViewsolflows.LargeImageList = myImageList;
                    listViewsolflows.TileSize = new Size(550, 45);

                    // Add column headers so the subitems will appear.
                    listViewsolflows.Columns.AddRange(new ColumnHeader[]
                        {new ColumnHeader(), new ColumnHeader()});


                    QueryExpression query = new QueryExpression("solutioncomponent");//EntityALogicalName
                    query.ColumnSet = new ColumnSet("objectid", "solutioncomponentid");

                    LinkEntity EntityB = new LinkEntity("solutioncomponent", "workflow", "objectid", "workflowid", JoinOperator.Inner);
                    EntityB.Columns = new ColumnSet("name", "statecode", "clientdata");
                    EntityB.EntityAlias = "wkentty716";
                    // Can put condition like this to any Linked entity
                    EntityB.LinkCriteria.Conditions.Add(new ConditionExpression("category", ConditionOperator.Equal, 5));//flows
                    query.LinkEntities.Add(EntityB);
                    query.Criteria.Conditions.Add(new ConditionExpression("solutionid", ConditionOperator.Equal, solutionid));
                    var result = ConnectionDetail.ServiceClient.RetrieveMultiple(query);
                    foreach (var entity in result.Entities)
                    {
                        aliasedname = entity.GetAttributeValue<AliasedValue>("wkentty716.name");
                        aliasedstatecode = entity.GetAttributeValue<AliasedValue>("wkentty716.statecode");
                        clientData = entity.GetAttributeValue<AliasedValue>("wkentty716.clientdata");
                        if (aliasedname != null && aliasedstatecode != null)
                        {
                            optionSetValue = aliasedstatecode.Value as OptionSetValue;
                            wfstate = optionSetValue.Value == 0 ? "Active" : "Inactive";
                            item = new ListViewItem(new string[] { Convert.ToString(aliasedname.Value), wfstate }, 0, groupTrg);
                            if (clientData.Value != null)
                            {
                                item.Tag = clientData.Value.ToString().Replace("/", "");
                            }
                            listViewsolflows.Items.Add(item);
                        }
                    }
                }
                ///load PA josn file

                System.Net.WebClient wc = new System.Net.WebClient();
                var json = wc.DownloadString("https://raw.githubusercontent.com/yesadahmed/xrmtoolboxdocumentation/main/pics/shortconnfile.json");
                if (!string.IsNullOrWhiteSpace(json))
                    connectorModel = JsonConvert.DeserializeObject<ConnectorModel>(json);

            }

        }

        void ClearEveryThing()
        {
            cmbpub.Items.Clear();
            cmbSolutions.Items.Clear();
            listViewTriggers.Items.Clear();
            listViewActions.Items.Clear();
            listViewsolflows.Items.Clear();
            txtboxflowname.Text = string.Empty;
            txtsol.Text = string.Empty;
            listViewsolflows.Items.Clear();

        }


        private void GetSolutions()
        {
            ClearEveryThing();
            toolTipflow.SetToolTip(btnshareflow, "Share flow with ReadAccess only.");
            toolTipshareflow.SetToolTip(btnflowowner, "Adding another owner allows others to edit, " + Environment.NewLine
                + "update and delete this flow. All owners can also" + Environment.NewLine + " access and run history and add or remove other owners.");
            solutionNames = new List<CustomComboboxItem>();
            WorkAsync(new WorkAsyncInfo
            {
                Message = "loading solutions ....",
                Work = (worker, args) =>
                {
                    Dictionary<string, List<string>> ODataHeaders = new Dictionary<string, List<string>>() {
                    { "Accept", new List<string>() { "application/json" } },
                    {"OData-MaxVersion", new List<string>(){"4.0"}},
                    {"OData-Version", new List<string>(){"4.0"}}
                  };

                    args.Result = ConnectionDetail.ServiceClient.ExecuteCrmWebRequest(HttpMethod.Get,
                        "solutions?$select=uniquename,friendlyname,solutionid&$orderby=uniquename asc", null, ODataHeaders, "application/json");
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
                        //HideNotification();
                        var status = result.StatusCode;
                        var json = result.Content.ReadAsStringAsync().Result;
                        JavaScriptSerializer ser = new JavaScriptSerializer();
                        cmbSolutions.Items.Clear();
                        Soltuion soltuions = ser.Deserialize<Soltuion>(json);
                        StringBuilder text = new StringBuilder();
                        if (soltuions != null && soltuions.value != null && soltuions.value.Count > 0)
                        {

                            foreach (var sol in soltuions.value)
                            {
                                if (HelperProject.excludesolution.Contains(sol.uniquename)) continue;
                                comboboxItem = new CustomComboboxItem();
                                comboboxItem.Text = sol.friendlyname;
                                comboboxItem.Value = sol.uniquename;
                                comboboxItem.extra = sol.solutionid;
                                text.AppendLine(sol.uniquename);
                                cmbSolutions.Items.Add(comboboxItem);
                                solutionNames.Add(comboboxItem);//as cache for later use
                            }
                        }
                        LoadTriggers();
                        LoadActions();
                        loadusers();
                        lstpadep.Items.Clear();
                        lstpadep.InsertionMark.Color = Color.Green;
                        lstpadep.View = View.LargeIcon;
                        lstpadep.AllowDrop = false;
                        // Initialize the tile size.
                        listViewTriggers.TileSize = new Size(400, 45);
                        string hash = Convert.ToString(Guid.NewGuid().GetHashCode());
                        txtsol.Text = $"solution{hash.Replace("-", "")}";
                        txtboxflowname.Text = $"powerautomate{hash.Replace("-", "")}";
                        //ShowInfoNotification("Please select crm solution or create a new solution in order to create a new flow or add existing flow to selected solution.", new Uri("https://github.com/yesadahmed/xrmtoolboxdocumentation"));

                    }
                    else
                    {
                        VerfiyOAuthConnection();
                    }
                    //var image = LoadImage("https://raw.githubusercontent.com/yesadahmed/xrmtoolboxdocumentation/main/pics/rating.PNG");
                    //picrate.Image=image;
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
                            ShowErrorNotification(alert, new Uri("https://github.com/yesadahmed/xrmtoolboxdocumentation"));
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

                    ShowErrorNotification("Something wrong with your connection, Try again or make a new OAuth connection.", new Uri("https://github.com/yesadahmed/xrmtoolboxdocumentation"));
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
            listViewTriggers.View = View.LargeIcon;
            listViewTriggers.AllowDrop = false;
            // Initialize the tile size.
            listViewTriggers.TileSize = new Size(400, 45);

            // Initialize the item icons.
            ImageList myImageList = new ImageList();

            myImageList.ColorDepth = ColorDepth.Depth32Bit;
            myImageList.Images.Add(
            LoadImage("https://connectoricons-prod.azureedge.net/releases/v1.0.1419/1.0.1419.2241/commondataservice/icon.png"));

            myImageList.Images.Add(
        LoadImage("https://psuxemea.azureedge.net/Content/Images/DesignerOperations/http.png"));//http

            myImageList.Images.Add(
        LoadImage("https://github.com/yesadahmed/xrmtoolboxAddins/blob/main/JsonToCSharp/images/schedule.PNG?raw=true"));//schedule

            myImageList.Images.Add(
        LoadImage("https://psuxemea.azureedge.net/Content/Images/DesignerOperations/excelonlinebusiness.png"));//excel online

            myImageList.ImageSize = new Size(45, 45);


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
                {"When_a_record_is_created".Replace("_"," ")}, 0, groupTrg);
            item0.Tag = CommonCrmTriggers.When_a_record_is_created;

            ListViewItem item1 = new ListViewItem(new string[]
                {"When_a_record_is_selected".Replace("_"," ")}, 0, groupTrg);
            item1.Tag = CommonCrmTriggers.When_a_record_is_deleted;

            ListViewItem item2 = new ListViewItem(new string[]
                {"Update_a_record"}, 0, groupTrg);
            item2.Tag = CommonCrmTriggers.Update_a_record;

            ListViewItem item3 = new ListViewItem(new string[]
                {"When_a_record_is_deleted".Replace("_"," ")}, 0, groupTrg);
            item3.Tag = CommonCrmTriggers.When_a_record_is_deleted;


            ListViewItem item4 = new ListViewItem(new string[]
                {"When a record_is_created_updated_or_deleted".Replace("_"," ")}, 0, groupTrgCurr);
            item4.Tag = CurrentCrmTriggers.When_a_record_is_created_updated_or_deleted;

            ListViewItem item6 = new ListViewItem(new string[]
            {"Recurrance"}, 2, groupGeneral);
            item6.Tag = PowerAppsTemplates.Triggers.Recurrance;

            ListViewItem item5 = new ListViewItem(new string[]
                {"When a HTTP request is received".Replace("_"," ")}, 1, groupGeneral);
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
            listViewActions.View = View.LargeIcon;
            listViewActions.AllowDrop = false;

            // Initialize the tile size.
            listViewActions.TileSize = new Size(400, 45);

            // Initialize the item icons.
            ImageList myImageList = new ImageList();
            myImageList.ColorDepth = ColorDepth.Depth32Bit;
            myImageList.Images.Add(
            LoadImage("https://connectoricons-prod.azureedge.net/releases/v1.0.1419/1.0.1419.2241/commondataservice/icon.png"));

            myImageList.Images.Add(
        LoadImage("https://connectoricons-prod.azureedge.net/releases/v1.0.1382/1.0.1382.2097/powerplatformforadmins/icon.png"));

            myImageList.Images.Add(
                    LoadImage("https://connectoricons-prod.azureedge.net/releases/v1.0.1429/1.0.1429.2295/powerappsforadmins/icon.png"));


            myImageList.ImageSize = new Size(45, 45);
            listViewActions.LargeImageList = myImageList;

            // Add column headers so the subitems will appear.
            listViewActions.Columns.AddRange(new ColumnHeader[]
                {new ColumnHeader()});

            // Create items and add them to listView2.
            var groupTrg = new ListViewGroup("Actions (Common Data Service) ");
            listViewActions.Groups.Add(groupTrg);
            var groupTrgCurr = new ListViewGroup("Actions (Current envirnoment) ");
            listViewActions.Groups.Add(groupTrgCurr);

            var groupGeneral = new ListViewGroup("Actions (General for admins) ");
            listViewActions.Groups.Add(groupGeneral);

            // Create items and add them to myListView.
            ListViewItem item0 = new ListViewItem(new string[]
                {"ListRecords"}, 0, groupTrg);
            item0.Tag = CommonCrmActions.List_records;

            ListViewItem item1 = new ListViewItem(new string[]
                {"CreateRecord"}, 0, groupTrg);
            item1.Tag = CommonCrmActions.Create_a_new_record;

            ListViewItem item2 = new ListViewItem(new string[]
                {"UpdateRecord"}, 0, groupTrg);
            item2.Tag = CommonCrmActions.Update_a_record;

            ListViewItem item3 = new ListViewItem(new string[]
                {"GetRecord"}, 0, groupTrg);
            item3.Tag = CommonCrmActions.Get_record;

            //ListViewItem item4 = new ListViewItem(new string[]
            //	{"Delete_a_record"}, 0, groupTrg);
            //item4.Tag = CommonCrmActions.Delete_a_record;

            //current

            ListViewItem item5 = new ListViewItem(new string[]
                {"ListRecords"}, 0, groupTrgCurr);
            //item4.Tag = CurrentCrmActions.List_records;

            ListViewItem item6 = new ListViewItem(new string[]
            {"GetRecord"}, 0, groupTrgCurr);
            item6.Tag = CurrentCrmActions.Get_record;

            ListViewItem item7 = new ListViewItem(new string[]
            {"CreateRecord"}, 0, groupTrgCurr);
            item7.Tag = CurrentCrmActions.Create_a_new_record;

            ListViewItem item8 = new ListViewItem(new string[]
            {"UpdateRecord"}, 0, groupTrgCurr);
            item8.Tag = CurrentCrmActions.Update_a_record;

            ListViewItem item9 = new ListViewItem(new string[]
            {"RelateRecords"}, 0, groupTrgCurr);
            item9.Tag = CurrentCrmActions.Relate_records;

            ListViewItem item10 = new ListViewItem(new string[]
            {"UnrelateRecords"}, 0, groupTrgCurr);
            item10.Tag = CurrentCrmActions.Unrelate_records;

            //ListViewItem item11 = new ListViewItem(new string[]
            //{"List_records"}, 0, groupTrgCurr);
            //item11.Tag = CurrentCrmActions.List_records;

            //ListViewItem item12 = new ListViewItem(new string[]
            //{"Executes_a_changeset_request"}, 0, groupTrgCurr);
            //item12.Tag = CurrentCrmActions.Executes_a_changeset_request;

            ListViewItem item13 = new ListViewItem(new string[]
            {"BoundAction"}, 0, groupTrgCurr);
            item13.Tag = CurrentCrmActions.Perform_a_bound_action;

            ListViewItem item14 = new ListViewItem(new string[]
            {"UnboundAction"}, 0, groupTrgCurr);
            item14.Tag = CurrentCrmActions.Perform_an_unbound_action;

            ListViewItem item15 = new ListViewItem(new string[]
            {"ListEnvirnoments"}, 1, groupGeneral);
            item15.Tag = PowerAppsTemplates.Actions.PowerPlatFormListAllEnvirnoments;

            ListViewItem item16 = new ListViewItem(new string[]
        {"ListPowerApss"}, 2, groupGeneral);
            item16.Tag = PowerAppsTemplates.Actions.AllPowerApps;

            listViewActions.Items.AddRange(
            new ListViewItem[] { item0, item1, item2, item3,
                item5,item6,item7,item8,item9,item10
            , item13,item14,item15,item16});

        }

        //void loadFlows()
        //{
        //    //https://yt3.ggpht.com/ytc/AAUvwnhFJfURr8yQoGO1YMAOhLWIrh5cHd4OVjMKZvTTWA=s68-c-k-c0x00ffffff-no-rj

        //    WorkAsync(new WorkAsyncInfo
        //    {
        //        Work = (worker, args) =>
        //        {
        //            Dictionary<string, List<string>> ODataHeaders = new Dictionary<string, List<string>>() {
        //            { "Accept", new List<string>() { "application/json" } },
        //            {"OData-MaxVersion", new List<string>(){"4.0"}},
        //            {"OData-Version", new List<string>(){"4.0"}}
        //          };

        //            args.Result = ConnectionDetail.ServiceClient.ExecuteCrmWebRequest(HttpMethod.Get,
        //                "workflows?$select=statecode,workflowid,name,solutionid,_ownerid_value&$filter=category eq 5&$orderby=modifiedon desc", null, ODataHeaders, "application/json");
        //        },
        //        PostWorkCallBack = (args) =>
        //        {
        //            RootFlow rootFlow = null;
        //            var result = args.Result as HttpResponseMessage;
        //            if (args.Error != null)
        //            {
        //                MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //            if (result != null && result.IsSuccessStatusCode)
        //            {
        //                //HideNotification();
        //                var status = result.StatusCode;
        //                var json = result.Content.ReadAsStringAsync().Result;
        //                rootFlow = JsonConvert.DeserializeObject<RootFlow>(json);
        //                if (rootFlow != null && rootFlow.value != null && rootFlow.value.Count > 0)
        //                {
        //                    listViewsolflows.Items.Clear();
        //                    var groupTrg = new ListViewGroup("System: All PowerAutomate");
        //                    listViewsolflows.Groups.Add(groupTrg);
        //                    ImageList myImageList = new ImageList();
        //                    myImageList.ColorDepth = ColorDepth.Depth32Bit;
        //                    myImageList.Images.Add(
        //                    LoadImage("https://yt3.ggpht.com/ytc/AAUvwnhFJfURr8yQoGO1YMAOhLWIrh5cHd4OVjMKZvTTWA=s68-c-k-c0x00ffffff-no-rj"));
        //                    myImageList.ImageSize = new Size(30, 35);
        //                    listViewFlows.LargeImageList = myImageList;
        //                    listViewFlows.TileSize = new Size(550, 50);

        //                    // Add column headers so the subitems will appear.
        //                    listViewFlows.Columns.AddRange(new ColumnHeader[]
        //                        {new ColumnHeader(), new ColumnHeader(), new ColumnHeader()});
        //                    ListViewItem lvitem = null;
        //                    string sharedUserName = string.Empty;
        //                    string owerName = string.Empty;
        //                    string solutionName = string.Empty;

        //                    foreach (var item in rootFlow.value)
        //                    {
        //                        sharedUserName = string.Empty;
        //                        solutionName = string.Empty;
        //                        sharedUserName = String.Empty;

        //                        if (userNames != null && userNames.Count > 0)
        //                        {
        //                            var resultUser = userNames.FirstOrDefault(u => u.Value.Equals(item._ownerid_value));
        //                            if (resultUser != null)
        //                                owerName = $"Owner: {resultUser.Text}";
        //                        }


        //                        lvitem = new ListViewItem(new string[] { item.name, item.statecode == 0 ? "Active" : "Inactive",
        //                            owerName }, 0, groupTrg);

        //                        lvitem.Tag = Convert.ToString(item.workflowid);
        //                        listViewFlows.Items.Add(lvitem);
        //                    }
        //                }

        //            }
        //        }
        //    });
        //}

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

        string GetFlowSharedUsers(string flowId)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                var accessRequest = new RetrieveSharedPrincipalsAndAccessRequest
                {
                    Target = new EntityReference("workflow", new Guid(flowId))
                };

                var accessResponse = (RetrieveSharedPrincipalsAndAccessResponse)ConnectionDetail.ServiceClient.Execute(accessRequest);
                foreach (var principalAccess in accessResponse.PrincipalAccesses)
                {
                    if (userNames != null && userNames.Count > 0)
                    {
                        var resultUser = userNames.FirstOrDefault(u => u.Value.Equals(principalAccess.Principal.Id.ToString()));
                        if (resultUser != null)
                            str.Append($"Shared: {resultUser.Text}, ");
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(principalAccess.Principal.Name))
                                str.Append($"Shared: {principalAccess.Principal.Name}, ");
                        }
                    }
                    else
                        if (!string.IsNullOrWhiteSpace(principalAccess.Principal.Name))
                        str.Append($"Shared: {principalAccess.Principal.Name}, ");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error (RetrieveSharedPrincipalsAndAccessRequest)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return str.ToString();

        }

        void enablecontrols(bool flag)
        {
            txtboxflowname.Enabled = flag;
            btnCreateflow.Enabled = flag;

        }

        void loadPublishers()
        {
            try
            {
                WorkAsync(new WorkAsyncInfo
                {
                    Work = (worker, args) =>
                    {
                        Dictionary<string, List<string>> ODataHeaders = new Dictionary<string, List<string>>() {
                                { "Accept", new List<string>() { "application/json" } },
                                {"OData-MaxVersion", new List<string>(){"4.0"}},
                                {"OData-Version", new List<string>(){"4.0"}}
                              };
                        var query = "not contains(friendlyname,'microsoft') and not contains(friendlyname,'linkedin') and  not contains(friendlyname,'ribbonworkbench') and not " +
                            "contains(friendlyname,'Dynamics 365 Customer Voice') and not " +
                            "contains(friendlyname,'Dynamics 365 Customer Engagement') and not " +
                            "contains(uniquename,'dynamics365customerengagement') and " +
                            "not contains(friendlyname,'Dynamics 365 Customer Voice') and " +
                            "not contains(uniquename,'microsoftdynamics') and " +
                            "not contains(uniquename,'msdynce') and " +
                            "not contains(uniquename,'microsoftformspro') and not contains(friendlyname,'CRM Developer Tools') and " +
                            "not contains(uniquename,'jasonlattimer') and not contains(friendlyname,'CDS Default Publisher')";
                        args.Result = ConnectionDetail.ServiceClient.ExecuteCrmWebRequest(HttpMethod.Get,
                        $"publishers?$select=friendlyname,uniquename,publisherid&$filter={query}", null, ODataHeaders, "application/json");


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
                            var json = result.Content.ReadAsStringAsync().Result;
                            Publisher publishers = JsonConvert.DeserializeObject<Publisher>(json);
                            if (publishers != null && publishers.value != null && publishers.value.Count > 0)
                            {
                                foreach (var pub in publishers.value)
                                {
                                    comboboxItem = new CustomComboboxItem();
                                    comboboxItem.Text = pub.friendlyname;
                                    comboboxItem.Value = pub.publisherid;
                                    cmbpub.Items.Add(comboboxItem);

                                }

                            }
                            else
                            {
                                comboboxItem = new CustomComboboxItem();
                                comboboxItem.Text = "CDS Default Publisher";
                                comboboxItem.Value = "d21aab71-79e7-11dd-8874-00188b01e34f";
                                cmbpub.Items.Add(comboboxItem);
                            }
                            cmbpub.SelectedIndex = 0; // 
                        }
                        else
                            MessageBox.Show(result.ReasonPhrase, "Error (unable to create solution)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error (unable to create solution)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        void CreateFlow()
        {
            HttpResponseMessage response = null;
            string solutionUniqueName = string.Empty;
            try
            {
                Dictionary<string, List<string>> ODataHeaders = new Dictionary<string, List<string>>() {
                                { "Accept", new List<string>() { "application/json" } },
                                {"OData-MaxVersion", new List<string>(){"4.0"}},
                                {"OData-Version", new List<string>(){"4.0"}}
                              };
                if (txtboxflowname.Text == "")
                {
                    Blink b = new Blink();
                    b.Text(lblerror, "Please provide a flow name.");

                    //lblerror.Text = "Please provide a flow name";
                    return;
                }
                if (listViewTriggers.SelectedItems.Count == 0 && listViewActions.SelectedItems.Count == 0)
                {

                    Blink b = new Blink();
                    b.Text(lblerror, "Please select at least one trigger and action to create flow.");

                    //lblerror.Text = "Please select a trigger and action to create a flow.";
                    return;
                }
                else if (listViewTriggers.SelectedItems.Count == 0)
                {

                    Blink b = new Blink();
                    b.Text(lblerror, "Please select at least one trigger to create flow.");
                    //lblerror.Text = "Please select a trigger for flow.";
                    return;
                }
                else if (listViewActions.SelectedItems.Count == 0)
                {

                    Blink b = new Blink();
                    b.Text(lblerror, "Please select at least one action to create flow.");
                    //lblerror.Text = "Please select a action for flow.";
                    return;
                }
                else
                {//good to go
                    var triggerTag = listViewTriggers.SelectedItems[0].Tag;
                    var actionTag = listViewActions.SelectedItems[0].Tag;
                    string currentTempalte = $"{CurrentCrmBaseTemplate.StartTemaplte}{actionTag}" +
                                            $",{triggerTag},{CurrentCrmBaseTemplate.EndTemaplte}";

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("{");
                    stringBuilder.Append("\"category\":5,");
                    stringBuilder.Append("\"statecode\":0,");
                    stringBuilder.Append("\"type\":1,");
                    stringBuilder.Append("\"name\":\"" + txtboxflowname.Text + "\",");
                    stringBuilder.Append("\"description\":\"" + txtboxflowname.Text + "\",");
                    stringBuilder.Append("\"primaryentity\":\"none\",");
                    stringBuilder.Append("\"clientdata\":" + currentTempalte + "");
                    stringBuilder.Append("}");

                    try
                    {
                        response = ConnectionDetail.ServiceClient.ExecuteCrmWebRequest(HttpMethod.Post,
                     "workflows", stringBuilder.ToString(), ODataHeaders, "application/json");
                    }
                    catch (Exception ex)
                    {


                    }


                    string flowId = "";
                    if (response.IsSuccessStatusCode)
                    {
                        var entityUri = response.Headers.GetValues("OData-EntityId").FirstOrDefault();
                        int idx = entityUri.IndexOf("(");
                        flowId = entityUri.Substring(++idx, entityUri.Length - idx - 1);
                        if (cmbSolutions.SelectedItem != null)// grab SolutionUniqueName
                        {
                            CustomComboboxItem item = cmbSolutions.SelectedItem as CustomComboboxItem;
                            if (item != null)
                                solutionUniqueName = Convert.ToString(item.Value);
                        }
                        if (!string.IsNullOrWhiteSpace(flowId))
                        {
                            if ((!string.IsNullOrWhiteSpace(solutionUniqueName)) & (!string.IsNullOrWhiteSpace(flowId)))
                            {

                                AddSolutionComponentRequest addReq = new AddSolutionComponentRequest()
                                {
                                    ComponentType = 29,
                                    ComponentId = new Guid(flowId),
                                    SolutionUniqueName = solutionUniqueName
                                };
                                ConnectionDetail.ServiceClient.Execute(addReq);

                                Blink b = new Blink();
                                b.Text(lblerror, $"Flow Added to solution: {solutionUniqueName}.");
                            }

                            GetSolutionFlows();


                        }

                    }
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error (create powerautomate)", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        void AddflowToSolution()
        {

            try
            {
                string solutionUniqueName = string.Empty;
                string workflowid = string.Empty;



                if (listViewsolflows.SelectedItems.Count <= 0)
                {
                    Blink b = new Blink();
                    b.Text(lblerror, "Please select power automate from list to add into solution.");
                    if (listViewsolflows.Items.Count > 0)
                        return;
                }
                else //add flow to solution
                {
                    if (cmbSolutions.SelectedItem != null)// grab SolutionUniqueName
                    {
                        CustomComboboxItem item = cmbSolutions.SelectedItem as CustomComboboxItem;
                        if (item != null)
                            solutionUniqueName = Convert.ToString(item.Value);
                    }

                    if (listViewsolflows.SelectedItems.Count > 0)
                    {
                        var fitem = listViewsolflows.SelectedItems[0];
                        if (fitem.Tag != null)
                            workflowid = Convert.ToString(fitem.Tag);
                    }
                    if ((!string.IsNullOrWhiteSpace(solutionUniqueName)) & (!string.IsNullOrWhiteSpace(workflowid)))
                    {

                        AddSolutionComponentRequest addReq = new AddSolutionComponentRequest()
                        {
                            ComponentType = 29,
                            ComponentId = new Guid(workflowid),
                            SolutionUniqueName = solutionUniqueName
                        };
                        ConnectionDetail.ServiceClient.Execute(addReq);

                        Blink b = new Blink();
                        b.Text(lblerror, $"Flow Added to solution: {solutionUniqueName}.");
                        GetSolutionFlows();

                    }
                }
            }
            catch (Exception)
            {


            }
        }

        void loadusers()
        {
            userNames = new List<CustomComboboxItem>();
            string email = string.Empty;
            CustomComboboxItem customComboboxItem = null;
            QueryExpression query = new QueryExpression("systemuser");//EntityALogicalName
            query.ColumnSet = new ColumnSet("fullname", "internalemailaddress");
            query.Criteria.Conditions.Add(new ConditionExpression("isdisabled", ConditionOperator.Equal, false));
            query.Criteria.Conditions.Add(new ConditionExpression("islicensed", ConditionOperator.Equal, true));
            query.Criteria.Conditions.Add(new ConditionExpression("internalemailaddress", ConditionOperator.NotNull));
            query.Criteria.Conditions.Add(new ConditionExpression("applicationiduri", ConditionOperator.Null));//not application users
            OrderExpression orderbyfullname = new OrderExpression("firstname", OrderType.Ascending);
            query.Orders.Add(orderbyfullname);
            var result = ConnectionDetail.ServiceClient.RetrieveMultiple(query);
            cmbusers.Items.Clear();
            foreach (var entity in result.Entities)
            {
                customComboboxItem = new CustomComboboxItem();
                customComboboxItem.Text = entity.GetAttributeValue<string>("fullname");
                customComboboxItem.Value = entity.Id.ToString(); ;
                customComboboxItem.extra = entity.GetAttributeValue<string>("internalemailaddress");
                cmbusers.Items.Add(customComboboxItem);
                userNames.Add(customComboboxItem);//as  a  cache
            }
        }
        #endregion




        private void cmbSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                enablecontrols(true);
                HideNotification();
                ExecuteMethod(GetSolutionFlows);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewSolution_Click(object sender, EventArgs e)
        {
            cmbpub.Items.Clear();
            pnlsol.Visible = true;
            string hash = Convert.ToString(Guid.NewGuid().GetHashCode());
            txtsol.Text = $"solution{hash.Replace("-", "")}";
            txtsol.SelectAll();
            txtsol.Focus();
            ExecuteMethod(loadPublishers);
        }

        private void btnCreateflow_Click(object sender, EventArgs e)
        {
            ExecuteMethod(CreateFlow);
        }

        private void btncreatesolution_Click(object sendepr, EventArgs e)
        {
            CustomComboboxItem comboboxItem = null;
            string publisherid = "";
            if (txtsol.Text == "")
            {
                Blink b = new Blink();
                b.Text(lblerror, "Please provide a solution name (adding default).");
                string hash = Convert.ToString(Guid.NewGuid().GetHashCode());
                txtsol.Text = $"solution{hash.Replace("-", "")}";
                //lblerror.Text = "Please provide a flow name";
                txtsol.SelectAll();
                txtsol.Focus();
                return;
            }
            else //create solution
            {
                if (cmbpub.SelectedItem != null)
                {
                    CustomComboboxItem item = cmbpub.SelectedItem as CustomComboboxItem;
                    if (item != null)
                        publisherid = Convert.ToString(item.Value);

                    Microsoft.Xrm.Sdk.Entity solution = new Microsoft.Xrm.Sdk.Entity("solution");
                    string solName = RemoveSpecialCharacters(txtsol.Text); //if any
                    solution["uniquename"] = solName;
                    solution["friendlyname"] = solName;
                    solution["publisherid"] = new Microsoft.Xrm.Sdk.EntityReference("publisher", new Guid(publisherid));
                    var newsolutionId = ConnectionDetail.ServiceClient.Create(solution);
                    if (newsolutionId != Guid.Empty)
                    {
                        comboboxItem = new CustomComboboxItem();
                        comboboxItem.Text = solName;
                        comboboxItem.Value = solName;
                        cmbSolutions.Items.Insert(0, comboboxItem);
                        cmbSolutions.SelectedIndex = cmbSolutions.FindStringExact(solName);
                        string hash = Convert.ToString(Guid.NewGuid().GetHashCode());
                        txtsol.Text = $"solution{hash.Replace("-", "")}";
                        pnlsol.Visible = false;//everything okp
                    }
                }
            }
        }

        private void btnaddflowtosol_Click(object sender, EventArgs e)
        {
            AddflowToSolution();
        }

        private void btnbrowser_Click(object sender, EventArgs e)
        {
            var formPopup = new Form();
            formPopup.ShowDialog(this);
        }

        private void btnshareflow_Click(object sender, EventArgs e)
        {

        }

        private void listViewFlows_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnshareflow_Click_1(object sender, EventArgs e)
        {
            string workflowId = string.Empty;
            Guid wrkflowId = Guid.Empty;
            Guid userId = Guid.Empty;
            //if (cmbSolutions.SelectedItem == null)
            //{
            //	Blink b = new Blink();
            //	b.Text(lblerror, "Please select crm solution or create a new solution to continue.");
            //	return;

            //}
            if (cmbusers.SelectedItem == null)
            {
                Blink b = new Blink();
                b.Text(lblerror, "Please select user to share a flow with (ReadAccess only).");
                return;
            }

            if (listViewsolflows.SelectedItems != null && listViewsolflows.SelectedItems.Count <= 0)
            {
                Blink b = new Blink();
                b.Text(lblerror, "Please select flow to share.");
                return;
            }

            if (listViewsolflows.SelectedItems.Count > 0)
            {
                var fitem = listViewsolflows.SelectedItems[0];
                if (fitem.Tag != null)
                    workflowId = Convert.ToString(fitem.Tag);

                var cmboUser = cmbusers.SelectedItem as CustomComboboxItem;
                if (cmboUser != null && (!string.IsNullOrWhiteSpace(workflowId)))
                {
                    if (cmboUser.Value != null)
                    {
                        var userIdvalue = Convert.ToString(cmboUser.Value);
                        if (Guid.TryParse(workflowId, out wrkflowId))
                        {
                            if (Guid.TryParse(userIdvalue, out userId))
                            {
                                var grantAccessRequest1 = new GrantAccessRequest
                                {
                                    PrincipalAccess = new Microsoft.Crm.Sdk.Messages.PrincipalAccess
                                    {
                                        AccessMask = AccessRights.ReadAccess,
                                        Principal = new EntityReference("systemuser", userId)
                                    },
                                    Target = new EntityReference("workflow", wrkflowId)
                                };

                                ConnectionDetail.ServiceClient.Execute(grantAccessRequest1);
                                Blink b = new Blink();
                                b.Text(lblerror, $"Successfully shared with {cmboUser.Value}.");
                                return;
                            }

                        }

                    }
                }
            }

        }

        private void btnflowowner_Click(object sender, EventArgs e)
        {
            Guid wrkflowId = Guid.Empty;
            Guid userId = Guid.Empty;
            //if (cmbSolutions.SelectedItem == null)
            //{
            //	Blink b = new Blink();
            //	b.Text(lblerror, "Please select crm solution or create a new solution to continue.");
            //	return;

            //}
            if (cmbusers.SelectedItem == null)
            {
                Blink b = new Blink();
                b.Text(lblerror, "Please select user to make as owner.");
                return;
            }

            if (listViewsolflows.SelectedItems != null && listViewsolflows.SelectedItems.Count <= 0)
            {
                Blink b = new Blink();
                b.Text(lblerror, "Please select flow to change owner.");
                return;
            }

            string workflowId = string.Empty;
            if (listViewsolflows.SelectedItems.Count > 0)
            {
                var fitem = listViewsolflows.SelectedItems[0];
                if (fitem.Tag != null)
                    workflowId = Convert.ToString(fitem.Tag);

                var cmboUser = cmbusers.SelectedItem as CustomComboboxItem;
                if (cmboUser != null && (!string.IsNullOrWhiteSpace(workflowId)))
                {
                    if (cmboUser.Value != null)
                    {
                        var userIdvalue = Convert.ToString(cmboUser.Value);
                        if (Guid.TryParse(workflowId, out wrkflowId))
                        {
                            if (Guid.TryParse(userIdvalue, out userId))
                            {
                                Entity entity = new Entity("workflow", wrkflowId);
                                entity["ownerid"] = new EntityReference("systemuser", userId);
                                ConnectionDetail.ServiceClient.Update(entity);
                                GetSolutionFlows();
                            }

                        }

                    }
                }
            }

        }

        private void btnunshare_Click(object sender, EventArgs e)
        {
            Guid wrkflowId = Guid.Empty;
            Guid userId = Guid.Empty;
            string workflowId = string.Empty;

            if (cmbusers.SelectedItem == null)
            {
                Blink b = new Blink();
                b.Text(lblerror, "Please select user to unshare flow.");
                return;
            }

            if (listViewsolflows.SelectedItems != null && listViewsolflows.SelectedItems.Count <= 0)
            {
                Blink b = new Blink();
                b.Text(lblerror, "Please select flow to unshare.");
                return;
            }

            if (listViewsolflows.SelectedItems.Count > 0)
            {
                var fitem = listViewsolflows.SelectedItems[0];
                if (fitem.Tag != null)
                    workflowId = Convert.ToString(fitem.Tag);

                var cmboUser = cmbusers.SelectedItem as CustomComboboxItem;
                if (cmboUser != null && (!string.IsNullOrWhiteSpace(workflowId)))
                {
                    if (cmboUser.Value != null)
                    {
                        var userIdvalue = Convert.ToString(cmboUser.Value);
                        if (Guid.TryParse(workflowId, out wrkflowId))
                        {
                            if (Guid.TryParse(userIdvalue, out userId))
                            {
                                var revokeUser2AccessReq = new RevokeAccessRequest
                                {
                                    Revokee = new EntityReference("systemuser", userId),
                                    Target = new EntityReference("workflow", wrkflowId)
                                };

                                ConnectionDetail.ServiceClient.Execute(revokeUser2AccessReq);
                                Blink b = new Blink();
                                b.Text(lblerror, $"Successfully unshared with {cmboUser.Value}.");
                                return;
                            }

                        }

                    }
                }
            }



        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listViewsolflows_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ListView.SelectedListViewItemCollection flows = this.listViewsolflows.SelectedItems;
            //lstpadep.Clear();
            //treeViewImgList = new ImageList();
            //treeViewImgList.ColorDepth = ColorDepth.Depth32Bit;
            //treeViewImgList.ImageSize = new Size(45, 45);
            //lstpadep.LargeImageList = treeViewImgList;
            //lstpadep.TileSize = new Size(450, 45);
            //lstpadep.Columns.AddRange(new ColumnHeader[]
            //     {new ColumnHeader()});

            //foreach (ListViewItem item in flows)
            //{
            //    if (item.Tag != null)
            //        BuildFlowDependencyTree(item.Tag.ToString());
            //}
        }

        void BuildFlowDependencyTree(string json)
        {
            try
            {
                JObject jObject = JObject.Parse(json);
                var count = AddConnectors(jObject);
                AddTablesTreeView(jObject);
                AddTriggers(jObject);
                AddActions(jObject);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void AddTablesTreeView(JObject jObject)
        {

            string _value = string.Empty;
            List<string> tables = null;
            TreeNode childNode = null;
            ListViewItem item_ = null;
            try
            {
                // Create items and add them to lstpadep.

                var totalentites = jObject.SelectTokens("..table");//total enties used			
                if (totalentites.Any())
                {
                    treeViewImgList.Images.Add("cdsicon",
                            LoadImage("https://connectoricons-prod.azureedge.net/releases/v1.0.1419/1.0.1419.2241/commondataservice/icon.png"));

                    var groupTable = new ListViewGroup($"Tables ({totalentites.Children().Count()})");
                    lstpadep.Groups.Add(groupTable);
                    tables = new List<string>();
                    //TreeNode roortNodeTable = new TreeNode($"Tables ({totalentites.Count()})");//invlaid imageindex
                    foreach (var table in totalentites.Children())
                    {
                        _value = Convert.ToString(table);
                        if (!tables.Contains(_value))
                        {
                            item_ = new ListViewItem(new string[] { _value }, "cdsicon", groupTable);
                            lstpadep.Items.Add(item_);
                            tables.Add(_value);//avoid duplicate

                        }
                    }


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        int AddConnectors(JObject jObject)
        {
            int connectorCount = -1;
            int count = 0;
            try
            {
                ListViewItem item_ = null;

                string _value = string.Empty;
                List<string> tables = null;

                var connectors = jObject.SelectToken("..connectionReferences");//total connectors used 
                if (connectors.Any())
                {

                    treeViewImgList.Images.Add("cdsicon",
                           LoadImage("https://connectoricons-prod.azureedge.net/releases/v1.0.1419/1.0.1419.2241/commondataservice/icon.png"));
                    var groupConn = new ListViewGroup($"Connectors ({connectors.Children().Count()})");
                    lstpadep.Groups.Add(groupConn);
                    tables = new List<string>();

                    foreach (var conn in connectors)
                    {
                        JProperty property = conn as JProperty;
                        if (property != null)
                        {
                            if (!tables.Contains(property.Name))
                            {
                                tables.Add(property.Name);
                                var connImg = GetConnectorImage(property.Name);
                                if (connImg != null)
                                {
                                    item_ = new ListViewItem(new string[] { connImg.Tag.ToString() }, property.Name, groupConn);
                                    treeViewImgList.Images.Add(property.Name, connImg);
                                    lstpadep.Items.Add(item_);

                                }

                            }

                        }

                    }

                    connectorCount = count;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return count;
        }


        void AddActions(JObject jObject)
        {
            try
            {

                string _value = string.Empty;
                ListViewItem item_ = null;
                var actions = jObject.SelectTokens("..actions");//total enties used		actions.Children().ToList()	

                if (actions.Any())
                {

                    treeViewImgList.Images.Add("paactions",
                          LoadImage("https://raw.githubusercontent.com/yesadahmed/xrmtoolboxdocumentation/main/pics/actions.PNG?raw=true"));
                    var groupAct = new ListViewGroup($"Actions ({actions.Children().Count()})");
                    lstpadep.Groups.Add(groupAct);
                    foreach (var act in actions.Children())
                    {

                        JProperty actionName = act as JProperty;
                        if (actionName != null)
                        {
                            item_ = new ListViewItem(new string[] { actionName.Name }, "paactions", groupAct);
                            lstpadep.Items.Add(item_);

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void AddTriggers(JObject jObject)
        {
            try
            {

                string _value = string.Empty;
                ListViewItem item_ = null;
                var triggers = jObject.SelectTokens("..triggers");//total enties used			

                if (triggers.Any())
                {


                    treeViewImgList.Images.Add("patriggers",
                          LoadImage("https://github.com/yesadahmed/xrmtoolboxdocumentation/blob/main/pics/triggers.PNG?raw=true"));
                    var groupTrgg = new ListViewGroup($"Main Trigger ({triggers.Children().Count()})");
                    lstpadep.Groups.Add(groupTrgg);
                    foreach (var act in triggers.Children())
                    {
                        JProperty property = act as JProperty;
                        if (property != null)
                        {
                            item_ = new ListViewItem(new string[] { property.Name }, "patriggers", groupTrgg);
                            lstpadep.Items.Add(item_);


                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Image GetConnectorImage(string key)
        {
            Image img = null;
            bool condtionLst = (connectorModel.value == null);
            bool condtionLstCount = (connectorModel.value.Count <= 0);

            if (connectorModel == null
                && condtionLst
                && condtionLstCount) return null;

            try
            {
                var connetor = connectorModel.value.FirstOrDefault(c => c.ActualName.Equals(key));
                if (connetor != null)
                {
                    img = LoadImage(connetor.iconUri);
                    img.Tag = connetor.DisplayName;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return img;
        }

        private void toolStripMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listViewsolflows_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection flows = this.listViewsolflows.SelectedItems;
            lstpadep.Clear();
            treeViewImgList = new ImageList();
            treeViewImgList.ColorDepth = ColorDepth.Depth32Bit;
            treeViewImgList.ImageSize = new Size(45, 45);
            lstpadep.LargeImageList = treeViewImgList;
            lstpadep.TileSize = new Size(450, 45);
            lstpadep.Columns.AddRange(new ColumnHeader[]
                 {new ColumnHeader()});

            foreach (ListViewItem item in flows)
            {
                if (item.Tag != null)
                    BuildFlowDependencyTree(item.Tag.ToString());
            }
        }

        private void selectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = @"C:\Dockers\adnan.csv";
            string flowjosn = string.Empty;
            if (listViewsolflows.SelectedItems != null && listViewsolflows.SelectedItems.Count <= 0)
            {
                Blink b = new Blink();
                b.Text(lblerror, "Please select flow to share.");
                return;
            }

            if (listViewsolflows.SelectedItems.Count > 0)
            {
                var fitem = listViewsolflows.SelectedItems[0];//one only
                if (fitem.Tag != null)
                {
                    flowjosn = Convert.ToString(fitem.Tag);
                    //save as json
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.Filter = "Json files (*.json)|*.json";
                    sf.FilterIndex = 2;
                    sf.RestoreDirectory = true;
                    sf.FileName = fitem.Text;
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sf.FileName, flowjosn);

                    }
                }


            }
        }



        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlowModel flowlst = null;
            FlowData flowData = null;
            string flowjosn = string.Empty;
            string solutionUniqueName = string.Empty;
            if (listViewsolflows.Items != null && listViewsolflows.Items.Count <= 0)
            {
                Blink b = new Blink();
                b.Text(lblerror, "Please select flow to share.");
                return;
            }

            var flows = listViewsolflows.Items;//one only
            if (flows != null)
            {
                flowlst = new FlowModel();
                flowlst.flows = new List<FlowData>();
                Blink b = new Blink();
                b.Text(lblerror, "Please wait Saving all flows dependencies as json file.");
                StringBuilder str = new StringBuilder();
                //show warning and build in runtime json
                foreach (var flow in flows)
                {
                    var item = flow as ListViewItem;
                    if (item != null)
                    {
                        flowjosn = Convert.ToString(item.Tag);
                        JObject jObject = JObject.Parse(flowjosn);
                        flowData = new FlowData();
                        flowData.name = item.Text;
                        flowData.Connectors = GetFlowConnectors(jObject);
                        flowData.Tables = GetFlowTables(jObject);
                        flowData.Triggers = GetFlowTriggers(jObject);
                        flowData.Actions = GetFlowActions(jObject);
                        flowlst.flows.Add(flowData);
                    }
                }

                if (flowlst != null && flowlst.flows != null && flowlst.flows.Count > 0)
                {
                    var jsontext = JsonConvert.SerializeObject(flowlst);
                    if (!string.IsNullOrWhiteSpace(jsontext))
                    {
                        if (cmbSolutions.SelectedItem != null)// grab SolutionUniqueName
                        {
                            CustomComboboxItem item = cmbSolutions.SelectedItem as CustomComboboxItem;
                            if (item != null)
                                solutionUniqueName = Convert.ToString(item.Value);
                        }

                        SaveFileDialog sf = new SaveFileDialog();
                        sf.Filter = "Json files (*.json)|*.json";
                        sf.FilterIndex = 2;
                        sf.RestoreDirectory = true;
                        sf.FileName = $"{solutionUniqueName}_flows";
                        if (sf.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllText(sf.FileName, jsontext);

                        }
                    }
                }
            }

        }


        #region Output File 

        FlowConnectors GetFlowConnectors(JObject jObject)
        {
            FlowConnectors connectorModel = new FlowConnectors();
            try
            {
                List<string> tables = null;
                var connectors = jObject.SelectToken("..connectionReferences");//total connectors used 
                if (connectors.Any())
                {
                    tables = new List<string>();//avoid duplicate
                    connectorModel.Connectors = new List<string>();
                    connectorModel.Total = connectors.Children().Count();
                    foreach (var conn in connectors)
                    {
                        JProperty property = conn as JProperty;
                        if (property != null)
                        {
                            if (!tables.Contains(property.Name))
                            {
                                tables.Add(property.Name);
                                connectorModel.Connectors.Add(property.Name);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return connectorModel;

        }

        FlowTables GetFlowTables(JObject jObject)
        {
            FlowTables flowTables = new FlowTables();
            try
            {
                string _value = "";
                List<string> tables = null;
                var totalentites = jObject.SelectTokens("..table");//total enties used			
                if (totalentites.Any())
                {
                    flowTables.Total = totalentites.Count();
                    flowTables.Tables = new List<string>();
                    tables = new List<string>();
                    foreach (var table in totalentites)
                    {
                        _value = Convert.ToString(table);
                        if (!tables.Contains(_value))
                        {
                            tables.Add(_value);//avoid duplicate
                            flowTables.Tables.Add(_value);

                        }
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }
            return flowTables;
        }

        FlowActions GetFlowActions(JObject jObject)
        {
            FlowActions flowActions = new FlowActions();
            try
            {
                string _value = string.Empty;
                ListViewItem item_ = null;
                var actions = jObject.SelectTokens("..actions");//total enties used			

                if (actions.Any())
                {
                    flowActions.Actions = new List<string>();
                    flowActions.Total = actions.Children().Count();

                    foreach (var act in actions.Children())
                    {
                        JProperty actionName = act as JProperty;
                        if (actionName != null)
                        {
                            flowActions.Actions.Add(actionName.Name);
                        }

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return flowActions;
        }

        FlowTriggers GetFlowTriggers(JObject jObject)
        {
            FlowTriggers flowTriggers = new FlowTriggers();
            try
            {
                string _value = string.Empty;
                ListViewItem item_ = null;
                var triggers = jObject.SelectTokens("..triggers");//total enties used			

                if (triggers.Any())
                {

                    flowTriggers.Total = triggers.Children().Count();
                    flowTriggers.Triggers = new List<string>();

                    foreach (var act in triggers.Children())
                    {
                        if (act.HasValues)
                        {
                            JProperty actionName = act as JProperty;
                            if (actionName != null)
                            {
                                flowTriggers.Triggers.Add(actionName.Name);

                            }
                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            return flowTriggers;
        }

        #endregion
    }
}