using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace _33_Million
{
    public partial class Form1 : Form
    {
        private List<Tieto> tiedot = new List<Tieto>();
        private const string FilePath = "tiedot.json";
        private int selectedIndex = -1;

        

        public Form1()
        {
            InitializeComponent();
            PäivitäNettovarallisuus();

            listBox1.Location = new System.Drawing.Point(35, 293);
            listBox1.Size = new System.Drawing.Size(350, 300);
            this.Controls.Add(listBox1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Handle form load event if needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nimi = textBox3.Text;
            if (float.TryParse(textBox1.Text, out float arvo) && float.TryParse(textBox2.Text, out float laina))
            {
                Tieto tieto = new Tieto
                {
                    Nimi = nimi,
                    Arvo = arvo,
                    Laina = laina
                };

                tiedot.Add(tieto);
                listBox1.Items.Add(tieto);

                PäivitäNettovarallisuus();
            }
            else
            {
                MessageBox.Show("Syötä kelvolliset numerot arvoksi ja lainaksi.");
            }
        }


        private void PäivitäNettovarallisuus()
        {
            float nettovarallisuus = tiedot.Sum(t => t.Arvo - t.Laina);
            label6.Text = nettovarallisuus + " €";
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            listBox1.Items.Clear();
            label6.Text = "0 €";
            tiedot.Clear();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string json = JsonSerializer.Serialize(tiedot, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
                MessageBox.Show("Tiedot tallennettu onnistuneesti.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tallennus epäonnistui: {ex.Message}");
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    tiedot = JsonSerializer.Deserialize<List<Tieto>>(json);
                    listBox1.Items.Clear();
                    foreach (var tieto in tiedot)
                    {
                        listBox1.Items.Add(tieto);
                    }
                    PäivitäNettovarallisuus();
                    MessageBox.Show("Tiedot ladattu onnistuneesti.");
                }
                else
                {
                    MessageBox.Show("Tiedostoa ei löydy.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lataus epäonnistui: {ex.Message}");
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                selectedIndex = listBox1.SelectedIndex;
                var selectedTieto = listBox1.SelectedItem as Tieto; 

                if (selectedTieto != null)
                {
                    textBox3.Text = selectedTieto.Nimi;
                    textBox1.Text = selectedTieto.Arvo.ToString();
                    textBox2.Text = selectedTieto.Laina.ToString();
                }
                else
                {
                    MessageBox.Show("Virhe valitun kohteen latauksessa.");
                }
            }
            else
            {
                MessageBox.Show("Valitse muokattava kohde.");
            }
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                string nimi = textBox3.Text;
                if (float.TryParse(textBox1.Text, out float arvo) && float.TryParse(textBox2.Text, out float laina))
                {
                    Tieto tieto = new Tieto
                    {
                        Nimi = nimi,
                        Arvo = arvo,
                        Laina = laina
                    };

                    tiedot[selectedIndex] = tieto;
                    listBox1.Items[selectedIndex] = tieto;

                    PäivitäNettovarallisuus();
                }
                else
                {
                    MessageBox.Show("Syötä kelvolliset numerot arvoksi ja lainaksi.");
                }
            }
            else
            {
                MessageBox.Show("Valitse muokattava kohde ja paina Muokkaa.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }
    }

    public class Tieto
    {
        public string Nimi { get; set; }
        public float Arvo { get; set; }
        public float Laina { get; set; }

        public override string ToString()
        {
            return $"{Nimi} / {Arvo} € / {Laina} €";
        }
    }
}
