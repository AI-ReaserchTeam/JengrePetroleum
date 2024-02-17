using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using Dotmim.Sync;
using Dotmim.Sync.SqlServer;
using JengrePetroleum.Blazor.Server.Services;

namespace JengrePetroleum.Blazor.Server;

public class JengrePetroleumBlazorApplication : BlazorApplication
{
    public JengrePetroleumBlazorApplication()
    {
        ApplicationName = "JengrePetroleum";
        CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
        DatabaseVersionMismatch += JengrePetroleumBlazorApplication_DatabaseVersionMismatch;
    }
    protected override void OnSetupStarted()
    {
        base.OnSetupStarted();
#if DEBUG
        if (System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
        {
            DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
        }
#endif
    }
    private async void JengrePetroleumBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e)
    {
        //await ProvisionServerAsync();
        //SqlSyncChangeTrackingProvider remoteProvider = new("Data Source=.;Initial Catalog=JengrePetroleum;Integrated Security=True");
        //SqlSyncChangeTrackingProvider localProvider  = new("Data Source=.;Initial Catalog=JengrePetroleum;Integrated Security=True");

       
        


        e.Updater.Update();
        e.Handled = true;

#if EASYTEST
        e.Updater.Update();
        e.Handled = true;
#else
        if (System.Diagnostics.Debugger.IsAttached)
        {
            e.Updater.Update();
            e.Handled = true;
        }
        else
        {
            string message = "The application cannot connect to the specified database, " +
                "because the database doesn't exist, its version is older " +
                "than that of the application or its schema does not match " +
                "the ORM data model structure. To avoid this error, use one " +
                "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

            if (e.CompatibilityError != null && e.CompatibilityError.Exception != null)
            {
                message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
            }
            throw new InvalidOperationException(message);
        }
#endif

         async Task ProvisionServerAsync()
        {
            var serverProvider = new SqlSyncChangeTrackingProvider("Data Source=.;Initial Catalog=JengrePetroleum;Integrated Security=True");
            var remoteOrchestrator = new RemoteOrchestrator(serverProvider);
            var syncSetup = await remoteOrchestrator.GetAllTablesAsync();
            var tables = (IEnumerable<string>)syncSetup.Tables;
            var setup = new SyncSetup(tables);


            await remoteOrchestrator.ProvisionAsync(setup);
        }

        async Task ProvisionClientAsync()
        {
            var clientProvider = new SqlSyncChangeTrackingProvider("Data Source=.;Initial Catalog=JengrePetroleum;Integrated Security=True");
            var serverProvider = new SqlSyncChangeTrackingProvider("Data Source=.;Initial Catalog=JengrePetroleum;Integrated Security=True");

            var remoteOrchestrator = new RemoteOrchestrator(serverProvider);
            var localOrchestrator = new LocalOrchestrator(clientProvider);
            
            var serverScope = await remoteOrchestrator.GetScopeInfoAsync();
            await localOrchestrator.ProvisionAsync(serverScope);

        }



    }
}
