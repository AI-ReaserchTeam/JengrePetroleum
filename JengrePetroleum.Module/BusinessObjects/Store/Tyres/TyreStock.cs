using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.BusinessObjects.Store.Tyres
{
    [DefaultClassOptions]
    [NavigationItem("Stores")]
    [XafDisplayName("TYRESTOCK")]
    public class TyreStock : BaseObject
    {
        public TyreStock(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            paid = Balance == 0;
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            if (Session.IsNewObject(this) && !isTyreAdded)
            {
                for (int i = 0; i < Quantity; i++)
                {
                    Tyre tyre = new(Session)
                    {
                        TyreStock = this,
                        BrandName = Brand,
                        Price = sellingPrice

                    };
                    Tyres.Add(tyre);
                    isTyreAdded = true;
                }
            }
        }


        bool isTyreAdded;
        bool paid;

        decimal sellingPrice;
        decimal costPrice;
        Supplier supplier;
        string brand;
        int quantity;

        [XafDisplayName("QTY")]
        public int Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Brand
        {
            get => brand;
            set => SetPropertyValue(nameof(Brand), ref brand, value);
        }

        public Supplier Supplier
        {
            get => supplier;
            set => SetPropertyValue(nameof(Supplier), ref supplier, value);
        }

        [XafDisplayName("C.PRICE")]
        public decimal CostPrice
        {
            get => costPrice;
            set => SetPropertyValue(nameof(CostPrice), ref costPrice, value);
        }

        [XafDisplayName("S.PRICE")]
        public decimal SellingPrice
        {
            get => sellingPrice;
            set => SetPropertyValue(nameof(SellingPrice), ref sellingPrice, value);
        }

        [Association("TyreStock-Tyres")]
        public XPCollection<Tyre> Tyres
        {
            get
            {
                return GetCollection<Tyre>(nameof(Tyres));
            }
        }

        [Association("TyreStock-Payments"), DevExpress.Xpo.Aggregated]
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

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public bool IsTyreAdded
        {
            get
            {
                return isTyreAdded;
            }
            //set
            //{
            //    SetPropertyValue(nameof(IsTyreAdded), ref isTyreAdded, value);
            //}
        }


        [VisibleInListView(false)]
        public decimal PayedAmount => Payments.Sum(x => x.Amount);

        [VisibleInListView(false)]
        [PersistentAlias("Quantity * CostPrice")]
        public decimal TotalCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalCost))); }
        }

        [VisibleInListView(false)]
        [PersistentAlias("Quantity * SellingPrice")]
        public decimal TotalSellingPrice
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalSellingPrice))); }
        }

        [PersistentAlias("TotalSellingPrice - TotalCost")]
        public decimal Profit
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(Profit))); }
        }
        [PersistentAlias("TotalCost - PayedAmount")]
        public decimal Balance
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(Balance)) ?? 0); }
        }

    }
}