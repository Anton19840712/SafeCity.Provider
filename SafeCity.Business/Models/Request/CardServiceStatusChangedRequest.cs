namespace SafeCity.Business.Models.Request
{
	public class CardServiceStatusChangedRequest
	{
		public string GlobalId { get; set; }
		public int NCardState { get; set; }
		public int NEmergencyCardId { get; set; }
		public DateTime DtCreate { get; set; }
		public string StrOperatorName { get; set; }
	}
}
