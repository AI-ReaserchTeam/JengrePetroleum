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
    [NavigationItem("Fuel Stations")]
    [DefaultClassOptions]
   
    public class DailyRecord : BaseObject
    { 
        public DailyRecord(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
       

        decimal amount;
        double litresSold;
        Pump pump;
        DateTime date;
        double closingReading;
        double openningReading;

        public double OpenningReading
        {
            get => openningReading;
            set => SetPropertyValue(nameof(OpenningReading), ref openningReading, value);
        }



        public double ClosingReading
        {
            get => closingReading;
            set => SetPropertyValue(nameof(ClosingReading), ref closingReading, value);
        }



        public DateTime Date
        {
            get => date;
            //set => SetPropertyValue(nameof(Date), ref date, value);
        }


        [Association("Pump-DailyRecords")]
        public Pump Pump
        {
            get => pump;
            set => SetPropertyValue(nameof(Pump), ref pump, value);
        }


        public double LitresSold
        {
            get => litresSold;
            set => SetPropertyValue(nameof(LitresSold), ref litresSold, value);
        }


        public decimal Amount
        {
            get => amount;
            set => SetPropertyValue(nameof(Amount), ref amount, value);
        }




    }
}