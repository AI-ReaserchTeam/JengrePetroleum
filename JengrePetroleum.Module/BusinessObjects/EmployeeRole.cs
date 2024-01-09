using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;

namespace JengrePetroleum.Module.BusinessObjects
{
    [ImageName("BO_Role")]
    [DefaultClassOptions]
    public class EmployeeRole : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers
    {
        public EmployeeRole(Session session)
            : base(session)
        {
        }
        [Association("Employees-EmployeeRoles")]
        public XPCollection<EmployeeBase> Employees
        {
            get
            {
                return GetCollection<EmployeeBase>(nameof(Employees));
            }
        }
        IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users
        {
            get { return Employees.OfType<IPermissionPolicyUser>(); }
        }
    }
}
