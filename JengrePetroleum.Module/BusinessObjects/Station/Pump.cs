using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace JengrePetroleum.Module.BusinessObjects.Station
{
    //[NavigationItem("Fuel Stations")]
    //[DefaultClassOptions]
    public class Pump : BaseObject
    { 
        public Pump(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
       

        //[Association("Pump-DailyRecords")]
        //public XPCollection<DailyRecord> DailyRecords
        //{
        //    get
        //    {
        //        return GetCollection<DailyRecord>(nameof(DailyRecords));
        //    }
        //}


        Tank tank;
        string pumpNumber;
        FillingStation station;

        [Association("Station-Pumps")]
        public FillingStation Station
        {
            get => station;
            set => SetPropertyValue(nameof(Station), ref station, value);
        }


        public string PumpNumber
        {
            get => pumpNumber;
            set => SetPropertyValue(nameof(PumpNumber), ref pumpNumber, value);
        }


        public Tank Tank
        {
            get => tank;
            set => SetPropertyValue(nameof(Tank), ref tank, value);
        }

    }
}