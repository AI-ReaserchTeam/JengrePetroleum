using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Pdf.Native.BouncyCastle.Utilities;
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
    public class Part : UserFriendlyIdPersistentObject
    {
        public Part(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        Status status;
        Supplier supplier;
        PartName name;
        decimal price;
        DateTime date;
        private XPCollection<AuditDataItemPersistent> changeHistory;


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public PartName Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public DateTime Date
        {
            get => date;
            set => SetPropertyValue(nameof(Date), ref date, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public decimal Price
        {
            get => price;
            set => SetPropertyValue(nameof(Price), ref price, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public Supplier Supplier
        {
            get => supplier;
            set => SetPropertyValue(nameof(Supplier), ref supplier, value);
        }

        
        public Status Status
        {
            get => status;
            set => SetPropertyValue(nameof(Status), ref status, value);
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

    public enum Status
    {
        Instock,
        InUse
    }

}