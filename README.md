# Anpak

Framework instalasi aplikasi berbasis DotNet 9

## Deskripsi

Anpak adalah aplikasi packaging untuk aplikasi berbasis .NET 9. Aplikasi ini memungkinkan Anda untuk mengemas aplikasi .NET 9 ke dalam format yang siap untuk didistribusikan.

## Persyaratan

- .NET 9 SDK

## Instalasi

Clone repository ini dan build project:

```bash
git clone https://github.com/aandahliansyah/anpak.git
cd anpak
dotnet build
```

## Penggunaan

### Mengemas Aplikasi

Untuk mengemas aplikasi .NET 9:

```bash
dotnet run --project src/Anpak/Anpak.csproj -- pack <path-to-project.csproj>
```

Contoh:

```bash
dotnet run --project src/Anpak/Anpak.csproj -- pack MyApp/MyApp.csproj
```

### Menampilkan Bantuan

```bash
dotnet run --project src/Anpak/Anpak.csproj -- help
```

## Fitur

- 🏗️ Build otomatis aplikasi .NET 9
- 📦 Publish aplikasi ke format distribusi
- ✅ Validasi project file
- 🔍 Error handling yang informatif

## Pengembangan

### Menjalankan Tests

```bash
dotnet test
```

### Build Project

```bash
dotnet build
```

## Lisensi

MIT License - lihat file [LICENSE](LICENSE) untuk detail.

