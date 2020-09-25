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
		public Sprite sprite { get; set; }
		public SpriteState? stateSprites { get; set; }
		public Selectable.Transition? transition { get; set; }

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

		static SpriteState? ParseSpriteState (ConfigNode node)
		{
			if (node == null) {
				return null;
			}
			SpriteState sprites = new SpriteState();

			{if (SpriteLoader.GetSprite (node.GetValue ("highlightedSprite")) is Sprite s) {
				sprites.highlightedSprite = s;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("pressedSprite")) is Sprite s) {
				sprites.pressedSprite = s;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("selectedSprite")) is Sprite s) {
				sprites.selectedSprite = s;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("disabledSprite")) is Sprite s) {
				sprites.disabledSprite = s;
			}}
			return sprites;
		}

		static Selectable.Transition? ParseTransition (string str)
		{
			if (String.IsNullOrEmpty (str)) {
				return null;
			}
			Selectable.Transition transition = Selectable.Transition.ColorTint;
			transition = KodeUI_Utils.ToEnum<Selectable.Transition> (str, transition);
			return transition;
		}

		public Style ()
		{
			stateColors = ColorBlock.defaultColorBlock;
			color = Color.white;
			sprite = SpriteLoader.GetSprite ("KodeUI/Default/standard");
			// stateSprites can default to null
			transition = Selectable.Transition.ColorTint;
		}

		public Style (ConfigNode node)
		{
			Load (node);
		}

		public Style (Style style)
		{
			stateColors = style.stateColors;
			color = style.color;
			sprite = style.sprite;
			stateSprites = style.stateSprites;
			transition = style.transition;
		}

		public Style Merge (Style overrideStyle)
		{
			{if (overrideStyle.stateColors is ColorBlock cb) {
				stateColors = cb;
			}}
			{if (overrideStyle.color is Color c) {
				color = c;
			}}
			{if (overrideStyle.sprite is Sprite s) {
				sprite = s;
			}}
			{if (overrideStyle.stateSprites is SpriteState s) {
				stateSprites = s;
			}}
			{if (overrideStyle.transition is Selectable.Transition t) {
				transition = t;
			}}
			return this;
		}

		public void Load (ConfigNode node)
		{
			{if (ParseColorBlock (node.GetNode ("stateColors")) is ColorBlock cb) {
				stateColors = cb;
			}}
			{if (ParseColor (node.GetValue ("color")) is Color c) {
				color = c;
			}}
			{if (SpriteLoader.GetSprite (node.GetValue ("sprite")) is Sprite s) {
				sprite = s;
			}}
			{if (ParseSpriteState (node.GetNode ("stateSprites")) is SpriteState s) {
				stateSprites = s;
			}}
			{if (ParseTransition (node.GetValue ("transition")) is Selectable.Transition t) {
				transition = t;
			}}
		}
    }
}
