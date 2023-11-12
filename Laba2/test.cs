using System;
using System.IO;
using System.Text;
using System.Xml;

//class Program
//{
//    private static void ParsingXmlDocument()
//    {
//        XmlDocument xmlDoc = new XmlDocument();
//        xmlDoc.Load(GetFilePath("XmlDocumentTest.xml"));
//        RecurseNodes(xmlDoc.DocumentElement);
//    }

//    private static void RecurseNodes(XmlNode node)
//    {
//        var sb = new StringBuilder();
//        // Починаємо рекурсивний перегляд з рівня 0
//        RecurseNodes(node, 0, sb);
//        // Друкуємо сформований рядок
//        Console.WriteLine(sb.ToString());
//    }

//    private static void RecurseNodes(XmlNode node, int level, StringBuilder sb)
//    {
//        sb.AppendFormat("{0,-2} Type:{1,-9} Name:{2,-13} Attr:",
//            level, node.NodeType, node.Name);

//        foreach (XmlAttribute attr in node.Attributes)
//        {
//            sb.AppendFormat("{0}={1} ", attr.Name, attr.Value);
//        }
//        sb.AppendLine();

//        foreach (XmlNode n in node.ChildNodes)
//        {
//            RecurseNodes(n, level + 1, sb);
//        }
//    }

//    private static void SearchingInXmlDocument()
//    {
//        var xmlDoc = new XmlDocument();
//        xmlDoc.Load(GetFilePath("XmlDocumentTest.xml"));

//        // Використання XPath для пошуку елементу з ID = 2
//        var node = xmlDoc.SelectSingleNode("//MyChild[@ID='2']");

//        if (node != null)
//        {
//            Console.WriteLine("Node with ID=2 found:");
//            RecurseNodes(node);
//        }
//        else
//        {
//            Console.WriteLine("Node with ID=2 not found.");
//        }
//    }

//    private static void GetElementsByTagName()
//    {
//        var xmlDoc = new XmlDocument();
//        xmlDoc.Load(GetFilePath("XmlDocumentTest.xml"));

//        // Отримання списку вузлів з іменем тегу "MyGrandChild" за допомогою SelectNodes
//        var elements = xmlDoc.SelectNodes("//MyGrandChild");

//        var sb = new StringBuilder();
//        foreach (XmlNode node in elements)
//        {
//            RecurseNodes(node, 0, sb);
//        }
//        Console.WriteLine(sb.ToString());
//    }

//    // Шлях на робочий стіл
//    private static string GetFilePath(string fileName)
//    {
//        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
//    }

//    public static void Main(string[] args)
//    {
//        ParsingXmlDocument();
//        Console.WriteLine("\nSearching for node with ID=2:\n");
//        SearchingInXmlDocument();
//        Console.WriteLine("\nElements with tag name 'MyGrandChild' using GetElementsByTagName:\n");
//        GetElementsByTagName();
//        Console.WriteLine("\nElements with tag name 'MyGrandChild' using SelectNodes:\n");
//        SelectNodesTool();
//        Console.ReadKey();
//    }

//    private static void SelectNodesTool()
//    {
//        var xmlDoc = new XmlDocument();
//        xmlDoc.Load(GetFilePath("XmlDocumentTest.xml"));

//        // Використання XPath для отримання списку вузлів з іменем тегу "MyGrandChild" за допомогою SelectNodes
//        var elements = xmlDoc.SelectNodes("//MyGrandChild");

//        var sb = new StringBuilder();
//        foreach (XmlNode node in elements)
//        {
//            RecurseNodes(node, 0, sb);
//        }
//        Console.WriteLine(sb.ToString());
//    }
//}
