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
    public class Inventory : BaseObject
    {
        public Inventory(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        string supplier;
        string name;
        decimal price;
        decimal quantity;
        DateTime date;
        string description;
        private XPCollection<AuditDataItemPersistent> changeHistory;


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(nameof(Description), ref description, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), ref date, value);
        }
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public decimal Quantity
        {
            get => quantity;
            set => SetPropertyValue(nameof(Quantity), ref quantity, value);
        }
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public decimal Price
        {
            get => price;
            set => SetPropertyValue(nameof(Price), ref price, value);
        }
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Supplier
        {
            get => supplier;
            set => SetPropertyValue(nameof(Supplier), ref supplier, value);
        }

        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AuditDataItemPersistent> ChangeHistory
        {
            get
            {
                changeHistory ??= AuditedObjectWeakReference.GetAuditTrail(Session, this);
                return changeHistory;
            }
        }



    }

}