namespace AccountManagementAPI.Constants
{
    public static class EmailConstants
    {
        public static string senderName = "AccountManagement";
        public static string senderEmail { get; set; } = "chettykubeshan0@gmail.com";

        public static string CreateSubject { get; set; } = "TRANSACTION CREATED";
        public static string UpdateSubject { get; set; } = "TRANSACTION UPDATED";
        public static string DeleteSubject { get; set; } = "TRANSACTION DELETED";
    }
}
