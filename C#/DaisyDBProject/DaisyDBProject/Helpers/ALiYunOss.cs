using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.IO;
using System.Text;
using Aliyun.OSS;
using Aliyun.OSS.Common;

namespace DaisyDBProject.Helpers {
    static class ALiYunOss {

        static private readonly string endpoint = "oss-cn-shanghai.aliyuncs.com";
        static private readonly string accessKeyId = "LTAI4G6nTuGNFnHykXVr5ujD";
        static private readonly string accessKeySecret = "7elvGGv3ZbhJ1wFJIHbG6SavmuVd8E";
        static private readonly string bucketName = "daisydata";
        static private readonly OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        public static string GetImageFromPath(string name) {
            try{
                // 生成签名URL。
                var req = new GeneratePresignedUriRequest(bucketName, name, SignHttpMethod.Get);
                var uri = client.GeneratePresignedUri(req);
                return uri.ToString();
            }
            catch (OssException ex){
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}", 
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
            return "";
        }

        public static void PutImageIntoPath(string imageBase64, string name) {
            try
            {
                // 生成签名URL。
                var generatePresignedUriRequest = new GeneratePresignedUriRequest(bucketName, name, SignHttpMethod.Put)
                {
                    Expiration = DateTime.Now.AddHours(1),
                };
                var signedUrl = client.GeneratePresignedUri(generatePresignedUriRequest);
                // 使用签名URL上传文件。
                var buffer = Encoding.UTF8.GetBytes(imageBase64);
                using (var ms = new MemoryStream(buffer))
                {
                    client.PutObject(signedUrl, ms);
                }
                Console.WriteLine("Put object by signatrue succeeded. {0} ", signedUrl.ToString());
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }

        }
    }
}