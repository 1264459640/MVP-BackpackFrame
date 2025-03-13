namespace FrameWorks.BackpackFrame.ItemData
{
	[System.Serializable]
	public class WeaponItem : Item
	{
		public WeaponType weaponType;
		public int damage;
	}

	public enum WeaponType
	{
		Sword,
		Axe,
		Bow,
		Wand
		
	}
}
