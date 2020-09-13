using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.IO;

namespace DaisyDBProject {
    static class Helper {
        
        public static string GetImageFromPath(string path) {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            string base64String = Convert.ToBase64String(buffer);
            return base64String;
        }

        public static void PutImageIntoPath(string path, string imageBase64) {

        }
    }
}