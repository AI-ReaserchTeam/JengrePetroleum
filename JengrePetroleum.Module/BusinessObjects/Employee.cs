using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects;
using JengrePetroleum.Module.BusinessObjects.Transport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengerePetroleum.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("FullName")]
	[NavigationItem("Employees")]
	public class Employee : EmployeeBase
	{

		public Employee(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();
			
		}

		
		[Appearance("MaintainanceManager", Enabled = false, Criteria = "Position != '3'", Context = "DetailView")]
        [Association("Employee-MaintainanceManager")]
        public XPCollection<Maintainance> Maintainace
        {
            get
            {
                return GetCollection<Maintainance>(nameof(Maintainace));
            }
        }

    }
}