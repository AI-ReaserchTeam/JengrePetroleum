using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
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
    [DefaultProperty("DefaultName")]
    [NavigationItem("Transportation")]
    //[ImageName("BO_Contact")]
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
            maintainanceDate = DateTime.Now;
        }



        Driver driver;
        Employee manager;
        MaintainanceHistory history;
        MaintenanceTaskStatus maintenanceTaskStatus;
        MaintainanceType maintainanceType;
        DateTime maintainanceDate;
        private XPCollection<AuditDataItemPersistent> changeHistory;
        Truck truck;




        public DateTime MaintainanceDate
        {
            get => maintainanceDate;
           // set => SetPropertyValue(nameof(MaintainanceDate), ref maintainanceDate, value);
        }


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

        [PersistentAlias("Works.Sum(Amount)")]
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

        [DataSourceCriteria("Position == 3")]
        [Association("Employee-MaintainanceManager")]
        public Employee Manager
        {
            get => manager;
            set => SetPropertyValue(nameof(Manager), ref manager, value);
        }

        [Association("Employee-MaintainanceDriver")]
        [DataSourceCriteria("Position == 11")]
        public Driver Driver
        {
            get => driver;
            set => SetPropertyValue(nameof(Driver), ref driver, value);
        }

        
        [PersistentAlias("Concat(Truck.RegistrationNumber, ' ',[Driver.UserName], ' ', [Manager.UserName])")]
        public string DefaultName
        {
            get { return Convert.ToString(EvaluateAlias(nameof(DefaultName))); }
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

	public enum MaintainanceType { GarageWork, OutsideWork }
    public enum MaintenanceTaskStatus { Pending, InProgress, Completed }
    public enum MaintainanceHistory { Current, History }
}