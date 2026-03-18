namespace tata_momerial.web.ui.Business
{
    public class AjaxResponse
    {
        public const string SUCCESS = "success";
        public const string FAILURE = "failure";
        public const string ERROR = "error";
        public const string VALIDATION_FAILED = "validationFailed";
        public string result { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public string auxdata { get; set; } = string.Empty;
        public string errorcode { get; set; } = string.Empty;
        public int pagetotalcount { get; set; }
        public int currentpageindex { get; set; }

        public const string UNAUTHORIZED_ACCESS = "You are not authorized to complete this action.";

        public const string ERROR_MESSAGE = "Error : Please contact to support team.";

        public const string VALIDATION_MESSAGE = "Please fill all mandatory field.";

        public const string POP_UP_TITLE = "Failure";

        public const string POP_UP_TITLE_TYPE = "error";

        public string popuptitle { get; set; } = string.Empty;

        public string popupmessagetype { get; set; } = string.Empty;

        public AjaxResponse()
        {
            popuptitle = "Success";
            popupmessagetype = "success";
        }

    }
}


