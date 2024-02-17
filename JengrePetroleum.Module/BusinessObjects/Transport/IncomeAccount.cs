using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Station;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.BusinessObjects.Transport
{
    [DefaultClassOptions]
    [NavigationItem("Transportation")]
    public class IncomeAccount : BaseObject
    { 
        public IncomeAccount(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }


        decimal previousBalance;
        PaymentType paymentType;
        FillingStation recievedFrom;
        decimal amountRecieved;
        DateTime date;

        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), ref date, value);
        }


        public decimal PreviousBalance
        {
            get => previousBalance;
            set => SetPropertyValue(nameof(PreviousBalance), ref previousBalance, value);
        }



        public decimal AmountRecieved
        {
            get => amountRecieved;
            set => SetPropertyValue(nameof(AmountRecieved), ref amountRecieved, value);
        }


        [PersistentAlias("PreviousBalance + AmountRecieved")]
        public decimal CurrentBalance
        {
            get => (decimal)EvaluateAlias(nameof(CurrentBalance));
        }


        public FillingStation RecievedFrom
        {
            get => recievedFrom;
            set => SetPropertyValue(nameof(RecievedFrom), ref recievedFrom, value);
        }

        
        public PaymentType PaymentType
        {
            get => paymentType;
            set => SetPropertyValue(nameof(PaymentType), ref paymentType, value);
        }



    }

    public enum PaymentType
    {
        Cash,
        Cheque,
        BankTransfer
    }
}