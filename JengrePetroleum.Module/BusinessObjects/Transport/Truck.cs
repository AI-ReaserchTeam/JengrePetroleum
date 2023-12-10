using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraPrinting;
using JengrePetroleum.Module.BusinessObjects.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.BusinessObjects.Transport
{
    [DefaultClassOptions]
    [NavigationItem("Transportation")]
    [ObjectCaptionFormat("{0:RegistrationNumber}")]
    [ImageName("BO_Vendor")]
    [DefaultProperty("RegistrationNumber")]
    [FileAttachment(nameof(File))]
	[ListViewFilter("Available", "[TruckStatus] = 'Available'", true, Index = 1)]
	[ListViewFilter("On Trip", "[TruckStatus] = 'OnTrip'", true, Index = 2)]
	[ListViewFilter("Under Maintenance", "[TruckStatus] = 'UnderMaintenance'", true, Index = 3)]
	[ListViewFilter("All", "", true, Index = 0)]

    public class Truck : BaseObject
    {

        public Truck(Session session) : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
			this.TruckStatus = TruckStatus.Available;
        }



        XPCollection<Tyre> tyres;
        TruckOwner vehicleOwner;
        TruckStatus truckStatus;
        string model;
        string registrationNumber;
        private XPCollection<AuditDataItemPersistent> changeHistory;

        [RuleRequiredField,RuleUniqueValue()]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string RegistrationNumber
        {
            get => registrationNumber;
            set => SetPropertyValue(nameof(RegistrationNumber), ref registrationNumber, value);
        }

        [RuleRequiredField]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Model
        {
            get => model;
            set => SetPropertyValue(nameof(Model), ref model, value);
        }

     
        public TruckStatus TruckStatus
        {
            get => truckStatus;
            set => SetPropertyValue(nameof(TruckStatus), ref truckStatus, value);
        }


        public TruckOwner TruckOwner
        {
            get => vehicleOwner;
            set => SetPropertyValue(nameof(TruckOwner), ref vehicleOwner, value);
        }

        [VisibleInDetailView(false)]
        public Trip CurrentTrip
        {
            get => Trips.FirstOrDefault(t => t.Status == TripStatus.InProgress);
        }



        [Association("Vehicle-Trips")]
        public XPCollection<Trip> Trips
        {
            get
            {
                return GetCollection<Trip>(nameof(Trips));
            }
        }

        [Association("Truck-Maintainance")]
        public XPCollection<Maintainance> Maintainance
        {
            get
            {
                return GetCollection<Maintainance>(nameof(Maintainance));
            }
        }

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public Maintainance CurrentMaintainance
        {
            get => Maintainance.FirstOrDefault(t => t.History == MaintainanceHistory.Current);
        }

        [VisibleInListView(false)]
        public decimal CurrentMaintainanceTaskCost
        {
            get => CurrentMaintainance?.TotalCost ?? 0;
        }


        [Association("Truck-TruckFiles"), DevExpress.Xpo.Aggregated]
        public XPCollection<TruckFiles> TruckFiles
        {
            get
            {
                return GetCollection<TruckFiles>(nameof(TruckFiles));
            }
        }
        
        public XPCollection<Tyre> Tyres
        {
            get => tyres;
            set => SetPropertyValue(nameof(Tyres), ref tyres, value);
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

    public enum TruckStatus { Available, OnTrip, UnderMaintenance }
    public enum TruckOwner { Jengere, MLAWAN, Babaliye, Danasabe, Danburai, NonJP }
}