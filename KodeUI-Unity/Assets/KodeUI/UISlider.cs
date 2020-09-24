using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KodeUI
{
	public class UISlider : UIObject
	{
		public Slider slider { get; private set; }

		UIImage background;
		UIImage fill;
		UIEmpty fillArea;
		UIImage handle;
		UIEmpty handleArea;

		public override void CreateUI()
		{
			slider = gameObject.AddComponent<Slider>();

			var anchorMin = new Vector2 (0, 0.25f);
			var anchorMax = new Vector2 (1, 0.75f);

			Add<UIImage> (out background, "Background").Type(Image.Type.Sliced).Anchor(anchorMin, anchorMax).SizeDelta(0, 0).Finish();
			Add<UIEmpty>(out fillArea, "Fill Area").Anchor(anchorMin, anchorMax).SizeDelta (-20, 0).X(-5).Y(0).Pivot(PivotPresets.MiddleCenter)
				.Add<UIImage> (out fill, "Fill").Type(Image.Type.Sliced).SizeDelta(10, 0).Pivot(PivotPresets.MiddleCenter).Finish()
			.Finish();
			Add<UIEmpty>(out handleArea, "Handle Area").Anchor(AnchorPresets.StretchAll).SizeDelta (-20, 0)
				.Add<UIImage> (out handle, "Handle").SizeDelta(20, 0).Pivot(PivotPresets.MiddleCenter).Finish()
			.Finish();

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
			if (enabled) {
				float width = handle.rectTransform.sizeDelta.x;
				fillArea.X(-width / 4).WidthDelta(-width);
				fill.WidthDelta(width/2);
			} else {
				fillArea.X(0).WidthDelta(0);
				fill.WidthDelta(0);
			}
			return this;
		}
	}
}
