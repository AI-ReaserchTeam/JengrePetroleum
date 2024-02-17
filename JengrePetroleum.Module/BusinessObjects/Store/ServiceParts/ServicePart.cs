using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Transport;

namespace JengrePetroleum.Module.BusinessObjects.Store.ServiceParts
{
    [DefaultClassOptions]
    [NavigationItem("Stores")]
    [XafDisplayName("SERVICEPART")]
    [ListViewFilter("All", "")]
    [ListViewFilter("Stocked", "[Status] = 'Instock'", true, Index = 0)]
    [ListViewFilter("In Use", "[Status] = ' InUse'", true, Index = 1)]
    public class ServicePart : Part
    {
        public ServicePart(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }


        Work work;

        [Association("Work-ServiceParts")]
        public Work Work
        {
            get => work;
            set => SetPropertyValue(nameof(Work), ref work, value);
        }

        [PersistentAlias("Concat('JENST', PadLeft(ToStr(SequentialNumber), 6, '0'))")]
        public string PartNumber
        {
            get => Convert.ToString(EvaluateAlias(nameof(PartNumber)));
        }

    }
}