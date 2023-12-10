using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;
using DisplayNameAttribute = DevExpress.Xpo.DisplayNameAttribute;

namespace JengrePetroleum.Module.BusinessObjects.Transport
{
    [DefaultClassOptions]
    [NavigationItem("Employees")]
    [ImageName("BO_Customer")]
	[DefaultProperty(nameof(FullName))]
	public class Driver : EmployeeBase
    {
      
        public Driver(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Position = BusinessObjects.Position.Driver;
            Department = Department.Transportation;

        }



        XPCollection<Trip> trips;
        DateTime licenseExpiryDate;
        string licenseNumber;

        [RuleUniqueValue(),RuleRequiredField]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string LicenseNumber
        {
            get => licenseNumber;
            set => SetPropertyValue(nameof(LicenseNumber), ref licenseNumber, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public DateTime LicenseExpiryDate
        {
            get => licenseExpiryDate;
            set => SetPropertyValue(nameof(LicenseExpiryDate), ref licenseExpiryDate, value);
        }

        public XPCollection<Trip> Trips
        {
            get => trips;
            set => SetPropertyValue(nameof(Trips), ref trips, value);
        }



        [VisibleInListView(false)]
        public int TotalTrips
        {
            get
            {
				return Trips?.Count(t => t.TripDate.Month == DateTime.Now.Month && t.TripDate.Year == DateTime.Now.Year) ?? 0;
			}
        }
       
        [VisibleInListView(false)]
        public int TotalTripsThisYear
        {
            get
            {
				return Trips?.Count(t => t.TripDate.Year == DateTime.Now.Year) ?? 0;
			}
        }

        [VisibleInListView(false)]
        public int TotalTripsThisMonth
        {
            get
            {
				return Trips?.Count(t => t.TripDate.Month == DateTime.Now.Month && t.TripDate.Year == DateTime.Now.Year) ?? 0;
			}
        }

        [DisplayName("Documents")]
        [Association("Driver-TruckFiles")]
        public XPCollection<TruckFiles> Documents
        {
            get
            {
                return GetCollection<TruckFiles>(nameof(Documents));
            }
        }

        
        [Association("Employee-MaintainanceDriver")]
        public XPCollection<Maintainance> Maintainance
        {
            get
            {
                return GetCollection<Maintainance>(nameof(Maintainance));
            }
        }
    }
}

