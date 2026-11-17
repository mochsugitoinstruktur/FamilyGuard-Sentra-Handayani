# 🛡️ FamilyGuard - Perlindungan Konten Dewasa & Radikalisme untuk Anak

**FamilyGuard** adalah aplikasi desktop Windows yang dirancang khusus untuk membatasi dan melindungi pengguna komputer (terutama anak-anak di bawah 17 tahun) agar tidak dapat mengakses konten dewasa (18+), pornografi, materi radikalisme/terorisme/ekstremisme, kekerasan, serta alat bypass (proxy/VPN web).

---

### 👨‍🏫 Informasi Pembuat Aplikasi:
* **Nama Pembuat** : **Moch. Sugito S.T**
* **Jabatan**      : **Instruktur Ahli Pertama**
* **NIP**          : **199205262025061002**

---

## ✨ Fitur & Mekanisme 5 Lapis Perlindungan

1. **Lapis 1: DNS Family Shield (Jaringan)**
   - Mengalihkan jalur lalu lintas domain ke server DNS perlindungan keluarga terpercaya:
     - **CleanBrowsing Family Filter (Rekomendasi)**: Memblokir konten dewasa, situs radikal, judi, phishing, serta otomatis menyalakan SafeSearch.
     - **Cloudflare Family (1.1.1.3)**: Proteksi ultra-cepat terhadap malware dan situs dewasa 18+.
     - **AdGuard Family Protection**: Memblokir konten tidak senonoh, iklan dewasa, dan pencarian aman.
2. **Lapis 2: Wajib SafeSearch (Mesin Pencari & YouTube)**
   - Mengunci mesin pencari utama (**Google, YouTube, Bing, DuckDuckGo**) pada mode **SafeSearch Ketat**.
   - Menghilangkan gambar/video dewasa dan radikal dari hasil pencarian, dan **tetap aktif meskipun pengguna menggunakan mode Incognito / Private Window**.
3. **Lapis 3: Pemblokiran Sistem Windows (File Hosts)**
   - Memblokir langsung di tingkat sistem ratusan portal terlarang:
     - **Radikalisme, Terorisme & Hate Speech**: Situs propaganda kelompok radikal, forum kebencian, portal kekerasan ekstrem.
     - **Konten Dewasa & Pornografi**: Jaringan situs pornografi global & lokal terbesar.
     - **Web Proxy & Unblocker**: Memblokir situs web proxy gratisan (kproxy, hide.me, croxyproxy, dll) yang sering dipakai remaja untuk mem-bypass filter.
4. **Lapis 4: Kunci Keamanan PIN Orang Tua (Parental PIN)**
   - Pengaturan dapat dikunci dengan PIN (4–8 digit) berenkripsi aman (SHA-256 + Salt).
   - Pengguna/anak tidak bisa mengubah pengaturan, menghapus blokir, atau mematikan proteksi tanpa memasukkan PIN.
5. **Lapis 5: Daftar Blokir Kustom Orang Tua**
   - Orang tua dapat dengan mudah menambahkan alamat web apa pun yang ingin diblokir secara manual (misalnya forum tertentu, media sosial, atau situs game).

---

## 🚀 Cara Menjalankan Aplikasi

1. Buka folder:
   ```
   C:\Users\humas\.gemini\antigravity\scratch\FamilyGuard
   ```
2. Klik kanan pada file **`FamilyGuard.exe`** dan pilih **"Run as administrator"** (Jalankan sebagai administrator).
   > *Catatan: Hak Administrator dibutuhkan oleh Windows untuk mengizinkan perubahan konfigurasi DNS dan file proteksi sistem.*
3. Pada jendela aplikasi:
   - Pilih profil DNS (disarankan tetap pada **CleanBrowsing Family**).
   - Pastikan centang **SafeSearch** dan **Blokir Radikalisme & 18+** dalam kondisi aktif.
   - Klik tombol hijau **"Aktifkan Proteksi"**.
   - Buka tab **"Keamanan PIN"** lalu klik **"Pasang PIN Orang Tua"** agar anak tidak bisa mematikan proteksi ini.

---

## 🛠️ Kompilasi Ulang (Opsional)

Aplikasi dibuat menggunakan C# murni tanpa ketergantungan library pihak ketiga. Jika Anda mengubah kode sumber, cukup jalankan:
```bat
build.bat
```
Script akan otomatis mengompilasi ulang menggunakan compiler bawaan Windows (`csc.exe`) menjadi file `FamilyGuard.exe` yang sangat ringan.
