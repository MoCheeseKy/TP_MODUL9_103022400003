using TP_MODUL9_103022400003;

CovidConfig config = new CovidConfig();

string satuan = config.Get("satuan_suhu");
int batasHari = int.Parse(config.Get("batas_hari_deman"));
string ditolak = config.Get("pesan_ditolak");
string diterima = config.Get("pesan_diterima");

Console.Write($"Berapa suhu badan anda saat ini? Dalam nilai <{satuan}>? ");
double suhu = double.Parse(Console.ReadLine()!);

Console.Write("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala deman? ");
int hariDeman = int.Parse(Console.ReadLine()!);

bool suhuOk = satuan == "celsius"
    ? suhu >= 36.5 && suhu <= 37.5
    : suhu >= 97.7 && suhu <= 99.5;
bool hariOk = hariDeman < batasHari;

if (suhuOk && hariOk)
    Console.WriteLine(diterima);
else
    Console.WriteLine(ditolak);

Console.WriteLine($"Sebelum : {suhu} {config.Get("satuan_suhu")}");
double suhuBaru = config.UbahSatuan(suhu);
Console.WriteLine($"Sesudah : {suhuBaru:F1} {config.Get("satuan_suhu")}");