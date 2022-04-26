using System;
using System.Security.Cryptography;
using System.Text;

namespace DDD.NHibernate.Libs.Biblioteca.Util
{
    /// <summary>
    /// Classe utilitária para criptografia do tipo MD5
    /// </summary>
    public static class CriptografiaMD5
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="chave"></param>
        /// <returns></returns>
        public static string Criptografar(string entrada, string chave)
        {
            var tripledescryptoserviceprovider = new TripleDESCryptoServiceProvider();
            var md5Cryptoserviceprovider = new MD5CryptoServiceProvider();

            try
            {
                if (entrada.Trim() != "")
                {
                    tripledescryptoserviceprovider.Key = md5Cryptoserviceprovider.ComputeHash(Encoding.ASCII.GetBytes(chave));
                    tripledescryptoserviceprovider.Mode = CipherMode.ECB;
                    var desdencrypt = tripledescryptoserviceprovider.CreateEncryptor();
                    var myAsciiEncoding = new ASCIIEncoding();
                    var buff = Encoding.ASCII.GetBytes(entrada);
                    return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));

                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tripledescryptoserviceprovider = null;
                md5Cryptoserviceprovider = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="chave"></param>
        /// <returns></returns>
        public static string Descriptografar(string entrada, string chave)
        {
            var tripledescryptoserviceprovider = new TripleDESCryptoServiceProvider();
            var md5Cryptoserviceprovider = new MD5CryptoServiceProvider();

            try
            {
                if (entrada.Trim() != "")
                {
                    tripledescryptoserviceprovider.Key = md5Cryptoserviceprovider.ComputeHash(Encoding.ASCII.GetBytes(chave));
                    tripledescryptoserviceprovider.Mode = CipherMode.ECB;
                    var desdencrypt = tripledescryptoserviceprovider.CreateDecryptor();
                    var buff = Convert.FromBase64String(entrada);

                    return Encoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tripledescryptoserviceprovider = null;
                md5Cryptoserviceprovider = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        public static string Criptografar(string entrada)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(entrada));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
