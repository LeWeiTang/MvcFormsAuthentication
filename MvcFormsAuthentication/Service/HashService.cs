using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace MvcFormsAuthentication.Service
{
    public class HashService
    {
        public static string MD5Hash(string rawString)
        {
            if(string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;
            using (MD5 md5 = MD5.Create())
            {
                //將字串轉回Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);

                //進行MD5雜湊加密
                byte[] encryptin = md5.ComputeHash(byteArray);

                sb = new StringBuilder();
                for(int i = 0;i<encryptin.Length;i++)
                {
                    //2, 8, 10, 16進位
                    //hexdecimal - 十六進位
                    sb.Append(encryptin[i].ToString("x2"));
                }
            }
                return sb.ToString();
        }



    }
}