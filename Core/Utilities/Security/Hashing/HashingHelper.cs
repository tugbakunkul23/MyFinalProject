using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //CreatePasswordHash: Kullanıcı şifresini hash + salt olarak üretir, DB’ye kaydedilir.
        //Şifreyi hash’lemek için
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;     // Şifreleme için kullanılan "anahtar" (her kullanıcı için farklı)
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));   // Şifrenin hashlenmiş hali
            }
        }

        //VerifyPasswordHash: Girişte girilen şifreyi hashleyip DB’deki ile karşılaştırır.
        //Şifreyi doğrulamak için
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));// Girilen şifreden hash üret
                for (int i=0; i < computedHash.Length; i++) 
                {
                    if (computedHash[i] != passwordHash[i])  // Eğer veritabanındaki hash ile aynı değilse → yanlış şifre
                    {
                        return false;
                    }
                }
            }

            return true;  // Bütün byte’lar aynıysa → doğru şifre
        }
    }
}
