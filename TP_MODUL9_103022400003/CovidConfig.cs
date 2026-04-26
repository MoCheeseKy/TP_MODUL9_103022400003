using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace TP_MODUL9_103022400003
{
    internal class CovidConfig
    {
        private static readonly Dictionary<string, string> _defaults = new()
        {
            { "CONFIG1", "celsius" },
            { "CONFIG2", "14" },
            { "CONFIG3", "Anda tidak diperbolehkan masuk ke dalam gedung ini" },
            { "CONFIG4", "Anda dipersilahkan untuk masuk ke dalam gedung ini" }
        };

        private Dictionary<string, string> _configMap;

        public CovidConfig()
        {
            _configMap = new Dictionary<string, string>();

            try
            {
                string json = File.ReadAllText("covid_config.json");
                var raw = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                foreach (var entry in raw!)
                {
                    if (_defaults.TryGetValue(entry.Value, out string? resolved))
                        _configMap[entry.Key] = resolved;
                    else
                        _configMap[entry.Key] = entry.Value;
                }
            }
            catch
            {
                _configMap["satuan_suhu"] = _defaults["CONFIG1"];
                _configMap["batas_hari_deman"] = _defaults["CONFIG2"];
                _configMap["pesan_ditolak"] = _defaults["CONFIG3"];
                _configMap["pesan_diterima"] = _defaults["CONFIG4"];
            }
        }

        public string Get(string key)
        {
            return _configMap.TryGetValue(key, out string? val) ? val : string.Empty;
        }

        public double UbahSatuan(double suhu)
        {
            if (_configMap["satuan_suhu"] == "celsius")
            {
                _configMap["satuan_suhu"] = "fahrenheit";
                return (suhu * 9 / 5) + 32; 
            }
            else
            {
                _configMap["satuan_suhu"] = "celsius";
                return (suhu - 32) * 5 / 9; 
            }
        }
    }
}