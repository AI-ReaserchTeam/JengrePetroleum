using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Store.ServiceParts;
using JengrePetroleum.Module.BusinessObjects.Store.SpareParts;
using JengrePetroleum.Module.BusinessObjects.Store.Tyres;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.BusinessObjects.Transport
{

    [Appearance("DisableFields", TargetItems = "*", Criteria = "Approval == 'Approved'", Enabled = false)]
    public class Work : BaseObject
    { 
        public Work(Session session): base(session){ }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            date = DateTime.Now;
            approval = ApprovalStatus.Pending;
        }



        ApprovalStatus approval;
        DateTime date;
        string description;
        decimal amount;
        Maintainance maintainance;

        [RuleRequiredField]
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

        [RuleRequiredField]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(nameof(Description), ref description, value);
        }

        [RuleRequiredField]
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

        
        public ApprovalStatus Approval
        {
            get => approval;
            set => SetPropertyValue(nameof(Approval), ref approval, value);
        }


        [PersistentAlias("Tyres.Sum(Price)")]
        public decimal TotalTyresPrice
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalTyresPrice))); }
        }

        [PersistentAlias("ServiceParts.Sum(Price)")]
        public decimal TotalServicePartsCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalServicePartsCost))); }
        }

        [PersistentAlias("SpareParts.Sum(Price)")]
        public decimal TotalSparePartsCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalSparePartsCost))); }
        }

        [PersistentAlias("TotalSparePartsCost + TotalServicePartsCost + TotalTyresPrice + Amount")]
        public decimal TotalCost
        {
            get { return Convert.ToDecimal(EvaluateAlias(nameof(TotalCost))); }
        }

    }

    public enum ApprovalStatus
    {

        [XafDisplayName("Approved")]
        Approved,
        [XafDisplayName("Not Approved")]
        Rejected,
        [XafDisplayName("Pending")]
        Pending
    }
}