using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Store.ServiceParts;
using JengrePetroleum.Module.BusinessObjects.Store.SpareParts;
using JengrePetroleum.Module.BusinessObjects.Store.Tyres;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.BusinessObjects.Store
{

    public class Payment : BaseObject
    {
        public Payment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

       override protected void OnSaving()
        {
            base.OnSaving();
            if (Session.IsNewObject(this))
            {
                if (PaymentType == PaymentType.Cash)
                {
                    bank = "NULL";
                    account = "NULL";
                }
                //else
                //{
                //    bank = this.tyreStock.Supplier.Bank;
                //    account = this.tyreStock.Supplier.AccountNumber;
                //}
            }
        }


        ServicePartStock servicePartStock;
        SparePartStock sparePartStock;
        TyreStock tyreStock;
        PaymentType paymentType;
        string account;
        string bank;
        DateTime paymentDate;
        decimal amount;

        public decimal Amount
        {
            get => amount;
            set => SetPropertyValue(nameof(Amount), ref amount, value);
        }


        public DateTime PaymentDate
        {
            get => paymentDate;
            set => SetPropertyValue(nameof(PaymentDate), ref paymentDate, value);
        }

        //dont show this in the UI if payment type is cash
        [Appearance("HideBankAccount", Criteria = "PaymentType = 0", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Bank
        {
            get => bank;
            set => SetPropertyValue(nameof(Bank), ref bank, value);
        }

        [ModelDefault("EditMask", "0000000000")]
        [Appearance("HideAccount", Criteria = "PaymentType = 0", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Account
        {
            get => account;
            set => SetPropertyValue(nameof(Account), ref account, value);
        }


        public PaymentType PaymentType
        {
            get => paymentType;
            set => SetPropertyValue(nameof(PaymentType), ref paymentType, value);
        }

        [VisibleInListView(false)]
        [Association("TyreStock-Payments")]
        public TyreStock TyreStock
        {
            get => tyreStock;
            set => SetPropertyValue(nameof(TyreStock), ref tyreStock, value);
        }

        [VisibleInListView(false)]
        [Association("SparePartStock-Payments")]
        public SparePartStock SparePartStock
        {
            get => sparePartStock;
            set => SetPropertyValue(nameof(SparePartStock), ref sparePartStock, value);
        }



        [Association("ServicePartStock-Payments")]
        public ServicePartStock ServicePartStock
        {
            get => servicePartStock;
            set => SetPropertyValue(nameof(ServicePartStock), ref servicePartStock, value);
        }



    }

    public enum PaymentType
    {
        [ImageName("BO_Sale")]
        Cash,
        [ImageName("BO_Order")]
        Cheque,
        [ImageName("BO_Invoice")]
        BankTransfer
    }
}