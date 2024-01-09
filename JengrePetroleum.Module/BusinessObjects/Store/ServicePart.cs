using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Transport;

namespace JengrePetroleum.Module.BusinessObjects.Store
{
    [DefaultClassOptions]
    [NavigationItem("Stores")]
    [XafDisplayName("SERVICEPART")]
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