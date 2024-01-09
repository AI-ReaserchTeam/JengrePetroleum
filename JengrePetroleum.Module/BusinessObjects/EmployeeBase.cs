using DevExpress.DashboardExport.Map;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.AuditTrail;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using JengerePetroleum.Module.BusinessObjects;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JengrePetroleum.Module.BusinessObjects
{
    //[XafDisplayName("EmployeeBase")]
    //[NavigationItem("Employees")]
    [ImageName("BO_Person")]
    [CurrentUserDisplayImage("Photo")]
    [ListViewFilter("Active", "[Status] = 'Active'", true, Index = 1)]
    [ListViewFilter("Resigned", "[Status] = 'Resigned'", true, Index = 2)]
    [ListViewFilter("Deceased", "[Status] = 'Deceased'", true, Index = 3)]
    [ListViewFilter("Dismissed", "[Status] = 'Dismissed'", true, Index = 4)]
    [ListViewFilter("Leave", "[Status] = 'OnLeave'", true, Index = 5)]
    [ListViewFilter("All", "", true, Index = 0)]
    public class EmployeeBase : BaseObject, ISecurityUser, IAuthenticationStandardUser, IAuthenticationActiveDirectoryUser, ISecurityUserWithRoles, IPermissionPolicyUser, ICanInitialize, ISecurityUserWithLoginInfo
    {
        public EmployeeBase(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.ChangePasswordOnFirstLogon = true;

        }

        Status status;
        Position? position;
        Gender gender;
        string phoneContact;
        DateTime birthDate;
        Department department;
        string address;
        string email;
        string lastName;
        string firstName;

        [RuleRequiredField]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                SetPropertyValue(nameof(FirstName), ref firstName, value);
            }
        }

        [RuleRequiredField]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string LastName
        {
            get => lastName;
            set => SetPropertyValue(nameof(LastName), ref lastName, value);
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleUniqueValue]
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Address
        {
            get => address;
            set => SetPropertyValue(nameof(Address), ref address, value);
        }

        [RuleRequiredField]
        [ModelDefault("EditMask", "0000 000 0000")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string PhoneContact
        {
            get => phoneContact;
            set => SetPropertyValue(nameof(PhoneContact), ref phoneContact, value);
        }

        public Department Department
        {
            get => department;
            set => SetPropertyValue(nameof(Department), ref department, value);
        }

        [VisibleInListView(false)]
        public DateTime BirthDate
        {
            get => birthDate;
            set => SetPropertyValue(nameof(BirthDate), ref birthDate, value);
        }

        [Required]
        public Gender Gender
        {
            get => gender;
            set => SetPropertyValue(nameof(Gender), ref gender, value);
        }
        [VisibleInListView(false)]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }



        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
            DetailViewImageEditorMode = ImageEditorMode.PictureEdit,
            DetailViewImageEditorFixedHeight = 250, DetailViewImageEditorFixedWidth = 250)]
        [VisibleInListView(false)]
        [Delayed(true)]
        public byte[] Photo
        {
            get { return GetDelayedPropertyValue<byte[]>(nameof(Photo)); ; }
            set { SetDelayedPropertyValue(nameof(Photo), value); }
        }

        [RuleRequiredField]
        public Position? Position
        {
            get => position;
            set => SetPropertyValue(nameof(Position), ref position, value);
        }


        public Status Status
        {
            get => status;
            set => SetPropertyValue(nameof(Status), ref status, value);
        }

      


       

        [Association("EmployeeBase-Bank")]
        public XPCollection<BankDetails> Bank
        {
            get
            {
                return GetCollection<BankDetails>(nameof(Bank));
            }
        }

        #region ISecurityUser Members
        private bool isActive = true;
        [CaptionsForBoolValues("YES", "NO")]
        public bool IsActive
        {
            get { return isActive; }
            set { SetPropertyValue(nameof(IsActive), ref isActive, value); }
        }
        private string userName = String.Empty;

        [XafDisplayName("Nick Name")]
        [RuleRequiredField("EmployeeUserNameRequired", DefaultContexts.Save)]
        [RuleUniqueValue("EmployeeUserNameIsUnique", DefaultContexts.Save, "The login with the entered user name was already registered within the system.")]
        public string UserName
        {
            get { return userName; }
            set { SetPropertyValue(nameof(UserName), ref userName, value); }
        }
        #endregion

        #region IAuthenticationStandardUser Members
        private bool changePasswordOnFirstLogon;


        [VisibleInListView(false)]
        [CaptionsForBoolValues("YES", "NO")]
        public bool ChangePasswordOnFirstLogon
        {
            get { return changePasswordOnFirstLogon; }
            set
            {
                SetPropertyValue(nameof(ChangePasswordOnFirstLogon), ref changePasswordOnFirstLogon, value);
            }
        }
        private string storedPassword;
        [Browsable(false), Size(SizeAttribute.Unlimited), Persistent, SecurityBrowsable]
        protected string StoredPassword
        {
            get { return storedPassword; }
            set { storedPassword = value; }
        }
        public bool ComparePassword(string password)
        {
            return PasswordCryptographer.VerifyHashedPasswordDelegate(this.storedPassword, password);
        }
        public void SetPassword(string password)
        {
            this.storedPassword = PasswordCryptographer.HashPasswordDelegate(password);
            OnChanged(nameof(StoredPassword));
        }
        #endregion

        #region ISecurityUserWithRoles Members
        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get
            {
                IList<ISecurityRole> result = new List<ISecurityRole>();
                foreach (EmployeeRole role in EmployeeRoles)
                {
                    result.Add(role);
                }
                return result;
            }
        }

        [Association("Employees-EmployeeRoles")]
        //[RuleRequiredField("EmployeeRoleIsRequired", DefaultContexts.Save,
        //	TargetCriteria = "IsActive",
        //	CustomMessageTemplate = "An active employee must have at least one role assigned")]
        public XPCollection<EmployeeRole> EmployeeRoles
        {
            get
            {
                return GetCollection<EmployeeRole>(nameof(EmployeeRoles));
            }
        }
        #endregion

        #region IPermissionPolicyUser Members
        IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles
        {
            get { return EmployeeRoles.OfType<IPermissionPolicyRole>(); }
        }
        #endregion

        #region ICanInitialize Members
        void ICanInitialize.Initialize(IObjectSpace objectSpace, SecurityStrategyComplex security)
        {
            EmployeeRole newUserRole = (EmployeeRole)objectSpace.FirstOrDefault<EmployeeRole>(role => role.Name == security.NewUserRoleName);
            if (newUserRole == null)
            {
                newUserRole = objectSpace.CreateObject<EmployeeRole>();
                newUserRole.Name = security.NewUserRoleName;
                newUserRole.IsAdministrative = true;
                newUserRole.Employees.Add(this);
            }
        }
        #endregion
        #region ISecurityUserWithLoginInfo Members


        [Browsable(false)]
        [DevExpress.Xpo.Aggregated, Association("User-LoginInfo")]
        public XPCollection<EmployeeLoginInfo> LoginInfo
        {
            get { return GetCollection<EmployeeLoginInfo>(nameof(LoginInfo)); }
        }

        IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

        ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey)
        {
            EmployeeLoginInfo result = new EmployeeLoginInfo(Session);
            result.LoginProviderName = loginProviderName;
            result.ProviderUserKey = providerUserKey;
            result.User = this;
            return result;
        }
        #endregion
    }


    public enum Department
    {
        TRANSPORTATION,
        ACCOUNTING,
        ADMINISTRATION,
        MARKETING,
        MANAGEMENT
    }

    public enum Gender
    {
        [ImageName("male")]

        MALE,
        [ImageName("female")]
        FEMALE
    }


    public enum Position
    {
        Employee,
        DieselClerk,
        StationManager,
        MaintainanceManager,
        TripClerk,
        TruckClerk,
        EmployeeClerk,
        StoreKeeper,
        TransportAccountant,
        StationAccountant,
        TransportManager,
        Auditor,
        Driver
    }

    //status of the employee enum
    public enum Status
    {
        [ImageName("Action_Grant")]
        Active,
        [ImageName("Action_Deny")]
        Resigned,
        [ImageName("Action_Delete")]
        Deceased,
        [ImageName("Action_Close")]
        Dismissed,
        [ImageName("Action_Grant")]
        OnLeave,

        OnTrip
    }

  

}
