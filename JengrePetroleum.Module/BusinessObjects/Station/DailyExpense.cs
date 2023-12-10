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

namespace JengrePetroleum.Module.BusinessObjects.Station
{
   
    public class DailyExpense : BaseObject
    { 
        public DailyExpense(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        

        FillingStation station;
        string item;
        string description;
        double amount;

        public double Amount
        {
            get => amount;
            set => SetPropertyValue(nameof(Amount), ref amount, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(nameof(Description), ref description, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Item
        {
            get => item;
            set => SetPropertyValue(nameof(Item), ref item, value);
        }


        [Association("Station-DailyExpenses")]
        public FillingStation Station
        {
            get => station;
            set => SetPropertyValue(nameof(Station), ref station, value);
        }


    }
}