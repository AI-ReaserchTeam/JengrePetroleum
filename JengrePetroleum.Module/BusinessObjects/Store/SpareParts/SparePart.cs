using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
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

namespace JengrePetroleum.Module.BusinessObjects.Store.SpareParts
{
    [DefaultClassOptions]
    [NavigationItem("Stores")]
    [XafDisplayName("SPAREPART")]
    [ListViewFilter("All", "")]
    [ListViewFilter("In Use", "[Status] = ' InUse'", true, Index = 1)]
    [ListViewFilter("Stocked", "[Status] = 'Instock'", true, Index = 0)]
    public class SparePart : Part
    {
        public SparePart(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        Work work;


        [VisibleInListView(false)]
        [Association("Work-Parts")]
        public Work Work
        {
            get => work;
            set => SetPropertyValue(nameof(Work), ref work, value);
        }

        [PersistentAlias("Concat('JENSP', PadLeft(ToStr(SequentialNumber), 6, '0'))")]
        public string PartNumber
        {
            get => Convert.ToString(EvaluateAlias(nameof(PartNumber)));
        }


    }
}