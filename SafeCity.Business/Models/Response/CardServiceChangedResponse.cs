namespace SafeCity.Business.Models.Response
{
	public class CardServiceChangedResponse
	{
		public int? NEmergencyCardId { get; set; }
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}
}
