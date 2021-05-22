using Microsoft.Win32;
using System.Windows;
using System.Xml;
using FightSimulator.Classes;

namespace FightSimulator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dice dice = new Dice();
            Warrior w1 = CreateWarrior(classSelector1.SelectedItem.ToString(), "Player1", (int)slider_health1.Value, (int)slider_attack1.Value, (int)slider_defense1.Value, (int)slider_ultimate1.Value);
            Warrior w2 = CreateWarrior(classSelector2.SelectedItem.ToString(), "Player2", (int)slider_health2.Value, (int)slider_attack2.Value, (int)slider_defense2.Value, (int)slider_ultimate2.Value);

            Arena arena = new Arena(w1, w2, dice);

            arena.Fight();
        }

        private Warrior CreateWarrior(string selector, string name, int health, int attack, int defense, int ultimate)
        {
            Warrior w;

            Dice dice = new Dice();

            switch(selector)
            {
                case "System.Windows.Controls.ComboBoxItem: Warrior":
                    w = new Warrior(name, health, attack, defense, dice);
                    break;
                case "System.Windows.Controls.ComboBoxItem: Wizard":
                    w = new Wizard(name, health, attack, defense, dice, 40, ultimate);
                    break;
                case "System.Windows.Controls.ComboBoxItem: Healer":
                    w = new Healer(name, health, attack, defense, dice, 40, ultimate);
                    break;
                default:
                    w = new Warrior(name, health, attack, defense, dice);
                    break;
            }
            return w;
        }


        public virtual void CreateFile(string name)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
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
                            element = xr.Name;
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

                Dice dice = new Dice();

                Warrior w1 = CreateWarrior(classes[0], "Player1", health[0], attack[0], defense[0], ultimate[0]);
                Warrior w2 = CreateWarrior(classes[1], "Player2", health[1], attack[1], defense[1], ultimate[1]);

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