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
    //[DefaultClassOptions]
    //[NavigationItem("Stores")]
    public class ServicePartStockItem : UserFriendlyIdPersistentObject
    {
        public ServicePartStockItem(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            IsStocked = false;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (Session.IsNewObject(this) && !IsStocked)
            {
                for (int i = 0; i < Quantity; i++)
                {
                    ServicePart servicePart = new(Session)
                    {
                        Name = Name,
                        Price = SellingPrice,
                        Date = DateTime.Now,
                        Supplier = Stock.Supplier,

                    };

                    servicePart.Save();

                }
                IsStocked = true;
            }
        }

        bool isStocked;
        PartName name;
        ServicePartStock stock;
        decimal sellingPrice;
        decimal costPrice;
        int quantity;


        public PartName Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }


        [XafDisplayName("QTY")]
        public int Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
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

        [VisibleInListView(false), VisibleInDetailView(false)]
        public bool IsStocked
        {
            get => isStocked;
            set => SetPropertyValue(nameof(IsStocked), ref isStocked, value);
        }



        [VisibleInListView(false)]
        [PersistentAlias("Quantity * CostPrice")]
        public decimal TotalCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalCost))); }
        }


        [Association("ServicePartStock-StockItems")]
        public ServicePartStock Stock
        {
            get => stock;
            set => SetPropertyValue(nameof(Stock), ref stock, value);
        }

        [PersistentAlias("Concat('JENG', PadLeft(ToStr(SequentialNumber), 4, '0'))")]
        public string SKU
        {
            get => Convert.ToString(EvaluateAlias(nameof(SKU)));
        }
    }


}
