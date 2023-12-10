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
using System.Xml.Linq;

namespace JengrePetroleum.Module.BusinessObjects.Transport
{

    [ImageName("BO_FileAttachment")]
    public class TruckFiles : FileAttachmentBase
    { 
        public TruckFiles(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            documentType = DocumentType.Unknown;
            dateUploaded = DateTime.Now;
            
        }


        Driver driver;
        DateTime dateUploaded;
        DateTime expirationDate;
        string name;
        Truck truck;

        [Association("Truck-TruckFiles")]
        public Truck Truck
        {
            get => truck;
            set => SetPropertyValue(nameof(Truck), ref truck, value);
        }

       
        [Association("Driver-TruckFiles")]
        public Driver Driver
        {
            get => driver;
            set => SetPropertyValue(nameof(Driver), ref driver, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }



        public DateTime ExpirationDate
        {
            get => expirationDate;
            set => SetPropertyValue(nameof(ExpirationDate), ref expirationDate, value);
        }

        
        public DateTime DateUploaded
        {
            get => dateUploaded;
            //set => SetPropertyValue(nameof(DateUploaded), ref dateUploaded, value);
        }



        private DocumentType documentType;
        public DocumentType DocumentType
        {
            get
            {
                return documentType;
            }
            set
            {
                SetPropertyValue<DocumentType>(nameof(DocumentType), ref documentType, value);
            }
        }

    }


    public enum DocumentType
    {
        Licence,
        Caliberation,
        Unknown
    };
}