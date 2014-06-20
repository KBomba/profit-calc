using System;

namespace CudaProfitCalc
{
    static class HashAlgo
    {
        public enum Algo
        {
            Scrypt,
            ScryptN,
            ScryptJane15,
            ScryptJane13,
            Keccak,
            Heavy,
            Mjollnir,
            Fugue256,
            Groestl,
            MyriadGroestl,
            DiamondGroestl,
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
            if (proper.Contains("chacha"))
            {
                if (proper.Contains("13"))
                {
                    return Algo.ScryptJane13;
                }

                return Algo.ScryptJane15;
            }
                
                
            switch (proper)
            {
                case "scrypt-n":
                case "scrypt-adaptive-nfactor":
                    return Algo.ScryptN;
                case "scrypt-jane":
                    return Algo.ScryptJane15;
                case "heavycoin":
                    return Algo.Heavy;
                case "myr-gr":
                    return Algo.MyriadGroestl;
                case "dmd-gr":
                    return Algo.DiamondGroestl;
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
                case Algo.ScryptN:
                    return "scrypt-n";
                case Algo.ScryptJane15:
                case Algo.ScryptJane13:
                    return "scrypt-jane";
                case Algo.MyriadGroestl:
                    return "myr-gr";
                case Algo.DiamondGroestl:
                    return "dmd-gr";
                case Algo.JHA:
                    return "jackpot";
                default:
                    return algo.ToString().ToLowerInvariant();
            }
        }

        public static double CalcAvgSpeed(double prev, double newSpeed)
        {
            return prev != 0 ? (prev + newSpeed) / 2 : newSpeed;
        }
    }
}
