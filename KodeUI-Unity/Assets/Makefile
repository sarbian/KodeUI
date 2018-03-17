export MODNAME
MODNAME		:= KodeUI
KSPDIR		:= ${HOME}/ksp/KSP_linux
MANAGED		:= ${KSPDIR}/KSP_Data/Managed
GAMEDATA	:= ${KSPDIR}/GameData
MODGAMEDATA := ${GAMEDATA}/${MODNAME}
PLUGINDIR	:= ${MODGAMEDATA}/Plugins

TARGETS		:= ${MODNAME}.dll

MOD_FILES := \
	KodeUI/RectTransformExtensions.cs	\
	KodeUI/Layout.cs	\
	KodeUI/UIText.cs	\
	KodeUI/UIKit.cs	\
	KodeUI/UIButton.cs	\
	KodeUI/Unity-Helpers/UnityUtils.cs	\
	KodeUI/Unity-Helpers/UnityExtensions.cs	\
	KodeUI/LayoutPanel.cs	\
	KodeUI/UIObject.cs	\
	KodeUI/UIToggle.cs	\
	KodeUI/UIToggleBase.cs	\
	SupportScript/UICameraResizer.cs	\
	SupportScript/UITester.cs	\
	$e

RESGEN2		:= resgen2
GMCS		:= mcs
GMCSFLAGS	:= -optimize -warnaserror -unsafe
GIT			:= git
TAR			:= tar
ZIP			:= zip

SUBDIRS=

all: version ${TARGETS}
	@for dir in ${SUBDIRS}; do \
		make -C $$dir $@ || exit 1; \
	done

.PHONY: version
version:
	@../tools/git-version.sh Assembly/AssemblyInfo

info:
	@echo "${MODNAME} Build Information"
	@echo "    resgen2:    ${RESGEN2}"
	@echo "    gmcs:       ${GMCS}"
	@echo "    gmcs flags: ${GMCSFLAGS}"
	@echo "    git:        ${GIT}"
	@echo "    tar:        ${TAR}"
	@echo "    zip:        ${ZIP}"
	@echo "    KSP Data:   ${KSPDIR}"

${MODNAME}.dll: ${MOD_FILES}
	${GMCS} ${GMCSFLAGS} -t:library -lib:${MANAGED} \
		-r:Assembly-CSharp,Assembly-CSharp-firstpass \
		-r:UnityEngine,UnityEngine.UI \
		-out:$@ $^

clean:
	@for dir in ${SUBDIRS}; do \
		make -C $$dir $@ || exit 1; \
	done
	rm -f ${TARGETS} Assembly/AssemblyInfo.cs

install: all
	@for dir in ${SUBDIRS}; do \
		make -C $$dir $@ || exit 1; \
	done
	mkdir -p ${PLUGINDIR}
	cp ${TARGETS} ${PLUGINDIR}

.PHONY: all clean install
