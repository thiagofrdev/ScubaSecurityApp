using System;
using System.IO;
using System.Security.Cryptography;

namespace ScubaSecurityServer.Security
{
    public static class CryptoHelper
    {
        private static byte[] _key = new byte[32]; // Chave AES de 256 bits
        private static byte[] _iv = new byte[16];  // Vetor de Inicialização
        private static bool _isInitialized = false;

        /// <summary>
        /// Inicializa a criptografia usando o ruído acústico de um mergulho como fonte de entropia real (TRNG).
        /// Complexidade: O(B) onde B é o número de bytes do arquivo de áudio.
        /// </summary>
        public static void InicializarComRuidoAcustico(string caminhoArquivoWav)
        {
            if (!File.Exists(caminhoArquivoWav))
                throw new FileNotFoundException($"Áudio de entropia não encontrado: {caminhoArquivoWav}");

            byte[] entropiaFisica = File.ReadAllBytes(caminhoArquivoWav);

            // Usa SHA-256 para destilar o caos em uma chave de 32 bytes para o algoritmo AES
            using (SHA256 sha256 = SHA256.Create())
            {
                _key = sha256.ComputeHash(entropiaFisica);
            }

            // Usa MD5 apenas para gerar um Vetor de Inicialização (IV) a partir da mesma entropia
            using (MD5 md5 = MD5.Create())
            {
                _iv = md5.ComputeHash(entropiaFisica);
            }

            _isInitialized = true;
        }

        /// <summary>
        /// Encripta o texto usando a chave gerada pelo áudio.
        /// Complexidade: O(T) onde T é o tamanho da string a ser encriptada.
        /// </summary>
        public static string Encriptar(string textoPlano)
        {
            if (!_isInitialized) throw new Exception("Criptografia não inicializada.");
            
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = _key;
            aesAlg.IV = _iv;

            using MemoryStream msEncrypt = new MemoryStream();
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(textoPlano);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        /// <summary>
        /// Decripta o texto base64 de volta para texto legível.
        /// Complexidade: O(C) onde C é o tamanho do texto cifrado.
        /// </summary>
        public static string Decriptar(string textoCifradoBase64)
        {
            if (!_isInitialized) throw new Exception("Criptografia não inicializada.");
            
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = _key;
            aesAlg.IV = _iv;

            using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(textoCifradoBase64));
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);
            
            return srDecrypt.ReadToEnd();
        }
    }
}