namespace PsVDecrypt.Domain
{
    public class VideoEncryption
    {
        public static string string1_v2 = "pluralsight";
        public static string string2_v2 = "#©>Å£Q\x0005¤°";
        private static int currentClipReadCrypto;
        internal static readonly string[][] CryptoKeys = new string[4][]
        {
            new string[2]
            {
                "pluralsight",
                "\x0006?zY¢\x00B2\x0085\x009FL\x00BEî0Ö.ì\x0017#©>Å£Q\x0005¤°\x00018Þ^\x008Eú\x0019Lqß'\x009D\x0003ßE\x009EM\x0080'x:\0~\x00B9\x0001ÿ 4\x00B3õ\x0003Ã§Ê\x000EAË\x00BC\x0090è\x009Eî~\x008B\x009Aâ\x001B¸UD<\x007FKç*\x001Döæ7H\v\x0015Arý*v÷%Âþ\x00BEä;pü"
            },
            new string[2]
            {
                string1_v2,
                string2_v2
            },
            new string[1]
            {
                "os22$!sKHyy9jnGlgHB&vP21CK96tx!l2uhK1K%Fbubree9%o0wT44zwvJ446iAdA%M!@RopKCmOWMOqTt1*BIw@lF68x3itctw"
            },
            new string[1]
            {
                "XlmDvIlD*^uyZAfCMZ%M0h#o6Z7!4eMZZSBs@dZ12%rMvubV#2iFJLfh8@LSyVWhu37#b%z3MCF3u4244%SRMBO@zIG2YEi!i6y"
            }
        };
        public static string String1V2
        {
            get
            {
                return string1_v2;
            }
            set
            {
                CryptoKeys[1][0] = value;
                string1_v2 = value;
            }
        }
        public static string String2V2
        {
            get
            {
                return string2_v2;
            }
            set
            {
                CryptoKeys[1][1] = value;
                string2_v2 = value;
            }
        }


        public static void DecryptBuffer(byte[] buff, int length, long position)
        {
            if (position == 0L)
            {
                for (int index1 = CryptoKeys.Length - 1; index1 >= 0; --index1)
                {
                    currentClipReadCrypto = index1;
                    XorBuffer(buff, length, position);
                    bool flag = (uint)buff.Length > 0U;
                    for (int index2 = 0; index2 < buff.Length && index2 < 3; ++index2)
                        flag = flag && buff[index2] == 0;
                    if (flag)
                        return;
                    XorBuffer(buff, length, position);
                }
            }
            XorBuffer(buff, length, position);
        }
        private static void XorBuffer(byte[] buff, int length, long position)
        {
            XorBuffer(currentClipReadCrypto, buff, length, position);
        }

        internal static void XorBuffer(int key, byte[] buff, int length, long position)
        {
            for (int index1 = 0; index1 < length; ++index1)
            {
                string[] cryptoKey = CryptoKeys[key];
                string str1 = cryptoKey[0];
                int num1 = (int)position + index1;
                char ch = str1[num1 % str1.Length];
                for (int index2 = 1; index2 < cryptoKey.Length; ++index2)
                {
                    string str2 = cryptoKey[index2];
                    ch ^= str2[num1 % str2.Length];
                }
                int num2 = ch ^ num1 % 251;
                buff[index1] = (byte)(buff[index1] ^ (uint)num2);
            }
        }
    }
}
