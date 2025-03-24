namespace AccountManagementAPI.HelperServices.Email.Models;

public class EmailModel
{
    public string recieverName { get; set; } = "Caleb Bogan";            //note this will not be hardcoded values this will be passed in from the current user context

    public string recieverEmail { get; set; } = "caleb.bogan47@ethereal.email";      //note this will not be hardcoded values this will be passed in from the current user context

    public string subject { get; set; } = string.Empty;

    public string body { get; set; } = string.Empty;
}
