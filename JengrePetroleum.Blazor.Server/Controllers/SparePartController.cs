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
using JengrePetroleum.Module.BusinessObjects.Store;
using JengrePetroleum.Module.BusinessObjects.Store.SpareParts;
using JengrePetroleum.Module.BusinessObjects.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Blazor.Server.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class SparePartController : ObjectViewController<ListView, SparePart>
    {
        public SingleChoiceAction GiveToAction { get; set; }

        public SparePartController()
        {

            InitializeComponent();

            GiveToAction = new SingleChoiceAction(this, "GiveToAction", PredefinedCategory.Edit)
            {
                Caption = "GiveTo",
                ItemType = SingleChoiceActionItemType.ItemIsMode,
                ImageName = "Action_Grant",
                ConfirmationMessage = "Are you sure you want to give this part to the selected work?",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
                TargetObjectsCriteria = "Status = 'Instock'",
                TargetObjectType = typeof(SparePart),
                ToolTip = "Give this part to the selected work"
            };

            GiveToAction.Execute += GiveToAction_Execute;
        }

        private void GiveToAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var selectedMainataince = e.SelectedChoiceActionItem.Data as Maintainance;
            var work = ObjectSpace.CreateObject<Work>();

            foreach (SparePart sparePart in e.SelectedObjects)
            {
                work.SpareParts.Add(sparePart);
                sparePart.Status = Status.InUse;

            }
            //work.Maintainance = selectedMainataince;
            selectedMainataince.Works.Add(work);
            ObjectSpace.CommitChanges();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            GiveToAction.Execute += GiveToAction_Execute;
            GiveToAction.Items.Clear();

            var maintainances = ObjectSpace.GetObjects<Maintainance>().Where(m => m.History == MaintainanceHistory.Current && m.Status == MaintenanceTaskStatus.InProgress);
            foreach (var maintainance in maintainances)
            {
                GiveToAction.Items.Add(new ChoiceActionItem(maintainance.Maintainace, maintainance));
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            GiveToAction.Execute -= GiveToAction_Execute;
            base.OnDeactivated();
        }
    }
}
