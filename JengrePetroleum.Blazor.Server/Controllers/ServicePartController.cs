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
using JengrePetroleum.Module.BusinessObjects;
using JengrePetroleum.Module.BusinessObjects.Store;
using JengrePetroleum.Module.BusinessObjects.Store.ServiceParts;
using JengrePetroleum.Module.BusinessObjects.Store.SpareParts;
using JengrePetroleum.Module.BusinessObjects.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Blazor.Server.Controllers
{

    public partial class ServicePartController : ObjectViewController<ListView, ServicePart>
    {
        public SingleChoiceAction serviePartChoiceAction { get; set; }
        public ServicePartController()
        {
            InitializeComponent();

            serviePartChoiceAction = new SingleChoiceAction(this, "ServicePartAction", PredefinedCategory.Edit)
            {
                Caption = "ServicePartTo",
                ItemType = SingleChoiceActionItemType.ItemIsMode,
                ImageName = "Action_Grant",
                ConfirmationMessage = "Are you sure you want to give this part to the selected work?",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
                TargetObjectsCriteria = "Status = 'Instock'",
                TargetObjectType = typeof(ServicePart),
                ToolTip = "Give this part to the selected work"
            };

            serviePartChoiceAction.Execute += ServicePartChoiceAction_Execute;

        }

        private void ServicePartChoiceAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var selectedMainataince = e.SelectedChoiceActionItem.Data as Maintainance;


            var works = selectedMainataince.Works.Where(w => w.ServiceParts != null);
            var work = ObjectSpace.CreateObject<Work>();

            foreach (ServicePart servicepart in e.SelectedObjects)
            {
                work.Description = $"{servicepart.Name.Name} Service Part";
                work.ServiceParts.Add(servicepart);
                servicepart.Status = Module.BusinessObjects.Store.Status.InUse;

            }
            //work.Maintainance = selectedMainataince;
            selectedMainataince.Works.Add(work);
            ObjectSpace.CommitChanges();
        }

        protected override void OnActivated()
        {


            base.OnActivated();
            serviePartChoiceAction.Execute += ServicePartChoiceAction_Execute;

            serviePartChoiceAction.Items.Clear();

            var maintainances = ObjectSpace.GetObjects<Maintainance>().Where(m => m.History == MaintainanceHistory.Current && m.Status == MaintenanceTaskStatus.InProgress);
            foreach (var maintainance in maintainances)
            {
                serviePartChoiceAction.Items.Add(new ChoiceActionItem(maintainance.Maintainace, maintainance));
            }

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

        }
        protected override void OnDeactivated()
        {
            serviePartChoiceAction.Execute -= ServicePartChoiceAction_Execute;

            base.OnDeactivated();
        }
    }
}
