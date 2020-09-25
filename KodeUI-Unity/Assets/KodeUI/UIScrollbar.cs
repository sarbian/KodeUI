using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KodeUI
{
	public class UIScrollbar : UIObject
	{
		Image image;
		UIEmpty slidingArea;
		UIImage handle;
		public Scrollbar scrollbar { get; private set; }

		public override void CreateUI()
		{
			image = gameObject.AddComponent<Image>();
			image.color = UnityEngine.Color.white;
			image.type = Image.Type.Sliced;

			scrollbar = gameObject.AddComponent<Scrollbar>();

			Add<UIEmpty> (out slidingArea, "Sliding Area").Anchor(AnchorPresets.StretchAll).SizeDelta(0, 0)
				.Add<UIImage> (out handle, "Handle").Type(Image.Type.Sliced).Anchor(AnchorPresets.StretchAll).SizeDelta(0, 0).Finish()
			.Finish();

			scrollbar.handleRect = handle.rectTransform;
		}

		public override void Style()
		{
			scrollbar.colors = style.stateColors ?? ColorBlock.defaultColorBlock;
			handle.image.sprite = style.standard;
			handle.image.color = style.color ?? UnityEngine.Color.white;

			image.sprite = style.background;
			image.color = style.color ?? UnityEngine.Color.white;
		}

		public UIScrollbar Direction(Scrollbar.Direction direction)
		{
			scrollbar.direction = direction;
			return this;
		}
	}
}
