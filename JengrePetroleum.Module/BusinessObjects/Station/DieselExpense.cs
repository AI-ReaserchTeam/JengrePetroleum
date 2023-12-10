using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Transport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.BusinessObjects.Station
{
    [DefaultClassOptions]
    
    public class DieselExpense : BaseObject
    { 
        public DieselExpense(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        FillingStation station;
        decimal tripAllowance;
        string truck;
        string driver;
        double price;
        double diesel;
        DateTime date;
        Trip trip;

        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), ref date, value);
        }


        public double Diesel
        {
            get => diesel;
            set => SetPropertyValue(nameof(Diesel), ref diesel, value);
        }


        public double Price
        {
            get => price;
            set => SetPropertyValue(nameof(Price), ref price, value);
        }

        
        public decimal TripAllowance
        {
            get => tripAllowance;
            set => SetPropertyValue(nameof(TripAllowance), ref tripAllowance, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Driver
        {
            get => driver;
            set => SetPropertyValue(nameof(Driver), ref driver, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Truck
        {
            get => truck;
            set => SetPropertyValue(nameof(Truck), ref truck, value);
        }


        public Trip Trip
        {
            get => trip;
            set => SetPropertyValue(nameof(Trip), ref trip, value);
        }

        
        [Association("FillingStation-DieselExpenses")]
        public FillingStation Station
        {
            get => station;
            set => SetPropertyValue(nameof(Station), ref station, value);
        }


      
    }
}