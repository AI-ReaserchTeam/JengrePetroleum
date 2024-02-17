using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Transport;


namespace JengrePetroleum.Module.BusinessObjects.Store.Tyres
{
    [DefaultClassOptions]
    [XafDisplayName("TYRES")]
    [ListViewFilter("Damaged", "Status = 'Damaged'", true, Index = 3)]
    [ListViewFilter("InUse", "Status = 'InUse'", true, Index = 2)]
    [ListViewFilter("InStock", "Status = 'InStock'", true, Index = 0)]
    [ListViewFilter("All", "", true, Index = 1)]
    [NavigationItem("Stores")]
    public class Tyre : BaseObject
    {
        public Tyre(Session session) : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }


        decimal price;
        Work work;
        Truck currenttruck;
        TyreStatus status;
        TyreStock tyreStock;
        string code;
        string serialNumber;
        private string brandName;

        [XafDisplayName("Brand")]
        public string BrandName
        {
            get { return brandName; }
            set { SetPropertyValue(nameof(BrandName), ref brandName, value); }
        }

        [XafDisplayName("S/N")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SerialNumber
        {
            get => serialNumber;
            set => SetPropertyValue(nameof(SerialNumber), ref serialNumber, value);
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Code
        {
            get => code;
            set => SetPropertyValue(nameof(Code), ref code, value);
        }

        [VisibleInListView(false)]
        [Association("TyreStock-Tyres")]
        public TyreStock TyreStock
        {
            get => tyreStock;
            set => SetPropertyValue(nameof(TyreStock), ref tyreStock, value);
        }

        public TyreStatus Status
        {
            get => status;
            set => SetPropertyValue(nameof(Status), ref status, value);
        }


        [Association("Truck-Tyres")]
        public Truck CurrentTruck
        {
            get => currenttruck;
            set => SetPropertyValue(nameof(CurrentTruck), ref currenttruck, value);
        }


        [Association("Work-Tyres")]
        public Work Work
        {
            get => work;
            set => SetPropertyValue(nameof(Work), ref work, value);
        }

        
        public decimal Price
        {
            get => price;
            set => SetPropertyValue(nameof(Price), ref price, value);
        }

    }

    public enum TyreStatus
    {
        [ImageName("State_Priority_Low")]
        [XafDisplayName("In Stock")]
        InStock,
        [ImageName("State_Priority_Normal")]
        [XafDisplayName("In Use")]
        InUse,
        [ImageName("State_Priority_High")]
        [XafDisplayName("Damaged")]
        Damaged,
        [ImageName("State_Priority_Highest")]
        [XafDisplayName("Sold")]
        Sold
    }
}