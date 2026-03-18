using tata_momerial.web.common.Cookie;
using tata_momerial.web.common;
using tata_momerial.web.dto;
using System.Drawing.Imaging;
using System.Drawing;


namespace tata_momerial.web.ui.Business
{
    public class CommonClass
    {
        internal readonly SessionManager SessionManager;
        internal readonly ICookieService CookieService;

        public const string DATE_DISPLAY_FORMAT = "dd MMM yyyy";
        public const string RESOURCE_NOT_FOUND = "ResourceNotFound";

        public CommonClass(ICookieService objCookieService)
        {
            CookieService = objCookieService;
            SessionManager = new SessionManager(objCookieService);
        }
        public CommonClass()
        {
            SessionManager = new SessionManager(null);
        }

        public static bool IsAuthorized(string authorizationTypeID, string permissionId, usersDTO _usersDTO)
        {
            bool isUnAuthorzie = false;
            bool isLoggedIn = false;
            bool isPermissionFalse = true;

            CommonClass.CheckAuthorization(authorizationTypeID, permissionId, _usersDTO, out isUnAuthorzie, out isLoggedIn, out isPermissionFalse);

            return !isUnAuthorzie && !isLoggedIn && isPermissionFalse;
        }

        public static void CheckAuthorization(string authorizationTypeID, string permissionId, usersDTO _usersDTO, out bool isUnAuthorzie, out bool isLoggedIn, out bool isPermissionFalse)
        {
            isUnAuthorzie = false;
            isLoggedIn = false;
            isPermissionFalse = true;

            if (_usersDTO != null)
            {
                if (_usersDTO.lstroleauthorizationtyperelationshipDTO != null &&
                    _usersDTO.lstroleauthorizationtyperelationshipDTO.Count > 0)
                {
                    string varAuthorizationTypeID = authorizationTypeID;
                    int intAuthorizationTypeID = 0;
                    int.TryParse(varAuthorizationTypeID, out intAuthorizationTypeID);
                    roleauthorizationtyperelationshipDTO objRoleAuthorizationTypeRelationshipDto =
                        _usersDTO.lstroleauthorizationtyperelationshipDTO.FirstOrDefault(x => x.authorizationtypeid == intAuthorizationTypeID);

                    if (objRoleAuthorizationTypeRelationshipDto != null)
                    {
                        string[] userPermissions = permissionId.Split(',');
                        foreach (string item in userPermissions)
                        {
                            if (item == "2" && objRoleAuthorizationTypeRelationshipDto.isinsert == 0)
                            {
                                isPermissionFalse = false;
                                break;
                            }
                            if (item == "3" && objRoleAuthorizationTypeRelationshipDto.isupdate == 0)
                            {
                                isPermissionFalse = false;
                                break;
                            }
                            if (item == "4" && objRoleAuthorizationTypeRelationshipDto.isdelete == 0)
                            {
                                isPermissionFalse = false;
                                break;
                            }
                            if (item == "1" && objRoleAuthorizationTypeRelationshipDto.isview == 0)
                            {
                                isPermissionFalse = false;
                                break;
                            }
                        }
                    }
                    else
                        isUnAuthorzie = true;
                }
                else
                    isUnAuthorzie = true;
            }
            else
                isLoggedIn = true;

        }
        public static void CheckUserPermission(usersDTO? _usersDTO, out bool isReviewPermission, out bool isApprovePermission, out bool isPublishPermission)
        {
            isReviewPermission = false;
            isApprovePermission = false;
            isPublishPermission = false;

            if (_usersDTO != null)
            {
                if (_usersDTO.lstroleauthorizationtyperelationshipDTO != null &&
                    _usersDTO.lstroleauthorizationtyperelationshipDTO.Count > 0)
                {

                    roleauthorizationtyperelationshipDTO? _roleauthorizationtyperelationshipDTO =
                     _usersDTO.lstroleauthorizationtyperelationshipDTO.FirstOrDefault
                     (x => x.authorizationtypeid == 5);

                    if (_roleauthorizationtyperelationshipDTO != null && _roleauthorizationtyperelationshipDTO.isview == 1 &&
                        (_roleauthorizationtyperelationshipDTO.isinsert == 1 ||
                        _roleauthorizationtyperelationshipDTO.isupdate == 1))
                        isReviewPermission = true;

                    _roleauthorizationtyperelationshipDTO =
                 _usersDTO.lstroleauthorizationtyperelationshipDTO.FirstOrDefault
                 (x => x.authorizationtypeid == 6);

                    if (_roleauthorizationtyperelationshipDTO != null && _roleauthorizationtyperelationshipDTO.isview == 1 &&
                        (_roleauthorizationtyperelationshipDTO.isinsert == 1 ||
                        _roleauthorizationtyperelationshipDTO.isupdate == 1))
                        isApprovePermission = true;

                    _roleauthorizationtyperelationshipDTO =
             _usersDTO.lstroleauthorizationtyperelationshipDTO.FirstOrDefault
             (x => x.authorizationtypeid == 7);

                    if (_roleauthorizationtyperelationshipDTO != null && _roleauthorizationtyperelationshipDTO.isview == 1 &&
                        (_roleauthorizationtyperelationshipDTO.isinsert == 1 ||
                        _roleauthorizationtyperelationshipDTO.isupdate == 1))
                        isPublishPermission = true;

                }
            }
        }

