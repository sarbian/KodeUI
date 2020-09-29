# This is a makefile fragment intended to be included from a directory one
# level up. eg,
#    include KodeUI/KodeUI.inc
# This sets the KodeUI make variable to hold all the c# files required for
# adding KodeUI to the parent project

KodeUI = \
	KodeUI/AnchorLayoutGroup.cs \
	KodeUI/ImageLoader.cs \
	KodeUI/Layout.cs \
	KodeUI/LayoutAnchor.cs \
	KodeUI/LayoutPanel.cs \
	KodeUI/Loader.cs \
	KodeUI/RectTransformExtensions.cs \
	KodeUI/ScrollView.cs \
	KodeUI/Skin.cs \
	KodeUI/SpriteLoader.cs \
	KodeUI/Style.cs \
	KodeUI/Titlebar.cs \
	KodeUI/Touchable.cs \
	KodeUI/UIButton.cs \
	KodeUI/UIDropdown.cs \
	KodeUI/UIEmpty.cs \
	KodeUI/UIImage.cs \
	KodeUI/UIInputField.cs \
	KodeUI/UIKit.cs \
	KodeUI/UILayoutElement.cs \
	KodeUI/UIObject.cs \
	KodeUI/UIRectMask.cs \
	KodeUI/UIScrollbar.cs \
	KodeUI/UISlider.cs \
	KodeUI/UIText.cs \
	KodeUI/UIToggle.cs \
	KodeUI/Unity-Helpers/UnityExtensions.cs \
	KodeUI/Unity-Helpers/UnityUtils.cs \
	KodeUI/Utils.cs \
	KodeUI/VertiLinkLayoutGroup.cs \
	KodeUI/Viewport.cs \
	KodeUI/Window.cs \
	$e
