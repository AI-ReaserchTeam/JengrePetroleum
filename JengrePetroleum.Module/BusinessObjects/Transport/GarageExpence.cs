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
    //[DefaultClassOptions]
    //[NavigationItem("Transportation")]
    public class GarageExpence : BaseObject
    { 
        public GarageExpence(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        protected override void OnSaving()
        {
            base.OnSaved();

            if (Session.IsNewObject(this))
            {
                Session.GetObjectByKey<Work>(MaintainanceWork.Oid).Amount = Amount;
                Session.Save(Session.GetObjectByKey<Work>(MaintainanceWork.Oid));

            }

        }


        GarageCashOut cash;
        decimal amount;
        Work maintainanceWork;
        Maintainance maintainance;
        Employee supervisor;


        [DataSourceCriteria("Position = 'MAINTAINANCESUPERVISOR'")]
        public Employee Supervisor
        {
            get => supervisor;
            set => SetPropertyValue(nameof(Supervisor), ref supervisor, value);
        }


        public Maintainance Maintainance
        {
            get => maintainance;
            set => SetPropertyValue(nameof(Maintainance), ref maintainance, value);
        }


        [DataSourceProperty("Maintainance.Works")]
        public Work MaintainanceWork
        {
            get => maintainanceWork;
            set => SetPropertyValue(nameof(MaintainanceWork), ref maintainanceWork, value);
        }


        public decimal Amount
        {
            get => amount;
            set => SetPropertyValue(nameof(Amount), ref amount, value);
        }


        [Association("GarageCashOut-Expences")]
        public GarageCashOut Cash
        {
            get => cash;
            set => SetPropertyValue(nameof(Cash), ref cash, value);
        }

    }
}