
using System;
using AppScan;
using AppScan.Extensions;
using System.Threading;
using MultiSTPExt.Properties;
using System.Windows.Forms;
using System.Linq;

namespace MultiSTPExt
{
    public class MultiSTPExt : IExtensionLogic
    {
        public ExtensionVersionInfo GetUpdateData(Edition edition, Version targetAppVersion)
        {
            return null;
        }

        SynchronizationContext _uiContext;
        IAppScan _appScan;
        Form _mainForm;

        public void Load(IAppScan appScan, IAppScanGui appScanGui, string extensionDir)
        {
            appScanGui.MainToolBar.Add(new ToolbarItem<EventArgs>("Multiple STP", "Set multiple STPs in the SDK", Resources.stp, OnSetMultiSTP));
            _appScan = appScan;
            _uiContext = SynchronizationContext.Current;
            appScanGui.MainFormStarted += AppScanGui_MainFormStarted;
        }

        private void AppScanGui_MainFormStarted(object sender, MainFormStartedEventArgs e)
        {
            _mainForm = e.MainForm;
        }

        private void OnSetMultiSTP(EventArgs args)
        {
            _uiContext.Send(delegate
            {
                string stpUrls = string.Empty;
                if (_appScan.Scan.ScanData.Config.ListOfStartingUrls.Count > 0)
                {
                    stpUrls = _appScan.Scan.ScanData.Config.ListOfStartingUrls.Aggregate(string.Empty, (res, s) => res += s + Environment.NewLine);
                }

                MultiSTPForm multiSTP = new MultiSTPForm(stpUrls);
                multiSTP.ShowDialog(_mainForm);
                _appScan.Scan.ScanData.Config.ListOfStartingUrls = multiSTP.StpListTextBox.Lines.ToList();
            }, null);
        }
    }
}
