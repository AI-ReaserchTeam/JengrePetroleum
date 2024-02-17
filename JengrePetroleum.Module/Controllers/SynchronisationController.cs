using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dotmim;
using Dotmim.Sync.SqlServer;
using Dotmim.Sync;

namespace JengrePetroleum.Module.Controllers
{

    public partial class SynchronisationController : ViewController
    {
        SimpleAction synchroniseAction;
        SqlSyncChangeTrackingProvider remoteProvider;
        SqlSyncChangeTrackingProvider localProvider;
        SyncAgent agent;

        public SynchronisationController()
        {
            InitializeComponent();
            remoteProvider = new SqlSyncChangeTrackingProvider("Data Source=.;Initial Catalog=JengrePetroleum;Integrated Security=True");
            localProvider = new SqlSyncChangeTrackingProvider("Data Source=.;Initial Catalog=JengrePetroleum;Integrated Security=True");
            agent = new SyncAgent(localProvider, remoteProvider);

            synchroniseAction = new SimpleAction(this, "Synchronise", PredefinedCategory.Edit)
            {
                ImageName = "Action_Refresh",
                Caption   = "Synchronise",
                ToolTip   = "Synchronise the data with server"
            };

            synchroniseAction.Execute += SynchroniseAction_Execute;
        }
        void SynchroniseAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
           
        }
        protected override async void OnActivated()
        {
            base.OnActivated();

            var localOrchestrator = agent.LocalOrchestrator;
            var remoteOrchestrator = agent.RemoteOrchestrator;

            //SyncSetup setup = await remoteOrchestrator.GetAllTablesAsync();
            
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
