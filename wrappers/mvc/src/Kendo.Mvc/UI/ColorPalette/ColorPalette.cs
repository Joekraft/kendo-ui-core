namespace Kendo.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using Kendo.Mvc.Infrastructure;

    public class ColorPalette : WidgetBase
    {
        public ColorPalette(ViewContext viewContext, IJavaScriptInitializer initializer, ViewDataDictionary viewData)
            : base(viewContext, initializer, viewData)
        {
            Palette = ColorPickerPalette.Basic;

            Columns = ColumnsDefault;
        }

        public const int ColumnsDefault = 10;
        public int Columns { get; set; }

        public object Size { get; set; }

        public ColorPickerPalette Palette { get; set; }

        public IEnumerable<string> PaletteColors { get; set; }

        public string Value { get; set; }
       
        public override void WriteInitializationScript(TextWriter writer)
        {
            var options = new Dictionary<string, object>(Events);

            if (Palette == ColorPickerPalette.Basic)
            {
                options["palette"] = "basic";
            }
            else if (Palette == ColorPickerPalette.WebSafe)
            {
                options["palette"] = "websafe";
            }
            else if (PaletteColors != null && PaletteColors.Any())
            {
                options["palette"] = PaletteColors;
            }

            if (Columns != ColumnsDefault)
            {
                options["columns"] = Columns;
            }

            if (Size != null)
            {
                var size = Size as ColorPaletteTileSize;

                if (size != null)
                {
                    options["size"] = new { width = size.Width, height = size.Height };
                }
                else
                {
                    options["size"] = (int)Size;
                }
            }

            writer.Write(Initializer.Initialize(Selector, "ColorPalette", options));

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            new HtmlElement("div")
                .Attributes(new { id = this.Id })
                .Attributes(this.HtmlAttributes)
                .PrependClass(UIPrimitives.Widget, "k-colorpalette")
                .WriteTo(writer);

            base.WriteHtml(writer);
        }
    }
}