        public static string GetRandomNumber()
        {
            var chars = "abcdefgjkmnpqrstuvxyz123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        public static string GetRandomOTP()
        {
            var chars = "123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }


        public static bool ValidateImagePDF(IFormFile UploadAttachment, ApplicationConfiguration applicationConfiguration, out string errorMessage)
        {
            bool isValidExcel = true;
            errorMessage = string.Empty;
            if (UploadAttachment != null)
            {
                string extension = Path.GetExtension(UploadAttachment.FileName);
                if (!string.IsNullOrWhiteSpace(extension) && (extension.ToLower() == ".pdf"
                    || extension.ToLower() == ".jpg"
                     || extension.ToLower() == ".png"
                      || extension.ToLower() == ".jpeg"
                       || extension.ToLower() == ".gif"))
                {
                    if (UploadAttachment.Length > 0)
                    {
                        if (UploadAttachment.Length > applicationConfiguration.uploadcontentlength)
                        {
                            isValidExcel = false;
                            errorMessage = WebConstants.VALIDATION_MESSAGE_SIZE_EXCEEDED;
                        }
                    }
                    else
                    {
                        isValidExcel = false;
                        errorMessage = WebConstants.VALIDATION_ATTACHMENT_SELECT_EMPTY;
                    }
                }
                else
                {
                    isValidExcel = false;
                    errorMessage = WebConstants.VALIDATION_MESSAGE_DOCUMENT_FORMAT_CUSTOMER_REGISTRATION;
                }
            }
            return isValidExcel;
        }

        public static string Generate6DigitRandomCode()
        {
            Random generator = new Random();
            string otpCpde = generator.Next(0, 999999).ToString("D6");
            return otpCpde;
        }
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }


        public static bool ValidatePhotoImage(ApplicationConfiguration applicationConfiguration, IFormFile UploadAttachment, out string errorMessage)
        {
            bool isValidImage = true;
            errorMessage = string.Empty;

            if (UploadAttachment != null)
            {
                string extension = System.IO.Path.GetExtension(UploadAttachment.FileName);
                // check double . in file name
                int index = TotalIndexOf(UploadAttachment.FileName, ".");

                if (index == 2)
                {
                    isValidImage = false;
                    errorMessage = WebConstants.VALIDATION_MESSAGE_DOUBLE_INDEX;
                }
                else if (!string.IsNullOrWhiteSpace(extension) && (extension.ToLower() == ".jpg" ||
                              extension.ToLower() == ".jpeg" || extension.ToLower() == ".png" ||
                              extension.ToLower() == ".gif"))
                {
                    if (UploadAttachment.Length > 0)
                    {
                        if (UploadAttachment.Length < applicationConfiguration.uploadcontentlength)
                        {
                            if (!IsFileTypeValid(UploadAttachment))
                            {
                                isValidImage = false;
                                errorMessage = WebConstants.VALIDATION_MESSAGE_VALID_IMAGE;
                            }
                        }
                        else
                        {
                            isValidImage = false;
                            errorMessage = WebConstants.VALIDATION_MESSAGE_IMAGE_SIZE_EXCEEDED;
                        }
                    }
                    else
                    {
                        isValidImage = false;
                        errorMessage = WebConstants.VALIDATION_MESSAGE_SELECT_IMAGE_EMPTY;
                    }
                }
                else
                {
                    isValidImage = false;
                    errorMessage = WebConstants.VALIDATION_MESSAGE_IMAGE_FORMAT;
                }
            }

            return isValidImage;
        }

        public static int TotalIndexOf(string fileName, string variableSearch)
        {
            int totalIndex = 0;
            int firstIndex = fileName.IndexOf(variableSearch);
            int lastIndex = fileName.LastIndexOf(variableSearch);

            if (firstIndex != lastIndex && lastIndex >= 0 && firstIndex >= 0)
                totalIndex = 2;

            return totalIndex;
        }

        private static bool IsFileTypeValid(IFormFile file)
        {
            bool isValid = false;

            try
            {
                using (var img = System.Drawing.Image.FromStream(file.OpenReadStream()))
                {
                    if (IsOneOfValidFormats(img.RawFormat))
                    {
                        isValid = true;
                    }
                }
            }
            catch
            {
                //Image is invalid
            }
            return isValid;
        }

        private static bool IsOneOfValidFormats(ImageFormat rawFormat)
        {
            List<ImageFormat> formats = GetValidFormats();

            foreach (ImageFormat format in formats)
            {
                if (rawFormat.Equals(format))
                {
                    return true;
                }
            }
            return false;
        }

        private static List<ImageFormat> GetValidFormats()
        {
            List<ImageFormat> formats = new List<ImageFormat>();
#pragma warning disable CA1416 // Validate platform compatibility
            formats.Add(ImageFormat.Png);
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning disable CA1416 // Validate platform compatibility
            formats.Add(ImageFormat.Jpeg);
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning disable CA1416 // Validate platform compatibility
            formats.Add(ImageFormat.Gif);
#pragma warning restore CA1416 // Validate platform compatibility
            //add types here
            return formats;
        }

        public static bool ValidateAttachmentPdf(ApplicationConfiguration applicationConfiguration, IFormFile UploadAttachment, out byte[] file, out string errorMessage)
        {
            bool isValidPdf = true;
            file = null;
            errorMessage = string.Empty;

            if (UploadAttachment != null)
            {
                string extension = System.IO.Path.GetExtension(UploadAttachment.FileName);
                // check double . in file name
                int index = TotalIndexOf(UploadAttachment.FileName, ".");

                if (index == 2)
                {
                    isValidPdf = false;
                    errorMessage = WebConstants.VALIDATION_MESSAGE_DOUBLE_INDEX;
                }
                else if (!string.IsNullOrWhiteSpace(extension) && (extension.ToLower() == ".pdf"))
                {
                    if (UploadAttachment.Length > 0)
                    {
                        if (UploadAttachment.Length < applicationConfiguration.uploadcontentlength)
                        {
                            file = new byte[UploadAttachment.Length];
                            using (var ms = new MemoryStream())
                            {
                                UploadAttachment.CopyTo(ms);
                                file = ms.ToArray();
                            }
                            //if (!IsValidPdf(file))
                            //{
                            //    isValidPdf = false;
                            //    errorMessage = WebConstants.VALIDATION_MESSAGE_VALID_PDF;
                            //}
                        }
                        else
                        {
                            isValidPdf = false;
                            errorMessage = WebConstants.VALIDATION_MESSAGE_PDF_SIZE_EXCEEDED;
                        }
                    }
                    else
                    {
                        isValidPdf = false;
                        errorMessage = WebConstants.VALIDATION_ATTACHMENT_SELECT_EMPTY;
                    }
                }
                else
                {
                    isValidPdf = false;
                    errorMessage = WebConstants.VALIDATION_MESSAGE_PDF_FORMAT;
                }
            }
            return isValidPdf;
        }

        public static string GetRandomText()
        {
            var charsALL = "ABCDEFGHJKMNPQRSTUVWXYZ123456789abcdefghjkmnpqrstuvwxyz";
            var randomIns = new Random();
            int N = 6;
            var rndChars = Enumerable.Range(0, N)
                            .Select(_ => charsALL[randomIns.Next(charsALL.Length)])
                            .ToArray();
            rndChars[randomIns.Next(rndChars.Length)] = "123456789"[randomIns.Next(9)];
            rndChars[randomIns.Next(rndChars.Length)] = "abcdefghjkmnpqrstuvwxyz"[randomIns.Next(23)];
            var randomstr = new String(rndChars);
            return randomstr.ToString().ToUpper();
        }

    }
}
