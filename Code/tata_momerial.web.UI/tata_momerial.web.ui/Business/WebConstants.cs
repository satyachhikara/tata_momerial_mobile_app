namespace tata_momerial.web.ui.Business
{
    public class WebConstants
    {
        public const string AJAX_FAILURE_RESULT = "failure";
        public const string AJAX_SUCCESS_RESULT = "success";

        public const String RestrictedChars = @"^[^\~\`\!\#\$\^\*\+\=\|\\\}\]\{\[\>\<\""]+$";
        public const String MESSAGE_SOMETHING_WENT_WRONG = "Something went wrong. Please contact with your management to resolve this issue (कुछ गड़बड़ हो गई है। कृपया इस समस्या को हल करने के लिए अपने प्रबंधन से संपर्क करें).";

        public const String RestrictedCharsMessage = @"Please do not enter special chars like (e.g. ‘~^!#$*<>'’).";
        public const String RestrictedCharsMessageTrainerName = @"Please do not enter special chars like (e.g. ‘~^!#$*<>'.’). Only enter plain trainer name.";

        public const String VALIDATION_MESSAGE_SIZE_EXCEEDED = "File size must be less then or equal to 2 MB.";
        public const String VALIDATION_ATTACHMENT_SELECT_EMPTY = "Selected Attachment is Empty. Please select another attachment to upload.";
        public const String VALIDATION_MESSAGE_DOCUMENT_FORMAT_CUSTOMER_REGISTRATION = "Please select attachment only .pdf, .jpg, .png, .jpeg, .gif file.";

        public const String VALIDATION_MESSAGE_VALID_IMAGE = "Please select a valid image type. image file you are trying to upload is not valid.";
        public const String VALIDATION_MESSAGE_VALID_PDF = "Please select a valid pdf type. pdf file you are trying to upload is not valid.";
        public const String VALIDATION_MESSAGE_DOUBLE_INDEX = "Please select a valid attachment. File can't have \".\" in file name.";

        public const String VALIDATION_MESSAGE_IMAGE_SIZE_EXCEEDED = "Image size must be less then or equal to 10 MB.";
        public const String VALIDATION_MESSAGE_SELECT_IMAGE_EMPTY = "Selected Image is Empty.Please select another image to upload.";
        public const String VALIDATION_MESSAGE_SELECT_IMAGE = "Please select any image to upload.";
        public const String VALIDATION_MESSAGE_IMAGE_FORMAT = "Please select image in format jpeg,tif,gif and png only.";

        public const String VALIDATION_MESSAGE_PDF_SIZE_EXCEEDED = "PDF size must be less then or equal to 10 MB.";
        public const String VALIDATION_MESSAGE_PDF_FORMAT = "Please select attachment in format pdf only.";

        
        public struct ActionViewNames
        {
            public const String UNAUTHORIZED_ACTION_NAME = "UnauthorizedAccess";
            public const String HOME_CONTROLLER_NAME = "Home";
            public const String ERROR_ACTION_NAME = "Error";
            public const String ACTION_LOGIN = "Login";
            public const String RESOURCE_NOT_FOUND = "ResourceNotFound";
        }

        public const string RESOURCENOTFOUND = "ResourceNotFound";
        public const string NOTAUTHRORIZEDPAGE = "UnauthorizedAccess";
        public const string HOME = "Home";
        public const String ERROR = "Error";
        public const string HOME_AREA_NAME = "";

        public const string NoAttachmentPDF = "wwwroot/images/NoAttachmentFound.pdf";
        public const string NoPhotoPath = "wwwroot/images/";


        

    }
}
