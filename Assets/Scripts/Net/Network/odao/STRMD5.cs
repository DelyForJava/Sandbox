using System.Collections;
using System.Text;
using System.Security.Cryptography;

public class STRMD5{

    public static string MD5Num(string strInput)         //MD5  EnCODE
    {
        MD5 md5 = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(strInput);
        byte[] hash = md5.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString(); 
    }
}
