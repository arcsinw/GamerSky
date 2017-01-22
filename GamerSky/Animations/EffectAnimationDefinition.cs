using Windows.UI.Composition;

namespace GamerSky.Animations
{
    /// <summary>
    /// Defines an <see cref="EffectAnimationDefinition"/> which is used by
    /// <see cref="AnimationSet"/> to link effect animations to Visuals
    /// </summary>
    internal class EffectAnimationDefinition
    {
        /// <summary>
        /// Gets or sets <see cref="CompositionEffectBrush"/> that will be animated
        /// </summary>
        public CompositionEffectBrush EffectBrush { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CompositionAnimation"/>
        /// </summary>
        public CompositionAnimation Animation { get; set; }

        /// <summary>
        /// Gets or sets the property name that will be animated on the <see cref="CompositionEffectBrush"/>
        /// </summary>
        public string PropertyName { get; set; }
    }
}
