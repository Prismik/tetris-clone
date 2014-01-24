using Microsoft.Xna.Framework;

namespace Tetris
{
    /// <summary>
    /// Different directions in which we can move a block or a tetromino.
    /// </summary>
    public static class Directions
    {
        private static Vector2
            T = new Vector2(0, -1),
            R = new Vector2(1, 0),
            B = new Vector2(0, 1),
            L = new Vector2(-1, 0);
        /// <summary>
        /// A vector pointing to the top.
        /// </summary>
        public static Vector2 Top { get { return T; } }
        /// <summary>
        /// A vector pointing to the right.
        /// </summary>
        public static Vector2 Right { get { return R; } }

        /// <summary>
        /// A vector pointing to the bottom.
        /// </summary>
        public static Vector2 Bottom { get { return B; } }

        /// <summary>
        /// A vector pointing to the left.
        /// </summary>
        public static Vector2 Left { get { return L; } }
    }
}
