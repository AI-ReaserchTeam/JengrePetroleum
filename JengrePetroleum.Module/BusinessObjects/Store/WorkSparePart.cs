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

namespace JengrePetroleum.Module.BusinessObjects.Store
{
    
    public class WorkSparePart : BaseObject
    { 
        public WorkSparePart(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }


        decimal unitPrice;
        int quantity;
        string spartPart;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SpartPart
        {
            get => spartPart;
            set => SetPropertyValue(nameof(SpartPart), ref spartPart, value);
        }


        public int Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }

        
        public decimal UnitPrice
        {
            get => unitPrice;
            set => SetPropertyValue(nameof(UnitPrice), ref unitPrice, value);
        }

        [PersistentAlias("Quantity * UnitPrice")]
        public decimal TotalCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalCost))); }
        }
        
        Work work;

        [Association("Work-WorkSpareParts")]
        public Work Work
        {
            get => work;
            set => SetPropertyValue(nameof(Work), ref work, value);
        }

    }
}