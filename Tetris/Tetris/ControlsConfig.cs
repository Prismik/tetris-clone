using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class ControlsConfig
    {
        public Keys Left    { get; set; }
        public Keys Bottom  { get; set; }
        public Keys Right   { get; set; }
        public Keys Hold    { get; set; }
        public Keys Rotate  { get; set; }
        public Keys Drop    { get; set; }
    }
}
