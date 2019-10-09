using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DimWin
{
    [RunInstaller(true)]
    public class DimWinInstaller : Installer
    {
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            Process.Start(Context.Parameters["AssemblyPath"], "runonstartup");
        }
    }
}
