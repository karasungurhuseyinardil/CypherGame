# 🎮 CyperGame - Modern Şifre Tahmin Oyunu

CyperGame, kullanıcıların gizli şifreleri tahmin etmeye çalıştığı eğlenceli ve modern bir web tabanlı oyundur. Wordle tarzı oyun mekaniği ile Türkçe karakterler ve rakamları destekler.

## 🎯 Özellikler

- **Modern UI/UX**: Glassmorphism efektleri, gradient renkler ve smooth animasyonlar
- **Virtual Keyboard**: Tam Türkçe karakter desteği ile sanal klavye
- **İpucu Sistemi**: Oyun başına maksimum 3 ipucu kullanabilme
- **Renk Kodları**: 
  - 🟢 Yeşil: Doğru harf, doğru pozisyon
  - 🟠 Turuncu: Doğru harf, yanlış pozisyon
  - 🔵 Mavi: Harf şifrede yok
- **İstatistikler**: Oyun geçmişi ve başarı oranları
- **Confetti Animasyonu**: Kazanınca kutlama efekti
- **Responsive Design**: Tüm cihazlarda sorunsuz çalışır

## 🛠️ Teknolojiler

- **Backend**: ASP.NET Core MVC (.NET 9.0)
- **Frontend**: HTML5, CSS3, JavaScript
- **Styling**: Bootstrap 5 + Custom CSS
- **Fonts**: Google Fonts (Inter)
- **Animations**: CSS Transitions & Keyframes, Confetti.js

## 📋 Gereksinimler

- .NET 9.0 SDK veya üzeri
- Herhangi bir modern web tarayıcı

## 🚀 Kurulum ve Çalıştırma

1. Repoyu klonlayın:
```bash
git clone https://github.com/[kullanici-adi]/CyperGame.git
cd CyperGame/CyperGame
```

2. Projeyi çalıştırın:
```bash
dotnet run
```

3. Tarayıcınızda aşağıdaki adresi açın:
```
http://localhost:5196
```

## 🎮 Nasıl Oynanır?

1. Oyun 5-7 karakter arası rastgele bir şifre belirler
2. Tahminlerinizi girin (harf ve rakam kullanabilirsiniz)
3. Her tahmin sonrası harflerin renkleri değişir:
   - Yeşil: Doğru harf, doğru yer
   - Turuncu: Doğru harf, yanlış yer
   - Mavi: Harf şifrede yok
4. İhtiyaç duyarsanız ipucu alabilirsiniz (her ipucu 1 tahmin hakkı götürür)
5. Şifreyi bulmaya çalışın!

## 📁 Proje Yapısı

```
CyperGame/
├── Controllers/          # MVC Controllers
│   └── GameController.cs
├── Models/              # View Models
│   ├── GameViewModel.cs
│   ├── LetterGuess.cs
│   └── GameStatistics.cs
├── Services/            # Business Logic
│   ├── IGameService.cs
│   └── GameService.cs
├── Helpers/             # Helper Extensions
│   └── SessionExtensions.cs
├── Views/               # Razor Views
│   ├── Game/
│   │   ├── Index.cshtml
│   │   └── About.cshtml
│   └── Shared/
│       └── _Layout.cshtml
├── wwwroot/             # Static Files
│   ├── css/
│   │   ├── site.css
│   │   └── custom-game.css
│   └── js/
│       └── site.js
└── Program.cs           # Application Entry Point
```

## 🎨 Özelleştirme

CSS dosyalarını düzenleyerek renk şemasını ve tasarımı özelleştirebilirsiniz:
- `wwwroot/css/custom-game.css`: Oyun spesifik stiller
- `wwwroot/css/site.css`: Genel site stilleri

## 👨‍💻 Geliştirici

Hüseyin Ardıl Karasungur

## 🤝 Katkıda Bulunma

1. Fork yapın
2. Feature branch oluşturun (`git checkout -b feature/YeniOzellik`)
3. Değişikliklerinizi commit edin (`git commit -m 'Yeni özellik eklendi'`)
4. Branch'inizi push edin (`git push origin feature/YeniOzellik`)
5. Pull Request oluşturun

---

⭐ Projeyi beğendiyseniz yıldız vermeyi unutmayın!
