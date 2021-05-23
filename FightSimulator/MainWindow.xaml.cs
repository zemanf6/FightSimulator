using Microsoft.Win32;
using System.Windows;
using System.Xml;
using FightSimulator.Classes;
using System.Collections.Generic;

namespace FightSimulator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillItems();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dice dice = new Dice();
            Warrior w1 = CreateWarrior(classSelector1.SelectedItem.ToString(), Name1.Text, (int)slider_health1.Value, (int)slider_attack1.Value, (int)slider_defense1.Value, (int)slider_ultimate1.Value, (Item)ItemSelector1.SelectedItem);
            Warrior w2 = CreateWarrior(classSelector2.SelectedItem.ToString(), Name2.Text, (int)slider_health2.Value, (int)slider_attack2.Value, (int)slider_defense2.Value, (int)slider_ultimate2.Value, (Item)ItemSelector2.SelectedItem);

            Arena arena = new Arena(w1, w2, dice);

            arena.Fight();
        }

        private Warrior CreateWarrior(string selector, string name, int health, int attack, int defense, int ultimate, Item item)
        {
            Warrior w;

            Dice dice = new Dice();

            switch(selector)
            {
                case "System.Windows.Controls.ComboBoxItem: Warrior":
                    w = new Warrior(name, health + item.HealthBonus, attack + item.AttackBonus, defense + item.DefenseBonus, dice);
                    break;
                case "System.Windows.Controls.ComboBoxItem: Wizard":
                    w = new Wizard(name, health + item.HealthBonus, attack + item.AttackBonus, defense + item.DefenseBonus, dice, 40, ultimate + item.UltimateBonus);
                    break;
                case "System.Windows.Controls.ComboBoxItem: Healer":
                    w = new Healer(name, health + item.HealthBonus, attack + item.AttackBonus, defense + item.DefenseBonus, dice, 40, ultimate + item.UltimateBonus);
                    break;
                default:
                    w = new Warrior(name, health + item.HealthBonus, attack + item.AttackBonus, defense + item.DefenseBonus, dice);
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
                xw.WriteElementString("Name", Name1.Text);
                xw.WriteElementString("Class", classSelector1.SelectedItem.ToString());
                xw.WriteElementString("Health", slider_health1.Value.ToString());
                xw.WriteElementString("Attack", slider_attack1.Value.ToString());
                xw.WriteElementString("Defense", slider_defense1.Value.ToString());
                xw.WriteElementString("Ultimate", slider_ultimate1.Value.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("Character");
                xw.WriteElementString("Name", Name2.Text);
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
            string[] name = new string[2];
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
                                case "Name":
                                    name[i] = xr.Value;
                                    break;
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

                Item item = new Item("Select Item", 0, 0, 0, 0);
                Warrior w1 = CreateWarrior(classes[0], name[0], health[0], attack[0], defense[0], ultimate[0], item);
                Warrior w2 = CreateWarrior(classes[1], name[1], health[1], attack[1], defense[1], ultimate[1], item);

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

        private void FillItems()
        {
            var items = new List<Item>
            {
                new Item("Select Item", 0, 0, 0, 0),
                new Item("Health boost", 10, 0, 0, 0),
                new Item("Strength boost", 0, 10, 0, 0),
                new Item("Defense boost", 0, 0, 10, 0),
                new Item("Ultimate boost", 0, 0, 0, 10),
            };

            foreach (Item i in items)
            {
                ItemSelector1.Items.Add(i);
                ItemSelector2.Items.Add(i);
            }
        
            ItemSelector1.SelectedIndex = 0;
            ItemSelector2.SelectedIndex = 0;
        }
    }
}