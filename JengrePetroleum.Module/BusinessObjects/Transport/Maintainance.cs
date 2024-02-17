using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengerePetroleum.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AggregatedAttribute = DevExpress.Xpo.AggregatedAttribute;

namespace JengrePetroleum.Module.BusinessObjects.Transport
{
    [DefaultClassOptions]
    [DefaultProperty(nameof(Maintainace))]
    [NavigationItem("Transportation")]
    [Appearance("DisableFields", TargetItems = "*", Criteria = "Status == 'Completed'", Enabled = false)]
 
    public class Maintainance : BaseObject
    {
        public Maintainance(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            History = MaintainanceHistory.Current;
            Status = MaintenanceTaskStatus.InProgress;
            maintainanceDate = DateTime.Now;


        }
        protected override void OnSaving()
        {
            base.OnSaving();
            if (Session.IsNewObject(this) && this.Truck is not null)
            {
                Session.GetObjectByKey<Truck>(Truck.Oid).TruckStatus = TruckStatus.UnderMaintenance;
            }
        }


        Driver driver;
        Employee supervisor;
        MaintainanceHistory history;
        MaintenanceTaskStatus maintenanceTaskStatus;
        MaintainanceType maintainanceType;
        DateTime maintainanceDate;
        private XPCollection<AuditDataItemPersistent> changeHistory;
        Truck truck;



        [RuleRequiredField]
        [DevExpress.Xpo.DisplayName("Date")]
        public DateTime MaintainanceDate
        {
            get => maintainanceDate;
            set => SetPropertyValue(nameof(MaintainanceDate), ref maintainanceDate, value);
        }

        [DevExpress.Xpo.DisplayName("Type")]
        public MaintainanceType MaintainanceType
        {
            get => maintainanceType;
            set => SetPropertyValue(nameof(MaintainanceType), ref maintainanceType, value);
        }


        public MaintenanceTaskStatus Status
        {
            get => maintenanceTaskStatus;
            set => SetPropertyValue(nameof(Status), ref maintenanceTaskStatus, value);
        }

        [PersistentAlias("Works.Sum(TotalCost)")]
        public decimal TotalCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalCost))); }
        }



        [Association("Maintainance-Works"), Aggregated]
        public XPCollection<Work> Works
        {
            get
            {
                return GetCollection<Work>(nameof(Works));
            }
        }

        [DataSourceCriteria("TruckStatus == 'Available'")]
        [RuleRequiredField(CustomMessageTemplate = "Please select a Truck")]
        [Association("Truck-Maintainance")]
        public Truck Truck
        {
            get => truck;
            set => SetPropertyValue(nameof(Truck), ref truck, value);
        }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public MaintainanceHistory History
        {
            get => history;
            set => SetPropertyValue(nameof(History), ref history, value);
        }

        [RuleRequiredField(CustomMessageTemplate ="Kindly Select a Maintainance Executive!")]
        [DataSourceCriteria("Position == 'MAINTAINANCEEXECUTIVE'")]
        [Association("Employee-MaintainanceManager")]
        public Employee Executive
        {
            get => supervisor;
            set => SetPropertyValue(nameof(Executive), ref supervisor, value);
        }

        [RuleRequiredField(CustomMessageTemplate = "Please Select a Driver!")]
        [Association("Employee-MaintainanceDriver")]
        [DataSourceCriteria("Position == 'DRIVER' AND DriverStatus == 'Available'")]
        public Driver Driver
        {
            get => driver;
            set => SetPropertyValue(nameof(Driver), ref driver, value);
        }


        [PersistentAlias("Concat(Truck.RegistrationNumber, ' ',[Driver.UserName], ' ', [Executive.UserName])")]
        public string Maintainace
        {
            get { return Convert.ToString(EvaluateAlias(nameof(Maintainace))); }
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

        [Action(Caption = "Complete", ConfirmationMessage = "Are you sure to complete this maintainace?", ImageName = "Action_Grant", AutoCommit = true)]
        public void Complete()
        {
            Status = MaintenanceTaskStatus.Completed;
            History = MaintainanceHistory.History;
        }

    }

    public enum MaintainanceType { GarageWork, OutsideWork }
    public enum MaintenanceTaskStatus { Pending, InProgress, Completed }
    public enum MaintainanceHistory { Current, History }
}