using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class ControlsConfig
    {
        /// <summary>
        /// Key that moves a tetromino to the left.
        /// </summary>
        public Keys Left    { get; set; }

        /// <summary>
        /// Key that moves a tetromino to the bottom.
        /// </summary>
        public Keys Bottom  { get; set; }

        /// <summary>
        /// Key that moves a tetromino to the right.
        /// </summary>
        public Keys Right   { get; set; }

        /// <summary>
        /// Key that holds a tetromino in the holder.
        /// </summary>
        public Keys Hold    { get; set; }

        /// <summary>
        /// Key that rotates a tetromino clockwise.
        /// </summary>
        public Keys Rotate  { get; set; }

        /// <summary>
        /// Key that hard drops a tetromino.
        /// </summary>
        public Keys Drop    { get; set; }
    }
}
