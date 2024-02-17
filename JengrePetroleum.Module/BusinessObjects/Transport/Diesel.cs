using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraSpreadsheet.Model.History;
using JengerePetroleum.Module.BusinessObjects;
using JengrePetroleum.Module.BusinessObjects.Station;
using System;
using System.ComponentModel;
using System.Linq;

namespace JengrePetroleum.Module.BusinessObjects.Transport
{
	[DefaultClassOptions]
	[DefaultProperty(nameof(Quantity))]
	[NavigationItem("Transportation")]
    [Appearance("DisableFields", TargetItems = "*", Criteria = "Approval == 'Approved'", FontColor = "Green", FontStyle = System.Drawing.FontStyle.Bold, Enabled = false)]
    [Appearance("Pending", TargetItems = "*", Criteria = "Approval == 'Pending'", FontColor = "Black", FontStyle = System.Drawing.FontStyle.Bold)]
    [Appearance("Requested", TargetItems = "*", Criteria = "Approval == 'Requested'", FontColor = "Blue", FontStyle = System.Drawing.FontStyle.Bold)]
	public class Diesel : BaseObject
	{ 
		public Diesel(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
			Date = DateTime.Now;
            approval = Approval.Pending;

            //dieselClerk = Session.Query<Employee>().Where(e => e.Position == Position.DIESELEXECUTIVE).FirstOrDefault();
            var emp = SecuritySystem.CurrentUser as Employee;
            dieselClerk = emp.Position == Position.DIESELEXECUTIVE ? emp : null;

		}



        DieselStation nONJPStation;
        Station? dieselStation;
        private XPCollection<AuditDataItemPersistent> changeHistory;
        FillingStation station;
        Approval approval;
        DateTime date;
        decimal? tripallowance;
        double? unitprice;
        double? quantity;
        Trip trip;
        Employee dieselClerk;

        [RuleRequiredField]
        [DataSourceCriteria("Position = ' DIESELEXECUTIVE'")]
        public Employee DieselClerk
        {
            get => dieselClerk;
            set => SetPropertyValue(nameof(DieselClerk), ref dieselClerk, value);
        }

        [RuleRequiredField]
        public Trip Trip
        {
            get => trip;

            set
            {
                if (Trip != value)
                {
                    Trip prevTrip = Trip;
                    Trip newTrip = value;
                    trip = newTrip;
                    if (prevTrip != null)
                    {
                        prevTrip.Diesel = null;
                    }
                    if (newTrip != null)
                    {
                        newTrip.Diesel = this;
                    }
                    OnChanged(nameof(Trip), prevTrip, newTrip);
                }
            }
        }

        [RuleRequiredField]
        public Station? DieselStation
        {
            get => dieselStation;
            set => SetPropertyValue(nameof(DieselStation), ref dieselStation, value);
        }

        [RuleRequiredField]
        [DevExpress.Xpo.DisplayName("Diesel(Ltr)")]
        public double? Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }

        [RuleRequiredField]
        [DevExpress.Xpo.DisplayName("Price/Ltr")]
        public double? UnitPrice
        {
            get => unitprice;
            set => SetPropertyValue(nameof(UnitPrice), ref unitprice, value);
        }



        [PersistentAlias("Quantity * UnitPrice")]
        public decimal DieselCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(DieselCost))); }
        }

        [RuleRequiredField]
        [XafDisplayName("Allowance")]
        public decimal? TripAlowance
        {
            get => tripallowance;
            set => SetPropertyValue(nameof(TripAlowance), ref tripallowance, value);
        }


        [PersistentAlias("TripAlowance + DieselCost")]
        public decimal TotalCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalCost))); }
        }

        [RuleRequiredField]
        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), ref date, value);
        }


        //show only if diesel station is not JENGERE
        [Appearance("HideStation", Criteria = "DieselStation = 'JENGERE'", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public DieselStation NONJPStation
        {
            get => nONJPStation;
            set => SetPropertyValue(nameof(NONJPStation), ref nONJPStation, value);
        }


        public Approval Approval
        {
            get => approval;
            set => SetPropertyValue(nameof(Approval), ref approval, value);
        }


   
        [Appearance("HideStationJP", Criteria = "DieselStation != 'JENGERE'", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [XafDisplayName("Branch")]
        public FillingStation Station
        {
            get => station;
            set => SetPropertyValue(nameof(Station), ref station, value);
        }


        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AuditDataItemPersistent> ChangeHistory
        {
            get
            {
                changeHistory ??= AuditedObjectWeakReference.GetAuditTrail(Session, this);
                return changeHistory;
            }
        }


    }

    public enum Approval
    {
        [ImageName("State_Priority_Low")]
        Pending,
        [ImageName("State_Priority_Normal")]
        Requested,
        [ImageName("State_Priority_High")]
        Approved,
    }


    public enum Station
    {
        JENGERE,
        OTHERS
    }

}