﻿using HarmonyLib;
using UnityEngine;
using UnhollowerBaseLib;
using MeetingHud = OOCJALPKPEP;
using PlayerVoteArea = HDJGDMFCHDN;
using HudManager = PIEFJFEOGOL;
using PlayerControl = FFGALNAPKCD;

namespace CustomRolesMod
{
	[HarmonyPatch]
	public static class HudPatch
	{
		public static void updateMeetingHUD(MeetingHud __instance)
		{
			foreach (PlayerVoteArea player in __instance.HBDFFAHBIGI)
			{
				if (player.NameText.Text == PlayerControlPatch.Torch.name && PlayerControlPatch.isTorch(PlayerControl.LocalPlayer))
				{
					player.NameText.Color = new Color(1f, 0.37f, 0.125f, 1f);
				}
				if (player.NameText.Text == PlayerControlPatch.Sweeper.name && PlayerControlPatch.IsSweeper(PlayerControl.LocalPlayer))
				{
					player.NameText.Color = new Color(0f, 0.33f, 0.79f, 1f);
				}
				if (player.NameText.Text == PlayerControlPatch.Mayor.name && PlayerControlPatch.IsMayor(PlayerControl.LocalPlayer))
				{
					player.NameText.Color = new Color(0.44f, 0.31f, 0.66f, 1f);
				}
			}
		}

		[HarmonyPatch(typeof(HudManager), "Update")]
		public static void Postfix(HudManager __instance)
		{
			if (MeetingHud.Instance != null)
			{
				HudPatch.updateMeetingHUD(MeetingHud.Instance);
			}
			if (PlayerControl.AllPlayerControls.Count > 1 && PlayerControlPatch.Torch != null && PlayerControlPatch.isTorch(PlayerControl.LocalPlayer))
			{
				PlayerControl.LocalPlayer.nameText.Color = new Color(1f, 0.37f, 0.125f, 1f);
			}
			if (PlayerControl.AllPlayerControls.Count > 1 && PlayerControlPatch.Mayor != null && PlayerControlPatch.IsMayor(PlayerControl.LocalPlayer))
			{
				PlayerControl.LocalPlayer.nameText.Color = new Color(0.44f, 0.31f, 0.66f, 1f);
			}
			if (PlayerControl.AllPlayerControls.Count > 1 && PlayerControlPatch.Sweeper != null && PlayerControlPatch.IsSweeper(PlayerControl.LocalPlayer))
			{
				PlayerControl.LocalPlayer.nameText.Color = new Color(0f, 0.33f, 0.79f, 1f);
			}
		}

		[HarmonyPatch(typeof(MeetingHud), "CCEPEINGBCN")]
		public static bool Prefix(MeetingHud __instance, ref Il2CppStructArray<byte> __result)
		{
			byte[] array = new byte[11];
			foreach (PlayerVoteArea player in __instance.HBDFFAHBIGI)
			{
				if (player.didVote)
				{
					int num = (int)(player.votedFor + 1);
					if (num >= 0 && num < array.Length)
					{
						byte[] array2 = array;
						int num2 = num;
						byte voteImportance = player.NameText.Text == PlayerControlPatch.Mayor.nameText.Text ? (byte)2 : (byte)1;
						array2[num2] += voteImportance;
					}
				}
			}
			__result = array;
			return false;
		}

		private static System.Random random = new System.Random();
	}
}
