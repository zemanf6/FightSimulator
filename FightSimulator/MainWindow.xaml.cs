using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using FightSimulator.Classes;

namespace FightSimulator
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Warrior w1;
            Warrior w2;
            
            Dice dice = new Dice();
            switch (classSelector1.SelectedItem.ToString())
            {
                case "System.Windows.Controls.ComboBoxItem: Warrior":
                    w1 = new Warrior("Warrior", (int)slider_health1.Value, (int)slider_attack1.Value, (int)slider_defense1.Value, dice);
                    break;

                case "System.Windows.Controls.ComboBoxItem: Wizard":
                    w1 = new Wizard("Wizard", (int)slider_health1.Value, (int)slider_attack1.Value, (int)slider_defense1.Value, dice, 40, (int)slider_ultimate1.Value);
                    break;
                case "System.Windows.Controls.ComboBoxItem: Healer":
                    w1 = new Healer("Healer", (int)slider_health1.Value, (int)slider_attack1.Value, (int)slider_defense1.Value, dice, 40, (int)slider_ultimate1.Value);
                    break;
                default:
                    w1 = new Warrior("Warrior", (int)slider_health1.Value, (int)slider_attack1.Value, (int)slider_defense1.Value, dice);
                    break;
            }

            switch (classSelector2.SelectedItem.ToString())
            {
                case "System.Windows.Controls.ComboBoxItem: Warrior":
                    w2 = new Warrior("Warrior", (int)slider_health2.Value, (int)slider_attack2.Value, (int)slider_defense2.Value, dice);
                    break;

                case "System.Windows.Controls.ComboBoxItem: Wizard":
                    w2 = new Wizard("Wizard", (int)slider_health2.Value, (int)slider_attack2.Value, (int)slider_defense2.Value, dice, 40, (int)slider_ultimate2.Value);
                    break;
                case "System.Windows.Controls.ComboBoxItem: Healer":
                    w2 = new Healer("Healer", (int)slider_health2.Value, (int)slider_attack2.Value, (int)slider_defense2.Value, dice, 40, (int)slider_ultimate2.Value);
                    break;
                default:
                    w2 = new Warrior("Warrior", (int)slider_health2.Value, (int)slider_attack2.Value, (int)slider_defense2.Value, dice);
                    break;
            }

            Arena arena = new Arena(w1, w2, dice);

            arena.Fight();
        }

        public virtual void CreateFile(string name)
        {
            if (File.Exists(name))
            {
                name += "(1)";
            }

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                ConformanceLevel = ConformanceLevel.Auto
            };

            using (XmlWriter xw = XmlWriter.Create($@"{name}", settings))
            {
                xw.WriteStartDocument();

                xw.WriteStartElement("Characters");
                xw.WriteStartElement("Character");
                xw.WriteElementString("Class", classSelector1.SelectedItem.ToString());
                xw.WriteElementString("Health", slider_health1.Value.ToString());
                xw.WriteElementString("Attack", slider_attack1.Value.ToString());
                xw.WriteElementString("Defense", slider_defense1.Value.ToString());
                xw.WriteElementString("Ultimate", slider_ultimate1.Value.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("Character");
                xw.WriteElementString("Class", classSelector2.SelectedItem.ToString());
                xw.WriteElementString("Health", slider_health2.Value.ToString());
                xw.WriteElementString("Attack", slider_attack2.Value.ToString());
                xw.WriteElementString("Defense", slider_defense2.Value.ToString());
                xw.WriteElementString("Ultimate", slider_ultimate2.Value.ToString());
                xw.WriteEndElement();

                xw.WriteEndElement();

                xw.WriteEndDocument();

            }
        }

        private void LoadFight()
        {
            int[] health = new int[2];
            int[] attack = new int[2];
            int[] defense = new int[2];
            int[] ultimate = new int[2];
            string[] classes = new string[2];
            string element = "";

            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                using (XmlReader xr = XmlReader.Create($@"{openFileDialog.FileName}"))
                {
                    int i = 0;
                    while (xr.Read())
                    {

                        if (xr.NodeType == XmlNodeType.Element)
                        {
                            element = xr.Name; // název aktuálního elementu
                            if (element == "uzivatel")
                            {
                                
                            }
                        }
                        else if (xr.NodeType == XmlNodeType.Text)
                        {
                            switch (element)
                            {
                                case "Class":
                                    classes[i] = xr.Value;
                                    break;
                                case "Health":
                                    health[i] = int.Parse(xr.Value);
                                    break;
                                case "Attack":
                                    attack[i] = int.Parse(xr.Value);
                                    break;
                                case "Defense":
                                    defense[i] = int.Parse(xr.Value);
                                    break;
                                case "Ultimate":
                                    ultimate[i] = int.Parse(xr.Value);
                                    i++;
                                    break;
                            }
                        }
                        
                    }
                }
                Warrior w1;
                Warrior w2;
                Dice dice = new Dice();
                switch (classes[0])
                {
                    case "System.Windows.Controls.ComboBoxItem: Warrior":
                        w1 = new Warrior("Warrior", health[0], attack[0], defense[0], dice);
                        break;
                    case "System.Windows.Controls.ComboBoxItem: Wizard":
                        w1 = new Wizard("Wizard", health[0], attack[0], defense[0], dice, 40, ultimate[0]);
                        break;
                    case "System.Windows.Controls.ComboBoxItem: Healer":
                        w1 = new Healer("Healer", health[0], attack[0], defense[0], dice, 40, ultimate[0]);
                        break;
                    default:
                        w1 = new Warrior("Warrior", health[0], attack[0], defense[0], dice);
                        break;
                }

                switch (classes[1])
                {
                    case "System.Windows.Controls.ComboBoxItem: Warrior":
                        w2 = new Warrior("Warrior", health[1], attack[1], defense[1], dice);
                        break;
                    case "System.Windows.Controls.ComboBoxItem: Wizard":
                        w2 = new Wizard("Wizard", health[1], attack[1], defense[1], dice, 40, ultimate[1]);
                        break;
                    case "System.Windows.Controls.ComboBoxItem: Healer":
                        w2 = new Healer("Healer", health[1], attack[1], defense[1], dice, 40, ultimate[1]);
                        break;
                    default:
                        w2 = new Warrior("Warrior", health[1], attack[1], defense[1], dice);
                        break;
                }

                Arena arena = new Arena(w1, w2, dice);

                arena.Fight();
            }
        }

        private void SaveFight(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
                CreateFile(saveFileDialog.FileName);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadFight();
        }
    }
}
