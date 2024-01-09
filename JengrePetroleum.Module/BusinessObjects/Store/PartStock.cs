using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;

namespace JengrePetroleum.Module.BusinessObjects.Store
{
    [DefaultClassOptions]
    [NavigationItem("Stores")]
    [XafDisplayName("SPAREPARTSTOCK")]
    
    public class PartStock : BaseObject
    { 
        public PartStock(Session session): base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

       override protected void OnLoaded()
        {
            base.OnLoaded();
            paid = Balance == 0;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }



        decimal wayBill;
        bool paid;
        Supplier supplier;

        public Supplier Supplier
        {
            get => supplier;
            set => SetPropertyValue(nameof(Supplier), ref supplier, value);
        }


        [VisibleInListView(false)]
        [Association("SparePartStock-StockItems"), DevExpress.Xpo.Aggregated]
        public XPCollection<StockItem> StockItems
        {
            get
            {
                return GetCollection<StockItem>(nameof(StockItems));
            }
        }

        [Association("SparePartStock-Payments"), DevExpress.Xpo.Aggregated]
        public XPCollection<Payment> Payments
        {
            get
            {
                return GetCollection<Payment>(nameof(Payments));
            }
        }

        [ImmediatePostData]
        [CaptionsForBoolValues("YES", "NO")]
        [ImagesForBoolValues("Action_Grant", "Action_Deny")]
        public bool Paid
        {
            get => paid;

        }

        
        public decimal WayBill
        {
            get => wayBill;
            set => SetPropertyValue(nameof(WayBill), ref wayBill, value);
        }


        [VisibleInListView(false)]
        public decimal PayedAmount => Payments.Sum(x => x.Amount);

        public decimal TotalCost => StockItems.Sum(x => x.TotalCost) + WayBill;


        [PersistentAlias("TotalCost - PayedAmount")]
        public decimal Balance
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(Balance)) ?? 0); }
        }

      
    }

}
