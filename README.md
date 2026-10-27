# 🌱 Çiftlik Otomasyonu

Çiftlik Otomasyonu, sera yönetimini kolaylaştırmak amacıyla geliştirilmiş, C# ve Windows Forms kullanılarak hazırlanmış bir masaüstü uygulamasıdır.

Proje kapsamında seraların yönetilmesi, sulama ve havalandırma sistemlerinin kontrol edilmesi ve bu sistemler için çalışma süresi belirlenmesi amaçlanmıştır.

## ✨ Özellikler

- **Sera yönetimi:** Yeni sera ekleme, sera seçme ve sera silme.
- **Kalıcı kayıt:** Sera bilgilerinin JSON dosyasında saklanması.
- **Makine kontrolü:** Sulama ve havalandırma sistemlerini ayrı ayrı başlatma ve durdurma.
- **Süre ayarı:** Makineler için dakika cinsinden çalışma süresi belirleme.
- **Geri sayım:** Çalışma sırasında kalan sürenin gösterilmesi.
- **Otomatik durdurma:** Belirlenen süre tamamlandığında sistemin otomatik olarak durdurulması.
- **Kullanıcı arayüzü:** İşlemlerin Windows Forms üzerinden gerçekleştirilmesi.

## 🛠️ Kullanılan Teknolojiler

- C#
- .NET 10
- Windows Forms
- JSON
- Visual Studio Code

## ▶️ Kurulum ve Çalıştırma

1. Bilgisayarınızda Windows ve uyumlu bir .NET SDK kurulu olmalıdır.
2. Proje klasörünü açın.
3. Terminal üzerinden proje klasörüne gidin.
4. Aşağıdaki komutu çalıştırın:

```bash
dotnet run
```

## 💾 Veri Saklama

Sera bilgileri `seralar.json` dosyasında saklanır. Bu sayede uygulama kapatılıp yeniden açıldığında kayıtlı sereler tekrar yüklenebilir.

## 📌 Proje Hakkında

Bu proje, C# programlama, Windows Forms ile masaüstü arayüz geliştirme, dosya işlemleri ve zamanlayıcı kullanımı konularında uygulamalı deneyim kazanmak amacıyla geliştirilmiştir.

**Not:** Sulama ve havalandırma kontrolleri mevcut sürümde yazılım simülasyonu olarak çalışır. Gerçek fiziksel cihaz bağlantısı bulunmamaktadır.
