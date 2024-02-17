using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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

namespace JengrePetroleum.Module.BusinessObjects.Marketting
{
    //[DefaultClassOptions]

    public class Payments : BaseObject
    {

        Purchases purchase;
        PaymentType paymentType;
        string account;
        string bank;
        DateTime paymentDate;
        decimal amount;

        public Payments(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

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

        
        [Association("Purchases-Payments")]
        public Purchases Purchase
        {
            get => purchase;
            set => SetPropertyValue(nameof(Purchase), ref purchase, value);
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