# 📚 ASP.NET Core MVC Book Shopping Cart

Bu proje, **ASP.NET Core MVC** kullanılarak geliştirilmiş bir **kitap alışveriş sepeti uygulamasıdır**.  
Uygulama; kullanıcı yönetimi, rol bazlı yetkilendirme ve temel e-ticaret senaryolarını kapsar.

---

## 🚀 Özellikler

- 📖 Kitap listeleme ve detay sayfaları
- 🛒 Alışveriş sepeti sistemi
- 👤 ASP.NET Core Identity ile kullanıcı yönetimi
- 🔐 Rol bazlı yetkilendirme (Admin / User)
- 🧑‍💼 Admin paneli üzerinden CRUD işlemleri
- ✅ Entity Framework Core ile veritabanı işlemleri
- 🗄️ SQL Server & SQLite desteği
- 🎨 Razor Views + Bootstrap

---

## 🛠️ Kullanılan Teknolojiler

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **ASP.NET Core Identity**
- **SQL Server / SQLite**
- **Razor Pages**
- **Bootstrap**
- **Fluent Validation (Custom Validators)**

---


---

## 🔐 Varsayılan Roller & Admin Kullanıcı

Uygulama ilk çalıştırıldığında aşağıdaki roller otomatik oluşturulur:

- **Admin**
- **User**

### 👑 Admin Kullanıcısı
```txt
Email    : admin@gmail.com
Password : Admin@123
Role     : Admin



🚀 Projeyi Çalıştırma Adımları
1. Repoyu klonla
git clone https://github.com/KULLANICI_ADI/BookShoppingCard.git
cd BookShoppingCardUI

2. Gerekli paketleri yükle
dotnet restore

3. Veritabanını oluştur

Projede Entity Framework kullanıldığı için migration’ları çalıştır:

dotnet ef database update

4. (Opsiyonel) Connection string ayarla

Eğer SQL Server kullanacaksan, appsettings.json dosyasını düzenle:

"MsSqlConnection": "Server=localhost,1433;Database=booksDb;User Id=sa;Password=ŞİFREN;"


SQLite kullanıyorsan ekstra ayar yapmana gerek yok.

5. Projeyi çalıştır
dotnet run


Tarayıcıda aç:

https://localhost:5001

6. Admin girişi

Uygulama ilk çalıştığında otomatik olarak admin kullanıcı oluşturulur:

Email: admin@gmail.com
Password: Admin@123


## Proje Ekran Görüntüleri

<img width="700" height="300" alt="Ekran Resmi 2026-02-20 11 20 51" src="https://github.com/user-attachments/assets/5bf5cf6d-45ec-4baa-ba87-793249cd723d" />



