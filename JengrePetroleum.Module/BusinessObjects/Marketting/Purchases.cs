using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengrePetroleum.Module.BusinessObjects.Store;
using JengrePetroleum.Module.BusinessObjects.Transport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JengrePetroleum.Module.BusinessObjects.Marketting
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]

    public class Purchases : BaseObject
    {
        public Purchases(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();





        }

        protected override void OnSaving()
        {
            base.OnSaving();

            //if (Session.IsNewObject(this))
            //{
            //    var purchases = Session.Query<Purchases>().Where(x => x.Vendor == Vendor && x.Transported == false && x.Product == this.Product).ToList();
            //    if (purchases.Any())
            //    {

            //        forwardedQTY = purchases.Sum(x => x.RemainingQTY);
            //    }

            //}

        }

        int transportedQuantity;
        int forwardedQTY;
        bool transported;
        bool payed;
        decimal amount;
        int qTY;
        Product product;
        Vendor vendor;

        public Vendor Vendor
        {
            get => vendor;
            set
            {

                SetPropertyValue(nameof(Vendor), ref vendor, value);

                if (Session.IsNewObject(this))
                {
                    var purchases = Session.Query<Purchases>().Where(x => x.Vendor == Vendor && x.Transported == false && x.Product == this.Product).ToList();
                    if (purchases.Any())
                    {

                        forwardedQTY = purchases.Sum(x => x.RemainingQTY);
                    }
                }

            }


        }


        public Product Product
        {
            get => product;
            set => SetPropertyValue(nameof(Product), ref product, value);
        }


        public int QTY
        {
            get => qTY;
            set => SetPropertyValue(nameof(QTY), ref qTY, value);
        }



        public decimal Amount
        {
            get => amount;
            set => SetPropertyValue(nameof(Amount), ref amount, value);
        }


        public int ForwardedQTY
        {
            get => forwardedQTY;
            //set => SetPropertyValue(nameof(ForwardedQTY), ref forwardedQTY, value);
        }

        public bool Payed
        {
            get => payed;
            set => SetPropertyValue(nameof(Payed), ref payed, value);
        }


        public bool Transported
        {
            get => transported;
            set => SetPropertyValue(nameof(Transported), ref transported, value);
        }


        public int TransportedQuantity
        {
            get => transportedQuantity;
            //set => SetPropertyValue(nameof(TransportedQuantity), ref transportedQuantity, value);
        }


        [Association("Purchases-Trips")]
        public XPCollection<Trip> Trips
        {
            get
            {
                return GetCollection<Trip>(nameof(Trips));
            }
        }

        [Association("Purchases-Sales")]
        public XPCollection<Sales> Sales
        {
            get
            {
                return GetCollection<Sales>(nameof(Sales));
            }
        }

        [Association("Purchases-Payments")]
        public XPCollection<Payments> Payments
        {
            get
            {
                return GetCollection<Payments>(nameof(Payments));
            }
        }

        [PersistentAlias("QTY + ForwardedQTY")]
        public int TotalQTY
        {
            get
            {
                return Convert.ToInt32(EvaluateAlias(nameof(TotalQTY)));
            }
        }

        [PersistentAlias("TotalQTY - TransportedQuantity")]
        public int RemainingQTY
        {
            get
            {
                return Convert.ToInt32(EvaluateAlias(nameof(RemainingQTY)));
            }
        }
    }


}