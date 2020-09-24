using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KodeUI
{
	public class UISlider : UIObject
	{
		public Slider slider { get; private set; }

		UIImage background;
		UIImage handle;
		UIImage fill;

		public override void CreateUI()
		{
			slider = gameObject.AddComponent<Slider>();

			Add<UIImage> (out background, "Background").Type(Image.Type.Sliced).Anchor(AnchorPresets.StretchAll).SizeDelta(0, 0);
			Add<UIImage> (out handle, "Handle");
			Add<UIImage> (out fill, "Fill").Type(Image.Type.Sliced).Anchor(AnchorPresets.StretchAll).SizeDelta(0, 0);

			slider.handleRect = handle.rectTransform;
			slider.fillRect = fill.rectTransform;
		}

		public override void Style()
		{
			background.image.sprite = style.background;
			background.image.color = style.color ?? UnityEngine.Color.white;
			handle.image.sprite = style.knob;
			handle.image.color = style.color ?? UnityEngine.Color.white;
			fill.image.sprite = style.standard;
			fill.image.color = style.imageColor ?? UnityEngine.Color.white;

			slider.colors = style.stateColors ?? ColorBlock.defaultColorBlock;
		}

		public UISlider Direction(Slider.Direction direction)
		{
			slider.direction = direction;
			return this;
		}

		public UISlider Handle(bool enabled)
		{
			handle.gameObject.SetActive (enabled);
			return this;
		}
	}
}
