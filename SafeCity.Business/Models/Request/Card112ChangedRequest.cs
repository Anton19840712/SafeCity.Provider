using SafeCity.Business.Models.Common;

namespace SafeCity.Business.Models.Request
{
	public class Card112ChangedRequest
	{
		public string GlobalId { get; set; }
		public int NEmergencyCardId { get; set; }
		public DateTime DtCreate { get; set; }
		public string StrCgPN { get; set; }
		public string StrAddressString { get; set; }
		public string StrBuilding { get; set; }
		public string StrCorps { get; set; }
		public string StrRoom { get; set; }
		public string StrAdditionalLocationInfo { get; set; }
		public string StrIncidentDescription { get; set; }
		public int? NIncidentTypeID { get; set; }
		public string StrIncidentType { get; set; }
		public string StrCallerContactPhone { get; set; }
		public string StrDeclarantName { get; set; }
		public string StrDeclarantLastName { get; set; }
		public string StrDeclarantMiddleName { get; set; }
		public string StrDeclarantBuildingNumber { get; set; }
		public string StrDeclarantAddressString { get; set; }
		public string StrDeclarantCorps { get; set; }
		public string StrDeclarantFlat { get; set; }
		public string GeoLatitude { get; set; }
		public string GeoLongitude { get; set; }
		public string DeclarantGeoLatitude { get; set; }
		public string DeclarantGeoLongitude { get; set; }
		public string CallId { get; set; }
		public int? NCasualties { get; set; }
		public string StrStreetKLADR { get; set; }
		public string StrDeclarantStreetKLADR { get; set; }
		public string StrDistrictKLADR { get; set; }
		public string StrDeclarantDistrictKLADR { get; set; }
		public string StrCityKLADR { get; set; }
		public string StrDeclarantCityKLADR { get; set; }
		public string StrOperatorName { get; set; }
		public string LWithCall { get; set; }
		public string StrOKTMO { get; set; }
		public EraGlonassData EraGlonassData { get; set; }
		public CallHistory CallHistory { get; set; }
		public string StrLatestModifier { get; set; }
	}
}
