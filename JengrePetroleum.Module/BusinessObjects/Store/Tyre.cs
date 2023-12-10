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
	[DefaultClassOptions]
	[NavigationItem("Stores")]
	public class Tyre : Inventory
	{
		public Tyre(Session session)
			: base(session)
		{
		}
		public override void AfterConstruction()
		{
			base.AfterConstruction();

		}


		private string brandName;
		public string BrandName
		{
			get { return brandName; }
			set { SetPropertyValue(nameof(BrandName), ref brandName, value); }
		}

    }
}