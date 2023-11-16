using System;
using System.IO;
using System.Xml;
using Microsoft.Maui.Controls;
using System.Text;
using Microsoft.Maui.Controls.Compatibility;
using System.Xml.Linq;

namespace Laba2
{
    public partial class MainPage : ContentPage
    {
        //Клас MainPage:
        //Наслідується від ContentPage, що вказує на те, що це представляє собою сторінку додатка.
        //Містить приватну змінну екземпляру xmlContentLabel типу Label.

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

        //Обробник події для натискання кнопки create xml.
        private void CreateXML_Clicked(object sender, EventArgs e)
        {
            // Викликає метод CreateAndSaveXmlDocument для створення та збереження XML-документа.
            CreateAndSaveXmlDocument();

            // Показує сповіщення про створення XML-документа.
            DisplayAlert("XML Document Created", "XmlDocumentTest.xml has been created on the desktop.", "OK");
        }
        //Нижній код я взяв у книжці пані Омельчук. Він створює xml файл і додає його на робочий стіл.
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

        //Обробник події для натискання кнопки download XML.
        //async позначає, що цей метод є асинхронним, тобто він може виконуватися паралельно з іншими операціями.
        //sender і EventArgs додаються автоматично платформою, коли ви визначаєте обробник події.
        //sender вказує на об'єкт, який відправив (викликав) подію.
        //EventArgs(Event Arguments) є базовим класом для передачі додаткової інформації про подію.
        private async void LoadDataButton_Clicked(object sender, EventArgs e)
        {
            try
            {
            //await використовується для того, щоб програма могла продовжувати виконувати інші завдання під час очікування вибору файлу.
            //FilePicker: Це клас абстракції для вибору файлів в Xamarin-додатках.Він дозволяє користувачеві вибирати файли з файлової системи пристрою.
            //PickAsync: Цей метод викликається для відкриття діалогового вікна вибору файлу, де користувач може вибрати файл.
            //хотів змінити заголовок вікна вибору файлу на "Select an XML file" але воно щось не змінюється :< треба потім переробити
                var fileResult = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an XML file"
                });
                //Перевіряє, чи результат вибору файлу(fileResult) не є порожнім(null).
                //Перевіряє, чи ім'я вибраного файлу має розширення ".xml".
                //Це виконується за допомогою методу EndsWith, який перевіряє, чи рядок закінчується вказаним значенням (в даному випадку, ".xml").
                //StringComparison.OrdinalIgnoreCase вказує, що порівняння має бути регістронезалежним.
                if (fileResult != null && fileResult.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    //Викликається метод ReadXmlFile для асинхронного зчитування вмісту вибраного XML-файлу за його повним шляхом (fileResult.FullPath).
                    //Результат зчитування(вміст XML-файлу) зберігається у змінній xmlContent. 
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

        private async void DOM(object sender, EventArgs e)
        {
            string domResult = parsingXmlDocument();
            label1.Text = domResult;
        }

        private static string parsingXmlDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(getFilePath("XmlDocumentTest.xml"));
            return RecurseNodes(xmlDoc.DocumentElement);
        }

        private static string RecurseNodes(XmlNode node)
        {
            var sb = new StringBuilder();
            RecurseNodes(node, 0, sb);
            return sb.ToString();
        }

        private static void RecurseNodes(XmlNode node, int level, StringBuilder sb)
        {
            sb.AppendFormat("{0,-2} Тип:{1,-9} Назва:{2,-13} Атрибути:", level, node.NodeType, node.Name);
            foreach (XmlAttribute attr in node.Attributes)
            {
                sb.AppendFormat("{0}={1} ", attr.Name, attr.Value);
            }
            sb.AppendLine();
            foreach (XmlNode n in node.ChildNodes)
            {
                RecurseNodes(n, level + 1, sb);
            }
        }

        private static string getFilePath(string fileName)
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
    }

        private void SAX(object sender, EventArgs e)
        {
            parsingWithXmlTextReader();
        }

        private void parsingWithXmlTextReader()
        {
            var sb = new StringBuilder();
            var xmlReader = new XmlTextReader(getFilePath("XmlDocumentTest.xml"));

            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                    case XmlNodeType.Element:
                    case XmlNodeType.Comment:
                        sb.AppendFormat("{0}: {1} = {2}",
                            xmlReader.NodeType,
                            xmlReader.Name,
                            xmlReader.Value);
                        sb.AppendLine();
                        break;
                    case XmlNodeType.Text:
                        sb.AppendFormat(" - Value: {0}", xmlReader.Value);
                        sb.AppendLine();
                        break;
                }

                if (xmlReader.HasAttributes)
                {
                    while (xmlReader.MoveToNextAttribute())
                    {
                        sb.AppendFormat(" - Attribute: {0} = {1}",
                            xmlReader.Name,
                            xmlReader.Value);
                        sb.AppendLine();
                    }
                }
            }

            xmlReader.Close();
            label1.Text = sb.ToString();
        }
        private void LINQToXML(object sender, EventArgs e)
        {
            string linqResult = ParsingWithLINQToXML();
            label1.Text = linqResult;
        }

        private string ParsingWithLINQToXML()
        {
            try
            {
                XDocument xdoc = XDocument.Load(GetFilePath("XmlDocumentTest.xml"));

                // Знаходимо всі елементи верхнього рівня в документі
                var result = xdoc.Root.Elements()
                    .Select(element => new
                    {
                        ElementName = element.Name.LocalName,
                        Attributes = element.Attributes()
                            .ToDictionary(attr => attr.Name.LocalName, attr => attr.Value),
                        InnerText = element.Value
                    });

                StringBuilder sb = new StringBuilder();
                foreach (var item in result)
                {
                    sb.AppendLine($"Елемент: {item.ElementName}");
                    foreach (var attribute in item.Attributes)
                    {
                        sb.AppendLine($"  Атрибут: {attribute.Key} = {attribute.Value}");
                    }
                    sb.AppendLine($"  Текст: {item.InnerText}");
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                DisplayAlert("Помилка", $"Виникла помилка під час обробки XML за допомогою LINQ: {ex.Message}", "OK");
                return null;
            }
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