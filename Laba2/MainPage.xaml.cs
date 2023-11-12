using System;
using System.IO;
using System.Xml;
using Microsoft.Maui.Controls;
using System.Text;
using Microsoft.Maui.Controls.Compatibility;

namespace Laba2
{
    public partial class MainPage : ContentPage
    {
        private Label xmlContentLabel;

        public MainPage()
        {
            InitializeComponent();
            // Initialize the xmlContentLabel
            xmlContentLabel = new Label
            {
                Margin = new Thickness(20)
            };
        }

        private void CreateXML_Clicked(object sender, EventArgs e)
        {
            // Call the method to create and save the XML document
            CreateAndSaveXmlDocument();

            // Display alert indicating that the XML document has been created
            DisplayAlert("XML Document Created", "XmlDocumentTest.xml has been created on the desktop.", "OK");
        }

        // Rest of your existing code...

        // Add the following method from the second code snippet
        private static void CreateAndSaveXmlDocument()
        {
            var xmlDoc = new XmlDocument();
            XmlElement rootElement;
            int childCounter;
            int grandChildCounter;

            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
            rootElement = xmlDoc.CreateElement("MyRoot");
            xmlDoc.AppendChild(rootElement);

            for (childCounter = 1; childCounter <= 3; childCounter++)
            {
                XmlElement childElement;
                XmlAttribute childAttribute;

                childElement = xmlDoc.CreateElement("MyChild");
                childAttribute = xmlDoc.CreateAttribute("ID");
                childAttribute.Value = childCounter.ToString();
                childElement.Attributes.Append(childAttribute);

                rootElement.AppendChild(childElement);

                for (grandChildCounter = 1; grandChildCounter <= 4; grandChildCounter++)
                {
                    XmlElement grandChildElement;
                    XmlAttribute grandChildAttribute;

                    grandChildElement = xmlDoc.CreateElement("MyGrandChild");
                    grandChildAttribute = xmlDoc.CreateAttribute("NAME");
                    grandChildAttribute.Value = childAttribute.Value + " " + grandChildCounter.ToString();
                    grandChildElement.Attributes.Append(grandChildAttribute);
                    childElement.AppendChild(grandChildElement);
                }
            }

            xmlDoc.Save(GetFilePath("XmlDocumentTest.xml"));
            Console.WriteLine("XmlDocumentTest.xml Created\r\n");
        }

        private static string GetFilePath(string fileName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
        }
        private async void LoadDataButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var fileResult = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an XML file"
                });

                if (fileResult != null && fileResult.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    // Read the content of the XML file
                    string xmlContent = await ReadXmlFile(fileResult.FullPath);

                    // Display the XML content in the Label on the same page
                    DisplayXmlContent(xmlContent);
                }
                else
                {
                    DisplayAlert("File Not Selected or Invalid", "No XML file selected or the selected file is not a valid XML file", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async Task<string> ReadXmlFile(string filePath)
        {
            try
            {
                // Read the content of the XML file
                using (var reader = new StreamReader(filePath))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                DisplayAlert("Error", $"An error occurred while reading the XML file: {ex.Message}", "OK");
                return null;
            }
        }

        private void DisplayXmlContent(string xmlContent)
        {
            // Update the Text property of the label1 to display the XML content
            label1.Text = xmlContent;
            // Remove xmlContentLabel from stackLayout if it already exists
            stackLayout.Children.Remove(xmlContentLabel);
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Button Clicked", "Search button clicked", "OK");
        }

        async void ClearButton_Clicked(object sender, EventArgs e)
        {
            await label1.RelRotateTo(360, 1000);
        }
        async void GO (object sender, EventArgs e)
        {
            DisplayAlert("Button Clicked", "Search button clicked", "OK");
        }

        //стибрив нижній код з книги омельчук с.257
        private async void ConfirmExitButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Підтвердження", "Ви дійсно хочете вийти?", "Так", "Ні");
            if (answer)
            {
                System.Environment.Exit(0);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}