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
using DevExpress.Xpo;
using JengerePetroleum.Module.BusinessObjects;
using JengrePetroleum.Module.BusinessObjects;
using JengrePetroleum.Module.BusinessObjects.Station;
using JengrePetroleum.Module.BusinessObjects.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Blazor.Server.Controllers
{
    public partial class DieselAppovalController : ObjectViewController<ListView, DieselExpense>
    {
        public SimpleAction DieselRequestAction { get; set; }


        public DieselAppovalController()
        {
            InitializeComponent();
            DieselRequestAction = new SimpleAction(this, "Approve", PredefinedCategory.Edit)
            {
                Caption = "Approve",
                ImageName = "Action_SimpleAction",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
                //TargetObjectsCriteria = "Trip.Diesel.Approval = 'Pending'"
            };

            DieselRequestAction.Execute += DieselRequestAction_Execute;
        }

        private void DieselRequestAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var selectedObjects = View.SelectedObjects;
            foreach (DieselExpense item in selectedObjects)
            {
                item.Trip.Diesel.Approval = Approval.Approved;
                item.Trip.Status = TripStatus.InProgress;
                item.Save();
            }
            View.ObjectSpace.CommitChanges();
            View.Refresh();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            var user = SecuritySystem.CurrentUser as Employee;
            DieselRequestAction.Active.SetItemValue("StationManager", (user.Position == Position.StationManager));

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
            DieselRequestAction.Execute -= DieselRequestAction_Execute;
        }
    }
}
