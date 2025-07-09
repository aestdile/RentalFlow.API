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

## 👥 Muallif

**aestdile** - [GitHub](https://github.com/aestdile)

## 📞 Aloqa

Savollar yoki takliflar uchun:
- Issue yarating
- Telegram: [@aestdile](https://t.me/aestdile)
- Email: aestdile@gmail.com

---

⭐ Agar loyiha foydali bo'lsa, star bosishni unutmang!
