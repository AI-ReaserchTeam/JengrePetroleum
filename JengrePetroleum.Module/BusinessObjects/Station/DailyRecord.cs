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
            date = DateTime.Now;
            
        }


        double price;
        int dipping;
        DateTime date;
        double closingTotalizer;
        double openingTotalizer;
        double closingLitres;
        double openingLitres;
        decimal closingMoney;
        decimal openingMoney;

        [XafDisplayName("O-MONEY")]
        public decimal OpeningMoney
        {
            get => openingMoney;
            set => SetPropertyValue(nameof(OpeningMoney), ref openingMoney, value);
        }

        [XafDisplayName("C-MONEY")]
        public decimal ClosingMoney
        {
            get => closingMoney;
            set => SetPropertyValue(nameof(ClosingMoney), ref closingMoney, value);
        }


        [XafDisplayName("O-LITRES")]

        public double OpeningLitres
        {
            get => openingLitres;
            set => SetPropertyValue(nameof(OpeningLitres), ref openingLitres, value);
        }

        
        //[RuleCriteria("OpeningLitresGreaterThanClosingLitres", DefaultContexts.Save, "Opening litres cannot be greater than closing litres", UsedProperties = "OpeningLitres,ClosingLitres")]
        [XafDisplayName("C-LITRES")]
        public double ClosingLitres
        {
            get => closingLitres;
            set => SetPropertyValue(nameof(ClosingLitres), ref closingLitres, value);
        }

        [XafDisplayName("O-LITRES(A)")]
        public double OpeningTotalizer
        {
            get => openingTotalizer;
            set => SetPropertyValue(nameof(OpeningTotalizer), ref openingTotalizer, value);
        }

        //[RuleFromBoolProperty("OpeningTotalizerGreaterThanClosingTotalizer", DefaultContexts.Save, "Opening totalizer cannot be greater than closing totalizer", UsedProperties = "OpeningTotalizer,ClosingTotalizer")]
        [XafDisplayName("C-LITRES(A)")]
        public double ClosingTotalizer
        {
            get => closingTotalizer;
            set => SetPropertyValue(nameof(ClosingTotalizer), ref closingTotalizer, value);
        }

        [VisibleInListView(false)]
        public DateTime Date
        {
            get => date;
            //set => SetPropertyValue(nameof(Date), ref date, value);
        }

        [XafDisplayName("PRICE/LTR)")]
        public double Price
        {
            get => price;
            set => SetPropertyValue(nameof(Price), ref price, value);
        }

   

        [XafDisplayName("DIP")]
        public int Dipping
        {
            get => dipping;
            set => SetPropertyValue(nameof(Dipping), ref dipping, value);
        }
        [XafDisplayName("TOTAL(A)")]
        [PersistentAlias("ClosingTotalizer - OpeningTotalizer")]
        public double TotalizerAmount
        {
            get { return Convert.ToDouble(EvaluateAlias(nameof(TotalizerAmount))); }
        }



        [XafDisplayName("LITRES")]
        [PersistentAlias("ClosingLitres - OpeningLitres")]
        public double Litres
        {
            get { return Convert.ToDouble(EvaluateAlias(nameof(Litres))); }
        }
        [XafDisplayName("SALES")]
        [PersistentAlias("ClosingMoney - OpeningMoney")]
        public decimal Sales
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(Sales))); }
        }

        
        





    }
}