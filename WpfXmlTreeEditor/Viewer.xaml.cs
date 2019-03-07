using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;
using System.Linq;

namespace XMLViewer
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class Viewer : UserControl
    {
        private XmlDocument _xmldocument;
        public Viewer()
        {
            InitializeComponent();
        }

        public XmlDocument xmlDocument
        {
            get { return _xmldocument; }
            set
            {
                _xmldocument = value;
                BindXMLDocument();
            }
        }

        private void BindXMLDocument()
        {
            if (_xmldocument == null)
            {
                xmlTree.ItemsSource = null;
                return;
            }
            xmlTree.ItemsSource = null;

            XmlDataProvider provider = new XmlDataProvider();
            provider.Document = _xmldocument;
            Binding binding = new Binding
            {
                Source = provider,
                XPath = "child::node()"
            };
            xmlTree.SetBinding(TreeView.ItemsSourceProperty, binding);
        }


        private void AddAttribute_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is Button B && B.DataContext is XmlElement xml)
            {
                try
                {
                    var D = new Dialog("Set Parameter", ("Attribute Name", ""));
                    if (D.ShowDialog() == true)
                    {
                        xml.Attributes.Append(xml.OwnerDocument.CreateAttribute(D.Results[0] as string));
                        BindXMLDocument();
                    }
                }
                catch (System.Exception)
                {
                }
            }
        }
        private void AddChild_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is Button B && B.DataContext is XmlElement xml)
            {
                string Name = "";
                string Url = "";
                if (xml.HasChildNodes)
                {
                    Name = xml.LastChild.Name;
                    Url = xml.LastChild.NamespaceURI;
                }
                else
                {
                    try
                    {
                        var D = new Dialog("Set Parameter", ("Node Name", ""), ("Node URL (optional)", ""));
                        if (D.ShowDialog() == true)
                        {
                            Name = D.Results[0] as string;
                            Url = D.Results[1] as string;
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch (System.Exception)
                    {
                        return;
                    }
                }
                try
                {
                    var newChild = xml.OwnerDocument.CreateNode(XmlNodeType.Element, Name, Url);
                    if (xml.HasChildNodes)
                    { // Copy Attributes
                        foreach (var item in xml.FirstChild.Attributes.OfType<XmlAttribute>())
                        {
                            newChild.Attributes.Append(xml.OwnerDocument.CreateAttribute(item.Name));
                        }
                    }
                    xml.AppendChild(newChild);
                }
                catch (System.Exception)
                {
                }
            }
        }

        private void RemoveThis_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is Button B && B.DataContext is XmlElement xml)
            {
                xml.ParentNode?.RemoveChild(xml);
            }
        }
    }
}
