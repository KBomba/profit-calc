using System;

namespace CudaProfitCalc
{
    static class HashAlgo
    {
        public enum Algo
        {
            Scrypt,
            Keccak,
            Heavy,
            Fugue256,
            Groestl,
            MyriadGroestl,
            JHA,
            Quark,
            Nist5,
            X11,
            X13,
            Unknown
        } ;

        public static Algo GetAlgorithm(string algo)
        {
            Algo miningAlgo;
            if (Enum.TryParse(algo, out miningAlgo))
            {
                return miningAlgo;
            }

            string proper = algo.Trim().ToLowerInvariant();
            switch (proper)
            {
                case "heavycoin":
                    return Algo.Heavy;
                case "myr-gr":
                    return Algo.MyriadGroestl;
                case "jackpot":
                    return Algo.JHA;
                case "nist5":
                    return Algo.Nist5;
                default:
                    return Algo.Unknown;
            }
        }

        public static string GetAlgorithm(Algo algo)
        {
            switch (algo)
            {
                case Algo.MyriadGroestl:
                    return "myr-gr";
                case Algo.JHA:
                    return "jackpot";
                default:
                    return algo.ToString().ToLowerInvariant();
            }
        }
    }
}
