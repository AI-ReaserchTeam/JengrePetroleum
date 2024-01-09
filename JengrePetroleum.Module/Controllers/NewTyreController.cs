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
using DevExpress.XtraSpreadsheet.Model.CopyOperation;
using JengrePetroleum.Module.BusinessObjects.Store;
using JengrePetroleum.Module.BusinessObjects.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.Controllers
{
    public partial class NewTyreController : ViewController
    {
        
        public NewTyreController()
        {
            InitializeComponent();
            TargetObjectType = typeof(Tyre);

            var newTyreAction = new PopupWindowShowAction(this, "MoveTyre", PredefinedCategory.Edit)
            {
                Caption = "NewTyreTo",
                ImageName = "Action_New",
                TargetObjectsCriteria = "Status = 'InStock'",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects
               
            };
            newTyreAction.CustomizePopupWindowParams += NewTyreAction_CustomizePopupWindowParams;
            newTyreAction.Execute += NewTyreAction_Execute;
        }
        void NewTyreAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(TyreTransaction));
            TyreTransaction newTyreParameters = objectSpace.CreateObject<TyreTransaction>();

            foreach (Tyre selectedTyre in View.SelectedObjects)
            {
                var tyre = objectSpace.GetObject<Tyre>(selectedTyre);
                newTyreParameters.Tyres?.Add(tyre);
            }
             
            e.View = Application.CreateDetailView(objectSpace, newTyreParameters);
        }
        void NewTyreAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            TyreTransaction transaction = e.PopupWindowViewCurrentObject as TyreTransaction;
            IObjectSpace objectSpace = View.ObjectSpace;
            //Truck truck = objectSpace.GetObject<Truck>(transaction.Truck);
            //truck.Tyres ??= new XPCollection<Tyre>();

            if (transaction.Truck != null && transaction.Driver != null && transaction.Tyres != null)
            {
               
             
                foreach (Tyre tyre in transaction.Tyres)
                {
                    transaction.Truck.Tyres.Add(tyre);
                    tyre.Status = TyreStatus.InUse;
                    tyre.Save();
                }
                objectSpace.CommitChanges();
                View.Refresh();
            }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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
