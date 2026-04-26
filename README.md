🧩 3x3 Sliding Puzzle (8-Puzzle) Game in C
Bu proje, C programlama dili kullanılarak geliştirilmiş konsol tabanlı bir 8-Puzzle oyunudur. Proje, İzmir Bakırçay Üniversitesi Bilgisayar Mühendisliği Bölümü BİL1203 - Mühendislikte Proje Yönetimi dersi kapsamında geliştirilmiştir.

📝 Proje Hakkında
Oyun, 3x3'lük bir matris üzerinde 1'den 8'e kadar olan sayıların rastgele karıştırılmasıyla başlar. Amaç, boş hücreyi kullanarak sayıları 1'den 8'e kadar sıralı hale getirmektir.

Temel Özellikler:
Matematiksel Doğruluk: Oyun, sadece çözülebilir (solvable) tablolar üretir. (Inversion Count algoritması kullanılmıştır).

Hata Kontrolü: Kullanıcı harf veya geçersiz bir karakter girdiğinde sistem çökmez, giriş tamponunu (input buffer) temizleyerek yeni giriş bekler.

Dinamik Takip: Oyun sırasında toplam hamle sayısı anlık olarak gösterilir.

Modüler Yapı: Kod; karıştırma, kontrol, hareket ve çizim gibi mantıksal parçalara (fonksiyonlara) bölünerek yazılmıştır.

🚀 Nasıl Çalıştırılır?
Projeyi bilgisayarınızda çalıştırmak için bir C derleyicisine (GCC, Clang, Visual Studio vb.) ihtiyacınız vardır.

Bu depoyu klonlayın veya kodu indirin.

Terminali/Komut satırını açın.

Aşağıdaki komutları kullanarak derleyin ve çalıştırın:

Bash
gcc main.c -o puzzle
./puzzle
🛠️ Kullanılan Teknolojiler ve Algoritmalar
Dil: C

Veri Yapısı: 2D Array (Matris)

Karıştırma: Fisher-Yates Shuffle Algoritması

Mantık: Durum tabanlı kontrol (State-based control) ve Pointer kullanımı.

🕹️ Nasıl Oynanır?
Oyun başladığında karşınıza karışık bir tablo gelir.

Hareket ettirmek istediğiniz sayıyı (1-8 arası) girip Enter tuşuna basın.

Sadece boşluğun yanındaki (sağ, sol, üst, alt) sayılar hareket edebilir.

Sayıları şu düzene getirdiğinizde oyunu kazanırsınız:

 1  2  3 
 4  5  6 
 7  8    
