using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KodeUI
{
    public class Style
    {
		public ColorBlock? stateColors { get; set; }
		public Color? color { get; set; }
		public Color? textColor { get; set; }
		public Color? imageColor { get; set; }
		public Sprite standard { get; set; }
		public Sprite background { get; set; }
		public Sprite inputField { get; set; }
		public Sprite knob { get; set; }
		public Sprite checkmark { get; set; }
		public Sprite dropdown { get; set; }
		public Sprite mask { get; set; }

		public static Style defaultStyle { get; private set; }

		public static Dictionary<string, Style> styles { get; private set; }

		static Color? ParseColor (string str)
		{
			if (String.IsNullOrEmpty (str)) {
				return null;
			}
			return ConfigNode.ParseColor (str);
		}

		static float? ParseFloat (string str)
		{
			if (String.IsNullOrEmpty (str)) {
				return null;
			}
			float val;
			float.TryParse (str, out val);
			return val;
		}

		static ColorBlock? ParseColorBlock (ConfigNode node)
		{
			if (node == null) {
				return null;
			}
			var colorBlock = ColorBlock.defaultColorBlock;
			{if (ParseColor(node.GetValue ("normalColor")) is Color c) {
				colorBlock.normalColor = c;
			}}
			{if (ParseColor(node.GetValue ("highlightedColor")) is Color c) {
				colorBlock.highlightedColor = c;
			}}
			{if (ParseColor(node.GetValue ("pressedColor")) is Color c) {
				colorBlock.pressedColor = c;
			}}
			{if (ParseColor(node.GetValue ("selectedColor")) is Color c) {
				colorBlock.selectedColor = c;
			}}
			{if (ParseColor(node.GetValue ("disabledColor")) is Color c) {
				colorBlock.disabledColor = c;
			}}
			{if (ParseFloat(node.GetValue ("colorMultiplier")) is float f) {
				colorBlock.colorMultiplier = f;
			}}
			{if (ParseFloat(node.GetValue ("fadeDuration")) is float f) {
				colorBlock.fadeDuration = f;
			}}
			return colorBlock;
		}

		public Style ()
		{
			stateColors = ColorBlock.defaultColorBlock;
			color = Color.white;
			textColor = Color.gray;
			imageColor = Color.white;
			standard = SpriteLoader.GetSprite ("KodeUI/Default/standard");
			background = SpriteLoader.GetSprite ("KodeUI/Default/background");
			inputField = SpriteLoader.GetSprite ("KodeUI/Default/inputField");
			knob = SpriteLoader.GetSprite ("KodeUI/Default/knob");
			checkmark = SpriteLoader.GetSprite ("KodeUI/Default/checkmark");
			dropdown = SpriteLoader.GetSprite ("KodeUI/Default/dropdown");
			mask = SpriteLoader.GetSprite ("KodeUI/Default/mask");
			Debug.Log($"[Style] {stateColors} {color} {textColor} {imageColor} {standard} {background} {inputField} {knob} {checkmark} {dropdown} {mask}");
		}

		public Style (ConfigNode node)
		{
			Load (node);
		}

		public Style (Style style)
		{
			stateColors = style.stateColors;
			color = style.color;
			textColor = style.textColor;
			imageColor = style.imageColor;
			standard = style.standard;
			background = style.background;
			inputField = style.inputField;
			knob = style.knob;
			checkmark = style.checkmark;
			dropdown = style.dropdown;
			mask = style.mask;
		}

		public Style Merge (Style overrideStyle)
		{
			{if (overrideStyle.stateColors is ColorBlock cb) {
				stateColors = cb;
			}}
			{if (overrideStyle.color is Color c) {
				color = c;
			}}
			{if (overrideStyle.textColor is Color c) {
				textColor = c;
			}}
			{if (overrideStyle.imageColor is Color c) {
				imageColor = c;
			}}
			{if (overrideStyle.standard is Sprite s) {
				standard = s;
			}}
			{if (overrideStyle.background is Sprite s) {
				background = s;
			}}
			{if (overrideStyle.inputField is Sprite s) {
				inputField = s;
			}}
			{if (overrideStyle.knob is Sprite s) {
				knob = s;
			}}
			{if (overrideStyle.checkmark is Sprite s) {
				checkmark = s;
			}}
			{if (overrideStyle.dropdown is Sprite s) {
				dropdown = s;
			}}
			{if (overrideStyle.mask is Sprite s) {
				mask = s;
			}}
			return this;
		}

		void Load (ConfigNode node)
		{
			{if (ParseColorBlock (node.GetNode ("stateColors")) is ColorBlock cb) {
				stateColors = cb;
			}}
			{if (ParseColor (node.GetValue ("color")) is Color c) {
				color = c;
			}}
			{if (ParseColor (node.GetValue ("textColor")) is Color c) {
				textColor = c;
			}}
			{if (ParseColor (node.GetValue ("imageColor")) is Color c) {
				imageColor = c;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("standard")) is Sprite s) {
				standard = s;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("background")) is Sprite s) {
				background = s;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("inputField")) is Sprite s) {
				inputField = s;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("knob")) is Sprite s) {
				knob = s;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("checkmark")) is Sprite s) {
				checkmark = s;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("dropdown")) is Sprite s) {
				dropdown = s;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("mask")) is Sprite s) {
				mask = s;
			}}
		}

		public static IEnumerator LoadStyles()
		{
			var dbase = GameDatabase.Instance;
			var node_list = dbase.GetConfigNodes ("KodeUI_Style");
			defaultStyle = new Style ();
			for (int i = 0; i < node_list.Length; i++) {
				Style style;
				var node = node_list[i];
				string name = node.GetValue ("name");
				if (String.IsNullOrEmpty (name) || name == "default") {
					style = defaultStyle;
					style.Load (node);
				} else {
					style = new Style(node);
					styles[name] = style;
				}
				yield return null;
			}
		}
    }
}
