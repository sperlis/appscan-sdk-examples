using AppScan;
using SecurityData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPolicyLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Starting AppScan");
            IAppScan appScan = AppScanFactory.CreateInstance();
            CTestPolicy testPolicy = appScan.Scan.ScanData.Config.TestPolicy;

            Console.Out.WriteLine("Clear existing test policy");
            testPolicy.ClearVariants();

            Console.Out.WriteLine("Going over all the rules and variants in Security Data repository and adding to the test policy");
            foreach (IssueTypeData issueType in Repository.GetIssueTypesData())
            {
                if (issueType.UserDefined)
                    continue;

                if (testPolicy.UpdateSettings.isMatchFilters(issueType))
                {
                    foreach (VariantType variantType in issueType.VariantTypes)
                    {
                        testPolicy.IssueTypes.AddVariant(issueType.Name, variantType.TestPolicyID);
                    }
                }
            }
            string policyFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), appScan.Version.DisplayVerNum + "." + appScan.Version.RulesNumber + ".Policy");
            Console.Out.WriteLine("Export test policy to " + policyFile);
            using (FileStream fs = new FileStream(policyFile, FileMode.Create, FileAccess.Write))
            {
                testPolicy.Save(fs);
            }
        }
    }
}
