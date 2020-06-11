using Godot;

namespace UI
{
    public class ButtonBase<T> : Godot.Object where T : Button
    {
        public delegate void OnButtonClick(ButtonBase<T> @btn);
        public event OnButtonClick onButtonClick;
        public T target { get; private set; }
        public int Id { get; set; } // index of item
        public int VId { get; set; }// index of show item
        public ButtonBase(T @button) {
            target = button;
            button.Connect("pressed", this, nameof(_pressed));
            //button.Connect("focus_entered", this, nameof(_pressed));
        }

        void _pressed() {
            onButtonClick?.Invoke(this);
        }
    }
}