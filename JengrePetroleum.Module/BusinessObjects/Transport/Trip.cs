using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;
using DisplayNameAttribute = DevExpress.Xpo.DisplayNameAttribute;

namespace JengrePetroleum.Module.BusinessObjects.Transport
{
    [NavigationItem("Transportation")]
    [DefaultClassOptions]
    [ImageName("Action_StateMachine")]
    [DefaultProperty("TripDisplayName")]
    [ListViewFilter("All", "", true, Index = 0)]
    [ListViewFilter("In Progress", "[Status] = 'InProgress'", true, Index = 1)]
    [ListViewFilter("Completed", "[Status] = 'Completed'", true, Index = 2)]
    [ListViewFilter("Cancelled", "[Status] = 'Cancelled'", true, Index = 3)]
    [ListViewFilter("Pending", "[Status] = 'Pending'", true, Index = 4)]
    [ObjectCaptionFormat("{0:TripDisplayName}")]
    [Appearance("InProgress", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Status = 'InProgress'", FontColor = "Blue", FontStyle = System.Drawing.FontStyle.Bold)]
    [Appearance("Pending", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Status = 'Pending'", FontColor = "Red", FontStyle = System.Drawing.FontStyle.Bold)]
    [Appearance("Completed", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Status = 'Completed'", FontColor = "Green", FontStyle = System.Drawing.FontStyle.Bold)]
    public class Trip : BaseObject
    {
        Diesel diesel;
        double waybillRate;
        Product product;
        DateTime waybillDate;
        int quantity;
        decimal loadingFee;
        DateTime tripDate;
        Haulage haulage;
        TripStatus status;
        DateTime receivingDate;
        DateTime leavingDate;
        string meterTicket;
        string depot;
        string tolocation;
        string fromLocation;
        DateTime smrdate;
        string smrnumber;
        Driver driver;
        Truck truck;
        private XPCollection<AuditDataItemPersistent> changeHistory;

        public Trip(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TripDate = DateTime.Now;

            if (Truck != null)
            {
                Truck.TruckStatus = TruckStatus.OnTrip;
            }
            if (Driver != null)
            {
                Driver.DriverStatus = DriverStatus.OnTrip;
            }

        }

        [XafDisplayName("SMR/MTR NO.")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string SMRNumber
        {
            get => smrnumber;
            set => SetPropertyValue(nameof(SMRNumber), ref smrnumber, value);
        }

        [VisibleInListView(false)]
        public DateTime SMRDate
        {
            get => smrdate;
            set => SetPropertyValue(nameof(SMRDate), ref smrdate, value);
        }

        [XafDisplayName("FROM")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string FromLocation
        {
            get => fromLocation;
            set => SetPropertyValue(nameof(FromLocation), ref fromLocation, value);
        }

        [XafDisplayName("TO")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ToLcation
        {
            get => tolocation;
            set => SetPropertyValue(nameof(ToLcation), ref tolocation, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Depot
        {
            get => depot;
            set => SetPropertyValue(nameof(Depot), ref depot, value);
        }

        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string MeterTicket
        {
            get => meterTicket;
            set => SetPropertyValue(nameof(MeterTicket), ref meterTicket, value);
        }
        [VisibleInListView(false)]
        [DisplayName("L DATE")]
        public DateTime LeavingDate
        {
            get => leavingDate;
            set => SetPropertyValue(nameof(LeavingDate), ref leavingDate, value);
        }

        [VisibleInListView(false)]
        [DisplayName("R DATE")]
        public DateTime ReceivingDate
        {
            get => receivingDate;
            set => SetPropertyValue(nameof(ReceivingDate), ref receivingDate, value);
        }

        [VisibleInDetailView(false)]
        public TripStatus Status
        {
            get => status;
            set => SetPropertyValue(nameof(Status), ref status, value);
        }

        [XafDisplayName("Company")]
        public Haulage Haulage
        {
            get => haulage;
            set => SetPropertyValue(nameof(Haulage), ref haulage, value);
        }

        [RuleRequiredField]
        [Association("Vehicle-Trips")]
        [DataSourceCriteria("TruckStatus = 'Available'")]
        public Truck Truck
        {
            get => truck;
            set => SetPropertyValue(nameof(Truck), ref truck, value);
        }

        [RuleRequiredField]
        public Driver Driver
        {
            get => driver;
            set => SetPropertyValue(nameof(Driver), ref driver, value);
        }

        [XafDisplayName("Expense")]
        [PersistentAlias("LoadingFee + Diesel.TotalCost + TotalMaintainaceCost")]
        public decimal TotalExpense
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalExpense))); }
        }


        public DateTime TripDate
        {
            get => tripDate;
            set => SetPropertyValue(nameof(TripDate), ref tripDate, value);
        }

        [VisibleInListView(false)]
        public decimal LoadingFee
        {
            get => loadingFee;
            set => SetPropertyValue(nameof(LoadingFee), ref loadingFee, value);
        }

        [VisibleInListView(false)]
        [PersistentAlias("Quantity * WaybillRate")]
        public decimal WayBillAmount
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(WayBillAmount))); }
        }

        [VisibleInListView(false)]
        public DateTime WaybillDate
        {
            get => waybillDate;
            set => SetPropertyValue(nameof(WaybillDate), ref waybillDate, value);
        }

        [VisibleInListView(false)]
        public double WaybillRate
        {
            get => waybillRate;
            set => SetPropertyValue(nameof(WaybillRate), ref waybillRate, value);
        }

        [XafDisplayName("Balance")]
        [PersistentAlias("WayBillAmount - TotalExpense")]
        public decimal TripBalance
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TripBalance))); }
        }

        [XafDisplayName("QTY")]
        public int Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }


        public Product Product
        {
            get => product;
            set => SetPropertyValue(nameof(Product), ref product, value);
        }

        [XafDisplayName("Maintaince Cost")]
        [VisibleInListView(false)]
        [PersistentAlias("Truck.CurrentMaintainanceTaskCost")]
        public decimal TotalMaintainaceCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalMaintainaceCost))); }
        }



        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public Diesel Diesel
        {
            get => diesel;
            set
            {
                if (Diesel != value)
                {
                    Diesel prevDiesel = Diesel;
                    Diesel newDiesel = value;
                    diesel = newDiesel;
                    if (prevDiesel != null)
                    {
                        prevDiesel.Trip = null;
                    }
                    if (newDiesel != null)
                    {
                        newDiesel.Trip = this;
                    }
                    OnChanged(nameof(Diesel), prevDiesel, newDiesel);
                }
            }
        }

        [VisibleInListView(false)]
        [PersistentAlias("Concat(Truck.RegistrationNumber, ' ', Driver.UserName, ' ', ToLcation)")]
        public string TripDisplayName
        {
            get { return Convert.ToString(EvaluateAlias(nameof(TripDisplayName))); }
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

        [Action(Caption = "Complete Trip", ConfirmationMessage = "Are you sure this trip is completed?", ImageName = "Action_Deny", AutoCommit = true, TargetObjectsCriteria = "Status = 'InProgress'")]
        public void CompleteTrip()
        {
            status = TripStatus.Completed;
            Truck.TruckStatus = TruckStatus.Available;
            Driver.DriverStatus = DriverStatus.Available;
            Truck.CurrentMaintainance.History = MaintainanceHistory.History;
        }


    }

    public enum TripStatus { Pending, InProgress, Completed, Cancelled }
    public enum Haulage { Jengre, NNPC, OVH, AYMSHAFA, AARANO, Others }
    public enum Product { PMS, AGO, LPG, DPK }
}