using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengerePetroleum.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.BusinessObjects.Transport
{
    [DefaultClassOptions]
    [XafDisplayName("GARAGECASHOUT")]
    [NavigationItem("Transportation")]
    public class GarageCashOut : BaseObject
    { 
        public GarageCashOut(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }



        Employee supervisor;
        decimal? amountPaid;
        DateTime date;

        [RuleRequiredField]
        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), ref date, value);
        }

        [RuleRequiredField]
        public decimal? AmountPaid
        {
            get => amountPaid;
            set => SetPropertyValue(nameof(AmountPaid), ref amountPaid, value);
        }


        [RuleRequiredField]
        [DataSourceCriteria("Position = 'MAINTAINANCESUPERVISOR'")]
        public Employee Supervisor
        {
            get => supervisor;
            set => SetPropertyValue(nameof(Supervisor), ref supervisor, value);
        }


        [PersistentAlias("Expences.Sum(Amount)")]
        public decimal? AmountExpended
        {
           
            get
            {
                if (EvaluateAlias(nameof(AmountExpended)) == null)
                {
                    return 0;
                }
                else
                {
                    return (decimal?)EvaluateAlias(nameof(AmountExpended));
                }
            }
        }


        [PersistentAlias("AmountPaid - AmountExpended")]
        public decimal? Balance
        {
            get => (decimal?)EvaluateAlias(nameof(Balance));
        }

        [Association("GarageCashOut-Expences")]
        public XPCollection<GarageExpence> Expences
        {
            get
            {
                return GetCollection<GarageExpence>(nameof(Expences));
            }
        }




    }
}