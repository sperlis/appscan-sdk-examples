using AppScan;
using AppScan.GuiLayerImpl.Reporting.SecurityReportNew.Utils;
using AppScan.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ReportAligner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Starting AppScan");
            IAppScan appScan = AppScanFactory.CreateInstance();
            Console.Out.WriteLine("Loading: " + args[0]);
            appScan.Scan.LoadScanData(args[0]);
            Console.Out.WriteLine("Creating security report");
            ISecurityReport report = ReportFactory.CreateSecurityReport(appScan.Scan.ScanData);
            report.Config.Name = "Sample";
            report.Config.Issues = true;
            report.Config.AdditionalIssueInformation = true;
            string location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "temp.xml");
            File.Delete(location);
            Console.Out.WriteLine("Exporting report to " + location);
            report.Export(ReportFileType.Xml, location);

            XmlDocument xmlReport = new XmlDocument();
            xmlReport.Load(location);
            XmlNodeList issues = xmlReport.SelectNodes("xml-report/issue-group/item");
            foreach (XmlNode issue in issues)
            {
                XmlNode variantGroup = issue.SelectSingleNode("variant-group");
                XmlNode group = issue.InsertBefore(xmlReport.CreateElement("attributes-group"), variantGroup);
                group.InnerXml = GetIssueCustomAttributes(issue);
            }
            location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "report.xml");
            File.Delete(location);
            Console.Out.WriteLine("Writing updated XML to " + location);
            xmlReport.Save(location);

            FileStream fs = new FileStream(location, FileMode.Open);
            IXmlReportData reportData = ReportFactory.CreateXmlReportData(fs, Path.GetTempPath());
            Console.Out.WriteLine("Generating PDF report");
            FileStream pdfReportStream = reportData.ExportSecurityReport(ReportFileType.Pdf);
            pdfReportStream.Close();
            location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "report.pdf");
            File.Delete(location);
            Console.Out.WriteLine("Copying PDF output to " + location);
            File.Copy(pdfReportStream.Name, location);
        }

        private static string GetIssueCustomAttributes(XmlNode issue)
        {
            XmlNode issueType = issue.SelectSingleNode("issue-type/ref");
            if (_customAttibutes.ContainsKey(issueType.InnerText))
            {
                return _customAttibutes[issueType.InnerText];
            }
            return "<attribute><name>Alternate Name</name><value>Missing Name</value></attribute><attribute><name>Ent. Severity</name><value>0</value></attribute>";
        }

        private static Dictionary<string, string> _customAttibutes = new Dictionary<string, string>
        {
            {"SSL_CertWithBadCN", "<attribute><name>Alternate Name</name><value>Bad Certificate \"Common Name\" Field</value></attribute><attribute><name>Ent. Severity</name><value>3</value></attribute>"},
            {"attSqlInjectionChecks", "<attribute><name>Alternate Name</name><value>Databse Injection (SQL)</value></attribute><attribute><name>Ent. Severity</name><value>1</value></attribute>"},
            {"attCrossSiteScripting", "<attribute><name>Alternate Name</name><value>Cross Site Scripting (Type II)</value></attribute><attribute><name>Ent. Severity</name><value>1</value></attribute>"},

        };
    }
}
