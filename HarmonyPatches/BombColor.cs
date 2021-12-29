﻿using HarmonyLib;
using System;
using UnityEngine;

namespace Tweaks55.HarmonyPatches {
	[HarmonyPatch(typeof(BombNoteController), nameof(BombNoteController.Init))]
	static class BombColor {
		[HarmonyPriority(int.MaxValue)]
		static void Prefix(BombNoteController __instance) {
			if(Config.Instance.bombColor == Color.black)
				return;

			var c = __instance.transform.GetChild(0);

			if(c == null)
				return;

			// If CustomNotes "HMD Only" is active, there will be another nested child (The HMD bomb), which we should apply the color to instead
			if(c.childCount != 0)
				c = c.GetChild(0);

			c.GetComponent<Renderer>()?.material?.SetColor("_SimpleColor", Config.Instance.bombColor);
		}

		static Exception Cleanup(Exception ex) => Plugin.PatchFailed(ex);
	}
}
