using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraPrinting;
using JengrePetroleum.Module.BusinessObjects.Store.Tyres;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
    [Appearance("Undermaintainace", TargetItems = "*", Criteria = "TruckStatus == 'UnderMaintenance'",FontColor = "Red", FontStyle = FontStyle.Bold)]
    [Appearance("Ontrip", TargetItems = "*", Criteria = "TruckStatus == 'OnTrip'", FontColor = "Blue", FontStyle = FontStyle.Bold)]
    [Appearance("available", TargetItems = "*", Criteria = "TruckStatus == 'Available'", FontColor = "Green", FontStyle = FontStyle.Bold)]

    public class Truck : BaseObject
    {

        public Truck(Session session) : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
			TruckStatus = TruckStatus.Available;
           
       
        }

        string chasisNumber;
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

        [RuleUniqueValue]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ChasisNumber
         {
         	get => chasisNumber;
         	set => SetPropertyValue(nameof(ChasisNumber), ref chasisNumber, value);
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

        [DevExpress.Xpo.DisplayName("CM COST")]
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

        [Association("Truck-Tyres")]
        public XPCollection<Tyre> Tyres
        {
            get
            {
                return GetCollection<Tyre>(nameof(Tyres));
            }
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