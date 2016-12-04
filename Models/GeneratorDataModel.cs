using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows;

namespace SqlConferenceManagementSystemGenerator.Models
{
    public class GeneratorDataModel
    {
        public List<string> LastNames { get; }
        public List<string> FirstNames { get; }
        public List<string> Emails { get; }
        public List<string> Cities { get; }
        public List<string> Streets { get; }
        public List<string> CompanyNames { get; }

        public GeneratorDataModel()
        {
            LastNames = new List<string>();
            FirstNames = new List<string>();
            Emails = new List<string>();
            Cities = new List<string>();
            Streets = new List<string>();
            CompanyNames = new List<string>();
            try
            {
                var currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "FirstNames.txt")))
                {
                    var tmp = File.ReadLines(System.IO.Path.Combine(currentDirectory, "FirstNames.txt")).ToList();
                    tmp.ForEach(x => FirstNames.Add(new StringBuilder().Append(x.First()).Append(x.Substring(1).ToLower()).ToString()));
                }
                else
                    throw new FileNotFoundException();
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "LastNames.txt")))
                {
                    var tmp = File.ReadLines(System.IO.Path.Combine(currentDirectory, "LastNames.txt")).ToList();
                    tmp.ForEach(x => LastNames.Add(new StringBuilder().Append(x.First()).Append(x.Substring(1).ToLower()).ToString()));
                }
                else
                    throw new FileNotFoundException();
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "CompanyNames.txt")))
                    CompanyNames = File.ReadLines(System.IO.Path.Combine(currentDirectory, "CompanyNames.txt")).ToList();
                else
                    throw new FileNotFoundException();
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "CitiesNames.txt")))
                    Cities = File.ReadLines(System.IO.Path.Combine(currentDirectory, "CitiesNames.txt")).ToList();
                else
                    throw new FileNotFoundException();
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "StreetsNames.txt")))
                    Streets = File.ReadLines(System.IO.Path.Combine(currentDirectory, "StreetsNames.txt")).ToList();
                else
                    throw new FileNotFoundException();
                if (File.Exists(System.IO.Path.Combine(currentDirectory, "Emails.txt")))
                    Emails = File.ReadLines(System.IO.Path.Combine(currentDirectory, "Emails.txt")).ToList();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Brak pliku(ów) z danymi");
                Application.Current.Shutdown();
            }
        }
    }
}
