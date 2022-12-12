using Newtonsoft.Json;
using System.Xml.Serialization;

namespace PRACTICE_2
{
    internal class Files
    {
        private string url;

        public Files(string URL)
        {
            url = URL;
            readFile();
        }

        private void readFile()
        {
            if (url.Contains(".txt"))
            {
                string text = File.ReadAllText(url);
                Console.WriteLine(text);
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.F1)
                    {
                        Console.WriteLine("==================================================");
                        Console.WriteLine("Введите полный путь до файла с полным расширением: ");
                        string new_url = Console.ReadLine();
                        convertFile(new_url);
                        break;
                    }
                    if (key.Key == ConsoleKey.Escape) { break; }
                }
            }
            else if (url.Contains(".json"))
            {
                string text = File.ReadAllText(url);
                List<Class1> figures = JsonConvert.DeserializeObject<List<Class1>>(text);
                foreach(Class1 item in figures)
                {
                    Console.WriteLine(item.Name + "\n" + item.Height + "\n" + item.Width);
                }
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.F1)
                    {
                        Console.WriteLine("==================================================");
                        Console.WriteLine("Введите полный путь до файла с полным расширением: ");
                        string new_url = Console.ReadLine();
                        convertFile(new_url);
                        break;
                    }
                    if (key.Key == ConsoleKey.Escape) { break; }
                }
            }
            else if (url.Contains(".xml"))
            {
                List<Class1> figures = new List<Class1>();
                XmlSerializer xml = new XmlSerializer(typeof(List<Class1>));
                using (FileStream fs = new FileStream(url, FileMode.Open))
                {
                    figures = (List<Class1>)xml.Deserialize(fs);
                }

                foreach (Class1 item in figures)
                {
                    Console.WriteLine(item.Name + "\n" + item.Height + "\n" + item.Width);
                }

                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.F1)
                    {
                        Console.WriteLine("==================================================");
                        Console.WriteLine("Введите полный путь до файла с полным расширением: ");
                        string new_url = Console.ReadLine();
                        convertFile(new_url);
                        break;
                    }
                    if (key.Key == ConsoleKey.Escape) { break; }
                }
            }
        }

        private void convertFile(string new_url)
        {
            Class1 figure_kvadrat = new Class1();
            figure_kvadrat.Name = "Kvadrat";
            figure_kvadrat.Height = 35;
            figure_kvadrat.Width = 35;

            Class1 figure_triangle = new Class1();
            figure_triangle.Name = "Triangle";
            figure_triangle.Height = 10;
            figure_triangle.Width = 15;

            Class1 figure_circle = new Class1();
            figure_circle.Name = "Circle";
            figure_circle.Height = 20;
            figure_circle.Width = 25;

            List<Class1> figures = new List<Class1>();
            figures.Add(figure_kvadrat);
            figures.Add(figure_triangle);
            figures.Add(figure_circle);

            if (new_url.Contains(".json"))
            {
                string json = JsonConvert.SerializeObject(figures);
                if (!File.Exists(new_url))
                {
                    File.Create(new_url);
                }
                File.WriteAllText(new_url, json);
            }
            else if (new_url.Contains(".txt"))
            {
                string text = "";
                foreach (Class1 item in figures)
                {
                    text += $"{item.Name}\n{item.Height}\n{item.Width}\n";
                }
                File.WriteAllText(new_url, text);
            }
            else if (new_url.Contains(".xml"))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Class1>));
                using (FileStream fs = new FileStream(new_url, FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, figures);
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите полный путь до файла: ");
            string url = Console.ReadLine();
            Files qrl = new Files(url);

        } 
    }
}