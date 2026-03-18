using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace tata_momerial.web.common
{
    public static class AppServicesHelper
    {
        static IServiceProvider _ServiceProvider = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider ServiceProvider
        {
            get { return _ServiceProvider; }
            set
            {
                if (_ServiceProvider != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                _ServiceProvider = value;
            }
        }

        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        public static HttpContext CurrentHttpContext
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = ServiceProvider.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }

        public static string WebsiteURL
        {
            get
            {
                string url = string.Empty;

                if (CurrentHttpContext != null && CurrentHttpContext.Request != null)
                {
                    Uri objUriCurrentRequest = new Uri(CurrentHttpContext.Request.GetDisplayUrl());

                    string[] Segments = objUriCurrentRequest.AbsoluteUri.ToString().Split('/');

                    string webApplicationName = ApplicationConfiguration.webappname;

                    if (!string.IsNullOrWhiteSpace(webApplicationName))
                        url = Segments[0] + "//" + Segments[2] + "/" + webApplicationName;
                    else
                        url = Segments[0] + "//" + Segments[2];
                }

                return url;
            }
        }

        /// <summary>
        /// Get Memory Cache
        /// </summary>
        public static IMemoryCache MemoryCache
        {
            get
            {
                IMemoryCache _memorycache = ServiceProvider.GetService(typeof(IMemoryCache)) as IMemoryCache;
                return _memorycache;
            }
        }

        /// <summary>
        /// Get Data Protector
        /// </summary>
        public static IDataProtector DataProtector
        {
            get
            {
                IDataProtector _dataprotector = ServiceProvider.GetDataProtector("MVCCore.Web");
                return _dataprotector;
            }
        }

        public static string Encrypt(string plaintext)
        {
            const string CINCEncryptorkeyValue = "ChasterItSolutions";
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(plaintext);

            // Get the key from config file
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                System.Security.Cryptography.MD5 hashmd5 = System.Security.Cryptography.MD5.Create();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(CINCEncryptorkeyValue));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(CINCEncryptorkeyValue);

            System.Security.Cryptography.TripleDES tdes = System.Security.Cryptography.TripleDES.Create();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string encryptedText)
        {
            const string CINCEncryptorkeyValue = "ChasterItSolutions";
            bool useHashing = true;
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(encryptedText);

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                System.Security.Cryptography.MD5 hashmd5 = System.Security.Cryptography.MD5.Create();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(CINCEncryptorkeyValue));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(CINCEncryptorkeyValue);
            }

            System.Security.Cryptography.TripleDES tdes = System.Security.Cryptography.TripleDES.Create();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }




        /// <summary>
        /// Get HttpContextAccessor
        /// </summary>
        public static IHttpContextAccessor HttpContextAccessor
        {
            get
            {
                IHttpContextAccessor objHttpContextAccessor = ServiceProvider.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return objHttpContextAccessor;
            }
        }

        /// <summary>
        /// Configuration settings from appsetting.json.
        /// </summary>
        public static ApplicationConfiguration ApplicationConfiguration
        {
            get
            {
                //This works to get file changes.
                var _optionsmonitor = ServiceProvider.GetService(typeof(IOptionsMonitor<ApplicationConfiguration>)) as IOptionsMonitor<ApplicationConfiguration>;
                ApplicationConfiguration objApplicationConfiguration = _optionsmonitor.CurrentValue;
                return objApplicationConfiguration;
            }
        }
    }
}
