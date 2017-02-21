using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace N2L_Need_2_Log.core
{
    static class Cript
    {
        public static string ComputeHash(string plainText, byte[] salt)
        {
            int minSaltLength = 4;
            int maxSaltLength = 16;
            byte[] SaltBytes = null;

            if (salt != null)
            {
                SaltBytes = salt;
            }
            else
            {
                Random r = new Random();
                int SaltLength = r.Next(minSaltLength, maxSaltLength);
                SaltBytes = new byte[SaltLength];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetNonZeroBytes(SaltBytes);
                rng.Dispose();
            }
            byte[] plainData = ASCIIEncoding.UTF8.GetBytes(plainText);
            byte[] plainDataAndSalt = new byte[plainData.Length + SaltBytes.Length];

            for (int i = 0; i < plainData.Length; i++)
                plainDataAndSalt[i] = plainData[i];
            for (int i = 0; i < SaltBytes.Length; i++)
                plainDataAndSalt[plainData.Length + i] = SaltBytes[i];

            byte[] hashValue = null;
            SHA256Managed sha = new SHA256Managed();
            hashValue = sha.ComputeHash(plainDataAndSalt);
            sha.Dispose();
            byte[] result = new byte[hashValue.Length + SaltBytes.Length];
            for (int i = 0; i < hashValue.Length; i++)
                result[i] = hashValue[i];
            for (int i = 0; i < SaltBytes.Length; i++)
                result[hashValue.Length + i] = SaltBytes[i];

            return Convert.ToBase64String(result);
        }
        public static bool Confirm(string plainText, string hashValue)
        {
            byte[] hashBytes = Convert.FromBase64String(hashValue);
            int hashSize = 32;
            byte[] saltBytes = new byte[hashBytes.Length - hashSize];
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashBytes[hashSize + i];
            string newHash = ComputeHash(plainText, saltBytes);
            return String.Equals(newHash, hashValue);
        }
        public static string generateRandomly(int length, int nNAC)
        {
            const string ANC = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const string NANC = "!@#$%^&*()_-+=[{]};:<>|./?";
            Random r = new Random();
            char[] result = new char[length];
            char[] tmpNanc = null;
            char[] tmpAnc = null;

            if (nNAC != 0)
            {
                tmpNanc = new char[nNAC];
                for (int i = 0; i < tmpNanc.Length; i++)
                    tmpNanc[i] = NANC[r.Next() % NANC.Length];
            }
            if (length != nNAC)
            {
                tmpAnc = new char[length - nNAC];
                for (int i = 0; i < tmpAnc.Length; i++)
                    tmpAnc[i] = ANC[r.Next() % ANC.Length];
            }

            for (int i = 0, rc; i < length; i++)
            {
                if (tmpNanc == null && tmpAnc != null)
                {
                    int ind = r.Next() % tmpAnc.Length;
                    result[i] = tmpAnc[ind];
                    string tmp = new string(tmpAnc).Remove(ind, 1);
                    tmpAnc = null;
                    tmp.CopyTo(0, (tmpAnc = new char[tmp.Length]), 0, tmp.Length);
                }
                else if (tmpNanc != null && tmpAnc == null)
                {
                    int ind = r.Next() % tmpNanc.Length;
                    result[i] = tmpNanc[ind];
                    string tmp = new string(tmpNanc).Remove(ind, 1);
                    tmpNanc = null;
                    tmp.CopyTo(0, (tmpNanc = new char[tmp.Length]), 0, tmp.Length);
                }
                else if (tmpNanc != null && tmpAnc != null)
                {
                    rc = r.Next(0, length);
                    if (tmpAnc.Length == 0)
                    { //NON ALPHANUMERIC CHARACTER
                        int ind = r.Next() % tmpNanc.Length;
                        result[i] = tmpNanc[ind];
                        string tmp = new string(tmpNanc).Remove(ind, 1);
                        tmpNanc = null;
                        tmp.CopyTo(0, (tmpNanc = new char[tmp.Length]), 0, tmp.Length);
                    }
                    else if (tmpNanc.Length == 0)
                    { //ALPHANUMERIC CHARACTER
                        int ind = r.Next() % tmpAnc.Length;
                        result[i] = tmpAnc[ind];
                        string tmp = new string(tmpAnc).Remove(ind, 1);
                        tmpAnc = null;
                        tmp.CopyTo(0, (tmpAnc = new char[tmp.Length]), 0, tmp.Length);
                    }
                    else if (tmpAnc.Length != 0 && tmpNanc.Length != 0)
                    {
                        if (rc % 2 != 0)
                        { //NON ALPHANUMERIC CHARACTER
                            int ind = r.Next() % tmpNanc.Length;
                            result[i] = tmpNanc[ind];
                            string tmp = new string(tmpNanc).Remove(ind, 1);
                            tmpNanc = null;
                            tmp.CopyTo(0, (tmpNanc = new char[tmp.Length]), 0, tmp.Length);
                        }
                        else
                        { //ALPHANUMERIC CHARACTER
                            int ind = r.Next() % tmpAnc.Length;
                            result[i] = tmpAnc[ind];
                            string tmp = new string(tmpAnc).Remove(ind, 1);
                            tmpAnc = null;
                            tmp.CopyTo(0, (tmpAnc = new char[tmp.Length]), 0, tmp.Length);
                        }
                    }
                }
                else
                {
                    return null;
                }

            }
            return new string(result);
            //System.Security.Cryptography.PasswordDeriveBytes()
        }
    }
}