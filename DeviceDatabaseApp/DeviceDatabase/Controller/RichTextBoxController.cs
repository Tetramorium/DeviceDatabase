using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;

namespace DeviceDatabase.Controller
{
    public static class RichTextBoxController
    {
        public static string ConvertFlowDocumentToString(FlowDocument _FlowDocument)
        {
            StringWriter wr = new StringWriter();
            XamlWriter.Save(_FlowDocument, wr);
            return wr.ToString();
        }

        public static FlowDocument ConvertStringToFlowDocument(string _Xaml)
        {
            return XamlReader.Parse(_Xaml) as FlowDocument;
        }
    }
}
