﻿using Render.Renders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Render
{
    /// <include file='Documentation.xml' path='documentation/members[@name="RendersFactory"]/RendersFactory/*'/>
    public static class RendersFactory
    {
        private static Dictionary<string, Func<IRender>> renders = new Dictionary<string, Func<IRender>>()
        {
            { "Jpegs collection", () => new JpegsRender() },
            { "Gif file", () => new GifRender() },
            { "Avi file", () => new AviRender() },
        };

        /// <include file='Documentation.xml' path='documentation/members[@name="RendersFactory"]/GetRendersNames/*'/>
        public static List<string> GetRendersNames()
        {
            return renders.Keys.ToList();
        }

        /// <include file='Documentation.xml' path='documentation/members[@name="RendersFactory"]/GetRender/*'/>
        public static IRender GetRender(string renderName)
        {
            return renders[renderName]();
        }
    }
}
