using System.Collections.Generic;
using System.ComponentModel;

namespace ProfitCalc
{
    internal class Profile
    {
        public double FiatPerKwh { get; set; }

        public int FiatOfChoice { get; set; }

        public int Multiplier { get; set; }

        public BindingList<CustomAlgo> CustomAlgoList { get; set; } 
    }

    internal class ApiSettingsJson
    {
        public Dictionary<string, string> ApiSettings { get; set; }

        public Dictionary<string, bool> CheckedApis { get; set; }

        public Dictionary<string, bool> CheckedMisc { get; set; }
    }

    internal class CustomCoin
    {
        public bool Use { get; set; }
        public string Tag { get; set; }
        public string FullName { get; set; }

        public string Algo { get; set; }
        public double Difficulty { get; set; }
        public double BlockReward { get; set; }
    }

    internal class CustomAlgo
    {
        public bool Use { get; set; }
        public string Name { get; set; }
        public string SynonymsCsv { get; set; }

        public CalcStyle Style { get; set; }
        public double HashRate { get; set; }
        public double Wattage { get; set; }
    }

    public enum CalcStyle
    {
        Classic,
        Quark,
        CryptoNight,
        NetHashRate
    }
}