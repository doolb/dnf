using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Godot;

namespace UI
{
    public class ScrollList : ScrollContainer
    {
        Control panel;
        public override void _Ready() {
            panel = (Control)GetNode(nameof(panel));
            panel.RectSize = this.RectSize;
        }

        // control, item index, object index
        protected Action<Control, int, int> OnItemShow;
        // return new control
        protected Func<Control> OnItemInit;
        protected Vector2 itemSize;
        protected float itemHeight => itemSize.y + 4;
        protected int ItemCount { get; set; }   // count of item to show
        protected int maxShowCount { get; set; } // max count in show
        protected int LineCount { get; set; } = 5;     // count of line
        public int PerLineCount { get; set; } = 1;   // count of each line
        protected int startIndex { get; set; }  // index of first show item
        protected int endIndex { get; set; }    // index of last show item
        public void Init(Func<Control> @init, Action<Control, int, int> @show, Vector2 @size) {
            this.OnItemInit = @init;
            this.OnItemShow = @show;
            this.itemSize = @size;

            LineCount = (int)(panel.RectSize.y / (@size.y + 4)) + 1;
            maxShowCount = LineCount * PerLineCount + 2 * PerLineCount; // max item cache count
        }
        public void Show(int @count) {
            needFlip = false;
            ItemCount = @count > 0 ? @count : 0;
            startIndex = 0;
            endIndex = LineCount * PerLineCount;
            Coroutine.Instance.StartCoroutine(delayShow(ItemCount, 0));
        }

        IEnumerator delayShow(int @count, int @jump) {
            yield return new WaitForFrame(1);

            int _showCount = ItemCount > maxShowCount ? maxShowCount : ItemCount; // need show item count

            panel.AddChild(new Control() { Name = "padden_head" });
            int _childCount = panel.GetChildCount() - 1;
            int _max = Mathf.Max(_childCount, _showCount);
            for (int i = 0; i < _max; i++) {
                if (i >= _childCount) {
                    var ctrl = OnItemInit();
                    ctrl.RectMinSize = itemSize;
                    panel.AddChild(ctrl);
                }
                else if (i >= _showCount) {
                    panel.SetProcess(false);
                    panel.Hide();
                }
                panel.SetProcess(true);
                panel.Show();
                OnItemShow((Control)panel.GetChild(i + 1), i + startIndex, i);
            }
            panel.AddChild(new Control()
            {
                Name = "padden_tail",
                RectMinSize = new Vector2(0, Mathf.Max(0, ItemCount - maxShowCount))
            });
            lastFisrt = 0;
            needFlip = true;
        }

        public bool needFlip;
        public int lastFisrt;
        public override void _Process(float delta) {
            if (!needFlip) return;
            var vbar = GetVScrollbar();
            int firstIndex = (int)(vbar.Value / itemHeight);
            firstIndex = Mathf.Clamp(firstIndex, 0, Mathf.Max(0, ItemCount - maxShowCount));
            (panel.GetChild(0) as Control).RectMinSize = new Vector2(0, firstIndex * itemHeight);

            (panel.GetChild(panel.GetChildCount() - 1) as Control).RectMinSize = new Vector2(0, Mathf.Max(0,
            ItemCount - firstIndex - maxShowCount) * itemHeight);
            if (lastFisrt == firstIndex) {
                return;
            }
            lastFisrt = firstIndex;


            for (int i = firstIndex; i < Mathf.Min(ItemCount, firstIndex + maxShowCount); i++) {
                var idx = i - firstIndex + 1;
                OnItemShow((Control)panel.GetChild(idx), i, idx - 1);
            }

            EditorDescription = $"{startIndex} {endIndex} {firstIndex} {base.GetHScroll()} {base.GetVScroll()}";
        }
    }
}