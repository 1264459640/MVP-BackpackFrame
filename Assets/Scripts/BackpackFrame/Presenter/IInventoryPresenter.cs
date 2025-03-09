using BackpackFrame.Model;

namespace BackpackFrame.Presenter
{
	public interface IInventoryPresenter
	{
		bool AddItem(Item item);
		void RemoveItem(int index);
		void InitializeView();
		void ViewDispose();
	}
	
}
