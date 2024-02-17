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

namespace JengrePetroleum.Module.BusinessObjects.Station
{
    [NavigationItem("Fuel Stations")]
    [DefaultClassOptions]
    [DefaultProperty(nameof(DefaultName))]
    public class FillingStation : BaseObject
    { 
        public FillingStation(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        Employee manager;
        string location;
        string name;


        [Association("Station-Pumps"), DevExpress.Xpo.Aggregated]
        public XPCollection<Pump> Pumps
        {
            get
            {
                return GetCollection<Pump>(nameof(Pumps));
            }
        }

        [Association("Station-DailyExpenses"), DevExpress.Xpo.Aggregated]
        public XPCollection<DailyExpense> DailyExpense
        {
            get
            {
                return GetCollection<DailyExpense>(nameof(DailyExpense));
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Location
        {
            get => location;
            set => SetPropertyValue(nameof(Location), ref location, value);
        }

        [DataSourceCriteria("Position == 'STATIONMANAGER'")]
        public Employee Manager
        {
            get => manager;
            set => SetPropertyValue(nameof(Manager), ref manager, value);
        }

     
        [PersistentAlias("Location + ' - ' + Manager")]
        public string DefaultName
        {
            get
            {
                return (string)EvaluateAlias(nameof(DefaultName));
            }
        }

        [Association("FillingStation-DieselExpenses"), DevExpress.Xpo.Aggregated]
        public XPCollection<DieselExpense> DieselExpenses
        {
            get
            {
                return GetCollection<DieselExpense>(nameof(DieselExpenses));
            }
        }

    }
}