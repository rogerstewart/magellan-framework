using System;
using Transitionals;

namespace Magellan.Transitionals
{
    /// <summary>
    /// Represents an item registered in the <see cref="NavigationTransitionRegistry" />.
    /// </summary>
    public class NavigationTransition
    {
        private readonly string _name;
        private readonly string _reverse;
        private readonly Func<Transition> _transitionBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationTransition"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="reverse">The reverse.</param>
        /// <param name="transitionBuilder">The transition builder.</param>
        public NavigationTransition(string name, string reverse, Func<Transition> transitionBuilder)
        {
            _name = name;
            _reverse = reverse;
            _transitionBuilder = transitionBuilder;
        }

        /// <summary>
        /// Gets the name of this transition.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the name of the reverse transition.
        /// </summary>
        public string Reverse
        {
            get { return _reverse; }
        }

        /// <summary>
        /// Creates an instance of the transition.
        /// </summary>
        public virtual Transition CreateTransition()
        {
            return _transitionBuilder();
        }
    }
}