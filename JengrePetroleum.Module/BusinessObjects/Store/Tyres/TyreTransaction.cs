using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Transport;

namespace JengrePetroleum.Module.BusinessObjects.Store.Tyres
{
    [DefaultClassOptions]
    [NavigationItem("Stores")]
    [XafDisplayName("NEWTYRETO")]
    public class TyreTransaction : BaseObject
    {
        public TyreTransaction(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Tyres ??= new XPCollection<Tyre>(Session);
            tyres.LoadingEnabled = false;
            transactionDate = DateTime.Now;
            status = TyreTransactionStatus.Pending;
        }




        TyreTransactionStatus status;
        DateTime transactionDate;
        Truck truck;
        Driver driver;
        XPCollection<Tyre> tyres;



        [RuleRequiredField]
        public XPCollection<Tyre> Tyres
        {
            get => tyres;
            set => SetPropertyValue(nameof(Tyres), ref tyres, value);
        }

        [RuleRequiredField]
        public Driver Driver
        {
            get => driver;
            set => SetPropertyValue(nameof(Driver), ref driver, value);
        }

        [RuleRequiredField]
        public Truck Truck
        {
            get => truck;
            set => SetPropertyValue(nameof(Truck), ref truck, value);
        }


        public DateTime TransactionDate
        {
            get => transactionDate;
            //set => SetPropertyValue(nameof(TransactionDate), ref transactionDate, value);
        }


        public TyreTransactionStatus Status
        {
            get => status;
            //set => SetPropertyValue(nameof(Status), ref status, value);
        }

    }

    public enum TyreTransactionStatus
    {
        [ImageName("State_Priority_Low")]
        [XafDisplayName("Pending")]
        Pending,
        [ImageName("State_Priority_Normal")]
        [XafDisplayName("Completed")]
        Completed,
        [ImageName("State_Priority_High")]
        [XafDisplayName("Cancelled")]
        Cancelled
    }

}