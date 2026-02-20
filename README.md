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


## 🔐 Varsayılan Roller & Admin Kullanıcı

Uygulama ilk çalıştırıldığında aşağıdaki roller otomatik oluşturulur:

- **Admin**
- **User**

### 👑 Admin Kullanıcısı

Email    : admin@gmail.com
Password : Admin@123
Role     : Admin



🚀 Projeyi Çalıştırma Adımları

1. Repoyu klonla
git clone https://github.com/Rumeysapat/Book-Shopping-Cart-with-Identity-and-Roles
cd BookShoppingCardUI
2. Gerekli paketleri yükle
dotnet restore
3. Veritabanını oluştur

Projede Entity Framework kullanıldığı için migration’ları çalıştır:

dotnet ef database update
4. (Opsiyonel) Connection String Ayarı

Eğer SQL Server kullanacaksan appsettings.json dosyasını düzenle:

"MsSqlConnection": "Server=localhost,1433;Database=booksDb;User Id=sa;Password=ŞİFREN;"

👉 SQLite kullanıyorsan ekstra ayar yapmana gerek yok.

5. Projeyi çalıştır
dotnet run
6. Tarayıcıda aç
https://localhost:5001
7. Admin Girişi

Uygulama ilk çalıştığında otomatik olarak admin kullanıcı oluşturulur:

Email: admin@gmail.com

Password: Admin@123

📸 Proje Ekran Görüntüleri
![Görüntü1](https://raw.githubusercontent.com/Rumeysapat/Book-Shopping-Cart-with-Identity-and-Roles/master/wwwroot/images/Screen1.png)
<img width="1440" height="569" alt="Screen2" src="https://github.com/user-attachments/assets/11cef543-a2d3-429b-8394-3d1bec3d560a" />
<img width="1440" height="651" alt="Screen3" src="https://github.com/user-attachments/assets/880e5c65-eaa3-4d19-86d6-585b6fea9e0c" />
<img width="1440" height="651" alt="Screen4" src="https://github.com/user-attachments/assets/5e4fd74e-e16e-4d18-b96e-e33ce2f32fdb" />
<img width="1440" height="651" alt="Screen5" src="https://github.com/user-attachments/assets/94835148-4e45-43f5-ba7c-9b992f5e33b3" />




