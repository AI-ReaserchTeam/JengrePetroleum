using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.BusinessObjects.Transport
{
   
  
    public class Work : BaseObject
    { 
        public Work(Session session): base(session){ }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            date = DateTime.Now;
        }


        DateTime date;
        string description;
        decimal amount;
        Maintainance maintainance;

        [Association("Maintainance-Works")]
        public Maintainance Maintainance
        {
            get => maintainance;
            set => SetPropertyValue(nameof(Maintainance), ref maintainance, value);
        }

        public decimal Amount
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

        
        public DateTime Date
        {
            get => date;
        }

        [Association("Work-Parts")]
        public XPCollection<SparePart> SpareParts
        {
            get
            {
                return GetCollection<SparePart>(nameof(SpareParts));
            }
        }

        [Association("Work-ServiceParts")]
        public XPCollection<ServicePart> ServiceParts
        {
            get
            {
                return GetCollection<ServicePart>(nameof(ServiceParts));
            }
        }

        [Association("Work-Tyres")]
        public XPCollection<Tyre> Tyres
        {
            get
            {
                return GetCollection<Tyre>(nameof(Tyres));
            }
        }


        [PersistentAlias("ServiceParts.Sum(TotalCost)")]
        public decimal TotalServicePartsCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalServicePartsCost))); }
        }

        [PersistentAlias("ServiceParts.Sum(TotalCost)")]
        public decimal TotalSparePartsCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalSparePartsCost))); }
        }

        [PersistentAlias("TotalSparePartsCost + Amount")]
        public decimal TotalCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalCost))); }
        }




    }
}