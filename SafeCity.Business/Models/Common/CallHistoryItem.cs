namespace SafeCity.Business.Models.Common
{
	public class CallHistoryItem
	{
		public CallData CallData { get; set; }
		public DateTime? CallTime { get; set; }
		public string OperatorPin { get; set; }
	}
}
