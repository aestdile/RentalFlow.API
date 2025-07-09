# RentalFlow.API

RentalFlow.API - turar-joy savdosi va ijara boshqaruvi uchun zamonaviy RESTful API xizmati. Ushbu loyiha uy-joylarni sotish, ijaraga berish va boshqarish jarayonlarini avtomatlashtirish uchun mo'ljallangan.

## 📋 Xususiyatlar

- **Mulk boshqaruvi**: Uy-joylarni qo'shish, tahrirlash va o'chirish
- **Ijara boshqaruvi**: Ijara shartnomalarini yaratish va kuzatish
- **Foydalanuvchi autentifikatsiyasi**: JWT asosida xavfsiz autentifikatsiya
- **Qidiruv va filtrlash**: Kengaytirilgan qidiruv imkoniyatlari
- **Hisob-kitoblar**: Ijara to'lovlari va hisobotlar
- **Xavfsizlik**: Zamonaviy xavfsizlik standartlari

## 🏗️ Arxitektura

Loyiha Clean Architecture prinsipiga muvofiq tashkil etilgan:

```
RentalFlow.API/
├── RentalFlow.API.Application/     # Biznes logika
├── RentalFlow.API.Domain/          # Domain modellari
├── RentalFlow.API.Infrastructure/  # Ma'lumotlar bazasi va tashqi xizmatlar
├── RentalFlow.API.Tests/          # Test fayllar
└── RentalFlow.IntegrationTests/   # Integratsiya testlari
```

## 🚀 Boshlash

### Talablar

- .NET 8.0 SDK
- SQL Server (LocalDB yoki to'liq versiya)
- Visual Studio 2022 yoki VS Code

### O'rnatish

1. **Loyihani klonlash**
   ```bash
   git clone https://github.com/aestdile/RentalFlow.API.git
   cd RentalFlow.API
   ```

2. **Paketlarni o'rnatish**
   ```bash
   dotnet restore
   ```

3. **Ma'lumotlar bazasini sozlash**
   ```bash
   dotnet ef database update
   ```

4. **Loyihani ishga tushirish**
   ```bash
   dotnet run --project RentalFlow.API
   ```

## 🔧 Konfiguratsiya

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RentalFlowDB;Trusted_Connection=true;"
  }
}
```

## 📚 API Endpoints

### Mulklar
- `GET /api/properties` - Barcha mulklarni olish
- `GET /api/properties/{id}` - Mulkni ID orqali olish
- `POST /api/properties` - Yangi mulk qo'shish
- `PUT /api/properties/{id}` - Mulkni yangilash
- `DELETE /api/properties/{id}` - Mulkni o'chirish

### Ijara
- `GET /api/rentals` - Ijara shartnomalarini olish
- `POST /api/rentals` - Yangi ijara shartnomasi yaratish
- `PUT /api/rentals/{id}` - Shartnomani yangilash
- `DELETE /api/rentals/{id}` - Shartnomani bekor qilish

## 🧪 Testlar

### Unit testlarni ishga tushirish
```bash
dotnet test RentalFlow.API.Tests/
```

### Integratsiya testlarni ishga tushirish
```bash
dotnet test RentalFlow.IntegrationTests/
```

### Test qamrovi
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Migratsiya yaratish
```bash
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

## 🔐 Xavfsizlik

- HTTPS majburiy ravishda yoqilgan
- Ma'lumotlar validatsiyasi
- SQL injection himoyasi
- CORS sozlamalari

## 🤝 Hissa qo'shish

1. Loyihani fork qiling
2. Yangi branch yarating (`git checkout -b feature/new-feature`)
3. O'zgarishlaringizni commit qiling (`git commit -am 'Add new feature'`)
4. Branch ni push qiling (`git push origin feature/new-feature`)
5. Pull Request yarating

## ✍️ Muallif
👤 Mukhtor Eshboyev\
🔗 GitHub: [@aestdile](https://github.com/aestdile)\

## 📞 Aloqa

Savollar yoki takliflar uchun:
- Issue yarating
- Telegram: [@aestdile](https://t.me/aestdile)
- Email: aestdile@gmail.com

---

⭐ Agar loyiha foydali bo'lsa, star bosishni unutmang!

## 🌐 Social Networks

<div align="center">
  <a href="https://t.me/aestdile"><img src="https://img.shields.io/badge/Telegram-2CA5E0?style=for-the-badge&logo=telegram&logoColor=white" /></a>
  <a href="https://github.com/aestdile"><img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white" /></a>
  <a href="https://leetcode.com/aestdile"><img src="https://img.shields.io/badge/LeetCode-FFA116?style=for-the-badge&logo=leetcode&logoColor=black" /></a>
  <a href="https://linkedin.com/in/aestdile"><img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white" /></a>
  <a href="https://youtube.com/@aestdile"><img src="https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white" /></a>
  <a href="https://instagram.com/aestdile"><img src="https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=instagram&logoColor=white" /></a>
  <a href="https://facebook.com/aestdile"><img src="https://img.shields.io/badge/Facebook-1877F2?style=for-the-badge&logo=facebook&logoColor=white" /></a>
  <a href="mailto:aestdile@gmail.com"><img src="https://img.shields.io/badge/Gmail-D14836?style=for-the-badge&logo=gmail&logoColor=white" /></a>
  <a href="https://twitter.com/aestdile"><img src="https://img.shields.io/badge/Twitter-1DA1F2?style=for-the-badge&logo=twitter&logoColor=white" /></a>
  <a href="tel:+998772672774"><img src="https://img.shields.io/badge/Phone:+998772672774-25D366?style=for-the-badge&logo=whatsapp&logoColor=white" /></a>
</div>
