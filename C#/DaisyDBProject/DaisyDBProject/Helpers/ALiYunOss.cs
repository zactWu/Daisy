using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.IO;
using System.Text;
using Aliyun.OSS;

namespace DaisyDBProject.Helpers {
    static class ALiYunOss {

        static private readonly string endpoint = "oss-cn-shanghai.aliyuncs.com";

        static private readonly string accessKeyId = "LTAI4G6nTuGNFnHykXVr5ujD";

        static private readonly string accessKeySecret = "7elvGGv3ZbhJ1wFJIHbG6SavmuVd8E";

        static private readonly string bucketName = "daisydata";

        static private readonly OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        public static string GetImageFromPath(string name) {
            string image = "";
            try {
                var obj = client.GetObject(bucketName, name);
                using (var requestStream = obj.Content) {
                    byte[] buf = new byte[10000];
                    var fs = File.Open(name, FileMode.OpenOrCreate);
                    var len = 0;
                    while ((len = requestStream.Read(buf, 0, 10000)) != 0) {
                        fs.Write(buf, 0, len);
                    }
                    fs.Close();
                    int count = 0;
                    for(int i = 0; i < 10000; i++) {
                        if (buf[i] == '\0') {
                            count = i;
                            break;
                        }
                    }
                    byte[] temBuf = new byte[count];
                    for (int i = 0; i < count; i++) {
                        temBuf[i] = buf[i];
                    }
                    image = Encoding.Default.GetString(temBuf);
                }            
            }
            catch (Exception ex) {
                Console.WriteLine("Get object failed. {0}", ex.Message);
            }
            return image;
        }

        public static void PutImageIntoPath(string imageBase64, string name) {
            try {
                byte[] binaryData = Encoding.ASCII.GetBytes(imageBase64);
                MemoryStream requestContent = new MemoryStream(binaryData);
                client.PutObject(bucketName, name, requestContent);
            }
            catch (Exception ex) {
                Console.WriteLine("Put object failed, {0}", ex.Message);
            }
        }
    }
}