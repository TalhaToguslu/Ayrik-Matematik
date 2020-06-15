using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kapsama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelsatir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelsatir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;           
            panelsütun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;                        
            button3.Enabled = false;
            button2.Enabled = false;
            listBox1.Items.Add("-Satır ve sütun değerlerini girin ve oluştura tıklayın.");
            listBox1.Items.Add("-Satır alanı 46'dan büyük olamaz.");
        }

        string[] harf = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "ı", "i", "j", "k", "l", "m", "n", "p", "r", "s", "t", "x", "v", "y", "z", "a2", "b2", "c2", "d2", "e2", "f2", "g2", "h2", "ı2", "i2", "j2", "k2", "l2", "m2", "n2", "p2", "r2", "s2", "t2", "x2", "v2", "y2", "z2" };

        Button[,] btndizi;
        int[,] dizi;

        int satir, sütun, i, j, left = 0, top = 0, boyut, sayac = 0, x, y, hesapkontrol = 2;
        int kalansatir = 0, kalansütun = 0;
        int devamet = 0;
        int kontrol;

        List<Label> labelsatir;
        List<Label> labelsütun;

        List<int> mutlaksütun = new List<int>();
        List<int> mutlaksatir = new List<int>();

        List<int> cevap = new List<int>();
        List<int> satir1sayisi = new List<int>();
        List<int> sütun1sayisi = new List<int>();

        List<int> kapsanansatir = new List<int>();
        List<int> kapsayansatir = new List<int>();

        List<int> rotasatir = new List<int>();
        List<int> rotasütun = new List<int>();


        private void restart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        public void rota()
        {
            listBox1.Visible = false;
            listBox2.Visible = true;

            int son = 0;
            double agirlik = 0;
            double agirlik2 = 0;
            satir = 0;
            sayac = 0;
            y = 0;
            kontrol = 0;

            while (son < 1)
            {
                agirlik = 0;
                agirlik2 = 0;

                rotasütun.Clear();
                sütun1sayisi.Clear();

                listBox2.Items.Add("-Sütun Ağırlıkları Hesaplanıyor.");
                listBox2.Items.Add("-Silinecek satır mavi ile gösterilir.");

                sayac = 0;

                for (j = 0; j < labelsütun.Count; j++)//her sütunda kaç bir var hesaplandı.
                {
                    for (i = 0; i < labelsatir.Count; i++)
                    {
                        if (btndizi[i, j].Text == "1")
                        {
                            sayac++;
                        }
                    }
                    sütun1sayisi.Add(sayac);
                    sayac = 0;
                }

                sayac = sütun1sayisi[0];

                for (i = 1; i < sütun1sayisi.Count; i++)
                {
                    if (sütun1sayisi[i] < sayac && sütun1sayisi[i] != 0)
                    {
                        rotasütun.Clear();
                        rotasütun.Add(i);
                        sayac = sütun1sayisi[i];
                    }

                    if (sütun1sayisi[i] == sayac && sütun1sayisi[i] != 0)
                    {
                        rotasütun.Add(i);
                        sayac = sütun1sayisi[i];
                    }
                }

                x = 0;

                while (x < rotasütun.Count)//bakılan sütun
                {
                    for (i = 0; i < labelsatir.Count; i++)
                    {
                        if (btndizi[i, rotasütun[x]].Text == "1")
                        {
                            for (j = 0; j < labelsütun.Count; j++)
                            {
                                if (btndizi[i, j].Text == "1")
                                {
                                    agirlik += (1 / Convert.ToDouble(sütun1sayisi[j]));
                                    kontrol = 1;
                                }
                            }

                            if (kontrol > 0)
                            {
                                agirlik = agirlik * labelsatir.Count;
                            }
                            
                            if (y == 0 && kontrol > 0)//ilk eleman 
                            {
                                agirlik2 = agirlik;
                                satir = i;
                                y++;
                            }

                            if (agirlik < agirlik2 && kontrol > 0)
                            {
                                satir = i;//en hafif satır
                                agirlik2 = agirlik; // ağırlığı
                            }
                            agirlik = 0;
                            kontrol = 0;
                            y = 0;
                        }                      
                    }

                    listBox2.Items.Add("Ağırlığı en az olan satır: " + harf[satir].ToString() + " ağırlığı: " + agirlik2);;

                    agirlik2 = 0;

                    for (j = 0; j < labelsütun.Count; j++)
                    {                     
                        btndizi[satir, j].Text = "X";
                        btndizi[satir, j].BackColor = Color.Blue;
                        labelsatir[satir].Text = "X";
                    }
                    cevap.Add(satir);
                    x++;
                }

                sayac = 0;

                for (i = 0; i < labelsatir.Count; i++)
                {
                    for (j = 0; j < labelsütun.Count; j++)
                    {
                        if (btndizi[i, j].Text == "X")
                        {
                            sayac++;
                        }
                    }
                }

                if (sayac == (labelsütun.Count * labelsatir.Count))
                {
                    sonuc();
                    son++;
                }

                sayac = 0;

                for (i = 0; i < labelsatir.Count; i++)
                {
                    if (labelsatir[i].Text == "X")
                    {
                        sayac++;
                    }
                }

                if (sayac == labelsatir.Count - 1)
                {
                    sonuc();
                    son++;
                }

                sayac = 0;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)//OLUŞTUR BUTONU
        {
            int intkontrol = 0;
            button2.Enabled = true;

            if (int.TryParse(satırBox1.Text, out satir) == false)
            {
                MessageBox.Show("Satır alanı sadece rakamlardan oluşabilir...");
                button2.Enabled = false;
            }
            else
            {
                intkontrol++;
            }

            if (satir > 46)
            {
                MessageBox.Show("Satır alanı 46dan büyük olamaz.");
                button2.Enabled = false;
            }
            else
            {
                intkontrol++;
            }

            if (int.TryParse(sütunBox1.Text, out sütun) == false)
            {
                MessageBox.Show("Sütun alanı sadece rakamlardan oluşabilir...");
                button2.Enabled = false;
            }
            else
            {
                intkontrol++;
            }

            if (intkontrol == 3)
            {

                satir = Convert.ToInt32(satırBox1.Text);
                sütun = Convert.ToInt32(sütunBox1.Text);


                if (satir > sütun)
                {
                    boyut = 340 / satir;
                }
                else
                {
                    boyut = 340 / sütun;
                }

                if (boyut < 20)
                {
                    boyut = 25;
                }

                btndizi = new System.Windows.Forms.Button[satir, sütun];
                dizi = new int[satir, sütun];

                for (i = 0; i < satir; i++)// tüm diziye 0 atandı
                {
                    for (j = 0; j < sütun; j++)
                    {
                        dizi[i, j] = 0;
                    }
                }

                for (i = 0; i < satir; i++)//butonlar oluşturuldu
                {
                    for (j = 0; j < sütun; j++)
                    {
                        btndizi[i, j] = new System.Windows.Forms.Button();
                        btndizi[i, j].Width = boyut - 5;
                        btndizi[i, j].Height = boyut;
                        btndizi[i, j].BackColor = Color.White;
                        btndizi[i, j].Left = left;
                        btndizi[i, j].Top = top;
                        btndizi[i, j].Text = "0";
                        btndizi[i, j].Click += new EventHandler(this.tikla);
                        buttonpanel.Controls.Add(btndizi[i, j]);
                        left += boyut;
                    }
                    top += boyut;
                    left = 0;
                }

                labelsatir = new List<Label>(satir);
                labelsütun = new List<Label>(sütun);

                left = 0;
                top = 0;

                for (i = 0; i < sütun; i++)//labeller oluşturuldu
                {
                    labelsütun.Add(new System.Windows.Forms.Label());
                    labelsütun[i].Text = sayac.ToString();
                    labelsütun[i].Top = 10;
                    labelsütun[i].Left += left;
                    labelsütun[i].Width = 20;
                    labelsütun[i].Height = 20;
                    panelsütun.Controls.Add(labelsütun[i]);
                    sayac++;
                    left += boyut;
                }

                left = 0;
                top = 0;
                sayac = 0;

                for (i = 0; i < satir; i++)//labeller oluşturuldu
                {
                    labelsatir.Add(new System.Windows.Forms.Label());
                    labelsatir[i].Text = harf[sayac].ToString();
                    labelsatir[i].Top += top;
                    labelsatir[i].Left = 10;
                    labelsatir[i].Width = 20;
                    labelsatir[i].Height = 20;
                    panelsatir.Controls.Add(labelsatir[i]);
                    sayac++;
                    top += boyut;
                }

                listBox1.Items.Clear();
                listBox1.Items.Add("-Butonlara her tıklamada 0 ise 1, 1 ise 0 olur.");
                listBox1.Items.Add("-Matrisi oluşturduktan sonra Hesapla butonuna basın.");
                button1.Enabled = false;

            }

        }

        private void button3_Click(object sender, EventArgs e)//devam et butonu
        {
            if (devamet == 5)
            {
                rota();
            }

            if (devamet == 4)
            {
                kapsamabirpanel();
            }

            if (devamet==3)
            {
                listBox1.Items.Clear();
                düzenleme();
                mutlaksatirtekrar();

            }

            if (devamet == 2)
            {
                düzenleme();
                kapsamabirpanel();
            }

            if (devamet == 1)//mutlaksatır varsa 1.kutu
            {
                mutlaksatirbirpanel();
            }
        }

        public void kapsamabirpanel()
        {

            /////////////////////////////////////////////////////////////////
            kontrol = 0;
            satir1sayisi.Clear();
            sayac = 0;
            int devam = 0;
            x = 0;
            y = 0;

            for (i = 0; i < labelsatir.Count; i++)
            {
                for (j = 0; j < labelsütun.Count; j++)
                {
                    if (btndizi[i, j].Text == "1")
                    {
                        sayac++;
                    }
                }
                satir1sayisi.Add(sayac);

                if (sayac == sütun)// tüm satir 1 den ibaret ise
                {
                    kontrol = 1;
                    listBox1.Items.Clear();
                    listBox1.Items.Add("-Kapsayan satırlar mavi renkle gösterilir.");
                    listBox1.Items.Add("-Kapsanan satırlar yeşil renkle gösterilir.");
                    cevap.Add(i);

                    for (x = 0; x < labelsatir.Count; x++)
                    {
                        for (y = 0; y < labelsütun.Count; y++)
                        {
                            if (x != i)
                            {
                                btndizi[x, y].BackColor = Color.Green;
                            }
                            else
                            {
                                btndizi[x, y].BackColor = Color.Blue;

                            }

                        }
                    }

                    sonuc();

                    devam = 1;
                    break;
                }
                sayac = 0;
            }
            
            
            if (devam < 1)//sonuc bulunduysa devam etmesin.
            {
                x = 0;
                y = 0;
                sayac = 0;
                int denek = 0;

                listBox1.Items.Clear();
                listBox1.Items.Add("-Kapsayan satırlar mavi,");
                listBox1.Items.Add("-Kapsanan satırlar yeşil renkle gösterilir.");

                while (x < labelsatir.Count)//kontrol satırı
                {

                    for (i = 0; i < satir1sayisi.Count; i++)// kıyas satırı
                    {

                        for (j = 0; j < labelsütun.Count; j++)
                        {
                            if(btndizi[x, j].Text == "1" && btndizi[i, j].Text == "1" && i!=x)
                            {
                                sayac++;
                                denek = 1;
                            }

                            if (sayac == satir1sayisi[i] && denek==1)
                            {
                                listBox1.Items.Add("-" + labelsatir[x].Text.ToString() + " satırı tarafından Kapsanan " + labelsatir[i].Text.ToString() + " satırı");
                                cevap.Add(x);

                                kontrol = 1;
                                
                                for (y = 0; y < labelsütun.Count; y++)
                                {
                                    btndizi[x, y].BackColor = Color.Blue;
                                    btndizi[i, y].BackColor = Color.Green;

                                    btndizi[x, y].Text = "X";
                                    btndizi[i, y].Text = "X";
                                }

                                kapsanansatir.Add(i);
                                kapsayansatir.Add(x);
                                labelsatir[i].Text = "X";
                                sayac = 0;
                                break;
                            }
                        }
                        denek = 0;
                        sayac = 0;
                    }
                    x++;
                }

                sayac = 0;

                for (i =0;i< labelsatir.Count; i++)//hepsi boyalıysa bitir.
                {
                    for (j = 0; j < labelsütun.Count; j++)
                    {
                        if (btndizi[i, j].BackColor != Color.White)
                        {
                            sayac++;
                        }
                    }
                }

                if (sayac == (labelsatir.Count * labelsütun.Count))
                {
                    sonuc();
                    devamet = 10;
                }

                sayac = 0;
                y = 0;

                for (i = 0; i < labelsatir.Count; i++)
                {
                    for (j = 0; j < labelsütun.Count; j++)
                    {

                        if (btndizi[i, j].BackColor == Color.White)
                        {
                            sayac++;
                        }
                    }
                    if (sayac == sütun)
                    {
                        y++;
                    }
                    sayac = 0;
                }

                if (y == 1)
                {
                    cevap.Add(y);
                    button3.Enabled = false;

                    sonuc();
                    devamet = 10;

                }
                else
                {
                    devamet = 3;
                    listBox1.Items.Add("-İşlemlere devam etmek için Devam ete basın.");                   
                }
                
                if(kontrol<1)
                {
                    listBox1.Items.Add("-Kapsama bulunamadı Rota algoritması için devam ete basın.");
                    devamet = 5;
                    //////////rotaaaaaaaaaaaaaaaa
                }              
            }
            



        }

        public void mutlaksatirbirpanel()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("-Mutlak satirlar ve sütunlar mavi ile, ");
            listBox1.Items.Add(" sadeleştirmeden sonra tüm değerleri 0 olan satırlar kırmızı ile gösterilir.");

            sayac = 0;


            while (sayac < mutlaksütun.Count)//sütunda tek olan 1 in satiri bulundu
            {
                for (i = 0; i < labelsatir.Count; i++)
                {
                    if (btndizi[i, mutlaksütun[sayac]].Text == "1")
                    {
                        mutlaksatir.Add(i);
                    }
                }
                sayac++;
            }

            sayac = 0;

            while (sayac < mutlaksatir.Count)//mutlak satirdaki kapatılacak sütunlar bulundu
            {
                for (j = 0; j < labelsütun.Count; j++)
                {
                    if (btndizi[mutlaksatir[sayac], j].Text == "1")
                    {
                        mutlaksütun.Add(j);
                    }
                }
                sayac++;
            }

            sayac = 0;

            while (sayac < mutlaksütun.Count)//mutlak satir ve diğer sütunlar kapatıldı
            {
                for (i = 0; i < labelsatir.Count; i++)
                {
                    //btndizi[i, mutlaksütun[sayac]].Visible = false;
                    btndizi[i, mutlaksütun[sayac]].BackColor = Color.Blue;

                    if (btndizi[i, mutlaksütun[sayac]].Text == "0")// button 0 ise x , 1 ise p koy
                    {
                        btndizi[i, mutlaksütun[sayac]].Text = "X";
                    }

                    if (btndizi[i, mutlaksütun[sayac]].Text == "1")
                    {
                        btndizi[i, mutlaksütun[sayac]].Text = "X";
                    }

                }
                labelsütun[mutlaksütun[sayac]].Text = "X";
                sayac++;
            }

            sayac = 0;

            while (sayac < mutlaksatir.Count)//mutlak satirlar kapatıldı
            {
                for (j = 0; j < labelsütun.Count; j++)
                {
                    //btndizi[mutlaksatir[sayac], j].Visible = false;
                    btndizi[mutlaksatir[sayac], j].BackColor = Color.Blue;

                    if (btndizi[mutlaksatir[sayac], j].Text == "0")// button 0 ise x , 1 ise p koy
                    {
                        btndizi[mutlaksatir[sayac], j].Text = "X";
                    }

                    if (btndizi[mutlaksatir[sayac], j].Text == "1")
                    {
                        btndizi[mutlaksatir[sayac], j].Text = "X";
                    }

                }
                labelsatir[mutlaksatir[sayac]].Text = "X";
                sayac++;
            }


            for (j = 0; j < sütun; j++)
            {
                if (labelsütun[j].Text != "X")
                {
                    kalansütun++;
                }
            }

            for (i = 0; i < mutlaksatir.Count; i++)
            {
                cevap.Add(mutlaksatir[i]);
            }

            sayac = 0;
            mutlaksatir.Clear();

            for (i = 0; i < satir; i++)//düzenlemeden sonra 0 dan oluşan satırlar bulundu
            {
                for (j = 0; j < sütun; j++)
                {
                    if (btndizi[i, j].Text == "0")
                    {
                        sayac++;
                    }
                }

                if (sayac == kalansütun)
                {
                    mutlaksatir.Add(i);
                }

                sayac = 0;
            }

            sayac = 0;

            while (sayac < mutlaksatir.Count)//düzenlemeden sonra 0 dan oluşan satırlar kapatıldı
            {
                for (j = 0; j < sütun; j++)
                {
                    //btndizi[mutlaksatir[sayac], j].Visible = false;

                    if (btndizi[mutlaksatir[sayac], j].BackColor != Color.Blue)
                    {
                        btndizi[mutlaksatir[sayac], j].BackColor = Color.Red;
                    }

                    if (btndizi[mutlaksatir[sayac], j].Text == "0")// button 0 ise x , 1 ise p koy
                    {
                        btndizi[mutlaksatir[sayac], j].Text = "X";
                    }

                    if (btndizi[mutlaksatir[sayac], j].Text == "1")
                    {
                        btndizi[mutlaksatir[sayac], j].Text = "X";
                    }

                }
                labelsatir[mutlaksatir[sayac]].Text = "X";
                sayac++;
            }

            sayac = 0;

            for (i = 0; i < labelsatir.Count; i++)
            {
                for (j = 0; j < labelsatir.Count; j++)
                {
                    if (btndizi[i, j].BackColor != Color.White)
                    {
                        sayac++;
                    }
                }
            }

            cevap = cevap.Distinct().ToList();

            if (sayac == labelsütun.Count * labelsatir.Count)
            {
                sonuc();
                devamet = 10;
            }
            else
            {
                listBox1.Items.Add("-Kapsanan satır olup olmadığını kontrol etmek için Devam ete basın.");
                devamet = 2;
            }

        }

        private void button2_Click(object sender, EventArgs e)//HESAPLA BUTONU
        {
            sayac = 0;

            for (i = 0; i < labelsatir.Count; i++)
            {
                for (j = 0; j < labelsütun.Count; j++)
                {
                    btndizi[i, j].Enabled = false;
                }
            }

            for (i = 0; i < labelsatir.Count; i++)
            {
                for (j = 0; j < labelsütun.Count; j++)
                {
                    if (btndizi[i, j].Text == "0")
                    {
                        sayac++;
                    }
                }
            }

            if (sayac == labelsatir.Count * labelsütun.Count)
            {
                MessageBox.Show("Tüm butonlar 0 uygulama yeniden başlatılıyor.");
                Application.Restart();
            }

            sayac = 0;

            button2.Enabled = false;
            button3.Enabled = true;

            mutlaksatir.Clear();
            mutlaksütun.Clear();

            if (hesapkontrol % 2 == 0)
            {

                sayac = 0;

                for (j = 0; j < sütun; j++)//mutlak satırlar varsa bulundu
                {
                    for (i = 0; i < satir; i++)
                    {
                        if (btndizi[i, j].Text == "1")
                        {
                            sayac++;
                        }
                    }

                    if (sayac == 1)
                    {
                        mutlaksütun.Add(j);
                    }
                    sayac = 0;
                }

                if (mutlaksütun.Count > 0)
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("-Mutlak satir ve sütun bulundu hesaplamak için Devam et'e basın.");
                    devamet = 1;
                }
                else
                {
                    listBox1.Items.Add("-Mutlak satir ve sütun bulunamadı.");
                    listBox1.Items.Add("-Kapsama olup olmadığını kontrol etmek için devam et e basın.");
                    devamet = 2;
                }

                sayac = 0;

            }

        }

        public void mutlaksatirtekrar()
        {
            mutlaksatir.Clear();
            mutlaksütun.Clear();

            if (hesapkontrol % 2 == 0)
            {

                sayac = 0;

                for (j = 0; j < sütun; j++)//mutlak satırlar varsa bulundu
                {
                    for (i = 0; i < satir; i++)
                    {
                        if (btndizi[i, j].Text == "1")
                        {
                            sayac++;
                        }
                    }

                    if (sayac == 1)
                    {
                        mutlaksütun.Add(j);
                    }
                    sayac = 0;
                }

                if (mutlaksütun.Count > 0)
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("-Mutlak satir ve sütun bulundu hesaplamak için Devam et'e basın.");
                    devamet = 1;
                }
                else
                {
                    listBox1.Items.Add("-Mutlak satir ve sütun bulunamadı.");
                    listBox1.Items.Add("-Kapsama olup olmadığını kontrol etmek için devam et'e basın.");
                    devamet = 4;
                }

                sayac = 0;

            }
        }

        public void sonuc()
        {
            listBox1.Visible = false;
            listBox2.Visible = true;
            listBox2.Items.Add("-Tüm satır ve sütunlar sadeleşti.");
            listBox2.Items.Add("-CEVAP= ");
            button3.Enabled = false;

            cevap = cevap.Distinct().ToList();

            for (i = 0; i < labelsatir.Count; i++)
            {
                labelsatir[i].Text = "X";
            }

            for (i = 0; i < labelsütun.Count; i++)
            {
                labelsütun[i].Text = "X";
            }

            for (i = 0; i < cevap.Count; i++)
            {             
                listBox2.Items.Add("   " + harf[cevap[i]].ToString());
            }

           listBox2.Items.Add("-Restart Butonuna basarak uygulamayı yeniden başlatabilirsiniz.");

            button3.Enabled = false;
            button3.Visible = false;

            restart.Visible = true;
            restart.Enabled = true;
            MessageBox.Show("Sonuç Bulundu.");
        }

        public void düzenleme()
        {
            sayac = 0;
            satir = 0;

            for (i = 0; i < labelsatir.Count; i++)//label sütun düzenlendi
            {
                if (labelsatir[i].Text == "X")
                {
                    sayac++;
                }
            }

            satir = labelsatir.Count - sayac;
            sayac = 0;
            top = 0;

            panelsatir.Controls.Clear();

            for (i = 0; i < labelsatir.Count; i++)//label sütun düzenlendi
            {
                if (labelsatir[i].Text != "X")
                {
                    labelsatir[i].Top = top;
                    labelsatir[i].Text = harf[i];
                    panelsatir.Controls.Add(labelsatir[i]);
                    top += boyut;
                    sayac++;
                }
            }

            panelsütun.Controls.Clear();
            left = 0;
            sütun = 0;
            sayac = 0;

            for (j = 0; j < labelsütun.Count; j++)
            {
                if (labelsütun[j].Text == "X")
                {
                    sayac++;
                }
            }

            sütun = labelsütun.Count - sayac;
            sayac = 0;

            for (j = 0; j < labelsütun.Count - sayac; j++)
            {

                if (labelsütun[j].Text != "X")
                {
                    labelsütun[j].Left = left;
                    panelsütun.Controls.Add(labelsütun[j]);
                    left += boyut;
                }

                if (sayac == sütun)
                {
                    break;
                }
            }

            buttonpanel.Controls.Clear();

            top = 0;
            left = 0;
            sayac = 0;

            for (i = 0; i < labelsatir.Count; i++)//buttonlar falan düzenlendi
            {
                for (j = 0; j < labelsütun.Count; j++)
                {
                    if (btndizi[i, j].Text != "X")
                    {
                        btndizi[i, j].Top = top;
                        btndizi[i, j].Left = left;
                        buttonpanel.Controls.Add(btndizi[i, j]);
                        left += boyut;
                        sayac++;
                    }
                }
                if (sayac > 1)
                {
                    top += boyut;
                    left = 0;
                }
                sayac = 0;
            }
        }
       
        public void tikla(Object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (clickedButton.Text == "1")
            {
                clickedButton.Text = "0";
            }
            else
            {
                clickedButton.Text = "1";
            }
        }
    }
}
