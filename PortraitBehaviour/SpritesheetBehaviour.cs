using DiskCardGame;
using HarmonyLib;
using UnityEngine;
using System.Collections;

namespace SpritesheetAPI.PortraitBehaviour
{
    internal class SpritesheetBehaviour : CardAppearanceBehaviour
    {
        (Sprite[] frames, int frameRate) Anim;
        bool coroutineStart, noAnim;

        public override void ApplyAppearance()
        {
            if (coroutineStart || noAnim) return;

            noAnim = !Library.AnimatedPortraits.ContainsKey(Card.Info.name);
            if (!noAnim)
            {
                Anim = Library.AnimatedPortraits[Card.Info.name];
                coroutineStart = true;
                StartCoroutine(AnimatePortrait());
            }
        }

        public IEnumerator AnimatePortrait()
        {
            Sprite[] frames = Anim.frames;
            float wait = 1f / Anim.frameRate;

            while (true) // Is 'while(true)' good here? I'm not sure.
            {
                for(int i = 0; i < frames.Length; i++)
                {
                    Card.RenderInfo.portraitOverride = frames[i];
                    // Card.RenderCard();
                    Card.StatsLayer.RenderCard(Card.RenderInfo);
                    yield return new WaitForSeconds(wait);
                }
            }
        }
    }
}
