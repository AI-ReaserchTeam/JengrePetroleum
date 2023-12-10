using DevExpress.CodeParser;
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
using JengrePetroleum.Module.BusinessObjects.Station;
using JengrePetroleum.Module.BusinessObjects.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Blazor.Server.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DieselController : ViewController
    {
        public SimpleAction RequestDieselAction { get; set; }

        public DieselController()
        {
            InitializeComponent();
            TargetObjectType = typeof(Diesel);

            RequestDieselAction = new SimpleAction(this, "RequestDiesel", PredefinedCategory.Edit)
            {
                Caption = "Request Diesel",
                ImageName = "Action_SimpleAction",
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };

            RequestDieselAction.Execute += RequestDieselAction_Execute;

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
            RequestDieselAction.Execute -= RequestDieselAction_Execute;

        }


        private void RequestDieselAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            var diesel = (Diesel)View.CurrentObject;

          
            //use switch statement to check for approval status
            if (diesel.Approval == Approval.Pending)
            {
                IObjectSpace objectSpace = View.ObjectSpace;

                var currentTrip = objectSpace.FindObject<Trip>(CriteriaOperator.Parse("Oid = ?", diesel.Trip.Oid));
                var fillingStation = objectSpace.FindObject<FillingStation>(CriteriaOperator.Parse("Oid = ?", diesel.Station.Oid));

                if (fillingStation != null)
                {
                    var dieselExpense = objectSpace.CreateObject<DieselExpense>();
                    dieselExpense.Diesel = diesel.Quantity;
                    dieselExpense.Price = diesel.UnitPrice;
                    dieselExpense.Date = diesel.Date;
                    dieselExpense.TripAllowance = diesel.TripAlowance;
                    dieselExpense.Driver = currentTrip.Driver.FullName;
                    dieselExpense.Truck = currentTrip.Truck.RegistrationNumber;
                    dieselExpense.Trip = currentTrip;
                    fillingStation.DieselExpenses.Add(dieselExpense);

                    diesel.Approval = Approval.Requested;
                    objectSpace.CommitChanges();

                    //display notification success message
                    Application.ShowViewStrategy.ShowMessage("Diesel Requested Successfully", InformationType.Success);

                }
            }
            else if (diesel.Approval == Approval.Requested)
            {
                //display notification error message
                Application.ShowViewStrategy.ShowMessage("Diesel Request have been requested", InformationType.Warning);
            }
            else
            {
                //display notification error message
                Application.ShowViewStrategy.ShowMessage("Diesel Request have been Approved", InformationType.Warning);
            }

        }
    }
}
