using ShareLabo.Application.Authentication;
using System.Security.Cryptography;

namespace ShareLabo.Infrastructure.PGSQL.Application.Authentication
{
    public static class AccountPasswordExtension
    {
        private const int HashSize = 32;      // ハッシュのサイズ (32バイト = 256ビット)
        private const int Iterations = 10000; // イテレーション回数
        private const int SaltSize = 16;      // ソルトのサイズ (16バイト = 128ビット)

        public static string GetHashStr(this AccountPassword password)
        {
            // 1. ソルトを生成する
            var salt = new byte[SaltSize];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // 2. パスワードとソルトをPBKDF2でハッシュ化する
            using(var pbkdf2 = new Rfc2898DeriveBytes(password.Value, salt, Iterations, HashAlgorithmName.SHA256))
            {
                var hash = pbkdf2.GetBytes(HashSize);

                // 3. ソルトとハッシュを結合してbase64文字列にする
                var saltBase64 = Convert.ToBase64String(salt);
                var hashBase64 = Convert.ToBase64String(hash);

                // 例: {salt}:{hash}:{iterations} の形式で保存する
                return $"{saltBase64}:{hashBase64}:{Iterations}";
            }
        }

        public static bool VerifyPassword(this AccountPassword password, string hashedPassword)
        {
            try
            {
                // 1. 保存されたハッシュ文字列からソルト、ハッシュ、イテレーション回数を取得する
                var parts = hashedPassword.Split(':');
                if(parts.Length != 3)
                {
                    return false; // フォーマットが不正
                }

                var salt = Convert.FromBase64String(parts[0]);
                var storedHash = Convert.FromBase64String(parts[1]);
                var iterations = int.Parse(parts[2]);

                // 2. 入力されたパスワードと取得したソルトでハッシュを再生成する
                using(var pbkdf2 = new Rfc2898DeriveBytes(password.Value, salt, iterations, HashAlgorithmName.SHA256))
                {
                    var newHash = pbkdf2.GetBytes(HashSize);

                    // 3. 生成されたハッシュと保存されたハッシュを比較する
                    // Byte配列を比較する際は、`SequenceEqual`を使用するのが安全です。
                    return newHash.SequenceEqual(storedHash);
                }
            }
            catch(Exception)
            {
                return false; // Base64文字列の変換に失敗した場合
            }
        }
    }
}