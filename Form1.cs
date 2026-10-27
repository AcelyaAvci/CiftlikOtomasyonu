using System.Text.Json;
using System.IO;

namespace CiftlikOtomasyonu;

public partial class Form1 : Form
{
    private readonly string dosyaYolu =
        Path.Combine(Directory.GetCurrentDirectory(), "seralar.json");
    private readonly List<string> seralar = new()
    {
        "Domates Serası",
        "Muz Serası"
    };

    private ListBox seraListesi = new();
    private TextBox seraAdiKutusu = new();
    private CheckBox sulamaSecimi = new();
    private CheckBox havalandirmaSecimi = new();

    private readonly Color koyuYesil = Color.FromArgb(30, 78, 54);
    private readonly Color anaYesil = Color.FromArgb(44, 122, 80);
    private readonly Color arkaPlan = Color.FromArgb(242, 247, 241);

    public Form1()
{
        InitializeComponent();
        SeralariYukle();
        AnaEkraniOlustur();
}
    private void AnaEkraniOlustur()
    {
        Text = "Çiftlik Otomasyonu";
        Size = new Size(900, 720);
        MinimumSize = new Size(900, 720);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = arkaPlan;
        ForeColor = koyuYesil;
        Font = new Font("Segoe UI", 10);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        var ustPanel = new Panel
        {
            Location = new Point(0, 0),
            Size = new Size(884, 110),
            BackColor = koyuYesil
        };
        Controls.Add(ustPanel);

        var baslik = new Label
        {
            Text = "ÇİFTLİK OTOMASYONU",
            Font = new Font("Segoe UI", 23, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(30, 22),
            AutoSize = true
        };
        ustPanel.Controls.Add(baslik);

        var altBaslik = new Label
        {
            Text = "Akıllı sera yönetim paneli",
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.FromArgb(211, 231, 215),
            Location = new Point(34, 68),
            AutoSize = true
        };
        ustPanel.Controls.Add(altBaslik);

        var seraGrubu = GrupOlustur("SERALAR", 30, 135);
        Controls.Add(seraGrubu);

        seraListesi.Location = new Point(18, 38);
        seraListesi.Size = new Size(340, 220);
        seraListesi.Font = new Font("Segoe UI", 11);
        seraListesi.BorderStyle = BorderStyle.FixedSingle;
        seraListesi.IntegralHeight = false;
        seraGrubu.Controls.Add(seraListesi);

        foreach (string sera in seralar)
        {
            seraListesi.Items.Add(sera);
        }

        seraListesi.SelectedIndex = 0;

        var seraAdiEtiketi = new Label
        {
            Text = "Yeni sera adı",
            Location = new Point(18, 273),
            AutoSize = true,
            ForeColor = koyuYesil
        };
        seraGrubu.Controls.Add(seraAdiEtiketi);

        seraAdiKutusu.Location = new Point(18, 300);
        seraAdiKutusu.Size = new Size(225, 30);
        seraAdiKutusu.Font = new Font("Segoe UI", 10);
        seraGrubu.Controls.Add(seraAdiKutusu);

        var seraEkleButonu = ButonOlustur(
            "+ Sera Ekle", 253, 297, 105, 36, anaYesil);

seraEkleButonu.Click += SeraEkle;
seraGrubu.Controls.Add(seraEkleButonu);

var seraSilButonu = ButonOlustur(
    "Sera Sil", 253, 337, 105, 36, Color.Firebrick);

seraSilButonu.Click += SeraSil;
seraGrubu.Controls.Add(seraSilButonu);

        var makineGrubu = GrupOlustur("MAKİNELER", 420, 135);
        Controls.Add(makineGrubu);

        sulamaSecimi.Text = "Sulama Sistemi";
        sulamaSecimi.Location = new Point(22, 48);
        sulamaSecimi.AutoSize = true;
        sulamaSecimi.Font = new Font("Segoe UI", 11);
        sulamaSecimi.Checked = true;
        makineGrubu.Controls.Add(sulamaSecimi);

        havalandirmaSecimi.Text = "Havalandırma";
        havalandirmaSecimi.Location = new Point(22, 92);
        havalandirmaSecimi.AutoSize = true;
        havalandirmaSecimi.Font = new Font("Segoe UI", 11);
        havalandirmaSecimi.Checked = true;
        makineGrubu.Controls.Add(havalandirmaSecimi);

        var makineBilgi = new Label
        {
            Text = "Kontrol etmek istediğin makineleri seç.",
            Location = new Point(22, 145),
            Size = new Size(325, 45),
            ForeColor = Color.FromArgb(90, 110, 96)
        };
        makineGrubu.Controls.Add(makineBilgi);

        var altBilgi = new Label
        {
            Text = "Devam etmek için bir sera seç ve otomasyon panelini aç.",
            Location = new Point(35, 615),
            Size = new Size(810, 35),
            ForeColor = Color.FromArgb(90, 110, 96),
            Font = new Font("Segoe UI", 10)
        };
        Controls.Add(altBilgi);

        var otomasyonButonu = ButonOlustur(
            "⚙  OTOMASYON PANELİNİ AÇ",
            420, 540, 390, 55, koyuYesil);

        otomasyonButonu.Font = new Font(
            "Segoe UI", 12, FontStyle.Bold);

        otomasyonButonu.Click += OtomasyonuAc;
        Controls.Add(otomasyonButonu);
    }

    private GroupBox GrupOlustur(string baslik, int x, int y)
    {
        return new GroupBox
        {
            Text = baslik,
            Location = new Point(x, y),
            Size = new Size(390, 390),
            BackColor = Color.White,
            ForeColor = koyuYesil,
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            Padding = new Padding(12)
        };
    }

    private Button ButonOlustur(
        string metin, int x, int y, int genislik, int yukseklik,
        Color renk)
    {
        var buton = new Button
        {
            Text = metin,
            Location = new Point(x, y),
            Size = new Size(genislik, yukseklik),
            BackColor = renk,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };

        buton.FlatAppearance.BorderSize = 0;
        return buton;
    }

  private void SeralariYukle()
{
    try
    {
        if (File.Exists(dosyaYolu))
        {
            string json = File.ReadAllText(dosyaYolu);

            var kayitliSeralar =
                JsonSerializer.Deserialize<List<string>>(json);

            if (kayitliSeralar != null && kayitliSeralar.Count > 0)
            {
                seralar.Clear();
                seralar.AddRange(kayitliSeralar);
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            "Seralar yüklenirken bir hata oluştu:\n" + ex.Message,
            "Dosya Hatası");
    }
}

private void SeraEkle(object? sender, EventArgs e)
{
    string seraAdi = seraAdiKutusu.Text.Trim();

    if (string.IsNullOrWhiteSpace(seraAdi))
    {
        MessageBox.Show(
            "Lütfen bir sera adı gir.",
            "Eksik Bilgi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        return;
    }

    if (seralar.Any(s => s.Equals(
        seraAdi, StringComparison.OrdinalIgnoreCase)))
    {
        MessageBox.Show(
            "Bu sera zaten mevcut.",
            "Bilgi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        return;
    }

    seralar.Add(seraAdi);
    seraListesi.Items.Add(seraAdi);
    seraListesi.SelectedItem = seraAdi;
    seraAdiKutusu.Clear();

    try
    {
        string json = JsonSerializer.Serialize(
            seralar,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(dosyaYolu, json);
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            "Seralar kaydedilirken bir hata oluştu:\n" + ex.Message,
            "Kayıt Hatası");
    }
}private void SeraSil(object? sender, EventArgs e)
{
    if (seraListesi.SelectedItem == null)
    {
        MessageBox.Show("Lütfen silmek için bir sera seç.");
        return;
    }

    string seraAdi = seraListesi.SelectedItem.ToString()!;

    var cevap = MessageBox.Show(
        $"'{seraAdi}' adlı serayı silmek istiyor musun?",
        "Sera Silme Onayı",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

    if (cevap != DialogResult.Yes)
        return;

    seralar.Remove(seraAdi);
    seraListesi.Items.Remove(seraAdi);

    try
    {
        string json = JsonSerializer.Serialize(
            seralar,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(dosyaYolu, json);
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            "Sera kaydedilirken bir hata oluştu:\n" + ex.Message,
            "Kayıt Hatası");
    }
}

    private void OtomasyonuAc(object? sender, EventArgs e)
    {
        if (seraListesi.SelectedItem == null)
        {
            MessageBox.Show("Lütfen önce bir sera seç.");
            return;
        }

        if (!sulamaSecimi.Checked && !havalandirmaSecimi.Checked)
        {
            MessageBox.Show("Lütfen en az bir makine seç.");
            return;
        }

        using var otomasyonFormu = new OtomasyonFormu(
            seraListesi.SelectedItem.ToString()!,
            sulamaSecimi.Checked,
            havalandirmaSecimi.Checked);

        otomasyonFormu.ShowDialog();
    }
}
