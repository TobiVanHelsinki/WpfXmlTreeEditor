using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace DemoApplication
{
    public partial class XMLViewerDemoApplication : Window
    {
        public XMLViewerDemoApplication()
        {
            InitializeComponent();
        }

        private void TxtFilePath_TextChanged(object sender, TextChangedEventArgs e) => OpenFile(txtFilePath.Text);
        private void BrowseXmlFile(object sender, RoutedEventArgs e) => OpenFile();

        private void OpenFile(string path = null)
        {
            if (path == null)
            {
                OpenFileDialog dlg = new OpenFileDialog
                {
                    CheckFileExists = true,
                    Filter = "XML Files (*.xml)|*.xml|All Files(*.*)|*.*",
                    Multiselect = false
                };

                if (dlg.ShowDialog() == true)
                {
                    path = dlg.FileName;
                }
                else
                {
                    return;
                }
                txtFilePath.Text = path;
            }
            XmlDocument XMLdoc = new XmlDocument();
            try
            {
                XMLdoc.Load(path);
            }
            catch (XmlException)
            {
                MessageBox.Show("The XML file is invalid");
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("The XML file could not be loaded");
                return;
            }
            vXMLViwer.xmlDocument = XMLdoc;
        }

        private void ClearXmlFile(object sender, RoutedEventArgs e)
        {
            txtFilePath.Text = string.Empty;
            vXMLViwer.xmlDocument = null;
        }


        private void SaveXmlFile(object sender, RoutedEventArgs e)
        {
            vXMLViwer.xmlDocument.Save(txtFilePath.Text);
        }
    }
}
