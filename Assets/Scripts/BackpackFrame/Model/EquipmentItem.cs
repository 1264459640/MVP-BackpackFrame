
namespace BackpackFrame.Model
{
	[System.Serializable]
	public class EquipmentItem : Item
	{
		public EquipmentType equipmentType;
	}
	
	public enum EquipmentType
	{
		Head,
		Body,
		Legs,
		Feet,
		Ring,
		Amulet,
		Belt,
		Cape,
		Necklace,
	}
}
