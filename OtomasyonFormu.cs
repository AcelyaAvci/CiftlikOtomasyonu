
namespace CiftlikOtomasyonu;

public class OtomasyonFormu : Form
{
    private readonly bool sulamaVar;
    private readonly bool havalandirmaVar;

    private bool sulamaCalisiyor;
    private bool havalandirmaCalisiyor;

    private Label sulamaDurumu = new();
    private Label havalandirmaDurumu = new();

    private NumericUpDown sulamaSuresi = new();
    private NumericUpDown havalandirmaSuresi = new();

    private Label sulamaGeriSayim = new();
    private Label havalandirmaGeriSayim = new();

    private readonly System.Windows.Forms.Timer sulamaZamanlayici = new();
    private readonly System.Windows.Forms.Timer havalandirmaZamanlayici = new();

    private int sulamaKalanSaniye;
    private int havalandirmaKalanSaniye;

    public OtomasyonFormu(
        string seraAdi,
        bool sulamaSecili,
        bool havalandirmaSecili)
    {
        sulamaVar = sulamaSecili;
        havalandirmaVar = havalandirmaSecili;

        Text = "Çiftlik Otomasyonu - Kontrol";
        Size = new Size(600, 470);
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(245, 248, 242);
        Font = new Font("Segoe UI", 10);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        var baslik = new Label
        {
            Text = seraAdi + " Kontrol Paneli",
            Font = new Font("Segoe UI", 16, FontStyle.Bold),
            ForeColor = Color.DarkGreen,
            Location = new Point(25, 20),
            AutoSize = true
        };
        Controls.Add(baslik);

        MakineKontroluEkle(
            "Sulama Sistemi", sulamaVar, 75,
            out sulamaDurumu, out sulamaSuresi,
            out sulamaGeriSayim,
            BaslatSulama, DurdurSulama);

        MakineKontroluEkle(
            "Havalandırma", havalandirmaVar, 225,
            out havalandirmaDurumu, out havalandirmaSuresi,
            out havalandirmaGeriSayim,
            BaslatHavalandirma, DurdurHavalandirma);

        sulamaZamanlayici.Interval = 1000;
        sulamaZamanlayici.Tick += (_, _) =>
        {
            sulamaKalanSaniye--;
            GeriSayimiGuncelle(sulamaGeriSayim, sulamaKalanSaniye);

            if (sulamaKalanSaniye <= 0)
            {
                DurdurSulama();
                sulamaGeriSayim.Text = "Süre doldu — otomatik durdu";
            }
        };

        havalandirmaZamanlayici.Interval = 1000;
        havalandirmaZamanlayici.Tick += (_, _) =>
        {
            havalandirmaKalanSaniye--;
            GeriSayimiGuncelle(havalandirmaGeriSayim, havalandirmaKalanSaniye);

            if (havalandirmaKalanSaniye <= 0)
            {
                DurdurHavalandirma();
                havalandirmaGeriSayim.Text = "Süre doldu — otomatik durdu";
            }
        };
    }

    private void MakineKontroluEkle(
        string makineAdi,
        bool secili,
        int y,
        out Label durumEtiketi,
        out NumericUpDown sureSecimi,
        out Label geriSayim,
        Action baslat,
        Action durdur)
    {
        var grup = new GroupBox
        {
            Text = makineAdi,
            Location = new Point(25, y),
            Size = new Size(530, 135),
            Enabled = secili
        };
        Controls.Add(grup);

        durumEtiketi = new Label
        {
            Text = "Durum: Durduruldu",
            Location = new Point(15, 25),
            Size = new Size(190, 25),
            ForeColor = Color.Firebrick
        };
        grup.Controls.Add(durumEtiketi);

        var sureEtiketi = new Label
        {
            Text = "Süre (dakika):",
            Location = new Point(15, 60),
            AutoSize = true
        };
        grup.Controls.Add(sureEtiketi);

        sureSecimi = new NumericUpDown
        {
            Location = new Point(125, 57),
            Size = new Size(75, 28),
            Minimum = 1,
            Maximum = 120,
            Value = 2
        };
        grup.Controls.Add(sureSecimi);

        var baslatButonu = new Button
        {
            Text = "▶ Başlat",
            Location = new Point(220, 52),
            Size = new Size(105, 35),
            BackColor = Color.ForestGreen,
            ForeColor = Color.White
        };
        baslatButonu.Click += (_, _) => baslat();
        grup.Controls.Add(baslatButonu);

        var durdurButonu = new Button
        {
            Text = "■ Durdur",
            Location = new Point(340, 52),
            Size = new Size(105, 35),
            BackColor = Color.Firebrick,
            ForeColor = Color.White
        };
        durdurButonu.Click += (_, _) => durdur();
        grup.Controls.Add(durdurButonu);

        geriSayim = new Label
        {
            Text = "Kalan süre: --:--",
            Location = new Point(15, 98),
            Size = new Size(400, 25),
            ForeColor = Color.DarkSlateGray
        };
        grup.Controls.Add(geriSayim);
    }

    private void BaslatSulama()
    {
        sulamaZamanlayici.Stop();

        sulamaKalanSaniye = (int)sulamaSuresi.Value * 60;
        sulamaCalisiyor = true;

        sulamaDurumu.Text = "Durum: Çalışıyor";
        sulamaDurumu.ForeColor = Color.DarkGreen;

        GeriSayimiGuncelle(sulamaGeriSayim, sulamaKalanSaniye);
        sulamaZamanlayici.Start();
    }

    private void DurdurSulama()
    {
        sulamaZamanlayici.Stop();
        sulamaCalisiyor = false;
        sulamaDurumu.Text = "Durum: Durduruldu";
        sulamaDurumu.ForeColor = Color.Firebrick;
    }

    private void BaslatHavalandirma()
    {
        havalandirmaZamanlayici.Stop();

        havalandirmaKalanSaniye = (int)havalandirmaSuresi.Value * 60;
        havalandirmaCalisiyor = true;

        havalandirmaDurumu.Text = "Durum: Çalışıyor";
        havalandirmaDurumu.ForeColor = Color.DarkGreen;

        GeriSayimiGuncelle(havalandirmaGeriSayim, havalandirmaKalanSaniye);
        havalandirmaZamanlayici.Start();
    }

    private void DurdurHavalandirma()
    {
        havalandirmaZamanlayici.Stop();
        havalandirmaCalisiyor = false;
        havalandirmaDurumu.Text = "Durum: Durduruldu";
        havalandirmaDurumu.ForeColor = Color.Firebrick;
    }

    private void GeriSayimiGuncelle(Label etiket, int saniye)
    {
        int dakika = Math.Max(0, saniye) / 60;
        int kalan = Math.Max(0, saniye) % 60;

        etiket.Text = $"Kalan süre: {dakika:00}:{kalan:00}";
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        sulamaZamanlayici.Stop();
        havalandirmaZamanlayici.Stop();

        sulamaZamanlayici.Dispose();
        havalandirmaZamanlayici.Dispose();

        base.OnFormClosed(e);
    }
}
