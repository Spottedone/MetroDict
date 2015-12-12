using System.Windows.Input;
using Windows.UI.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MetroDictLib.Helpers
{
    public class ItemClickedBehavior : Behavior<ListViewBase>
    {
        public static readonly DependencyProperty ItemClickedCommandProperty =
            DependencyProperty.Register(
                "ItemClickedCommand",
                typeof(ICommand),
                typeof(ItemClickedBehavior),
                new PropertyMetadata(null));

        public ICommand ItemClickedCommand
        {
            get { return (ICommand)GetValue(ItemClickedCommandProperty); }
            set { SetValue(ItemClickedCommandProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ItemClick += OnItemClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.ItemClick -= OnItemClick;
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClickedCommand.Execute(e.ClickedItem);
        }
    }
}
