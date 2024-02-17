using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Store.SpareParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.BusinessObjects.Store.ServiceParts
{
    [DefaultClassOptions]
    [NavigationItem("Stores")]
    [XafDisplayName("SERVICEPARTSTOCK")]
    public class ServicePartStock : BaseObject
    { 
        public ServicePartStock(Session session): base(session) {}
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        decimal wayBill;
        bool paid;
        Supplier supplier;



        [VisibleInListView(false)]
        [Association("ServicePartStock-StockItems"), DevExpress.Xpo.Aggregated]
        public XPCollection<ServicePartStockItem> StockItems
        {
            get
            {
                return GetCollection<ServicePartStockItem>(nameof(StockItems));
            }
        }

        public Supplier Supplier
        {
            get => supplier;
            set => SetPropertyValue(nameof(Supplier), ref supplier, value);
        }


        [Association("ServicePartStock-Payments"), DevExpress.Xpo.Aggregated]
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