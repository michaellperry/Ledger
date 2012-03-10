using System.Windows.Controls;
using System.Windows.Data;

namespace FacetedWorlds.Ledger.Views
{
    public partial class LedgerEntryControl : UserControl
    {
        public LedgerEntryControl()
        {
            InitializeComponent();

            ContextMenu contextMenu = new ContextMenu();
            MenuItem voidMenuItem = new MenuItem() { Header = "Void" };
            voidMenuItem.SetBinding(MenuItem.CommandProperty, new Binding("Void"));
            contextMenu.Items.Add(voidMenuItem);
            ContextMenuService.SetContextMenu(this, contextMenu);
        }
    }
}
