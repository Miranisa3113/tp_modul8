using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    public string satuan_suhu { get; set; }
    public string batas_hari_deman { get; set; } 
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }

    public static CovidConfig LoadConfig()
    {
        string filePath = "covid_config.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CovidConfig data = JsonSerializer.Deserialize<CovidConfig>(json);

            data.satuan_suhu = data.satuan_suhu == "CONFIG1" ? "celcius" : data.satuan_suhu;

            int batas = (data.batas_hari_deman == "CONFIG2") ? 14 : Convert.ToInt32(data.batas_hari_deman);

            data.pesan_ditolak = data.pesan_ditolak == "CONFIG3"
                ? "Anda tidak diperbolehkan masuk ke dalam gedung ini"
                : data.pesan_ditolak;

            data.pesan_diterima = data.pesan_diterima == "CONFIG4"
                ? "Anda dipersilahkan untuk masuk ke dalam gedung ini"
                : data.pesan_diterima;

            data.batas_hari_deman = batas.ToString();

            return data;
        }
        else
        {
            return new CovidConfig
            {
                satuan_suhu = "celcius",
                batas_hari_deman = "14",
                pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini",
                pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini"
            };
        }
    }

    public int GetBatasHari()
    {
        return Convert.ToInt32(batas_hari_deman);
    }

    public void UbahSatuan()
    {
        if (satuan_suhu == "celcius")
            satuan_suhu = "fahrenheit";
        else
            satuan_suhu = "celcius";
    }
}