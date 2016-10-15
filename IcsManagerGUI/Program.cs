using IcsManagerLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace IcsManagerGUI
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{6AA14EC7-49AF-4019-887A-C8A78F4B213A}");

        static void PrintNICInfo(NetworkInterface nic)
        {
            Console.WriteLine("Name: {0}", nic.Name);
            Console.WriteLine("Description: {0}", nic.Description);
            Console.WriteLine("GUID: {0}", nic.Id);
            Console.WriteLine("Status: {0}", nic.OperationalStatus);
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                var ipprops = nic.GetIPProperties();
                foreach (var a in ipprops.UnicastAddresses)
                {
                    if (a.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Console.WriteLine("Address: {0}/{1}", a.Address, a.IPv4Mask);
                    }
                }
                foreach (var a in ipprops.GatewayAddresses)
                {
                    if (a.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Console.WriteLine("Gateway ....... : {0}", a.Address);
                    }
                }
            }
        }
        static void Info()
        {
            foreach (NetworkInterface nic in IcsManager.GetAllIPv4Interfaces())
            {
                PrintNICInfo(nic);
                Console.WriteLine();
            }
            Status();
        }
        static bool Status()
        {
            NetworkInterface shared = null;
            NetworkInterface home = null;
            bool sharing = false;
            foreach (NetworkInterface nic in IcsManager.GetAllIPv4Interfaces())
            {
                var connection = IcsManager.GetConnectionById(nic.Id);
                if (connection != null)
                {
                    var props = IcsManager.GetProperties(connection);
                    var sc = IcsManager.GetConfiguration(connection);
                    if (sc.SharingEnabled)
                    {
                        if (sc.SharingConnectionType == NETCONLib.tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE)
                        {
                            home = nic;
                            sharing = true;
                        }
                        else if (sc.SharingConnectionType == NETCONLib.tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC)
                        {
                            shared = nic;
                            sharing = true;
                        }
                    }
                }
            }
            if (!sharing)
            {
                Console.WriteLine("ICS is DISABLED");
            } else
            {
                Console.WriteLine("ICS is ENABLED");
                Console.WriteLine("*** SHARED connection: ");
                PrintNICInfo(shared);
                Console.WriteLine("*** HOME connection: ");
                PrintNICInfo(home);
            }
            return sharing;
        }
        static bool EnableICS(string shared_uuid_or_name, string home_uuid_or_name)
        {
            var connectionToShare = IcsManager.FindConnectionByIdOrName(shared_uuid_or_name);
            if (connectionToShare == null)
            {
                throw new Exception(string.Format("Connection not found: {0}", shared_uuid_or_name));
            }
            var homeConnection = IcsManager.FindConnectionByIdOrName(home_uuid_or_name);
            if (homeConnection == null)
            {
                throw new Exception(string.Format("Connection not found: {0}", home_uuid_or_name));
            }

            var currentShare = IcsManager.GetCurrentlySharedConnections();
            if (currentShare.Exists)
            {
                Console.WriteLine("Internet Connection Sharing is already enabled:");
                Console.WriteLine(currentShare);
                Console.WriteLine("Sharing will be disabled first.");
            }
            IcsManager.ShareConnection(connectionToShare, homeConnection);
            return Status();
        }
        static bool DisableICS()
        {
            var currentShare = IcsManager.GetCurrentlySharedConnections();
            if (currentShare.Exists)
            {
                IcsManager.ShareConnection(null, null);
            }
            return !Status();
        }
        static void Usage()
        {
            var appName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
            appName = appName == null ? "" : appName.ToLower();
            Console.WriteLine("Usage: ");
            Console.WriteLine("  {0} info", appName);
            Console.WriteLine("  {0} status", appName);
            Console.WriteLine("  {0} enable {{GUID-OF-CONNECTION-TO-SHARE}} {{GUID-OF-HOME-CONNECTION}} [force]", appName);
            Console.WriteLine("  {0} enable \"Name of connection to share\" \"Name of home connection\" [force]", appName);
            Console.WriteLine("  {0} disable", appName);
            Console.WriteLine("  {0} --help", appName);
        }

        [STAThread]
        static int Main(string[] args)
        {
            bool showgui = true;
            int result = 0;

            if (args.Length > 0)
            {
                try
                {
                    string command = args[0];
                    if (command == "gui")
                    {
                        showgui = true;
                    } else
                    {
                        showgui = false;
                        if (command == "--help")
                        {
                            Usage();
                            return 0;
                        }
                        else if (command == "info")
                        {
                            Info();
                            return 0;
                        }
                        else if (command == "status")
                        {
                            return Status() ? 0 : 1;
                        }
                        else if (command == "enable")
                        {
                            try
                            {
                                var shared = args[1];
                                var home = args[2];
                                result = EnableICS(shared, home) ? 0 : 1;
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Usage();
                                return 2;
                            }
                        }
                        else if (command == "disable")
                        {
                            result = DisableICS() ? 0 : 1;
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("This operation requires elevation.");
                    return 2;
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine("This feature is not supported on your operating system.");
                    return 2;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 2;
                }
            }
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    if (showgui)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        IcsManagerForm form = new IcsManagerForm();
                        Application.Run(form);
                    }
                } finally
                {
                    mutex.ReleaseMutex();
                }
            } else
            {                
                NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST,NativeMethods.WM_SHOWME,IntPtr.Zero,IntPtr.Zero);
            }
            return result;
        }
    }
}
