using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Day10
{
    public struct Line
    {
        public BitArray lights;
        public List<BitArray> buttons;

        public Line(BitArray lights, List<BitArray> buttons)
        {
            this.lights = lights;
            this.buttons = buttons;
        }
    }

    public struct LineAttempt
    {
        public BitArray lights;
        public BitArray button;

        public LineAttempt(BitArray lights, BitArray button)
        {
            this.lights = lights;
            this.button = button;
        }
    }
}
