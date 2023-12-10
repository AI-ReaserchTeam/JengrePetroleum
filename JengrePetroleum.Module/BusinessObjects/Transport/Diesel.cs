using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
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
            Approval = Approval.Pending;
			DieselClerk = Session.Query<Employee>().Where(e => e.Position == Position.DieselClerk).FirstOrDefault();
            //check for null using null colescing operator in trip
        


		}


        private XPCollection<AuditDataItemPersistent> changeHistory;
        FillingStation station;
        Approval approval;
        DateTime date;
        decimal tripallowance;
        double unitprice;
        double quantity;
        Trip trip;
        Employee dieselClerk;

        public Employee DieselClerk
        {
            get => dieselClerk;
            set => SetPropertyValue(nameof(DieselClerk), ref dieselClerk, value);
        }

      
        public Trip Trip
        {
            get => trip;

            set
            {
                if(Trip != value)
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


        [DevExpress.Xpo.DisplayName("Diesel(Ltr)")]
        public double Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }

        [DevExpress.Xpo.DisplayName("Price/Ltr")]
        public double UnitPrice
        {
            get => unitprice;
            set => SetPropertyValue(nameof(UnitPrice), ref unitprice, value);
        }


        [PersistentAlias("Quantity * UnitPrice")]
        public decimal DieselCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(DieselCost))); }
        }

        public decimal TripAlowance
        {
            get => tripallowance;
            set => SetPropertyValue(nameof(TripAlowance), ref tripallowance, value);
        }


        [PersistentAlias("TripAlowance + DieselCost")]
        public decimal TotalCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalCost))); }
        }


        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), ref date, value);
        }


       
        public Approval Approval
        {
            get => approval;
            set => SetPropertyValue(nameof(Approval), ref approval, value);
        }


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
	
}