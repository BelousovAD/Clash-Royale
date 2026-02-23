using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;

namespace CodeExtensions
{
    public static class DoTweenExtensions
    {
        public static TweenerCore<string, string, StringOptions> DOText(
            this TMP_Text target,
            string endValue,
            float duration,
            bool richTextEnabled = true,
            ScrambleMode scrambleMode = ScrambleMode.None,
            string scrambleChars = null)
        {
            endValue ??= string.Empty;
            TweenerCore<string, string, StringOptions> tweener = DOTween.To(
                () => target.text,
                x => target.text = x,
                endValue,
                duration);
            tweener.SetOptions(richTextEnabled, scrambleMode, scrambleChars).SetTarget(target);
            
            return tweener;
        }
    }
}