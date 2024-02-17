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

namespace JengrePetroleum.Module.BusinessObjects
{
    //[DefaultClassOptions]
 
    [DefaultProperty(nameof(BVN))]
    public class BankDetails : BaseObject
    { 
        public BankDetails(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        EmployeeBase user;
        string accountNumber;
        string bVN;
        string bank;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Bank
        {
            get => bank;
            set => SetPropertyValue(nameof(Bank), ref bank, value);
        }

        [ModelDefault("EditMask", "00000000000")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string BVN
        {
            get => bVN;
            set => SetPropertyValue(nameof(BVN), ref bVN, value);
        }

        [ModelDefault("EditMask", "0000000000")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string AccountNumber
        {
            get => accountNumber;
            set => SetPropertyValue(nameof(AccountNumber), ref accountNumber, value);
        }

        [Association("EmployeeBase-Bank")]
        public EmployeeBase User
        {
            get => user;
            set => SetPropertyValue(nameof(User), ref user, value);
        }
    }
}